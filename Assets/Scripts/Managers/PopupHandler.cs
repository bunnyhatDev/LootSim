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
	private bool m_initedPopups = false;
	private int m_currentReward;

	void LateUpdate() {
		if (pendingPopups.Count > 0 && !m_initedPopups) {
			m_initedPopups = true;
			StartCoroutine(ShowPopups());
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
		// TODO: Add the reward money to total money
		achievementPopup.gameObject.SetActive(false);
		popupParent.SetActive(false);
		m_showingPopup = false;
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
					break;

				default:
					Debug.LogError("popup type not specified or it dose not have a case. Given type number: " + pendingPopups[i].popupType);
					break;
			}

			m_showingPopup = true;

			while (m_showingPopup) {
				yield return null;
			}
		}

		pendingPopups.Clear();
		m_initedPopups = false;
	}
}
