using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

// [System.Serializable]
// public class StatsTracker {
// 	public string timePlayed;
// 	public int tapCount, chestCount, lootCount, scenesCount, autoTapUpgrades, upgradeCount, currencyCount, achievementsCount;
// 	public float totalExpGained;
// 	public float autoTapPower, tapPower;

// }

public class HUDController : MonoBehaviour {
	[Header("Loading Screen Properties")]
	public GameObject loadingScreen;
	public TextMeshProUGUI loadingText;

	[Header("Tutorial Properties")]
	public int tutorialIndex;
	public GameObject tutorialPopups;
	public GameObject[] dialogueBoxes;
	public GameObject upgradesMenu;

	[Header("Menu Properties")]
	public Button menuButton;
	public GameObject menuPanel;
	public GameObject menuButtonsLayout;

	[Header("Notification Properties")]
	public UnityEvent OnNotify;
	public GameObject[] notificationIcons;
	public GameObject levelupNotification;
	public TextMeshProUGUI levelUpText;
	[SerializeField] float levelUpTimer = 2f;

	[Header("Floating Text Properties")]
	public GameObject damageText;
	public GameObject expText;

	[Header("Stats Properties")]
	// [SerializeField] public StatsTracker m_statsTracker;
	public TextMeshProUGUI earningsText;
	public TextMeshProUGUI pledgesText;
	public TextMeshProUGUI dpsTracker;
	public Slider expBar;
	public TextMeshProUGUI levelText;
	public Slider timerBar;
	public Slider healthBar;
	public TextMeshProUGUI healthText;

	[Header("Achievements Properties")]
	public AchievementCard[] availableAchievementCards;

	// [Header("Stats Menu Properties")]
	// public StatCard[] availableStatCards;
	// private string[] displayedStats = new string[] {
	// 	"Taps", "Chests", "Loot", "Scenes", "Upgrades", "Currency",
	// 	"Level",
	// 	"Experience"
	// };

	string formatedEarnings;

	GameManager m_gameManager;
	AchievementManager m_achievementManager;

	void Awake() {
		m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
		m_achievementManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<AchievementManager>();
		
		levelupNotification.SetActive(false);
		notificationIcons[4].SetActive(true);
	}

	void Update() {
		UpdateHUD();

		if(levelupNotification.activeSelf) {
			levelUpTimer -= Time.deltaTime;
			if(levelUpTimer <= 0) {
				levelupNotification.SetActive(false);
				levelUpTimer = 2f;
			}
		}

		if(menuPanel.activeSelf) {
			m_gameManager.isMenuOpen = true;
			if(levelupNotification.activeSelf) { levelupNotification.SetActive(false); }
		} else {
			m_gameManager.isMenuOpen = false;
		}
	}

	public void UpdateHUD() {
		dpsTracker.text = ">> DPS: " + m_gameManager.roundedDPS;
		
		formatedEarnings = string.Format("{0:#,###0}", m_gameManager.totalCurrency);
		earningsText.text = ">> $ " + formatedEarnings;
		pledgesText.text = "// " + m_gameManager.totalPledges.ToString();
		
		levelText.text = ">> Level " + m_gameManager.level.ToString();
		expBar.value = m_gameManager.totalXP;
		expBar.maxValue = m_gameManager.xpNeededToLevel;

		healthBar.value = (float)m_gameManager.roundedHP;
		healthBar.maxValue = m_gameManager.maxHP;
		healthText.text = ">> " + m_gameManager.roundedHP + " / " + m_gameManager.maxHP + " <<";
		
		timerBar.value = m_gameManager.currentTimer;
		timerBar.maxValue = m_gameManager.maxTimer;
	}

	public void CloseMenu() {
		if(menuPanel.activeSelf) {
			menuPanel.SetActive(false);
		} else {
			menuPanel.SetActive(true);
			menuButtonsLayout.SetActive(true);
		}
	}

