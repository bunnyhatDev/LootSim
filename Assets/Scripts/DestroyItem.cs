using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour {

	public float timer = 3;

	void Awake() {
		Destroy(this.gameObject, timer);
	}
}
