using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UPGRADES {

}

public enum ITEMS {

}

public class StoreManager : MonoBehaviour {
	GameManager m_gameManager;

	void Awake() {
		m_gameManager = GetComponent<GameManager>();
	}

	void Update() {

	}

	public void Store(int cost, Button buyButton) {
		if(m_gameManager.totalCurrency >= cost) {
			//TODO:You can buy this item because the Buy button is interactable
			buyButton.interactable = true;
		} else {
			buyButton.interactable = false;
		}
	}
	
}
