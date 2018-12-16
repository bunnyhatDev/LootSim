using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameManager : MonoBehaviour {
	public enum State {
		LOADING_SCREEN, SPAWN_CHEST, DAMAGE_PHASE, DESPAWN_CHEST, REWARD_SPAWN
	}

	public State currentState;
	public float loadingTimer, resetLoadingTimer;
	public bool isMenuOpen = false;

	[Header("Loot Box Properties")]
	public GameObject chestPrefab;
	public float currentHP;
	public double roundedHP;
	public float maxHP;
	public float tapDamage;
	public float autoDamage;
	public float currentTimer;
	public float maxTimer;
	public bool isDead = false;
	
	[Header("Player Attributes")]
	public string timePlayed;
	public int level;
	public float xpNeededToLevel, totalXP;
	// public float earnedXP, currentXP, maxXP;
	public float totalCurrency;
	public int pledges, totalPledges;
	public float currentDPS;
	public double roundedDPS;
	public int tapCount, chestsOpened, lootCollected, scenesUnlocked, upgradesUnlocked, autoTapUpgradesUnlocked, achievementsUnlocked;

	float pauseDPSCounter = 0.6f;
	float startTime;
	float remainderXp;
	
	TutorialManager m_tutorialManager;
	SaveManager m_saveManager;
	LootManager m_lootManager;
	HUDController m_hudController;

	void Awake() {
		m_tutorialManager = GetComponent<TutorialManager>();
		m_saveManager = GetComponent<SaveManager>();
		m_lootManager = GetComponent<LootManager>();
		m_hudController = GameObject.Find("HUD").GetComponent<HUDController>();

		startTime = Time.time;
		currentState = State.LOADING_SCREEN;
	}

	public void SetState(State newState) {
		switch(currentState) {
			case State.LOADING_SCREEN:
				newState = State.SPAWN_CHEST;
			break;
			case State.SPAWN_CHEST:
				newState = State.DAMAGE_PHASE;
			break;
			case State.DAMAGE_PHASE:
				newState = State.DESPAWN_CHEST;
			break;
			case State.DESPAWN_CHEST:
				newState = State.REWARD_SPAWN;
			break;
			case State.REWARD_SPAWN:
				newState = State.DAMAGE_PHASE;
			break;
		}
		currentState = newState;
	}

	void Update() {
		if(currentState != State.LOADING_SCREEN) {
			float t = Time.time - startTime;
			string minutes = ((int) t / 60).ToString();
			string seconds = (t % 60).ToString("f0");
			timePlayed = minutes + "m " + seconds + "s";
		}

		//TODO:Check and load in assets here to switch to next state for now it happens through key press
		if(currentState == State.LOADING_SCREEN) {
			if(Input.GetKeyDown(KeyCode.X)) {
				m_saveManager.DeleteData();
			}
			
			m_hudController.loadingScreen.SetActive(true);
			if(m_hudController.loadingScreen.activeSelf) { loadingTimer -= Time.deltaTime; }

			if(loadingTimer <= 0) {
				m_tutorialManager.isTutorialComplete = SaveManager.dataItems.tutorialCompleted;
				timePlayed = SaveManager.dataItems.timePlayed;
				totalXP = SaveManager.dataItems.totalXP;
				xpNeededToLevel = SaveManager.dataItems.xpNeededToLevel;
				tapDamage = SaveManager.dataItems.tapDamage;
				autoDamage = SaveManager.dataItems.autoDamage;
				currentHP = SaveManager.dataItems.health;
				currentTimer = SaveManager.dataItems.timer;

				level = SaveManager.dataItems.level;
				totalCurrency = SaveManager.dataItems.totalCurrency;
				totalCurrency = SaveManager.dataItems.overallCurrency;
				totalPledges = SaveManager.dataItems.totalPledges;
				totalPledges = SaveManager.dataItems.overallPledges;
				
				tapCount = SaveManager.dataItems.tapCount;
				chestsOpened = SaveManager.dataItems.chestCount;
				lootCollected = SaveManager.dataItems.lootCount;
				scenesUnlocked = SaveManager.dataItems.sceneCount;

				if(!m_tutorialManager.isTutorialComplete) {
					xpNeededToLevel = 50f;
					tapDamage = 0.75f;
					autoDamage = tapDamage * 1.45f;
					maxHP = 100f;
					SetState(State.SPAWN_CHEST);
				} else {
					SetState(State.SPAWN_CHEST);
				}

				m_hudController.UpdateHUD();

				loadingTimer = resetLoadingTimer;
				m_hudController.loadingScreen.SetActive(false);
				SimplePool.Preload(chestPrefab, 2);
			}
		} else if(currentState == State.SPAWN_CHEST) {
			SimplePool.Spawn(chestPrefab, new Vector3(0,-8.75f,6f), Quaternion.identity); //FIXME: Get rid of magic numbers
			if(totalXP >= xpNeededToLevel) {
				maxHP += 45.5f;
				currentHP = maxHP;
			} else {
				maxHP += 15.5f;
				currentHP = maxHP;
			}
			SetState(State.DAMAGE_PHASE);
		} else if(currentState == State.DAMAGE_PHASE) {
			roundedDPS = System.Math.Round(currentDPS, 3);
			roundedHP = System.Math.Round(currentHP, 2);
			if(currentHP <= 0) {
				currentHP = 0; 
				isDead = true;
			}

			if(currentTimer <= 0) { 
				currentTimer = maxTimer;
				currentHP = maxHP;
				isDead = false;
			}

			if(!isDead) {
				currentDPS = autoDamage / 60f;
				currentHP -= autoDamage * Time.deltaTime;
				currentTimer -= Time.deltaTime;

				if(currentTimer != 0) {
					if(Input.GetMouseButtonDown(0) && !isMenuOpen) {
						tapCount += 1;
						if(m_hudController.damageText) {
							m_hudController.DisplayDamageOutput();
						}
						currentHP -= tapDamage;
						pauseDPSCounter = 0.6f;
					}
					if(Input.GetKeyDown(KeyCode.Space)) {
						currentHP -= tapDamage * 5;
					}
				}
				if(Input.GetMouseButton(0)) {
					currentDPS = (autoDamage + tapDamage) / 60f;
					pauseDPSCounter -= Time.deltaTime;
					if(pauseDPSCounter <= 0) {
						currentDPS = autoDamage / 60f;
					}
				}
			} else {
				m_hudController.AnimateEarnings("Earnings");
				SetState(State.DESPAWN_CHEST);
			}
		} else if(currentState == State.DESPAWN_CHEST) {
			maxTimer -= 0.135f;
			currentHP = maxHP;
			currentTimer = maxTimer;
			SimplePool.Despawn(chestPrefab);
			for(int i = 0; i < m_lootManager.loot.Length; i++) {
				m_lootManager.loot[i].SetActive(true);
			}
			SetState(State.REWARD_SPAWN);
		} else if(currentState == State.REWARD_SPAWN) {
			AddCurrency(level * tapCount + 3);		//FIXME: get rid of magic numbers
			totalXP += 50;
			if(totalXP >= xpNeededToLevel) {
				LevelUp();
			} else {
				m_hudController.DisplayExpOutput(totalXP);
				remainderXp = 0;
			}

			chestsOpened += 1;
			lootCollected += 3;

			isDead = false;
			SetState(State.SPAWN_CHEST);
		}
	}

	void LevelUp() {
		remainderXp = xpNeededToLevel - totalXP;
		totalXP = Mathf.Abs(remainderXp);
		m_hudController.DisplayExpOutput(remainderXp + totalXP);

		level += 1;
		m_hudController.AnimateLevelUpPopup(level);
		xpNeededToLevel += 200.5f;

		tapDamage += 0.15f;
		autoDamage = tapDamage * 1.45f;
	}

	void AddCurrency(float money) {
		totalCurrency += money;
		m_hudController.AnimateEarnings("Currency");

		pledges = 2;
		totalPledges += pledges;
		m_hudController.AnimateEarnings("Pledges");
	}

}
