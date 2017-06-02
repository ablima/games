using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour {

	public ParticleSystem particles;
	public bool picked;

	void Start () {
		picked = false;
	}
	
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