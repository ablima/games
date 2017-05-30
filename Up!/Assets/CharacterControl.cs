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

	private Rigidbody2D body;
	private Vector2 swipeStart;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		speed = 0.3f;
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
					break;
				case TouchPhase.Ended:
					Debug.Log("finalizou");
					float swipeVertical = (new Vector3(0, t.position.y, 0) - new Vector3(0, swipeStart.y, 0)).magnitude;
					if(swipeVertical > minSwipeY){
						float swipeDirection = Mathf.Sign(t.position.y - swipeStart.y);
						if(swipeDirection > 0 && !jump){
							jump = true;
							var jumpVector = new Vector3(0.0f, 2.0f, 0.0f);
							body.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
						}
					}

					float swipeHorizontal = (new Vector3(t.position.x,0, 0) - new Vector3(swipeStart.x, 0, 0)).magnitude;

					if(swipeHorizontal > minSwipeX){
						float swipeDirection = Mathf.Sign(t.position.x - swipeStart.x);
						if(swipeDirection > 0)
							moveRight = true;
						if(swipeDirection < 0)
							moveLeft = true;
					}
					break;

			}
		}

		var direction = 0;
		if(moveRight){
			direction = 1;
		}
		if(moveLeft){
			direction = -1;
		}

		var move = new Vector3(direction, 0, 0);
         	transform.position += move * speed;
	}
}
