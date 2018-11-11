using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {
	#region "Broken Loot Stats"
	[Header("Broken Loot Stats")]
	public LootItemList brokenItems;
	public float brokenExp;
	public Material brokenMat;
	public Animation brokenAnimation;
	#endregion
	#region "Common Loot Stats"
	[Header("Common Loot Stats")]
	public LootItemList  commonItems;
	public float commonExp;
	public Material commonMat;
	public Animation commonAnimation;
	#endregion
	#region "Uncommon Loot Stats"
	[Header("Uncommon Loot Stats")]
	public LootItemList  uncommonItems;
	public float uncommonExp;
	public Material uncommonMat;
	public Animation uncommonAnimation;
	#endregion
	#region "Rare Loot Stats"
	[Header("Rare Loot Stats")]
	public LootItemList rareItems;
	public float rareExp;
	public Material rareMat;
	public Animation rareAnimation;
	#endregion
	#region "Legendary Loot Stats"
	[Header("Legendary Loot Stats")]
	public LootItemList legendaryItems;
	public float legendaryExp;
	public Material legendaryMat;
	public Animation legendaryAnimation;
	#endregion
	#region "Prime Loot Stats"
	[Header("Prime Loot Stats")]
	public LootItemList primeItems;
	public float primeExp;
	public Material primeMat;
	public Animation primeAnimation;
	#endregion

	[Header("RNG Properties")]
	public int randomLootIndex0;
	public int randomLootIndex1;
	public int randomLootIndex2;
	[SerializeField] private int[] randomLoot;

	ChestController m_chestController;
	
	void Awake() {
		m_chestController = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestController>();
		m_chestController.loot[0].name = brokenItems.lootItem[0].itemName;
		m_chestController.loot[1].name = brokenItems.lootItem[1].itemName;
		m_chestController.loot[2].name = brokenItems.lootItem[2].itemName;

		m_chestController.loot[0].GetComponent<MeshRenderer>().material = brokenItems.lootItem[0].lootMat;
		m_chestController.loot[1].GetComponent<MeshRenderer>().material = brokenItems.lootItem[1].lootMat;
		m_chestController.loot[2].GetComponent<MeshRenderer>().material = brokenItems.lootItem[2].lootMat;

		m_chestController.loot[0].GetComponent<Animation>().clip = brokenItems.lootItem[0].lootAnimation.clip;
		m_chestController.loot[1].GetComponent<Animation>().clip = brokenItems.lootItem[1].lootAnimation.clip;
		m_chestController.loot[2].GetComponent<Animation>().clip = brokenItems.lootItem[2].lootAnimation.clip;

		for(int i = 0; i < brokenItems.lootItem.Length; i++) {
			brokenItems.lootItem[i].exp = brokenExp;
			brokenItems.lootItem[i].lootMat = brokenMat;
			brokenItems.lootItem[i].lootAnimation = brokenAnimation;
		}
		for(int i = 0; i < commonItems.lootItem.Length; i++) {
			commonItems.lootItem[i].exp = commonExp;
			commonItems.lootItem[i].lootMat = commonMat;
			commonItems.lootItem[i].lootAnimation = commonAnimation;
		}
		for(int i = 0; i < uncommonItems.lootItem.Length; i++) {
			uncommonItems.lootItem[i].exp = uncommonExp;
			uncommonItems.lootItem[i].lootMat = uncommonMat;
			uncommonItems.lootItem[i].lootAnimation = uncommonAnimation;
		}
		for(int i = 0; i < rareItems.lootItem.Length; i++) {
			rareItems.lootItem[i].exp = rareExp;
			rareItems.lootItem[i].lootMat = rareMat;
			rareItems.lootItem[i].lootAnimation = rareAnimation;
		}
		for(int i = 0; i < legendaryItems.lootItem.Length; i++) {
			legendaryItems.lootItem[i].exp = legendaryExp;
			legendaryItems.lootItem[i].lootMat = legendaryMat;
			legendaryItems.lootItem[i].lootAnimation = legendaryAnimation;
		}
		for(int i = 0; i < primeItems.lootItem.Length; i++) {
			primeItems.lootItem[i].exp = primeExp;
			primeItems.lootItem[i].lootMat = primeMat;
			primeItems.lootItem[i].lootAnimation = primeAnimation;
		}

		PickLoot();
	}

	void PickLoot() {
		randomLootIndex0 = Random.Range(0, 22);
		randomLootIndex1 = Random.Range(0, 22);
		randomLootIndex2 = Random.Range(0, 22);
		for(int i = 0; i < brokenItems.lootItem.Length; i++) {
			randomLoot[0] = randomLootIndex0;
			randomLoot[1] = randomLootIndex1;
			randomLoot[2] = randomLootIndex2;
		}
		// Debug.Log(randomLootIndex0 + "/" + randomLootIndex1 + "/" + randomLootIndex2);
	}

}
