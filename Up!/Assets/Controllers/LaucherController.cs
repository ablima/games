using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Controller para o objeto para Spawnar outros objetos
*************************************************/

public class LaucherController : MonoBehaviour {

	public GameObject obj;
	public bool isProjectile;
	public bool isParticle;
	public float force;
	public float spawnTime;

	private float counter;

	//Cria uma pool com os objetos a serem spawnados
	void Start () {

		counter = 0;
		PoolManager.Instance.CreatePool( obj, 5, 10 );

	}

	//Atualiza o seu contador e verifica se já está na hora de spawnar um novo objeto.
	void Update () {

		counter += Time.deltaTime;

		//Caso dê o tempo de spawn, requesita um objeto do PoolManager para spawnar.
		if(counter >= spawnTime){

			counter = 0;
         	var newObj = PoolManager.Instance.GetObject(obj.name);
         	newObj.transform.position = transform.position;
         	newObj.transform.rotation = transform.rotation;

			//Se o objeto for um projétil, aplica a força para o projétil se movimentar.
         	if(isProjectile){
         		var body = newObj.GetComponent<Rigidbody>();
         		body.AddForce(transform.up * force);
			}

			//Se o objeto for uma partícula, inicia a animação da partícula ao spawna-lo.
			if(isParticle){
				var particle = newObj.GetComponent<ParticleSystem>();
				particle.Play();
			}

		}

	}

}
