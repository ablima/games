using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Controller para as partículas de chama
*************************************************/

public class FlameScript : MonoBehaviour {

	private ParticleSystem particles;

	//As partículas de chama não possuem loop, portando só são executadas uma vez após seu nascimento.
	//Dessa forma, ao finalizar sua animação, a partícula é setada para desativada, para poder ir para o
	//PoolManager e ser instanciada futuramente.

	void Start () {
		particles = gameObject.GetComponent<ParticleSystem>();
	}
	
	void Update () {

		if(!particles.isPlaying)
			gameObject.SetActive(false);

	}

}
