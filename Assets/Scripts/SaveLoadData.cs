using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "Save File", order = 1)]
public class SaveLoadData : ScriptableObject {
	public float earnedXP, currentXP;
	public float tapDamage, autoDamage;
	public float health, timer;
	public GameObject currentScene;

	[Header("Stats")]
	public int level;
	public float currency, pledges;
	public float timePlayed, totalExpGained, autoTapPower, tapPower;
	public int tapCount, currencyCount, pledgesCount, chestCount, lootCount, sceneCount, autoTapUpgrades, upgradeCount, achievementCount;

}
