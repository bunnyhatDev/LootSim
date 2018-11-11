using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

	[Header("Chest Variables")]
	public static float currentHP;
	public double roundedHP;
	public float maxHP;
	public float tapDamage;
	public float idleDamage;
	public float currentTimer;
	public float maxTimer;
	public GameObject[] loot;

	private bool isDead = false;

	LootManager m_lootManager;

    void Awake() {
		// maxHP = 10;
		currentHP = maxHP;

		idleDamage = tapDamage * 1.45f;

		// maxTimer = 59;
		currentTimer = maxTimer;

		//When chest spawns, let it pick 3 rewards from a given pool.
		m_lootManager = GameObject.FindGameObjectWithTag("Environment").GetComponent<LootManager>();
		m_lootManager.randomLootIndex0 = Random.Range(0, 22);
		m_lootManager.randomLootIndex1 = Random.Range(0, 22);
		m_lootManager.randomLootIndex2 = Random.Range(0, 22);

		loot[0].name = m_lootManager.brokenItems.lootItem[m_lootManager.randomLootIndex0].itemName;
		loot[1].name = m_lootManager.brokenItems.lootItem[m_lootManager.randomLootIndex1].itemName;
		loot[2].name = m_lootManager.brokenItems.lootItem[m_lootManager.randomLootIndex2].itemName;
    }
	
	void Update () {
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
				if(Input.GetMouseButtonDown(0)) {
					currentHP -= tapDamage;
				}
			}
		} else {
			for(int i = 0; i < loot.Length; i++) {
				loot[i].SetActive(true);
			}
		}

	}
}
