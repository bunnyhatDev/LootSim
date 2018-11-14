using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public int level;
	public float currentXP, maxXP;
	public float earnings;
	public int chestsOpened;
	
	private string formatedEarnings;

	LootManager m_lootManager;
	ChestController m_chestController;
	HUDController m_hudController;

	void Awake() {
		m_lootManager = GameObject.FindGameObjectWithTag("Environment").GetComponent<LootManager>();
		m_chestController = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestController>();
		m_hudController = GameObject.Find("HUD").GetComponent<HUDController>();

		//FIXME: This if function will be placed in an event system which will check through
		// the save file to see what the player's EXP is.
		if(currentXP == 0) {
			chestsOpened = 0;
			earnings = 0;
			level = 0;
			maxXP = 25f;
			m_chestController.tapDamage = 0.75f;
			m_chestController.idleDamage = m_chestController.tapDamage * 1.45f;
		}
	}

	void Update() {
		m_hudController.chestTrackerText.text = ">> Chests Opened: " + chestsOpened;
		formatedEarnings = string.Format("{0:#,###0}",earnings);
		m_hudController.earningsText.text = ">> $ " + formatedEarnings;
		m_hudController.levelText.text = ">> Level " + level.ToString();
		m_hudController.expBar.value = currentXP;
		m_hudController.expBar.maxValue = maxXP;

		if(Input.GetKeyDown(KeyCode.Space)) {
			currentXP += 10;
			Debug.Log(currentXP);
		}

		if(currentXP >= maxXP) {
			float remainderXp = currentXP - maxXP;
			currentXP = 0;
			currentXP += remainderXp;
			maxXP += 5.5f;
			level += 1;
			earnings += 20;
			m_chestController.tapDamage += 0.15f;
			m_chestController.idleDamage = m_chestController.tapDamage * 1.45f;
		}

		if(m_chestController.currentHP <= 0) {
			chestsOpened += 1;
			earnings += 15;
			currentXP += 25f;
			m_chestController.maxHP += 2.5f;
			m_chestController.maxTimer -= 0.135f;
			m_chestController.currentHP = m_chestController.maxHP;
			m_chestController.currentTimer = m_chestController.maxTimer;
		}
	}

}
