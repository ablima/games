using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Controller para o ponto de saída da fase
*************************************************/

public class EndPointController : MonoBehaviour {

	public ParticleSystem particles;
	public PointController flag1;
	public PointController flag2;
	public PointController flag3;
	public string level;

	private bool activated;

	void Start () {
		activated = false;
	}

	//Para que o ponto seja ativado, é necessário que o jogador tenha pegado as 3 chaves espalhadas no cenário.
	//Essas chaves são referenciadas nos objetos flag1, flag2 e flag3.
	void Update () {

		//Caso o jogador tenha pegado as 3 chaves, aciona o ponto de saída
		if(flag1.picked && flag2.picked && flag3.picked){

			if(!particles.isPlaying)
				particles.Play();

			activated = true;

		}

	}

	void OnTriggerEnter(Collider other){

		//Caso o jogador tenha colidido com o ponto de saída, e o mesmo estiver ativo, carrega a próxima fase.
		if(other.gameObject.tag == "Player" && activated){
			PoolManager.Instance.desactivateAll();
			Application.LoadLevel(level);
		}

	}

}
