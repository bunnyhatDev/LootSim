using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/* Shop TODOS:
 *	Make a list/dictionary of iteems sold with their own properties
 *		- Price, unlock Requriments, price, and if its already bought
 *	If the saved shop list exists load the info, if not create the default list
 *	Pupulate / Update the shop menu everything its opened with updated info
 *	Make it so it buy is available when the requriments are met
 *	Add the result of the bought upgrade or item
 *	resave the items as marked bought so the list is always up to date
*/

public enum UPGRADES {
	TAP_UPGRADE, AUTOTAP_UPGRADE, CURRENCY_MULTIPLIER
}

public enum ITEMS {
	
}

[System.Serializable]
public class StoreItem {
	public int typeIndex; // What type of item // Upgrades = 0 // and etc..
	public string name;
	public string desc;
	public int cost;
	public bool isBought;
}

public class StoreManager : MonoBehaviour {
	public static List<StoreItem> storeCatalouge = new List<StoreItem>();
	public static int selectStoreCatalougeIndex = -1;

	GameManager m_gameManager;

	public StoreItem[] itemsfordebug;

	[SerializeField] private GameObject m_storeListingPrefab;
	[SerializeField] private Transform m_storeListingsParent;
	[SerializeField] private List<StoreItemCard> m_storeListings;

	private string[] UpgradeShopList = new string[] {
		"Upgrade0", "100",
		"Upgrade1", "1000",
		"Upgrade2", "10000",
		"Upgrade3", "100000"
	};

	private string[] ItemShopList = new string[] {
		"Item0", "150",
		"Item1", "1500",
		"Item2", "15000",
		"Item3", "150000"
	};

	void Awake() {
		m_gameManager = GetComponent<GameManager>();

		InitStoreCatalouge();
		itemsfordebug = storeCatalouge.ToArray();

		PopulateStoreListings();
	}

	void LateUpdate() {
		if(selectStoreCatalougeIndex != -1) {
			// TODO: Hilight Item // or not
		}
	}

	public void Store(int cost, Button buyButton) {
		if(m_gameManager.totalCurrency >= cost) {
			//TODO:You can buy this item because the Buy button is interactable
			buyButton.interactable = true;
		} else {
			buyButton.interactable = false;
		}
	}

	public void BuyItem(int catalougeIndex) {
		// buy the item at the passes index
	}

	public void SaveStore() {
		// savedGames.Add(Game.current);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedGames.json");
		// FileStream file = File.Create(Path.Combine(Application.persistentDataPath,"/savedGames.gd"));
		bf.Serialize(file, storeCatalouge);
		file.Close();
	}
	
	public static void LoadStore() {
		if (File.Exists(Application.persistentDataPath + "/savedGames.json")) {
		// if (File.Exists(Path.Combine(Application.persistentDataPath, "/savedGames.gd"))) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.json", FileMode.Open);
			// FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "/savedGames.gd"), FileMode.Open);
			storeCatalouge = (List<StoreItem>)bf.Deserialize(file);
			file.Close();
		}
	}

	private void InitStoreCatalouge() {
		if (File.Exists(Application.persistentDataPath + "/savedGames.json")) {
			LoadStore();
		} else {
			SetInitialStoreStock();
			LoadStore();
		}
	}

	private void SetInitialStoreStock() {
		StoreItem temp0;

		for(int i = 0; i < UpgradeShopList.Length; i += 2) {
			temp0 = new StoreItem();
			temp0.typeIndex = 0;
			temp0.name = UpgradeShopList[i];
			if(!int.TryParse(UpgradeShopList[i + 1], out temp0.cost)) { Debug.LogError("cant convert to int in UpgradeShopList list at index " + i); }
			temp0.isBought = false;
			storeCatalouge.Add(temp0); 
		}

		for (int i = 0; i < ItemShopList.Length; i += 2) {
			temp0 = new StoreItem();
			temp0.typeIndex = 1;
			temp0.name = ItemShopList[i];
			if (!int.TryParse(ItemShopList[i + 1], out temp0.cost)) { Debug.LogError("cant convert to int in ItemShopList list at index " + i); }
			temp0.isBought = false;
			storeCatalouge.Add(temp0);
		}

		SaveStore();
	}

	private void PopulateStoreListings() {
		// TODO: Double check after th ui is made
		//for (int i = 0; i < storeCatalouge.Count; i++) {
		//	StoreItemCard spwanedItemsCard = GameObject.Instantiate(m_storeListingPrefab, Vector3.zero, Quaternion.identity, m_storeListingsParent).GetComponent<StoreItemCard>();
		//	spwanedItemsCard.SetInfo(i, storeCatalouge[i].name, storeCatalouge[i].desc, storeCatalouge[i].cost);
		//	m_storeListings.Add(spwanedItemsCard);
		//}
	}
}
