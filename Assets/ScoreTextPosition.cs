using UnityEngine;
using System.Collections;

public class ScoreTextPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RectTransform rec = GetComponent<RectTransform>();
        rec.anchoredPosition = new Vector2(rec.rect.width / 2, -rec.rect.height / 2);
	}
	
}
