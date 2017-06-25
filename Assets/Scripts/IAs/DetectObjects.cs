using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour {


	[SerializeField] private Stats stats;
	[SerializeField] private EnableAnimations anim;

	void OnTriggerEnter2D(Collider2D col){
		string name = col.transform.name;
		//Debug.Log ("Triggered : " + name);
		Stats myStat = new Stats ();

		switch (name)
		{

		case "BigDiamond":
			stats.Score += 300;
			stats.ScoreTemp += 300;
			Destroy (col.gameObject);
			break;

		case "LittleDiamond":
			stats.Score += 100;
			stats.ScoreTemp += 100;
			Destroy (col.gameObject);
			break;

		case "Exit":
			myStat.Win();
			break;

		case "Rock":
			anim.Death ();
			Destroy (col.gameObject);
			break;






		case "BigDiamond(Clone)":
			stats.Score += 300;
			stats.ScoreTemp += 300;
			Destroy (col.gameObject);
			break;

		case "LittleDiamond(Clone)":
			stats.Score += 100;
			stats.ScoreTemp += 100;
			Destroy (col.gameObject);
			break;

		case "Exit(Clone)":
			myStat.Win();
			break;

		case "Rock(Clone)":
			anim.Death ();
			Destroy (col.gameObject);
			break;

		default:
			break;
		}

	}
	void OnCollisionEnter2D(Collision2D col){
		string name = col.transform.name;
		Debug.Log ("Collided with : " + name);
		Stats myStat = new Stats ();

		switch (name)
		{
		case "Enemy" :
			anim.Death ();
			break;

		case "BigDiamond":
			anim.Death ();
			Destroy (col.gameObject);
			break;

		case "LittleDiamond":
			anim.Death ();
			Destroy (col.gameObject);
			break;

		case "Diamond_Dirt":
			Destroy (col.gameObject);
			break;

		case "Normal_Dirt":
			Destroy (col.gameObject);
			break;

//		case "RockDetector":
//			anim.Death ();
//			Destroy (col.transform.parent.gameObject);
//			break;



		case "Enemy(Clone)" :
			anim.Death ();
			break;

		case "BigDiamond(Clone)":
			anim.Death ();
			Destroy (col.gameObject);
			break;

		case "LittleDiamond(Clone)":
			anim.Death ();
			Destroy (col.gameObject);
			break;

		case "Diamond_Dirt(Clone)":
			Destroy (col.gameObject);
			break;

		case "Normal_Dirt(Clone)":
			Destroy (col.gameObject);
			break;

//		case "RockDetector(Clone)":
//			anim.Death ();
//			Destroy (col.transform.parent.gameObject);
//			break;


		default:
			break;
		}
	}


}
