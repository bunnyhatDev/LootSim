using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_SYSTEM {
	LOADING_SCREEN,				//Show loading screen, load all vars and assets
	CHEST_SPAWN,				//Chest will spawn and player can begin tapping
	REWARD_SPAWN				//Rewards spawn and player earns it
}

public class GameManager : MonoBehaviour {
	public EVENT_SYSTEM eventSystem;
	public bool isMenuOpen = false;

	[Header("Loot Box Properties")]
	public GameObject chestPrefab;
	public float currentHP;
	public double roundedHP;
	public float maxHP;
	public float tapDamage;
	public float idleDamage;
	public float currentTimer;
	public float maxTimer;
	public GameObject[] loot;
	private bool isDead = false;

	[Header("Player Attributes")]
	public int level;
	public float currentXP, maxXP;
	public float earnings;
	public int chestsOpened;
	
	private string formatedEarnings;

	LootManager m_lootManager;
	// ChestController m_chestController;
	HUDController m_hudController;

	void Awake() {
		m_lootManager = GameObject.FindGameObjectWithTag("Environment").GetComponent<LootManager>();
		// m_chestController = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestController>();
		m_hudController = GameObject.Find("HUD").GetComponent<HUDController>();

		//FIXME: This if function will be placed in an event system which will check through
		// the save file to see what the player's EXP is.
		if(currentXP == 0) {
			chestsOpened = 0;
			earnings = 0;
			level = 0;
			maxXP = 25f;
			tapDamage = 0.75f;
			idleDamage = tapDamage * 1.45f;
		}

		Debug.Log("Amount of broken items: " + m_lootManager.brokenItems.lootItem.Length);

		eventSystem = EVENT_SYSTEM.LOADING_SCREEN;
	}

	void Update() {
		Debug.Log(isDead);

		switch(eventSystem) {
			case EVENT_SYSTEM.LOADING_SCREEN:
				//Loading in HUD and variables that are pulled from the save file
				m_hudController.chestTrackerText.text = ">> Chests Opened: " + chestsOpened;
				formatedEarnings = string.Format("{0:#,###0}",earnings);
				m_hudController.earningsText.text = ">> $ " + formatedEarnings;
				m_hudController.levelText.text = ">> Level " + level.ToString();
				m_hudController.expBar.value = currentXP;
				m_hudController.expBar.maxValue = maxXP;

				//Loading assets
				SimplePool.Preload(chestPrefab, 5);
				currentHP = maxHP;
				currentTimer = maxTimer;

				eventSystem = EVENT_SYSTEM.CHEST_SPAWN;
				SimplePool.Spawn(chestPrefab, transform.position, transform.rotation);
				// StartCoroutine("SpawnChest");
			break;

			case EVENT_SYSTEM.CHEST_SPAWN:
				for(int i = 0; i < loot.Length; i++) {
					loot[i].SetActive(false);
				}
				// PickLoot();

				roundedHP = System.Math.Round(currentHP, 2);
				if(currentHP <= 0) {
					currentHP = 0; 
					isDead = true;
				}

				if(currentTimer <= 0) { 
					currentTimer = maxTimer;
					currentHP = maxHP;
					isDead = false;
				}

				if(!isDead) {
					currentHP -= idleDamage * Time.deltaTime;
					currentTimer -= Time.deltaTime;
					if(currentTimer != 0) {
						if(Input.GetMouseButtonDown(0) && !isMenuOpen) {
							currentHP -= tapDamage;
						}
						if(Input.GetKeyDown(KeyCode.Space)) {
							currentHP -= tapDamage * 5;
						}
					}
				} else {
					eventSystem = EVENT_SYSTEM.REWARD_SPAWN;
				}				
			break;

			case EVENT_SYSTEM.REWARD_SPAWN:
				chestsOpened += 1;

				// for(int i = 0; i < loot.Length; i++) {
				// 	loot[i].SetActive(true);
				// }

				chestsOpened += 1;
				earnings += 15;
				currentXP += 25f;
				maxHP += 2.5f;
				maxTimer -= 0.135f;
				currentHP = maxHP;
				currentTimer = maxTimer;

				if(currentXP >= maxXP) {
					float remainderXp = currentXP - maxXP;
					currentXP = 0;
					currentXP += remainderXp;
					maxXP += 5.5f;
					level += 1;
					earnings += 20;
					tapDamage += 0.15f;
					idleDamage = tapDamage * 1.45f;
				}
				StartCoroutine("SpawnChest");
			break;
		}

	}

	// void PickLoot() {
	// 	m_lootManager.randomLootIndex0 = Random.Range(0, m_lootManager.brokenItems.lootItem.Length);
	// 	m_lootManager.randomLootIndex1 = Random.Range(0, m_lootManager.brokenItems.lootItem.Length);
	// 	m_lootManager.randomLootIndex2 = Random.Range(0, m_lootManager.brokenItems.lootItem.Length);
		
	// 	for(int i = 0; i < m_lootManager.brokenItems.lootItem.Length; i++) {
	// 		m_lootManager.randomLoot[0] = m_lootManager.randomLootIndex0;
	// 		m_lootManager.randomLoot[1] = m_lootManager.randomLootIndex1;
	// 		m_lootManager.randomLoot[2] = m_lootManager.randomLootIndex2;
	// 	}
	// 	// Debug.Log(randomLootIndex0 + "/" + randomLootIndex1 + "/" + randomLootIndex2);
	// }

	IEnumerator SpawnChest() {
		SimplePool.Despawn(chestPrefab);
		yield return new WaitForSeconds(2);
		SimplePool.Spawn(chestPrefab, transform.position, transform.rotation);
		isDead = false;
		eventSystem = EVENT_SYSTEM.CHEST_SPAWN;
	}

}
