using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

	public float speed;
	public bool moveRight;
	public bool moveLeft;
	public bool jump;

	public float minSwipeY;
     	public float minSwipeX;
	public float jumpForce;

	private Rigidbody body;
	private Animator animator;
	private Vector2 swipeStart;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		speed = 0.3f;
		jumpForce = 300.0f;
		moveRight = false;
		moveLeft = false;
		jump = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.touchCount > 0){
			
			Touch t = Input.touches[0];
			switch(t.phase){
				case TouchPhase.Began:
					swipeStart = t.position;
					moveRight = false;
					moveLeft = false;
					Debug.Log("iniciou");
					animator.SetBool("run", false);
					break;
				case TouchPhase.Ended:
					Debug.Log("finalizou");
					float swipeVertical = (new Vector3(0, t.position.y, 0) - new Vector3(0, swipeStart.y, 0)).magnitude;
					if(swipeVertical > minSwipeY){
						float swipeDirection = Mathf.Sign(t.position.y - swipeStart.y);
						if(swipeDirection > 0 && !jump){
							jump = true;
						}
					}

					float swipeHorizontal = (new Vector3(t.position.x,0, 0) - new Vector3(swipeStart.x, 0, 0)).magnitude;

					if(swipeHorizontal > minSwipeX){
						float swipeDirection = Mathf.Sign(t.position.x - swipeStart.x);
						if(swipeDirection > 0)
							moveRight = true;
						if(swipeDirection < 0)
							moveLeft = true;
						animator.SetBool("run", true);
					}
					break;

			}
		}

		var direction = 0;
		if(moveRight){
			direction = 1;
			animator.SetBool("run", true);
			var scaleX = transform.localScale.x;
			if(scaleX < 0){
				scaleX = -scaleX;
			}
			transform.localScale = new Vector3( scaleX, transform.localScale.y, transform.localScale.z );
		}
		if(moveLeft){
			direction = -1;
			animator.SetBool("run", true);
			var scaleX = transform.localScale.x;
                        if(scaleX > 0){
                                scaleX = -scaleX;
                        }
                        transform.localScale = new Vector3( scaleX, transform.localScale.y, transform.localScale.z );
		}
		if(jump){
			body.AddForce(transform.up * jumpForce);
			jump = false;
			animator.SetTrigger("jump");
		}

		var move = new Vector3(direction, 0, 0);
         	transform.position += move * speed;
	}
}
