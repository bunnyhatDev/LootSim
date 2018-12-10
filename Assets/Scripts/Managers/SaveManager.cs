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
	// public GameObject currentScene; // <-- FIXES: a gameobject can't be serialized to be saved, caused error

	[Header("Stats")]
	public int level;
	public float totalCurrency, overallCurrency;
	public int totalPledges, overallPledges;
	public float totalExpGained;
	public int tapCount, chestCount, lootCount, sceneCount, autoTapUpgrades, upgradeCount, achievementCount;
}


public class SaveManager : MonoBehaviour {
	// public static List<Data> data = new List<Data>();
	public static Data dataItems = new Data(); // <-- FIXES: Had to init as a new data before starting to use it

	public Data tempForDebug; // <-- FIXES: to see the current values

	GameManager m_gameManager;

	void Awake() {
		m_gameManager = GetComponent<GameManager>();

		InitStats();

		tempForDebug = dataItems; // <-- FIXES: to see the current values
	}

	void LateUpdate() {
		// SaveData(m_gameManager);
	}

	public void WriteData() {

		// FIXES: ADDED TEMP NUMBERS FOR TESTING, remove them after testing

		Debug.Log("CREATE");
		dataItems.timePlayed = m_gameManager.timePlayed = "iii";
		dataItems.earnedXP = m_gameManager.earnedXP = 0.5f;
		dataItems.currentXP = m_gameManager.currentXP = 0.552f;
		dataItems.tapDamage = m_gameManager.tapDamage = 5.552f;
		dataItems.autoDamage = m_gameManager.autoDamage = 1000.552f;
		dataItems.health = m_gameManager.currentHP = 2f;
		dataItems.timer = m_gameManager.currentTimer = 1520f;

		dataItems.level = m_gameManager.level = 5;
		dataItems.totalCurrency = m_gameManager.totalCurrency = 50.552f;
		dataItems.overallCurrency = m_gameManager.totalCurrency = 20.552f;
		dataItems.totalPledges = m_gameManager.totalPledges= 10;
		dataItems.overallPledges = m_gameManager.totalPledges= 9;
		dataItems.totalExpGained = m_gameManager.totalXP = 10.5f;
		
		dataItems.tapCount = m_gameManager.tapCount = 1000000;
		dataItems.chestCount = m_gameManager.chestsOpened = 10000;
		dataItems.lootCount = m_gameManager.lootCollected = 1000;
		dataItems.sceneCount = m_gameManager.scenesUnlocked = 100;

		SaveData();
	}

	public void SaveData() {
		Debug.Log("SAVE");
		// savedGames.Add(Game.current);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/stats.json");
		// FileStream file = File.Create(Path.Combine(Application.persistentDataPath,"/savedGames.gd"));
		bf.Serialize(file, dataItems);
		file.Close();
	}
	
	public static void LoadData() {
		Debug.Log("LOAD");
		if (File.Exists(Application.persistentDataPath + "/stats.json")) {
		// if (File.Exists(Path.Combine(Application.persistentDataPath, "/savedGames.gd"))) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/stats.json", FileMode.Open);
			// FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "/savedGames.gd"), FileMode.Open);
			dataItems = (Data)bf.Deserialize(file); // <-- FIXES: I think as Data also works, but was too lazy to change back
			file.Close();
		} else {
			Debug.Log("NO SAVED DATA");
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
