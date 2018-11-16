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
	public static Achievement[] achievements;

	private int[] m_initialAchievementTargets = new int[] {
		100,	// TAP_COUNT
		5,		// CHEST_COUNT
		3,		// LOOT_COUNT
		1,		// SCENES_COUNT
		1,		// UPGRADES_COUNT
		1000,	// CURRENCY_COUNT
		5,		// LEVEL
		100,	// EXP_GAINED
		1,		// AUTOTAP_UPGRADES
		1,		// CHESTS_BROUGHT
		1		// TAP_POWER
	};
	private int m_numAchievements = 11;

	void Awake() {
		// DeleteAllSaveData(); // INFO: Un-comment this line if u want to delete saved Achievement data bofore start of game
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
			LoadAchievements();
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
			PlayerPrefs.SetInt(((ACHIEVEMENT_LIST)i).ToString() + "_Target", m_initialAchievementTargets[i]);
		}
	}

	public void DeleteAllSaveData() {
		if (PlayerPrefs.HasKey("AchievementsInited")) {
			for (int i = 0; i < m_numAchievements; i++) {
				PlayerPrefs.DeleteKey(((ACHIEVEMENT_LIST)i).ToString() + "_Level");
				PlayerPrefs.DeleteKey(((ACHIEVEMENT_LIST)i).ToString() + "_CurrProg");
				PlayerPrefs.DeleteKey(((ACHIEVEMENT_LIST)i).ToString() + "_Target");
			}
			PlayerPrefs.DeleteKey("AchievementsInited");
		}
	}

	// Use this to add to the achievement progress
	public static void AddAchievementProgress(ACHIEVEMENT_LIST achievementID, int ammount) {
		achievements[(int)achievementID].currentAchievementProgress += ammount;
		PlayerPrefs.SetInt(achievementID.ToString() + "_CurrProg", achievements[(int)achievementID].currentAchievementProgress);

		bool targetReached;

		// NOTE: The do while is so if the player completes 2  or more levels of an achievement in one go.
		do {
			targetReached = false;
			if (achievements[(int)achievementID].currentAchievementProgress >= achievements[(int)achievementID].nextAchievementTarget) {
				achievements[(int)achievementID].achievementLevel++;
				PlayerPrefs.SetInt(achievementID.ToString() + "_Level", achievements[(int)achievementID].achievementLevel);

				// TODO: Figure out a proper formule for the next target...
				achievements[(int)achievementID].nextAchievementTarget += (achievements[(int)achievementID].nextAchievementTarget * achievements[(int)achievementID].achievementLevel);
				PlayerPrefs.SetInt(achievementID.ToString() + "_Target", achievements[(int)achievementID].nextAchievementTarget);

				targetReached = true;

				// TODO: Add one to the achevement pop up list so it reasy to pop up
				// OR just pop up now and pause and resume code from here 
			} 
		} while(targetReached);
	}
}
