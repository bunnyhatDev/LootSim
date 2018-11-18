using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour {
	[Header("Menu Properties")]
	public GameObject[] notificationIcons;
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

	private string formatedEarnings;

	GameManager m_gameManager;

	// ChestController m_chestController;

	void Awake() {
		// m_chestController = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestController>();
		m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
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
	
}
