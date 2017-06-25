using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	[SerializeField] private Stats stats;

	
	void Start()
	{
		stats = GameObject.FindGameObjectWithTag ("Character").GetComponent<Stats>();
	}


	void Update () {

		if (stats.Score >= stats.ScoreToWin) {

			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<BoxCollider2D> ().enabled = true;
		//	Debug.Log ("The exit has appeared!");

		} else {
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<BoxCollider2D> ().enabled = false;
		}

	}
}
