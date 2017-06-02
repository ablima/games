using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Controller de comportamento do personagem
controlado pelo jogador.
*************************************************/

public class CharacterControl : MonoBehaviour {

	public GameObject respawnPoint;

	public float speed;
	public bool moveRight;
	public bool moveLeft;
	public bool jump;

	public float minSwipeY;
    public float minSwipeX;
	public float jumpForce;

    private bool isRunning;
	private bool rotate;
	private float rotationIncrement;
	private float toRotation;

	private Rigidbody body;
	private Animator animator;
	private Vector2 swipeStart;
	private Vector3 direction;

	void Start () {

		//Componente referente a física. Usado para aplicar força para o personagem pular.
		body = GetComponent<Rigidbody>();

		//Componente referente a animação. Usado para mudar as animações do personagem.
		animator = GetComponent<Animator>();

		jumpForce = 500.0f;
		moveRight = false;
		moveLeft = false;
		jump = false;
		rotate = false;
		isRunning = false;
		direction = new Vector3(0,0,0);

	}

	void Update () {

		//Verifica se teve algum toque na tela
		if(Input.touchCount > 0){
			
			Touch t = Input.touches[0];

			//Se sim, verifica em que estágio o toque está (inicio ou fim)
			switch(t.phase){

				//Caso seja o início de um toque na tela, guarda a posição do toque
				//e para a movimentação do personagem
				case TouchPhase.Began:

					swipeStart = t.position;
					stopMoving();
					break;

				//Caso seja o fim de um toque, verifica a direção do movimento feito pelo jogador
				case TouchPhase.Ended:

					float swipeVertical = (new Vector3(0, t.position.y, 0) - new Vector3(0, swipeStart.y, 0)).magnitude;

					//Caso o movimento tenha sido vertical e maior que o movimento mínimo estabelecido,
					//faz o personagem pular e ativa a animação de pulo
					if(swipeVertical > minSwipeY){
						float swipeDirection = Mathf.Sign(t.position.y - swipeStart.y);
						if(swipeDirection > 0 && !jump){
							jump = true;
							body.AddForce(transform.up * jumpForce);
                        	animator.SetTrigger("jump");
						}
					}

					float swipeHorizontal = (new Vector3(t.position.x,0, 0) - new Vector3(swipeStart.x, 0, 0)).magnitude;

					//Caso o movimento tenha sido horizontal e maior que o movimento mínimo estabelecido,
					//ativa o flag para o personagem andar para esquerda ou para direita
					if(swipeHorizontal > minSwipeX){
						float swipeDirection = Mathf.Sign(t.position.x - swipeStart.x);
						if(swipeDirection > 0)
							moveRight = true;
						if(swipeDirection < 0)
							moveLeft = true;
						isRunning = false;
					}
					break;

			}

		}

		var scaleX = transform.localScale.x;
		var rotationY = transform.eulerAngles.y;

		//Caso o flag para rotacionar esteja ativado, rotaciona o personagem (junto com a câmera) até
		//o angulo da variável "toRotation"
		if(rotate){

        	transform.rotation = Quaternion.Euler(0, rotationY+rotationIncrement, 0);
			var finished = false;

			//Caso o incremento da rotação seja positivo, verifica se a rotação atual é maior que o valor desejado.
			//Caso sim, finaliza a rotação.
			//Se a rotação desejada for igual ou maior que 360, verifica se a rotação atual está em 0, pois a rotação
			//do personagem, do tipo eulerAngles, só aceita valores entre 0 e 360. Logo, se a rotação é incrementada
			//acima de 360, o valor da rotação volta para 0.
			if(rotationIncrement > 0){

				if(toRotation >= 360){
					if(transform.eulerAngles.y + rotationIncrement < 10)
						finished = true;
				}else{
					if(transform.eulerAngles.y + rotationIncrement > toRotation)
						finished = true;
				}

			//Caso o incremento da rotação seja negativo, verifica se a rotação atual é menor que o valor desejado.
			//Caso sim, finaliza a rotação.
			}else{

				if(transform.eulerAngles.y + rotationIncrement < toRotation)
					finished = true;

			}

			//Caso uma das condições acima seja atendida, finaliza a rotação.
			if(finished){

				if(toRotation >= 360)
					toRotation = 0;

				transform.rotation = Quaternion.Euler(0, toRotation, 0);
				rotationY = toRotation;
                rotate = false;
                isRunning = false;

			}

        }

		//Caso o personagem deva andar para direita, e ainda não tenha iniciado seu movimento, verifica para qual
		//posição o personagem deve se movimentar de acordo com a sua rotação, que indica em qual plano do cenário
		//o personagem se encontra no momento.
		if(moveRight && !isRunning){

		    isRunning = true;
			animator.SetBool("run", true);

			if(rotationY > -5 && rotationY < 5){
				direction = new Vector3(1,0,0);
                if(scaleX < 0)
                    scaleX = -scaleX;
			}

			if(rotationY > 85 && rotationY < 95){
				direction = new Vector3(0,0,-1);
                if(scaleX < 0)
                    scaleX = -scaleX;
			}

			if(rotationY > 175 && rotationY < 185){
                direction = new Vector3(-1,0,0);
                if(scaleX < 0)
                    scaleX = -scaleX;
            }

			if(rotationY > 265 && rotationY < 275){
                direction = new Vector3(0,0,1);
                if(scaleX < 0)
                    scaleX = -scaleX;
            }

			transform.localScale = new Vector3( scaleX, transform.localScale.y, transform.localScale.z );

		}

		//Caso o personagem deva andar para esquerda, e ainda não tenha iniciado seu movimento, verifica para qual
        //posição o personagem deve se movimentar de acordo com a sua rotação, que indica em qual plano do cenário
        //o personagem se encontra no momento.
		if(moveLeft && !isRunning){

		    isRunning = true;
			animator.SetBool("run", true);

			if(rotationY > -5 && rotationY < 5){
                direction = new Vector3(-1,0,0);
                if(scaleX > 0)
                    scaleX = -scaleX;
            }

            if(rotationY > 85 && rotationY < 95){
                direction = new Vector3(0,0,1);
                if(scaleX > 0)
                    scaleX = -scaleX;
            }

            if(rotationY > 175 && rotationY < 185){
                direction = new Vector3(1,0,0);
                if(scaleX > 0)
                    scaleX = -scaleX;
            }

            if(rotationY > 265 && rotationY < 275){
                direction = new Vector3(0,0,-1);
                if(scaleX > 0)
                    scaleX = -scaleX;
            }

            transform.localScale = new Vector3( scaleX, transform.localScale.y, transform.localScale.z );

		}

		//Movimenta o personagem na direção especificada acima
        if(isRunning)
            transform.position += direction * speed;

	}

