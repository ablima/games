using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {

	private Rigidbody body;

	void Start () {

		body = GetComponent<Rigidbody>();

	}
	
	void Update () {
				
	}

	void OnCollisionEnter(Collision other){

		body.velocity = new Vector3(0,0,0);
		gameObject.SetActive(false);

	}

}
