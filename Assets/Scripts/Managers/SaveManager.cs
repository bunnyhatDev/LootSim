using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Data {
	public bool tutorialCompleted;
	public string timePlayed;
	public float totalXP, xpNeededToLevel;
	public float tapDamage, autoDamage;
	public float health, timer;
	// public GameObject currentScene; // <-- FIXES: a gameobject can't be serialized to be saved, caused error

	[Header("Stats")]
	public int level;
	public float totalCurrency, overallCurrency;
	public int totalPledges, overallPledges;
	public int tapCount, chestCount, lootCount, sceneCount, autoTapUpgrades, upgradeCount, achievementCount;
}


public class SaveManager : MonoBehaviour {
	// public static List<Data> data = new List<Data>();
	public static Data dataItems = new Data(); // <-- FIXES: Had to init as a new data before starting to use it

	public Data tempForDebug; // <-- FIXES: to see the current values

	[SerializeField] float timeToSave, resetTime;

	GameManager m_gameManager;
	TutorialManager m_tutorialManager;

	void Awake() {
		m_gameManager = GetComponent<GameManager>();
		m_tutorialManager = GetComponent<TutorialManager>();

		InitStats();

		tempForDebug = dataItems; // <-- FIXES: to see the current values
		resetTime = timeToSave;
	}

	void LateUpdate() {
		timeToSave -= Time.deltaTime;
		if(timeToSave <= 0) {
			WriteData();
			tempForDebug = dataItems;
			timeToSave = resetTime;
		}
	}

	public void WriteData() {
		// FIXES: ADDED TEMP NUMBERS FOR TESTING, remove them after testing
		Debug.Log("CREATE");
		dataItems.tutorialCompleted = m_tutorialManager.isTutorialComplete;

		dataItems.timePlayed = m_gameManager.timePlayed;
		dataItems.totalXP = m_gameManager.totalXP;
		dataItems.xpNeededToLevel = m_gameManager.xpNeededToLevel;
		dataItems.tapDamage = m_gameManager.tapDamage;
		dataItems.autoDamage = m_gameManager.autoDamage;
		dataItems.health = m_gameManager.currentHP;
		dataItems.timer = m_gameManager.currentTimer;

		dataItems.level = m_gameManager.level;
		dataItems.totalCurrency = m_gameManager.totalCurrency;
		dataItems.overallCurrency = m_gameManager.totalCurrency;
		dataItems.totalPledges = m_gameManager.totalPledges;
		dataItems.overallPledges = m_gameManager.totalPledges;
		
		dataItems.tapCount = m_gameManager.tapCount;
		dataItems.chestCount = m_gameManager.chestsOpened;
		dataItems.lootCount = m_gameManager.lootCollected;
		dataItems.sceneCount = m_gameManager.scenesUnlocked;

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

	public void DeleteData() {
		Debug.Log("DELETE");
		File.Delete(Application.persistentDataPath + "/stats.json");
		if(!File.Exists(Application.persistentDataPath + "/stats.json")) {
			dataItems.tutorialCompleted = m_tutorialManager.isTutorialComplete = false;
			
			dataItems.timePlayed = m_gameManager.timePlayed = "0m 0s";
			dataItems.totalXP = m_gameManager.totalXP = 0;
			dataItems.xpNeededToLevel = m_gameManager.xpNeededToLevel = 50f;
			dataItems.tapDamage = m_gameManager.tapDamage = 0.75f;
			dataItems.autoDamage = m_gameManager.autoDamage = m_gameManager.tapDamage * 1.45f;
			dataItems.health = m_gameManager.currentHP = 0;
			dataItems.timer = m_gameManager.currentTimer = 0;

			dataItems.level = m_gameManager.level = 0;
			dataItems.totalCurrency = m_gameManager.totalCurrency = 0;
			dataItems.overallCurrency = m_gameManager.totalCurrency = 0;
			dataItems.totalPledges = m_gameManager.totalPledges = 0;
			dataItems.overallPledges = m_gameManager.totalPledges = 0;
			
			dataItems.tapCount = m_gameManager.tapCount = 0;
			dataItems.chestCount = m_gameManager.chestsOpened = 0;
			dataItems.lootCount = m_gameManager.lootCollected = 0;
			dataItems.sceneCount = m_gameManager.scenesUnlocked = 0;
		}
		m_gameManager.currentState = GameManager.State.LOADING_SCREEN;
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
