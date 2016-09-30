using UnityEngine;
using System.Collections;

public class playerBehaviour : MonoBehaviour {

	//Our Pushing Force On the Player
	public float thrustHorizontal=10;
	public float thrustVertical=100;
	public float torque = 5;

	public bool grounded=false;
	public bool doubleJump=false;
	public float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

	private Rigidbody2D self;

	// Use this for initialization
	void Start () {
		self = GetComponent<Rigidbody2D>();
	}

	//Called Every Fixed Interval
	void FixedUpdate(){
		float horizontal = 0;
		float horizontalTorque = 0;
		float vertical = 0;

		//Update Grounded Check
		//checkGrounded();

		//On the Ground Control Schemes
		if (grounded) {
			//Horizontal Movement
			horizontal = Input.GetAxis("horizontalChar1") * thrustHorizontal;

			//Vertical Movement
			if(Input.GetButtonDown("verticalChar1")){
					vertical = thrustVertical;
					self.velocity = new Vector2(self.velocity.x, vertical);
					grounded = false;
			}

			self.AddForce (new Vector2(horizontal, 0));
		} 

		//Off the Ground Control Schemes
		else {
			//Horizontal Movement & Torque
			horizontalTorque = Input.GetAxis("horizontalChar1") * torque;
			horizontal = Input.GetAxis("horizontalChar1") * thrustHorizontal;

			//Vertical Movement
			if(Input.GetButtonDown("verticalChar1") && doubleJump){
				vertical = thrustVertical;
				self.velocity = new Vector2(self.velocity.x, vertical);
				doubleJump = false;
			}

			self.AddTorque (-1*horizontalTorque);
			self.AddForce (new Vector2(horizontal, 0));
		}
	}

	void checkGrounded(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (((1<<other.gameObject.layer) & whatIsGround) != 0) {
			grounded = true;
			doubleJump = true;
		}
	}

	/*
	void OnCollisionExit2D(Collision2D other) {
		if (((1<<other.gameObject.layer) & whatIsGround) != 0) {
			grounded = false;
		}
	}*/
}
