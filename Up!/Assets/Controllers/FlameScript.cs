using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour {

	private ParticleSystem particles;

	void Start () {
		particles = gameObject.GetComponent<ParticleSystem>();
	}
	
	void Update () {

		if(!particles.isPlaying)
			gameObject.SetActive(false);

	}

}
