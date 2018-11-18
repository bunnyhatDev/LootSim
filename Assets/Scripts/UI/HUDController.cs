using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour {
	[Header("Menu Properties")]
	public Button menuButton;
	public GameObject menuPanel;
	public GameObject menuButtonsLayout;
	public Button closeButton;
	public Toggle sfxToggle;
	public Toggle musicToggle;
	public Button achievementsButton;

	[Header("Stats Properties")]
	public TextMeshProUGUI earningsText;
	public TextMeshProUGUI dpsTracker;
	public Slider expBar;
	public TextMeshProUGUI levelText;
	public Slider timerBar;
	public Slider healthBar;
	public TextMeshProUGUI healthText;

	[Header("Achievements Properties")]
	public AchievementCard[] availableAchievementCards;

	[Header("Stats Menu Properties")]
	public StatCard[] availableStatCards;
	private string[] displaiedStats = new string[] {
		"Taps", "Chests", "Loot", "Scenes", "Upgrades", "Currency",
		"Level",
		"Experience"
	};

	private string formatedEarnings;

	GameManager m_gameManager;
	AchievementManager m_achievementManager;

	// ChestController m_chestController;

	void Awake() {
		// m_chestController = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestController>();
		m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
		m_achievementManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<AchievementManager>();
	}

	void Update() {
		dpsTracker.text = ">> DPS: " + m_gameManager.roundedDPS;
		formatedEarnings = string.Format("{0:#,###0}", m_gameManager.earnings);
		earningsText.text = ">> $ " + formatedEarnings;
		levelText.text = ">> Level " + m_gameManager.level.ToString();
		expBar.value = m_gameManager.currentXP;
		expBar.maxValue = m_gameManager.maxXP;

		healthBar.value = (float)m_gameManager.roundedHP;
		healthBar.maxValue = m_gameManager.maxHP;
		healthText.text = ">> " + m_gameManager.roundedHP + " / " + m_gameManager.maxHP + " <<";
		
		timerBar.value = m_gameManager.currentTimer;
		timerBar.maxValue = m_gameManager.maxTimer;

		if(menuPanel.activeSelf) {
			m_gameManager.isMenuOpen = true;
		} else {
			m_gameManager.isMenuOpen = false;
		}
	}

	public void ToggleAudio() {
		if(sfxToggle.isOn) {

		} else {

		}
		if(musicToggle.isOn) {

		} else {

		}
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

	public void UpdateStatsCards() {
		Achievement[] tmpAchievements = AchievementManager.achievements;
		for (int i = 0; i < displaiedStats.Length; i++) {
			availableStatCards[i].cardName.text = displaiedStats[i];
			availableStatCards[i].desc.text = tmpAchievements[i].currentAchievementProgress.ToString();
		}
	}


}
