using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public enum State {
		LOADING_SCREEN,	SPAWN_CHEST, DAMAGE_PHASE, DESPAWN_CHEST, REWARD_SPAWN
	}

	public State currentState;
	public bool isMenuOpen = false;
	
	float pauseDPSCounter = 0.6f;

	[Header("Loot Box Properties")]
	public GameObject chestPrefab;
	public float currentHP;
	public double roundedHP;
	public float maxHP;
	// public GameObject damageText;
	public float tapDamage;
	public float idleDamage;
	public float currentTimer;
	public float maxTimer;
	public bool isDead = false;
	
	[Header("Player Attributes")]
	public string timePlayed;
	public int level;
	public float earnedXP, currentXP, maxXP, totalXP;
	public float currency, totalEarnings;
	public float currentDPS;
	public double roundedDPS;
	public int tapCount, chestsOpened, lootCollected, scenesUnlocked, upgradesUnlocked, autoTapUpgradesUnlocked, achievementsUnlocked;

	float startTime;
	float remainderXp;
	
	LootManager m_lootManager;
	HUDController m_hudController;

	void Awake() {
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
		float t = Time.time - startTime;
		string minutes = ((int) t / 60).ToString();
		string seconds = (t % 60).ToString("f0");
		timePlayed = minutes + "m " + seconds + "s";	

		//TODO:Check and load in assets here to switch to next state for now it happens through key press
		if(currentState == State.LOADING_SCREEN && Input.GetKeyDown(KeyCode.X)) {
			if(currentXP == 0) {
				chestsOpened = 0;
				lootCollected = 0;
				totalEarnings = 0;
				level = 0;
				maxXP = 25f;
				tapDamage = 0.75f;
				idleDamage = tapDamage * 1.45f;
			}
			// Debug.Log("Amount of broken items: " + m_lootManager.brokenItems.lootItem.Length);
			SimplePool.Preload(chestPrefab, 2);
			SetState(State.SPAWN_CHEST);
		} else if(currentState == State.SPAWN_CHEST) {
			SimplePool.Spawn(chestPrefab, new Vector3(0,-8.75f,6f), Quaternion.identity);
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
				currentDPS = idleDamage / 60f;
				currentHP -= idleDamage * Time.deltaTime;
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
					currentDPS = (idleDamage + tapDamage) / 60f;
					pauseDPSCounter -= Time.deltaTime;
					if(pauseDPSCounter <= 0) {
						currentDPS = idleDamage / 60f;
					}
				}
			} else {
				m_hudController.AnimateEarnings();
				SetState(State.DESPAWN_CHEST);
			}
		} else if(currentState == State.DESPAWN_CHEST) {
			maxHP += 25f;
			maxTimer -= 0.135f;
			currentHP = maxHP;
			currentTimer = maxTimer;
			SimplePool.Despawn(chestPrefab);
			for(int i = 0; i < m_lootManager.loot.Length; i++) {
				m_lootManager.loot[i].SetActive(true);
			}
			SetState(State.REWARD_SPAWN);
		} else if(currentState == State.REWARD_SPAWN) {
			currency = 15;
			totalEarnings += currency;
			earnedXP = remainderXp + 10f;
			currentXP += earnedXP;
			totalXP += earnedXP + currentXP;
			
			chestsOpened += 1;
			lootCollected += 3;
			if(currentXP >= maxXP) {
				// Debug.Log("remainder: " + remainderXp);
				remainderXp = maxXP - earnedXP;
				currentXP = remainderXp;
				maxXP += 5.5f;
				level += 1;
				m_hudController.AnimateLevelUpPopup(level);
				currency = 20;
				totalEarnings += currency;
				tapDamage += 0.15f;
				idleDamage = tapDamage * 1.45f;
				m_hudController.DisplayExpOutput(remainderXp + earnedXP);
			} else {
				m_hudController.DisplayExpOutput(earnedXP);
				remainderXp = 0;
			}
			isDead = false;
			SetState(State.SPAWN_CHEST);
		}
	}

}
