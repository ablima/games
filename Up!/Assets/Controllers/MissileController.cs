using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Controller para os mísseis
*************************************************/

public class MissileController : MonoBehaviour {

	private Rigidbody body;

	void Start () {

		body = GetComponent<Rigidbody>();

	}
	
	void Update () {
				
	}

	//Caso o míssel entre em colisão com algum objeto, desativa o míssel para poder
	//ser instanciado pelo PoolManager
	void OnCollisionEnter(Collision other){

		body.velocity = new Vector3(0,0,0);
		gameObject.SetActive(false);

	}

}
