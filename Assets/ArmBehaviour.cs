using UnityEngine;
using System.Collections;

public class ArmBehaviour : MonoBehaviour {

    private readonly string ballname = "Ball";
    private KeyCode pickupbutton = KeyCode.Mouse0;
    private KeyCode jumpbutton = KeyCode.Space;

    private Vector3 mousePos;
    private Vector3 objectPos;
    private float angle;

    private GameObject ball;
    private Rigidbody2D ballRB;
    private Collider2D arm;
    private bool pickedup;
    private HingeJoint2D hinge;

    // Use this for initialization
    void Start() {
        arm = GetComponent<Collider2D>();
        ball = null;
        ballRB = null;
        pickedup = false;
        mousePos = Input.mousePosition;
        hinge = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        mousePos = Input.mousePosition;
        mousePos.z = 0;
        objectPos = Camera.main.WorldToScreenPoint(hinge.anchor);
        // reuse mousePos for difference vector
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetKeyDown(pickupbutton))
        {
            if (ball != null)
            {
                if (pickedup)
                {
                    ball.transform.parent = null;
                    ballRB.isKinematic = false;
                    pickedup = false;
                    ballRB.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
                }
                else {
                    ball.transform.parent = arm.transform;
                    ballRB.isKinematic = true;
                    ball.transform.localPosition = new Vector3(2, 0, 0);
                    pickedup = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == ballname && !pickedup) {
            ball = other.gameObject;
            ballRB = ball.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.Equals(ball)) {
            ball = null;
        }
    }
}
