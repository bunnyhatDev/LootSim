using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PopupInfo {
	public int popupType; // 0 == Achievement // 1 == something else?
	public string itemName;
	public string itemDesc;
	public int rewardAmmount;
}

public class PopupHandler : MonoBehaviour {

	public GameObject popupParent;
	public AchievementCard achievementPopup;
	public static List<PopupInfo> pendingPopups = new List<PopupInfo>();

	private bool m_showingPopup = false;
	private bool m_popupFullScaled = false;
	private bool m_popUpClosed = true;

	private bool m_initedPopups = false;
	private int m_currentReward;

	GameManager m_gameManager;

	void Awake() {
		m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
		popupParent.SetActive(false);	
	}

	void LateUpdate() {
		if (pendingPopups.Count > 0 && !m_initedPopups) {
			m_initedPopups = true;
			StartCoroutine(ShowPopups());
		}

		// For simple Animaiton
		if (m_showingPopup && !m_popupFullScaled) {
			popupParent.transform.localEulerAngles = Vector3.MoveTowards(popupParent.transform.localEulerAngles, Vector3.zero, 500f * Time.deltaTime);
			if(popupParent.transform.localEulerAngles == Vector3.zero) {
				m_popupFullScaled = true;
			}
		} else if(!m_showingPopup && !m_popUpClosed){
			popupParent.transform.localEulerAngles = Vector3.MoveTowards(popupParent.transform.localEulerAngles, new Vector3(0f, 180f, 0f), 500f * Time.deltaTime);
			if (popupParent.transform.localEulerAngles == new Vector3(0f, 180f, 0f)) {
				m_popUpClosed = true;
				achievementPopup.gameObject.SetActive(false);
				popupParent.SetActive(false);
			}
		}
	}

	// Type : // 0 == Achievement // 1 == something else?
	public static void AddPopup(int type, string name, string desc, int reward) {
		PopupInfo tmpInfo;
		tmpInfo.popupType = type;
		tmpInfo.itemName = name;
		tmpInfo.itemDesc = desc;
		tmpInfo.rewardAmmount = reward;
		pendingPopups.Add(tmpInfo);
	}

	public void ClosePopup() {
		if (m_popupFullScaled) {
			// TODO: Add the reward money to total money
			m_gameManager.totalEarnings += m_currentReward;
			m_showingPopup = false;
			m_popupFullScaled = false;
			popupParent.transform.localEulerAngles = new Vector3(0f, 355f, 0f);
		}
	}

	private IEnumerator ShowPopups() {
		// foreach(PopupInfo pui in pendingPopups) {
		for (int i = 0; i < pendingPopups.Count; i++) {
			popupParent.SetActive(true);

			switch (pendingPopups[i].popupType) {
				case 0:
					achievementPopup.cardName.text = pendingPopups[i].itemName;
					achievementPopup.desc.text = pendingPopups[i].itemDesc;
					achievementPopup.reward.text = "$ " + pendingPopups[i].rewardAmmount;
					achievementPopup.gameObject.SetActive(true);

					m_currentReward = pendingPopups[i].rewardAmmount;

					// For simple Animaiton
					popupParent.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
					break;

				default:
					Debug.LogError("popup type not specified or it dose not have a case. Given type number: " + pendingPopups[i].popupType);
					break;
			}

			m_popUpClosed = false;
			m_popupFullScaled = false;
			m_showingPopup = true;

			while (m_showingPopup || !m_popUpClosed) {
				yield return null;
			}
		}

		pendingPopups.Clear();
		m_initedPopups = false;
	}
}
