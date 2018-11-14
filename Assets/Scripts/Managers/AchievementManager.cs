using UnityEngine;

public enum ACHIEVEMENT_LIST {
	TAP_COUNT, CHEST_COUNT, LOOT_COUNT, SCENES_COUNT, UPGRADES_COUNT, CURRENCY_COUNT,
	LEVEL,
	EXP_GAINED,
	AUTOTAP_UPGRADES,
	CHESTS_BROUGHT,
	TAP_POWER
}

[System.Serializable]
public struct Achievement {
	public ACHIEVEMENT_LIST achievementIndex;
	public int achievementLevel;
	public int currentAchievementProgress;
	public int nextAchievementTarget;
}

public class AchievementManager : MonoBehaviour {
	public Achievement[] achievements;

	private int m_numAchievements = 11;

	void Awake() {
		CreateAchievementsList();
		CheckAchievements();
	}

	private void CreateAchievementsList() {
		achievements = new Achievement[m_numAchievements];

		for (int i = 0; i < m_numAchievements; i++) {
			achievements[i].achievementIndex = (ACHIEVEMENT_LIST)i;
			achievements[i].achievementLevel = 0;
		}
	}

	private void CheckAchievements() {
		if (PlayerPrefs.HasKey("AchievementsInited")) {
			LoadAchievements();
		} else {
			SaveInitialAchievements();
		}
	}

	private void LoadAchievements() {
		for (int i = 0; i < m_numAchievements; i++) {
			achievements[i].achievementLevel = PlayerPrefs.GetInt(((ACHIEVEMENT_LIST)i).ToString() + "_Level");
			achievements[i].currentAchievementProgress = PlayerPrefs.GetInt(((ACHIEVEMENT_LIST)i).ToString() + "_CurrProg");
			achievements[i].nextAchievementTarget = PlayerPrefs.GetInt(((ACHIEVEMENT_LIST)i).ToString() + "_Target");
		}
	}

	private void SaveInitialAchievements() {
		PlayerPrefs.SetInt("AchievementsInited", 1);
		for (int i = 0; i < m_numAchievements; i++) {
			PlayerPrefs.SetInt(((ACHIEVEMENT_LIST)i).ToString() + "_Level", 0);
			PlayerPrefs.SetInt(((ACHIEVEMENT_LIST)i).ToString() + "_CurrProg", 0);
			PlayerPrefs.SetInt(((ACHIEVEMENT_LIST)i).ToString() + "_Target", 0);
		}

		// TODO: Set the  intial target 
	}
}
