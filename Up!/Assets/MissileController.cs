using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {

	private Rigidbody body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		body.AddForce(transform.up * 400f);
	}
	
	// Update is called once per frame
	void Update () {
				
	}

	void OnCollisionEnter(Collision other){
		DestroyObject(gameObject);
	}

}
