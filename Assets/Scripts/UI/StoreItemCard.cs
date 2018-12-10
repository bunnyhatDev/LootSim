using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemCard : MonoBehaviour {
	public Image image; // TODO:
	public int myCatalougeIndex;
	public int price;
	public TextMeshProUGUI cardName;
	public TextMeshProUGUI desc;
	public TextMeshProUGUI priceText;

	private StoreManager m_storeManager;

	void Awake() {
		m_storeManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<StoreManager>();
	}

	public void SetInfo(int index, string name, string des, int cost) {
		myCatalougeIndex = index;
		cardName.text = name;
		desc.text = des;
		price = cost;
		priceText.text = price.ToString();
	}

	public void SelectItem() {
		StoreManager.selectStoreCatalougeIndex = myCatalougeIndex;
	}

	public void Buy() {
		m_storeManager.BuyItem(myCatalougeIndex);
	}
}
