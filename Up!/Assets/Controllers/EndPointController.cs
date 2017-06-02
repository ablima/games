using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour {

	public ParticleSystem particles;
	public PointController flag1;
	public PointController flag2;
	public PointController flag3;

	private bool activated;

	void Start () {
		activated = false;
	}
	
	void Update () {

		if(flag1.picked && flag2.picked && flag3.picked){

			if(!particles.isPlaying)
				particles.Play();

			activated = true;

		}

	}

	void OnTriggerEnter(Collider other){

		if(other.gameObject.tag == "Player" && activated)
			Application.LoadLevel("Menu");

	}

}
