using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointController : MonoBehaviour {

	public ParticleSystem particles;
	public bool picked;

	// Use this for initialization
	void Start () {
		picked = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
                if(other.gameObject.tag == "Player"){
			picked = true;
			particles.Play();
			gameObject.SetActive(false);
		}
	}

}
