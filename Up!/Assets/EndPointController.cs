using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour {

	public ParticleSystem particles;
	public pointController flag1;
	public pointController flag2;
	public pointController flag3;

	private bool activated;

	// Use this for initialization
	void Start () {
		activated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(flag1.picked && flag2.picked && flag3.picked){
			if(!particles.isPlaying)
				particles.Play();
			activated = true;
		}	
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player" && activated){
			Debug.Log("finalizou");
		}
	}

}
