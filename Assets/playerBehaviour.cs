using UnityEngine;
using System.Collections;

public class playerBehaviour : MonoBehaviour {

	//Our Pushing Force On the Player
	public float thrustHorizontal=10;
	public float thrustVertical=100;

	public bool grounded=false;
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
		float horizontal=0;
		float vertical=0;

		//Update Grounded Check
		checkGrounded();

		//On the Ground Control Schemes
		if (grounded) {
			//Horizontal Movement
			horizontal = Input.GetAxis("horizontalChar1") * thrustHorizontal;

			//Vertical Movement
			if(Input.GetButton("verticalChar1") && grounded){
				vertical = thrustVertical;
			}

			self.AddForce (new Vector2(horizontal, vertical));
		} 

		//Off the Ground Control Schemes
		else {
			//Horizontal Movement & Torque
			horizontal = Input.GetAxis("horizontalChar1") * thrustHorizontal;

			self.AddForce (new Vector2(horizontal, 0));
		}
	}

	void checkGrounded(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
	}
}
