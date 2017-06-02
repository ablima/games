using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		body = GetComponent<Rigidbody>();
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

		if(Input.touchCount > 0){
			
			Touch t = Input.touches[0];

			switch(t.phase){

				case TouchPhase.Began:

					swipeStart = t.position;
					stopMoving();
					break;

				case TouchPhase.Ended:

					float swipeVertical = (new Vector3(0, t.position.y, 0) - new Vector3(0, swipeStart.y, 0)).magnitude;

					if(swipeVertical > minSwipeY){
						float swipeDirection = Mathf.Sign(t.position.y - swipeStart.y);
						if(swipeDirection > 0 && !jump){
							jump = true;
							body.AddForce(transform.up * jumpForce);
                        	animator.SetTrigger("jump");
						}
					}

					float swipeHorizontal = (new Vector3(t.position.x,0, 0) - new Vector3(swipeStart.x, 0, 0)).magnitude;

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

		/********RETIRAR ESTA PARTE AO FINALIZAR********/

		if ( Input.GetKey(KeyCode.LeftArrow) ){
			moveRight = false;
			moveLeft = true;
			isRunning = false;
		}
		if ( Input.GetKey(KeyCode.RightArrow) ){
                        moveRight = true;
                        moveLeft = false;
                        isRunning = false;
                }	
		if ( Input.GetKey(KeyCode.DownArrow) ){
                        stopMoving();
                }
		if ( Input.GetKey(KeyCode.UpArrow) && !jump ){
                        jump = true;
			body.AddForce(transform.up * jumpForce);
                        animator.SetTrigger("jump");
                }

		/**********************************************/

		var scaleX = transform.localScale.x;
		var rotationY = transform.eulerAngles.y;

		if(rotate){

        	transform.rotation = Quaternion.Euler(0, rotationY+rotationIncrement, 0);
			var finished = false;

			if(rotationIncrement > 0){

				if(toRotation >= 360){
					if(transform.eulerAngles.y + rotationIncrement < 10)
						finished = true;
				}else{
					if(transform.eulerAngles.y + rotationIncrement > toRotation)
						finished = true;
				}

			}else{

				if(transform.eulerAngles.y + rotationIncrement < toRotation)
					finished = true;

			}

			if(finished){

				if(toRotation >= 360)
					toRotation = 0;

				transform.rotation = Quaternion.Euler(0, toRotation, 0);
				rotationY = toRotation;
                rotate = false;
                isRunning = false;

			}

        }

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

        if(isRunning)
            transform.position += direction * speed;

	}

    void OnParticleCollision(GameObject other) {

        var tag = other.tag;

        if(tag=="killer")
            die();

    }

	void OnCollisionEnter(Collision other){

		var tag = other.gameObject.tag;

        if(tag=="ground" && jump)
            jump = false;

		if(tag=="wall")
			stopMoving();

		if(tag=="killer")
			die();

    }

	void OnTriggerEnter(Collider other){

		var tag = other.gameObject.tag;

        if(tag=="rotate"){

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

        if(tag=="stop")
            stopMoving();

		if(tag=="killer")
			die();

	}

	void die(){

	    stopMoving();
		transform.position = respawnPoint.transform.position;
        transform.rotation = respawnPoint.transform.rotation;
        body.velocity = new Vector3(0,0,0);

	}

	void stopMoving(){

        moveRight = false;
        moveLeft = false;
        isRunning = false;
        direction = new Vector3(0,0,0);
        animator.SetBool("run", false);

    }

}
