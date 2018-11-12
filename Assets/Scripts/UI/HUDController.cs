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
	public Toggle soundToggle;
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

	ChestController m_chestController;

	void Awake() {
		m_chestController = GameObject.Find("Chest").GetComponent<ChestController>();

		healthBar.value = (float)m_chestController.roundedHP;
		healthBar.maxValue = m_chestController.maxHP;
		healthText.text = ">> " + m_chestController.roundedHP + " / " + m_chestController.maxHP + " <<";

		timerBar.value = m_chestController.currentTimer;
		timerBar.maxValue = m_chestController.maxTimer;
	}

	void Update() {
		healthBar.value = (float)m_chestController.roundedHP;
		healthText.text = ">> " + m_chestController.roundedHP + " / " + m_chestController.maxHP + " <<";
		
		timerBar.value = m_chestController.currentTimer;

		if(menuPanel.activeSelf) {
			m_chestController.isMenuOpen = true;
		} else {
			m_chestController.isMenuOpen = false;
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
