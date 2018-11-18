using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootType {
	public LootItemList itemList;
	public float itemExp;
	public Material itemMat;
	public Animation itemAnimation;
}

public class LootManager : MonoBehaviour {
	public LootType[] lootTypes;
	public LootItemList lootItemList;

	[Header("RNG Properties")]
	// public GameObject lootPrefab;
	public GameObject[] loot;
	// public int randomLootIndex0;
	// public int randomLootIndex1;
	// public int randomLootIndex2;
	[SerializeField] public int[] randomLoot;

	[SerializeField] private float lootTimer, resetLootTimer;

	GameManager m_gameManager;

	void Awake() {
		m_gameManager = GetComponent<GameManager>();
		for(int i = 0; i < loot.Length; i++) {
			loot[i].SetActive(false);
		}
		lootTimer = 5f;
		resetLootTimer = lootTimer;
	}

	void Update() {
		if(m_gameManager.roundedHP == m_gameManager.maxHP) {
			PickLoot(lootItemList);
		}

		for(int i = 0; i < loot.Length; i++) {
			if(loot[i].activeSelf) {
				lootTimer -= Time.deltaTime;
				if(lootTimer <= 0) {
					loot[0].SetActive(false);
					loot[1].SetActive(false);
					loot[2].SetActive(false);
					lootTimer = resetLootTimer;
				}
			}
		}
	}
	
	public void PickLoot(LootItemList lootCategory) {
		randomLoot[0] = Random.Range(0, lootCategory.lootItem.Length);
		randomLoot[1] = Random.Range(0, lootCategory.lootItem.Length);
		randomLoot[2] = Random.Range(0, lootCategory.lootItem.Length);

		loot[0].name = lootCategory.lootItem[randomLoot[0]].itemName;
		loot[0].GetComponent<MeshRenderer>().material = lootCategory.lootItem[randomLoot[0]].lootMat;
		// lootCategory.lootItem[i].exp = lootCategory.lootItem[randomLoot[0]].exp;
		// lootCategory.lootItem[i].lootMat = lootCategory.lootItem[randomLoot[0]].lootMat;
		// lootCategory.lootItem[i].lootAnimation = lootCategory.lootItem[randomLoot[0]].lootAnimation;

		loot[1].name = lootCategory.lootItem[randomLoot[1]].itemName;
		loot[1].GetComponent<MeshRenderer>().material = lootCategory.lootItem[randomLoot[1]].lootMat;
		// lootCategory.lootItem[i].exp = lootCategory.lootItem[randomLoot[1]].exp;
		// lootCategory.lootItem[i].lootMat = lootCategory.lootItem[randomLoot[1]].lootMat;
		// lootCategory.lootItem[i].lootAnimation = lootCategory.lootItem[randomLoot[1]].lootAnimation;

		loot[2].name = lootCategory.lootItem[randomLoot[2]].itemName;
		loot[2].GetComponent<MeshRenderer>().material = lootCategory.lootItem[randomLoot[2]].lootMat;
		// lootCategory.lootItem[i].exp = lootCategory.lootItem[randomLoot[2]].exp;
		// lootCategory.lootItem[i].lootMat = lootCategory.lootItem[randomLoot[2]].lootMat;
		// lootCategory.lootItem[i].lootAnimation = lootCategory.lootItem[randomLoot[2]].lootAnimation;

		// for(int i = 0; i < commonItems.lootItem.Length; i++) {
		// 	commonItems.lootItem[i].exp = commonExp;
		// 	commonItems.lootItem[i].lootMat = commonMat;
		// 	commonItems.lootItem[i].lootAnimation = commonAnimation;
		// }
		// for(int i = 0; i < uncommonItems.lootItem.Length; i++) {
		// 	uncommonItems.lootItem[i].exp = uncommonExp;
		// 	uncommonItems.lootItem[i].lootMat = uncommonMat;
		// 	uncommonItems.lootItem[i].lootAnimation = uncommonAnimation;
		// }
		// for(int i = 0; i < rareItems.lootItem.Length; i++) {
		// 	rareItems.lootItem[i].exp = rareExp;
		// 	rareItems.lootItem[i].lootMat = rareMat;
		// 	rareItems.lootItem[i].lootAnimation = rareAnimation;
		// }
		// for(int i = 0; i < legendaryItems.lootItem.Length; i++) {
		// 	legendaryItems.lootItem[i].exp = legendaryExp;
		// 	legendaryItems.lootItem[i].lootMat = legendaryMat;
		// 	legendaryItems.lootItem[i].lootAnimation = legendaryAnimation;
		// }
		// for(int i = 0; i < primeItems.lootItem.Length; i++) {
		// 	primeItems.lootItem[i].exp = primeExp;
		// 	primeItems.lootItem[i].lootMat = primeMat;
		// 	primeItems.lootItem[i].lootAnimation = primeAnimation;
		// }
	}

}
