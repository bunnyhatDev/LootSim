using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StatProperty {
	// public Image statImage;
	public TextMeshProUGUI statName;
	public TextMeshProUGUI statValue;
}

[System.Serializable]
public enum Stat {
		TimePlayed,
		TapCount, CurrencyCount, PledgesCount, ChestCount, LootCount, SceneCount, AutoTapUpgrades, UpgradeCount, AchievementCount,
		TotalExpGained, AutoTapPower, TapPower
}

public class StatCard : MonoBehaviour {
	[SerializeField] Stat stat;
	[SerializeField] StatProperty statProperty;

	// GameManager m_gameManager;
	SaveLoadData m_saveData;
	HUDController m_hudController;

	void Awake() {
		// m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
		m_saveData = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>().m_saveData;
		m_hudController = GameObject.Find("HUD").GetComponent<HUDController>();
		
	}

	void Update() {
		switch(stat) {
			case Stat.TimePlayed:				
				this.gameObject.name = "Time Played";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = m_saveData.timePlayed.ToString();
				break;
			case Stat.TapCount:
				this.gameObject.name = "Tap Count";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = m_saveData.tapCount.ToString();
				break;
			case Stat.CurrencyCount:
				this.gameObject.name = "Currency Count";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = "$" + m_saveData.totalCurrency.ToString();
				break;
			case Stat.PledgesCount:
				this.gameObject.name = "Pledges Count";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = "$" + m_saveData.totalPledges.ToString();
				break;
			case Stat.ChestCount:
				this.gameObject.name = "Chest Opened";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = m_saveData.chestCount.ToString();
				break;
			case Stat.LootCount:
				this.gameObject.name = "Loot Collected";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = m_saveData.lootCount.ToString();
				break;
			case Stat.SceneCount:
				this.gameObject.name = "Scenes Unlocked";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = "1";
				break;
			case Stat.AutoTapUpgrades:
				this.gameObject.name = "Auto Tap Upgrades";
				statProperty.statName.text = this.gameObject.name;
				// statProperty.statValue.text = 
				break;
			case Stat.UpgradeCount:
				this.gameObject.name = "Upgrades Count";
				statProperty.statName.text = this.gameObject.name;
				// statProperty.statValue.text = 
				break;
			case Stat.AchievementCount:
				this.gameObject.name = "Achievements Unlocked";
				statProperty.statName.text = this.gameObject.name;
				// statProperty.statValue.text = 
				break;
			case Stat.TotalExpGained:
				this.gameObject.name = "Total XP Gained";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = m_saveData.earnedXP.ToString() + "xp";
				break;
			case Stat.AutoTapPower:
				this.gameObject.name = "Auto Tap Power";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = "x" + m_saveData.autoDamage.ToString();
				break;	
			case Stat.TapPower:
				this.gameObject.name = "Tap Power";
				statProperty.statName.text = this.gameObject.name;
				statProperty.statValue.text = "x" + m_saveData.tapDamage.ToString();
				break;			
		}	
	}

}