	public void UpdateAchievementCards() {
		Achievement[] tmpAchievements = AchievementManager.achievements;
		for (int i = 0; i < tmpAchievements.Length; i++) {
			availableAchievementCards[i].cardName.text = ((ACHIEVEMENT_LIST)i).ToString();
			availableAchievementCards[i].desc.text = "Level: " + tmpAchievements[i].achievementLevel + " (" + tmpAchievements[i].currentAchievementProgress + "/" + tmpAchievements[i].nextAchievementTarget + ")";
			availableAchievementCards[i].reward.text = "$ " + (500 + (tmpAchievements[i].achievementLevel * 500)).ToString();

			// TODO: Set Claim Button
		}
	}

	// public void UpdateStatsCards() {
	// 	Achievement[] tmpAchievements = AchievementManager.achievements;
	// 	for (int i = 0; i < displayedStats.Length; i++) {
	// 		availableStatCards[i].cardName.text = displayedStats[i];
	// 		availableStatCards[i].desc.text = tmpAchievements[i].currentAchievementProgress.ToString();
	// 	}
	// }

	public void ShowTutorialUI() {
		switch(tutorialIndex) {
			case 0:		// Dialogue Box: TAP THE LOOT BOX  TO DAMAGE IT 
				Debug.Log("tutorial index: " + tutorialIndex);
				tutorialPopups.SetActive(true);
				dialogueBoxes[tutorialIndex].SetActive(true);
				if(m_gameManager.tapCount == 5) {
					dialogueBoxes[tutorialIndex].SetActive(false);
					tutorialIndex = 1;		
				}
			break;
			case 1:		// Dialogue Box: DEAL ENOUGH DAMAGE BEFORE TIME RUNS OUT TO OPEN IT
				Debug.Log("tutorial index: " + tutorialIndex);
				dialogueBoxes[tutorialIndex].SetActive(true);
				float seconds = 5f;
				if(seconds <= 0) {
					dialogueBoxes[tutorialIndex].SetActive(false);
					seconds = 5f;					
				}
			break;
			case 2:		// Dialogue Box: USE THE REWARDS FROM LOOT BOXES AND REWARDS TO BUY UPGRADES AND BETTER LOOT BOXES
				Debug.Log("tutorial index: " + tutorialIndex);
				dialogueBoxes[tutorialIndex].SetActive(true);
				if(menuPanel.activeSelf) {					
					dialogueBoxes[tutorialIndex].SetActive(false);
				}
			break;
			case 3:		// Dialogue Box: HEAD TO THE UPGRADES MENU TO BUY YOUR FIRST UPGRADE!
				Debug.Log("tutorial index: " + tutorialIndex);
				dialogueBoxes[tutorialIndex].SetActive(true);
				if(StoreManager.storeCatalouge[0].isBought) {
					dialogueBoxes[tutorialIndex].SetActive(false);				
				}
			break;
			case 4:		// Dialogue Box: CLOSE THE MENU TO COMPLETE THE TUTORIAL
				Debug.Log("tutorial index: " + tutorialIndex);
				if(!menuPanel.activeSelf) {
					dialogueBoxes[tutorialIndex].SetActive(false);
					m_gameManager.isTutorialComplete = true;				
				}
			break;
			case 5:
				tutorialPopups.SetActive(false);
			break;
		}
	}

	public void DisplayDamageOutput() {
		Vector3 chestPos = new Vector3(0, -3, 0);
		GameObject dmgClone = Instantiate(damageText, chestPos, Quaternion.identity);
		dmgClone.GetComponent<TextMesh>().text = m_gameManager.tapDamage.ToString();
	}

	public void DisplayExpOutput(float earnedXP) {
		Vector3 expPos = new Vector3(0, -3, 0);
		GameObject xpClone = Instantiate(expText, expPos, Quaternion.identity);
		xpClone.GetComponent<TextMesh>().text = earnedXP.ToString() + "xp";
	}

	public void AnimateEarnings(string currency) {
		if(currency == "Earnings") {
			earningsText.GetComponent<Animator>().Play("EarningsTextAnimation", 0);
		} else if(currency == "Pledges") {
			pledgesText.GetComponent<Animator>().Play("EarningsTextAnimation", 0);
		}
	}

	public void AnimateLevelUpPopup(int level) {
		levelupNotification.SetActive(true);
		if(levelupNotification.activeSelf) {
			levelUpText.text = "You Reached Level " + level.ToString() + "!";
		}
	}

}
