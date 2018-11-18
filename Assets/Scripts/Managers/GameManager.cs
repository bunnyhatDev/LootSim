using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public enum State {
		LOADING_SCREEN,	SPAWN_CHEST, DAMAGE_PHASE, DESPAWN_CHEST, REWARD_SPAWN
	}

	public State currentState;
	public bool isMenuOpen = false;

	[Header("Loot Box Properties")]
	public GameObject chestPrefab;
	public float currentHP;
	public double roundedHP;
	public float maxHP;
	public float tapDamage;
	public float idleDamage;
	public float currentTimer;
	public float maxTimer;
	public bool isDead = false;

	[Header("Player Attributes")]
	public int level;
	public float currentXP, maxXP;
	public float earnings;
	public int chestsOpened;
	
	LootManager m_lootManager;
	HUDController m_hudController;

	void Awake() {
		m_lootManager = GetComponent<LootManager>();
		m_hudController = GameObject.Find("HUD").GetComponent<HUDController>();

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
		//TODO:Check and load in assets here to switch to next state for now it happens through key press
		if(currentState == State.LOADING_SCREEN && Input.GetKeyDown(KeyCode.X)) {
			//FIXME: This if function will be placed in an event system which will check through
			// the save file to see what the player's EXP is.
			if(currentXP == 0) {
				chestsOpened = 0;
				earnings = 0;
				level = 0;
				maxXP = 25f;
				tapDamage = 0.75f;
				idleDamage = tapDamage * 1.45f;
			}
			// Debug.Log("Amount of broken items: " + m_lootManager.brokenItems.lootItem.Length);
			SimplePool.Preload(chestPrefab, 1);
			SetState(State.SPAWN_CHEST);
		} else if(currentState == State.SPAWN_CHEST) {
			SimplePool.Spawn(chestPrefab, new Vector3(0,-1.5f,0), transform.rotation);
			SetState(State.DAMAGE_PHASE);
		} else if(currentState == State.DAMAGE_PHASE) {
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
				currentHP -= idleDamage * Time.deltaTime;
				currentTimer -= Time.deltaTime;
				if(currentTimer != 0) {
					if(Input.GetMouseButtonDown(0) && !isMenuOpen) {
						currentHP -= tapDamage;
					}
					if(Input.GetKeyDown(KeyCode.Space)) {
						currentHP -= tapDamage * 5;
					}
				}
			} else {
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
			earnings += 15;
			currentXP += 25f;
			chestsOpened += 1;
			if(currentXP >= maxXP) {
				float remainderXp = currentXP - maxXP;
				currentXP = 0;
				currentXP += remainderXp;
				maxXP += 5.5f;
				level += 1;
				earnings += 20;
				tapDamage += 0.15f;
				idleDamage = tapDamage * 1.45f;
			}
			isDead = false;
			SetState(State.SPAWN_CHEST);
		}

	}

}
