using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_SYSTEM {
	LOADING_SCREEN,				//Show loading screen, load all vars and assets
	DAMAGE_PHASE,				//Chest will spawn and player can begin tapping
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
	public bool isDead = false;

	[Header("Player Attributes")]
	public int level;
	public float currentXP, maxXP;
	public float earnings;
	public int chestsOpened;
	
	LootManager m_lootManager;
	HUDController m_hudController;

	void Awake() {
		m_lootManager = GetComponent<LootManager>();
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

		// Debug.Log("Amount of broken items: " + m_lootManager.brokenItems.lootItem.Length);

		SimplePool.Preload(chestPrefab, 1);
		SimplePool.Spawn(chestPrefab, new Vector3(0,-1.5f,0), transform.rotation);

		eventSystem = EVENT_SYSTEM.LOADING_SCREEN;
	}

	void Update() {
		// Debug.Log(isDead);
		switch(eventSystem) {
			case EVENT_SYSTEM.LOADING_SCREEN:
				currentHP = maxHP;
				currentTimer = maxTimer;

				eventSystem = EVENT_SYSTEM.DAMAGE_PHASE;
			break;

			case EVENT_SYSTEM.DAMAGE_PHASE:
				roundedHP = System.Math.Round(currentHP, 2);
				if(roundedHP <= 0) {
					currentHP = 0; 
					isDead = true;
					chestsOpened += 1;
					earnings += 15;
					currentXP += 25f;
					maxHP += 25f;
					maxTimer -= 0.135f;
					currentHP = maxHP;
					currentTimer = maxTimer;
					eventSystem = EVENT_SYSTEM.REWARD_SPAWN;
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
				}
			break;

			case EVENT_SYSTEM.REWARD_SPAWN:
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

	IEnumerator SpawnChest() {
		SimplePool.Despawn(chestPrefab);
		yield return new WaitForSeconds(0.15f);
		isDead = false;
		eventSystem = EVENT_SYSTEM.DAMAGE_PHASE;
	}

}
