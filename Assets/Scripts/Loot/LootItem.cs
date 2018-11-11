using System.Collections;
using UnityEngine;

[System.Serializable]
public class LootItem {
	public string itemName = "";
	public float exp;
	public Material lootMat;
	public GameObject itemPrefab;
	public Animation lootAnimation;
}
