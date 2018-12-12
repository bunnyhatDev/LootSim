using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour {
	public Color flashColor, hiddenColor;
	
	Renderer m_renderer;
	GameManager m_gameManager;

	void Awake() {
		m_renderer = GetComponent<Renderer>();
		m_renderer.material.shader = Shader.Find("Outlined/Silhouetted Diffuse");
		m_gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
	}

	void Update () {
		if(Input.GetMouseButtonDown(0) && !m_gameManager.isMenuOpen) {
			// Debug.Log("damage color changing");
			StartCoroutine("Damage");
		}		
	}

	IEnumerator Damage() {
		m_renderer.material.color = flashColor;
		m_renderer.material.SetFloat("_OutlineColor", flashColor.a);
		yield return new WaitForSeconds(0.5f);
		m_renderer.material.color = hiddenColor;
		m_renderer.material.SetFloat("_OutlineColor", hiddenColor.a);

	}

}
