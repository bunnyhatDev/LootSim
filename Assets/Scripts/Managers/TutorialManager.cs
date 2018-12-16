using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    [Header("Tutorial Properties")]
	public GameObject[] dialogueBoxes;
    public int tutorialIndex;
    public bool isTutorialComplete;

    float seconds = 5f;

    GameManager m_gameManager;
    HUDController m_hud;

    void Awake() {
        m_gameManager = GetComponent<GameManager>();
        m_hud = GameObject.Find("HUD").GetComponent<HUDController>();

        if(m_gameManager.level < 1) {
            isTutorialComplete = false;
            tutorialIndex = 0;
        } else {
            isTutorialComplete = true;
            for(int i = 0; i < dialogueBoxes.Length; i++) {
                dialogueBoxes[i].SetActive(false);
            }
        }
    }

    void Update() {
        if(!isTutorialComplete) {
            switch(tutorialIndex) {
                case 0:		// Dialogue Box: TAP THE LOOT BOX  TO DAMAGE IT 
                    Debug.Log("tutorial index: " + tutorialIndex);
                    dialogueBoxes[tutorialIndex].SetActive(true);
                    if(m_gameManager.tapCount == 5) {
                        dialogueBoxes[tutorialIndex].SetActive(false);
                        tutorialIndex += 1;
                    }
                break;
                case 1:		// Dialogue Box: DEAL ENOUGH DAMAGE BEFORE TIME RUNS OUT TO OPEN IT
                    Debug.Log("tutorial index: " + tutorialIndex);
                    dialogueBoxes[tutorialIndex].SetActive(true);
                    seconds -= Time.deltaTime;
                    if(seconds <= 0) {
                        dialogueBoxes[tutorialIndex].SetActive(false);
                        seconds = 5f;
                        tutorialIndex += 1;			
                    }
                break;
                case 2:		// Dialogue Box: USE THE REWARDS FROM LOOT BOXES AND REWARDS TO BUY UPGRADES AND BETTER LOOT BOXES
                    Debug.Log("tutorial index: " + tutorialIndex);
                    dialogueBoxes[tutorialIndex].SetActive(true);
                    if(m_hud.menuPanel.activeSelf) {					
                        dialogueBoxes[tutorialIndex].SetActive(false);
                        tutorialIndex += 1;
                    }
                break;
                case 3:		// Dialogue Box: HEAD TO THE UPGRADES MENU TO BUY YOUR FIRST UPGRADE!
                    Debug.Log("tutorial index: " + tutorialIndex);
                    dialogueBoxes[tutorialIndex].SetActive(true);
                    if(StoreManager.storeCatalouge[0].isBought) {
                        dialogueBoxes[tutorialIndex].SetActive(false);
                        tutorialIndex += 1;
                    }
                break;
                case 4:		// Dialogue Box: CLOSE THE MENU TO COMPLETE THE TUTORIAL
                    Debug.Log("tutorial index: " + tutorialIndex);
                    if(!m_hud.menuPanel.activeSelf) {
                        dialogueBoxes[tutorialIndex].SetActive(false);
                        isTutorialComplete = true;	
                    }
                break;
            }
        }
    }
 
}
