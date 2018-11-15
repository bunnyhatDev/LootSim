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
	public TextMeshProUGUI chestTrackerText;
	public Slider expBar;
	public TextMeshProUGUI levelText;
	public Slider timerBar;
	public Slider healthBar;
	public TextMeshProUGUI healthText;

	GameManager m_gameManager;

	// ChestController m_chestController;

	void Update() {
		// m_chestController = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestController>();
		m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();

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
