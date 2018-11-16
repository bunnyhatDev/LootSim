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
	// public GameObject lootPrefab;
	// public GameObject[] loot;
	public int randomLootIndex0;
	public int randomLootIndex1;
	public int randomLootIndex2;
	[SerializeField] public int[] randomLoot;

	GameManager m_gameManager;

	// void Awake() {
	// 	m_gameManager = GetComponent<GameManager>();
		// SimplePool.Preload(loot[0], 1);
		// SimplePool.Preload(loot[1], 1);
		// SimplePool.Preload(loot[2], 1);
	// }

	// void LateUpdate() {
	// 	if(m_gameManager.isDead) {
	// 		StartCoroutine("PickLoot");
	// 	}
	// }
	
	// IEnumerator PickLoot() {
		// SimplePool.Spawn(loot[0], transform.position, transform.rotation);
		// SimplePool.Spawn(loot[1], transform.position, transform.rotation);
		// SimplePool.Spawn(loot[2], transform.position, transform.rotation);
		// Debug.Log("loot spawned");

		// randomLootIndex0 = Random.Range(0, brokenItems.lootItem.Length);
		// randomLootIndex1 = Random.Range(0, brokenItems.lootItem.Length);
		// randomLootIndex2 = Random.Range(0, brokenItems.lootItem.Length);
		
		// loot[0].name = brokenItems.lootItem[randomLootIndex0].itemName;
		// loot[1].name = brokenItems.lootItem[randomLootIndex1].itemName;
		// loot[2].name = brokenItems.lootItem[randomLootIndex2].itemName;

		// loot[0].GetComponent<MeshRenderer>().material = brokenItems.lootItem[0].lootMat;
		// loot[1].GetComponent<MeshRenderer>().material = brokenItems.lootItem[1].lootMat;
		// loot[2].GetComponent<MeshRenderer>().material = brokenItems.lootItem[2].lootMat;

		// for(int i = 0; i < brokenItems.lootItem.Length; i++) {
		// 	brokenItems.lootItem[i].exp = brokenExp;
		// 	brokenItems.lootItem[i].lootMat = brokenMat;
		// 	brokenItems.lootItem[i].lootAnimation = brokenAnimation;
		// }
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

		// yield return new WaitForSeconds(0.15f);
		// SimplePool.Despawn(loot[0]);
		// SimplePool.Despawn(loot[1]);
		// SimplePool.Despawn(loot[2]);
	// }

}
