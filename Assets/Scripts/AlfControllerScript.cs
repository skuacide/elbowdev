﻿using UnityEngine;
using System.Collections;

public class AlfControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 300f;
	bool doubleJump;
	Vector2 start;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		SetStart();
	}

	void SetStart(){
		start = rigidbody2D.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		if (grounded) {
			doubleJump = false;
		}

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs(move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip();		
		}
	}

	void Update(){
		if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
			if(!doubleJump && !grounded){
				doubleJump = true;
			}
		}
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Die(){
		transform.position = start;
	}
}
