using UnityEngine;
using System.Collections;

public class NetBehaviour : MonoBehaviour {

    private readonly string ballname = "Ball";
    private int score;
    private Canvas scoreText;

    private EdgeCollider2D scoreline;

	// Use this for initialization
	void Start () {
        scoreline = GetComponent<EdgeCollider2D>();
        scoreText = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (scoreText == null)
        {
            Debug.Log("Can't find canvas");
        }
        else {
            scoreText.enabled = false;
        }
	}
	

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == ballname) {
            Debug.Log("Scored!");
            scoreText.enabled = true;
        }
    }
}
