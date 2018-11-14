using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPoolBehaviour : MonoBehaviour {
	public Transform chestPrefab;

	void Awake() {
		SimplePool.Preload(chestPrefab.gameObject, 5);

		SimplePool.Spawn(chestPrefab.gameObject, transform.position, transform.rotation);
	}

	void Update() {

	}

}
