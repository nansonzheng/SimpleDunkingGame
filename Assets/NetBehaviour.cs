using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NetBehaviour : MonoBehaviour {


    private readonly string ballname = "Ball";
    private AudioSource scoreCue, johncena, wombo;
    private int score;
    private Text scoreText;
    public int random;
    public int cooldown;
    private EdgeCollider2D scoreline;
    private Collider2D[] net;

	// Use this for initialization
	void Start () {
        scoreline = GetComponent<EdgeCollider2D>();
        net = GetComponents<Collider2D>();
        scoreText = GameObject.Find("Canvas").GetComponentInChildren<Text>();
        scoreCue = GetComponent<AudioSource>();
        johncena = transform.Find("cena").GetComponent<AudioSource>();
        wombo = transform.Find("wombo").GetComponent<AudioSource>();
        score = 0;
        cooldown = 2;

        // Move net to somewhere playable (vertical only)
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 netextents = GameObject.Find("net").GetComponent<SpriteRenderer>().bounds.extents;
        transform.position = new Vector3(cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - netextents.x, transform.position.y, transform.position.z);

    }


    void OnTriggerEnter2D(Collider2D other) {
		if (other.name == ballname && other.transform.position.y - scoreline.transform.position.y >= 0) {
            scoreCue.Stop();
            johncena.Stop();
            wombo.Stop();
            UpdateScore(other.transform);

            // Play cue
            random = (int)Random.Range(0.0f, 10f);
            if (random == 9)
                johncena.Play();
            else if (random == 0)
                wombo.Play();
            else
                scoreCue.Play();

        }
    }

    // Update text
    void UpdateScore(Transform ball) {
        Debug.Log("Scored!");
        score += 1;
        if (ball.parent != null) {
            ball.parent = null;
        }
        scoreText.text = "Score: " + score + "!!!";
        // Disable colliders momentarily
        foreach (Collider2D c in net) {
            c.enabled = false;
        }
        StartCoroutine(ReenableNetIn(cooldown));
    }

    IEnumerator ReenableNetIn(int seconds) {
        yield return new WaitForSeconds(seconds);
        foreach (Collider2D c in net)
        {
            c.enabled = true;
        }
    }
}
