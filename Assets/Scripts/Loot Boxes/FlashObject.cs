using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour {

	public MeshRenderer[] m_renderer;
	public Material[] normalMat;

	GameManager m_gameManager;

	void Awake() {
		m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
	}

	void Update () {
		if(Input.GetMouseButtonDown(0) && !m_gameManager.isMenuOpen) {
			StartCoroutine("Damage");
		}		
	}

	IEnumerator Damage() {
		for(int i = 0; i < m_renderer.Length; i++) {
			m_renderer[i].material.color = Color.red;
			yield return new WaitForSeconds(0.5f);
			m_renderer[i].material.color = normalMat[i].color;
		}
	}

}
