using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {
	float blend;
	Animator m_animator;

	GameManager m_gameManager;

	void Awake() {
		m_animator = GetComponent<Animator>();
		m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();

		blend = 0;
		m_animator.SetFloat("Blend", blend);
	}

	void Update() {
		m_animator.SetFloat("Blend", blend);
		if(m_gameManager.currentHP >= 76) {
			blend = 0;
		} else if(m_gameManager.currentHP <= 75f && m_gameManager.currentHP >= 51f) {
			blend = 0.5f;
		} else if(m_gameManager.currentHP <= 50f && m_gameManager.currentHP >= 25f) {
			blend = 1f;
		}
	}
}
