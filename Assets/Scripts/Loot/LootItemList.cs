using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootData", menuName = "Loot/List", order = 1)]
public class LootItemList : ScriptableObject {

	public LootItem[] lootItem;
}
