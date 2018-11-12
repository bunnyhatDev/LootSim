using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public int level;
	public float currentXP, maxXP;
	public float earnings;
	
	private string formatedEarnings;

	LootManager m_lootManager;
	ChestController m_chestController;
	HUDController m_hudController;

	void Awake() {
		m_lootManager = GameObject.FindGameObjectWithTag("Environment").GetComponent<LootManager>();
		m_chestController = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestController>();
		m_hudController = GameObject.Find("HUD").GetComponent<HUDController>();

		earnings = 459568;
		formatedEarnings = string.Format("{0:#,###0}",earnings);
		m_hudController.earningsText.text = "$ " + formatedEarnings;
		m_hudController.levelText.text = "Level " + level.ToString();
		m_hudController.expBar.value = currentXP;
		m_hudController.expBar.maxValue = maxXP;

		if(currentXP == 0) {
			level = 1;
			maxXP = 25f;
		}
	}

	void Update() {
		formatedEarnings = string.Format("{0:#,###0}",earnings);
		m_hudController.earningsText.text = "$ " + formatedEarnings;
		m_hudController.levelText.text = "Level " + level.ToString();
		m_hudController.expBar.value = currentXP;
		m_hudController.expBar.maxValue = maxXP;

		if(Input.GetKeyDown(KeyCode.Space)) {
			currentXP += 10;
			Debug.Log(currentXP);
		}

		if(currentXP >= maxXP) {
			float remainderXp = currentXP - maxXP;
			currentXP = 0;
			level += 1;
			currentXP += remainderXp;
			earnings += 56;	
		}
	}

}
