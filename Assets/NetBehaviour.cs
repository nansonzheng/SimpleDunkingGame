using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NetBehaviour : MonoBehaviour {

    private AudioSource johncena;


    private readonly string ballname = "Ball";
    private AudioSource scoreCue;
    private int score;
    private Text scoreText;
    public int random;
    public int cooldown;
    private EdgeCollider2D scoreline;
    private PolygonCollider2D net;

	// Use this for initialization
	void Start () {
        scoreline = GetComponent<EdgeCollider2D>();
        net = GetComponent<PolygonCollider2D>();
        scoreText = GameObject.Find("Canvas").GetComponentInChildren<Text>();
        scoreCue = GetComponent<AudioSource>();
        johncena = transform.Find("cena").GetComponent<AudioSource>();
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
            Debug.Log("Scored!");
            score += 1;
            UpdateScore();
            random = (int)Random.Range(0.0f, 10f);
            if (random < 9)
                scoreCue.Play();
            else
                johncena.Play();

            // Disable colliders momentarily
            net.enabled = false;
            scoreline.enabled = false;
            StartCoroutine(ReenableNetIn(cooldown));
        }
    }

    // Update text
    void UpdateScore() {
        scoreText.text = "Score: " + score + "!!!";
    }

    IEnumerator ReenableNetIn(int seconds) {
        yield return new WaitForSeconds(seconds);
        net.enabled = true;
        scoreline.enabled = true;
    }
}
