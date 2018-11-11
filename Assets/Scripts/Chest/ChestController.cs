﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

	[Header("Chest Variables")]
	public static float currentHP;
	public double roundedHP;
	public float maxHP;
	public float tapDamage;
	public float idleDamage;
	public float currentTimer;
	public float maxTimer;
	public GameObject[] loot;

	public bool isMenuOpen = false;		//FIXME:(1) Remove this from here and put it in GameManager
	private bool isDead = false;

    void Awake() {
		// maxHP = 10;
		currentHP = maxHP;

		idleDamage = tapDamage * 1.45f;

		// maxTimer = 59;
		currentTimer = maxTimer;
    }
	
	void Update () {
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
			}
		} else {
			for(int i = 0; i < loot.Length; i++) {
				loot[i].SetActive(true);
			}
		}

	}
}
