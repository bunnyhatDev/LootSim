using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Data {
	public string timePlayed;
	public float earnedXP, currentXP;
	public float tapDamage, autoDamage;
	public float health, timer;
	public GameObject currentScene;

	[Header("Stats")]
	public int level;
	public float totalCurrency, overallCurrency;
	public int totalPledges, overallPledges;
	public float totalExpGained;
	public int tapCount, chestCount, lootCount, sceneCount, autoTapUpgrades, upgradeCount, achievementCount;
}


public class SaveManager : MonoBehaviour {
	// public static List<Data> data = new List<Data>();
	public static Data dataItems;

	GameManager m_gameManager;

	void Awake() {
		m_gameManager = GetComponent<GameManager>();

		InitStats();
	}

	void LateUpdate() {
		// SaveData(m_gameManager);
	}

	public void WriteData() {
		dataItems.timePlayed = m_gameManager.timePlayed;
		dataItems.earnedXP = m_gameManager.earnedXP;
		dataItems.currentXP = m_gameManager.currentXP;
		dataItems.tapDamage = m_gameManager.tapDamage;
		dataItems.autoDamage = m_gameManager.autoDamage;
		dataItems.health = m_gameManager.currentHP;
		dataItems.timer = m_gameManager.currentTimer;

		dataItems.level = m_gameManager.level;
		dataItems.totalCurrency = m_gameManager.totalCurrency;
		dataItems.overallCurrency = m_gameManager.totalCurrency;
		dataItems.totalPledges = m_gameManager.totalPledges;
		dataItems.overallPledges = m_gameManager.totalPledges;
		dataItems.totalExpGained = m_gameManager.totalXP;
		
		dataItems.tapCount = m_gameManager.tapCount;
		dataItems.chestCount = m_gameManager.chestsOpened;
		dataItems.lootCount = m_gameManager.lootCollected;
		dataItems.sceneCount = m_gameManager.scenesUnlocked;

		SaveData();
	}

	public void SaveData() {
		// savedGames.Add(Game.current);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/stats.json");
		// FileStream file = File.Create(Path.Combine(Application.persistentDataPath,"/savedGames.gd"));
		bf.Serialize(file, dataItems);
		file.Close();
	}
	
	public static void LoadData() {
		if (File.Exists(Application.persistentDataPath + "/stats.json")) {
		// if (File.Exists(Path.Combine(Application.persistentDataPath, "/savedGames.gd"))) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/stats.json", FileMode.Open);
			// FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "/savedGames.gd"), FileMode.Open);
			dataItems = bf.Deserialize(file) as Data;
			file.Close();
		}
	}

	private void InitStats() {
		if (File.Exists(Application.persistentDataPath + "/stats.json")) {
			LoadData();
		} else {
			WriteData();
			LoadData();
		}
	}

}
