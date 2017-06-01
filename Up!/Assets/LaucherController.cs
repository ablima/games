using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaucherController : MonoBehaviour {

	public GameObject obj;
	public float spawnTime;

	private float counter;

	// Use this for initialization
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if(counter >= spawnTime){
			counter = 0;
         		Instantiate(obj, transform.position, transform.rotation);
		}
	}
}
