using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoadData {
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

	public SaveLoadData(GameManager gm) {
		timePlayed = gm.timePlayed;
		earnedXP = gm.earnedXP;
		currentXP = gm.currentXP;
		tapDamage = gm.tapDamage;
		autoDamage = gm.autoDamage;
		health = gm.currentHP;
		timer = gm.currentTimer;

		level = gm.level;
		totalCurrency = gm.totalCurrency;
		overallCurrency = gm.totalCurrency;
		totalPledges = gm.totalPledges;
		overallPledges = gm.totalPledges;
		totalExpGained = gm.totalXP;
		
		tapCount = gm.tapCount;
		chestCount = gm.chestsOpened;
		lootCount = gm.lootCollected;
		sceneCount = gm.scenesUnlocked;
	}

}
