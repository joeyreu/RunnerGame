using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpPower; // finishing jump

	bool started;
	public bool isJumping;
	float startHeight;
	bool isGrounded;
	bool isRWall;
	bool isLWall;


	Rigidbody2D rb;

	Animator ani;

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
		ani = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		started = false;
		isJumping = false;
		isGrounded = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Set Animation Parameters
		ani.SetFloat ("speedx", rb.velocity.x);
		ani.SetFloat ("speedy", rb.velocity.y);
		ani.SetBool ("grounded", isGrounded);
		ani.SetBool ("rwall", isRWall);
		ani.SetBool ("lwall", isLWall);
		if (rb.velocity.x == 0) {
			ani.SetBool ("zerox", true);
		} else {
			ani.SetBool ("zerox", false);
		}
		if (rb.velocity.y == 0) {
			ani.SetBool ("zeroy", true);
		} else {
			ani.SetBool ("zeroy", false);
		}


		if (!started) {
			if (Input.GetMouseButtonDown (0)) {
				rb.velocity = new Vector2 (speed, rb.velocity.y);
				started = true;
			}
		} else {

			// check if player is up to speed - adjust if not
			if (isGrounded && isRWall) {
				// nothing
			} else if (speed < 6) {
				if (isGrounded) {
					speed = 6;
					if (!isLWall) {
						Vector2 temp = new Vector2 (rb.velocity.x + 1, rb.velocity.y);
						rb.velocity = temp;
					}
				}
			} else if (rb.velocity.x < speed) {
				Vector2 temp = new Vector2 (rb.velocity.x + 1, rb.velocity.y);
				rb.velocity = temp;
			} else if (rb.velocity.x > speed) {
				Vector2 temp = new Vector2 (speed, rb.velocity.y);
				rb.velocity = temp;
			}
				
			// Check if jump is finished
			if (isJumping) {
				float diff = transform.position.y - startHeight;
				if (diff <= 3) {
					rb.velocity = new Vector2 (speed, 7f);
				} else {
					isJumping = false;
				}
			}// Check if on wall
			if (isRWall || isLWall) {
				if (rb.velocity.y < -3.5f) {
					rb.velocity = new Vector2 (rb.velocity.x, -3.5f);
				}
			}
		}
			
	}
		
	public void StartJump(){
		if (started) {
			if (isGrounded == true) {
				isGrounded = false;
				isJumping = true;
				startHeight = transform.position.y;
			} else if (isRWall) {
				isJumping = true;
				startHeight = transform.position.y;
				SwitchDirection ();
			} else if (isLWall) {
				isJumping = true;
				startHeight = transform.position.y;
				SwitchDirection ();
			}
		}



	}

	public void EndJump(){
		isJumping = false;
	}

	public void SwitchDirection(){
		speed = -speed;
	}

	void OnCollisionEnter2D(Collision2D col) {
		// TODO: Check if Collision is with Terrain or Hostile
		if(col.contacts.Length > 0){
			ContactPoint2D contact = col.contacts[0];
			if (Vector2.Dot (contact.normal, Vector2.up) > 0.5) {
				// bottom
				isGrounded = true;
			} else if (Vector2.Dot (contact.normal, -Vector2.up) > 0.5) {
				// top
				EndJump ();
			} else if (Vector2.Dot (contact.normal, Vector2.right) < 0.5) {
				// right
				isRWall = true;
			} else {
				// left
				isLWall = true;
			}	
		}
	}

	void OnCollisionStay2D(Collision2D col){
		// TODO: Check if Collision is with Terrain or Hostile
		if(col.contacts.Length > 0){
			ContactPoint2D contact = col.contacts[0];
			if (Vector2.Dot (contact.normal, Vector2.up) > 0.5) {
				isGrounded = true;
			} else if (Vector2.Dot (contact.normal, -Vector2.up) > 0.5) {
			} else if (Vector2.Dot (contact.normal, Vector2.right) < 0.5) {
				isRWall = true;
			} else {
				isLWall = true;
			}	
		}
	}

	void OnCollisionExit2D(Collision2D col){
		// reset collision bools in case
		isGrounded = false;
		isRWall = false;
		isLWall = false;
			
	}


	public void Reset(){
		SceneManager.LoadScene (0);
	}


}
