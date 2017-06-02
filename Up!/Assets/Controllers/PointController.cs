using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Controller para as chaves
*************************************************/

public class PointController : MonoBehaviour {

	public ParticleSystem particles;
	public bool picked;

	//As chaves são necessárias para que o ponto de fim seja ativado. Portanto é necessário um flag para
	//indicar se a chave foi pega pelo jogador, para sinalizar ao ponto de fim se ele deve ser ativado ou não.
	void Start () {
		picked = false;
	}
	
	void Update () {
	}

	//Caso o jogador entre em contato com a chave, seta o flag "picked" para true, e desativa o mesmo.
	void OnTriggerEnter(Collider other){
    	if(other.gameObject.tag == "Player"){
			picked = true;
			particles.Play();
			gameObject.SetActive(false);
		}
	}

}