	//Verifica as colisões do personagem com partículas
    void OnParticleCollision(GameObject other) {

        var tag = other.tag;

		//Caso a partícula seja do tipo "killer", mata o personagem
        if(tag=="killer")
            die();

    }

	//Verifica as colisões do personagem com objetos
	void OnCollisionEnter(Collision other){

		var tag = other.gameObject.tag;

		//Caso seja o tipo "ground", significa que o personagem pousou de um pulo ou queda.
        if(tag=="ground" && jump)
            jump = false;

		//Caso seja uma parede, para a movimentação do personagem.
		if(tag=="wall")
			stopMoving();

		//Caso seja um tipo "killer", mata o personagem
		if(tag=="killer")
			die();

    }

	//Verifica as colisões do personagem com triggers
	void OnTriggerEnter(Collider other){

		var tag = other.gameObject.tag;

		//Caso seja um cubo de rotação, inicia a rotação para um determinado ângulo.
        if(tag=="rotate"){

			//Verifica para qual ângulo o personagem deve ser rotacionado, de acordo com sua movimentação e
			//e rotação atual.
			if(!rotate){

				var rotationY = transform.eulerAngles.y;
				if(moveLeft){
					toRotation = rotationY + 90;
				}
				if(moveRight){
					toRotation = rotationY - 90;
					if(toRotation < 0){
						rotationY = 360;
						toRotation = rotationY + toRotation;
						transform.rotation = Quaternion.Euler(0, rotationY, 0);
					}
				}

				if(rotationY > toRotation)
					rotationIncrement = -5f;
				else
					rotationIncrement = 5f;

				rotate = true;
				direction = new Vector3(0,0,0);

			}

        }

		//Caso seja um trigger tipo "stop", para o personagem.
        if(tag=="stop")
            stopMoving();

		//Caso seja do tipo "killer", mata o personagem.
		if(tag=="killer")
			die();

	}

	//Mata o personagem, zerando suas variáveis para condições iniciais e o reposicionando para o local
	//de respawn, indicado pelo objeto "respawnPoint"
	void die(){

	    stopMoving();
		transform.position = respawnPoint.transform.position;
        transform.rotation = respawnPoint.transform.rotation;
        body.velocity = new Vector3(0,0,0);

	}

	//Para a movimentação do personagem
	void stopMoving(){

        moveRight = false;
        moveLeft = false;
        isRunning = false;
        direction = new Vector3(0,0,0);
        animator.SetBool("run", false);

    }

}
