using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

	[SerializeField] private MoveObject moveRock;
	[SerializeField] private string nameOfThisGameObject;
	[SerializeField] private Animator anim;

	void Start(){
		moveRock = transform.parent.GetComponent<MoveObject> ();
		nameOfThisGameObject = transform.parent.name;
		anim = transform.parent.GetComponent<Animator> ();
	}




	void OnCollisionEnter2D(Collision2D col){
		string name = col.transform.name;
		//Debug.Log ("The object's collider triggered with : " + name);

		if (nameOfThisGameObject == "Rock" || nameOfThisGameObject == "Rock(Clone)" || name == "BigDiamond" ||
			name == "BigDiamond(Clone)" || name == "LittleDiamond" || name == "LittleDiamond(Clone)") {
			if (name != "Character")
				moveRock.isFalling = false;
		}
	}



	void OnCollisionExit2D(Collision2D col){
		string name = col.transform.name;
		//Debug.Log ("The object's collider triggered with : " + name);

		if (nameOfThisGameObject == "Rock" || nameOfThisGameObject == "Rock(Clone)" || name == "BigDiamond" ||
			name == "BigDiamond(Clone)" || name == "LittleDiamond" || name == "LittleDiamond(Clone)") {
			
			if (name != "Character")
				moveRock.isFalling = true;
		}
	}






		void OnTriggerEnter2D(Collider2D col){
			string name = col.transform.name;
			//Debug.Log ("The object's collider triggered with : " + name);
	
		if (name == "BigDiamond" || name == "BigDiamond(Clone)" || name == "LittleDiamond" || name == "LittleDiamond(Clone)") {
				anim.Play ("idle");
			moveRock.isFalling = false;
	
			}
		
		}





		void OnTriggerExit2D(Collider2D col){
			string name = col.transform.name;
			//Debug.Log ("The object's collider triggered with : " + name);

		if (name == "BigDiamond" || name == "BigDiamond(Clone)" || name == "LittleDiamond" || name == "LittleDiamond(Clone)") {
			moveRock.isFalling = true;
			}
		}

}
