using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnimations : MonoBehaviour {

	[SerializeField] private Animator anim;
	[SerializeField] private GridMove GridMove;
	[SerializeField] private Stats stats;

	void Update () {

		if(GridMove.enabled){

			anim.SetFloat ("HorizontalAxis", Input.GetAxis("Horizontal"));
			anim.SetFloat ("VerticalAxis", Input.GetAxis("Vertical"));

			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				anim.Play ("moveLeft");
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				anim.Play ("moveRight");
			}
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				anim.Play ("moveUp");
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				anim.Play ("moveDown");
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				anim.Play ("moveLeft");
			}
			if (Input.GetKey (KeyCode.None)) {
				anim.Play ("idle");
			}
			if (Input.GetKey (KeyCode.C)) {
				Death ();
			}

		}



	}


	public void Death()
	{
		if (GridMove.enabled) {
			anim.Play ("suicide");
			GridMove.enabled = false;
			StartCoroutine (DelayBeforeRespawn ());
		}
	}


	public IEnumerator DelayBeforeRespawn ()
	{

		yield return new WaitForSeconds (1.5f);
		stats.RemainingLives -= 1;
		stats.Reset ();
	}


	}

