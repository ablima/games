using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaucherController : MonoBehaviour {

	public GameObject obj;
	public bool isProjectile;
	public bool isParticle;
	public float force;
	public float spawnTime;

	private float counter;

	void Start () {

		counter = 0;
		PoolManager.Instance.CreatePool( obj, 5, 10 );

	}
	
	void Update () {

		counter += Time.deltaTime;

		if(counter >= spawnTime){

			counter = 0;
         	var newObj = PoolManager.Instance.GetObject(obj.name);
         	newObj.transform.position = transform.position;
         	newObj.transform.rotation = transform.rotation;

         	if(isProjectile){
         		var body = newObj.GetComponent<Rigidbody>();
         		body.AddForce(transform.up * force);
			}

			if(isParticle){
				var particle = newObj.GetComponent<ParticleSystem>();
				particle.Play();
			}

		}

	}

}
