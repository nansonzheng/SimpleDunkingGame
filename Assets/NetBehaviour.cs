using UnityEngine;
using System.Collections;

public class NetBehaviour : MonoBehaviour {

    private AudioSource johncena;


    private readonly string ballname = "Ball";
    private AudioSource scoreCue;
    private int score;
    private Canvas scoreText;
    public int random;

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
        scoreCue = GetComponent<AudioSource>();
        johncena = transform.Find("cena").GetComponent<AudioSource>();
	}
	

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == ballname) {
            scoreCue.Stop();
            johncena.Stop();
            Debug.Log("Scored!");
            scoreText.enabled = true;
            random = (int)Random.Range(0.0f, 10f);
            if (random < 9)
                scoreCue.Play();
            else
                johncena.Play();
        }
    }
}
