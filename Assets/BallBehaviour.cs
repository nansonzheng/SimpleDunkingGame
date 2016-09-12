using UnityEngine;
using System.Collections;


public class BallBehaviour : MonoBehaviour {
    

    private Rigidbody2D self;
    private AudioSource sound;

	// Use this for initialization
	void Start () {
        self = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Buttons to test ball physics
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            self.velocity = new Vector2(self.velocity.x - 1, self.velocity.y);
            Debug.Log("Ball pushed left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            self.velocity = new Vector2(self.velocity.x + 1, self.velocity.y);
            Debug.Log("Ball pushed right");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            self.velocity = new Vector2(self.velocity.x, self.velocity.y + 4);
            Debug.Log("Ball pushed up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            self.velocity = new Vector2(self.velocity.x, self.velocity.y - 4);
            Debug.Log("Ball pushed down");
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D c) {
        sound.Play();
    }
}
