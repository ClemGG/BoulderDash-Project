using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

	[SerializeField] private float moveSpeed = 2f;
	[SerializeField] private float gridSize = 0.16f;
	[SerializeField] private enum Orientation {
		Horizontal,
		Vertical
	};
	[SerializeField] private bool allowDiagonals = false;
	[SerializeField] private bool correctDiagonalSpeed = true;
	[SerializeField] private Vector2 input;
	[SerializeField] private bool isMoving = false;
	[SerializeField] private Vector3 startPosition;
	[SerializeField] private Vector3 endPosition;
	[SerializeField] private float t;
	[SerializeField] private float factor;



	[SerializeField] private int MirrorX = 1;
	[SerializeField] private int MirrorY = 0;
	[SerializeField] private bool isColliding = false;
	[SerializeField] private Transform PrefabLittleDiamond;
	[SerializeField] private GameObject Character;
	[SerializeField] private float DistanceToCharacter;
	public Vector3 InitPos;


	public void Start()
	{
		InitPos.x = transform.position.x;
		InitPos.y = transform.position.y;
		InitPos.z = transform.position.z;
		MirrorX = 1;
		MirrorY = 0;
		Character = GameObject.FindGameObjectWithTag ("Character");
	}



	public void Update() {


		if (!isColliding) {
			StartCoroutine (move (transform));

		} else {
			StopCoroutine (move (transform));
		}

	}




	public IEnumerator move(Transform transform) {





			startPosition = transform.position;
			t = 0;

		DistanceToCharacter = Vector3.Distance(Character.transform.position, transform.position);
		if (DistanceToCharacter <= 2 * gridSize) {
			endPosition = new Vector3 (
				Character.transform.position.x + MirrorX * gridSize + gridSize,
				Character.transform.position.y + MirrorY * gridSize + gridSize, 
				Character.transform.position.z
			);
		} 

		else {
			endPosition = new Vector3 (
				startPosition.x + MirrorX * gridSize,
				startPosition.y + MirrorY * gridSize, 
				startPosition.z
			);
		}


		if (allowDiagonals && correctDiagonalSpeed && MirrorX != 0 && MirrorY != 0) {
				factor = 0.7071f;
			} else {
				factor = 1f;
			}

			while (t < 0.5f) {
				t += Time.deltaTime * (moveSpeed / gridSize) * factor;
				transform.position = Vector3.Lerp (startPosition, endPosition, t/2);
				yield return null;
			}


			yield return 0;

	}



	void OnCollisionEnter2D (Collision2D col)
	{

		string name = col.transform.name;

		if (name == "BigDiamond" || name == "LittleDiamond" || name == "BigDiamond(Clone)" || name == "LittleDiamond(Clone)") {
			Instantiate (PrefabLittleDiamond, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	    else {
				
		Vector2 relativePosition = transform.InverseTransformPoint (col.transform.position);
		//Debug.Log ("x = " + relativePosition.x + ", y = " + relativePosition.y);
			ChangeDirection (relativePosition);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{

		string name = col.transform.name;
		if (name == "Rock" || name == "Rock(Clone)") {
			Instantiate (PrefabLittleDiamond, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}


	void ChangeDirection(Vector2 relativePosition)
	{
		isColliding = true;
		int alea = Random.Range (-1, 0);
		if (alea == 0)
			alea = 1;


			//if the right side of the collider is hit
		if (relativePosition.x > 0f && relativePosition.y < 0.1f && relativePosition.y > -0.1f) {
				//print ("Collided with the right side");
				MirrorX = 0;
				MirrorY = alea;
			}
			//if the left side of the collider is hit
		else if (relativePosition.x < 0 && relativePosition.y < 0.1f && relativePosition.y > -0.1f) {
				//print ("Collided with the left side");
				MirrorX = 0;
				MirrorY = alea;
			}

			//if the upper side of the collider is hit
		if (relativePosition.y > 0 && relativePosition.x < 0.1f && relativePosition.x > -0.1f) {
				//print ("Collided with the upper side");
				MirrorX = alea;
				MirrorY = 0;
			}
			//if the lower side of the collider is hit
		if (relativePosition.y < 0 && relativePosition.x < 0.1f && relativePosition.x > -0.1f) {
				//print ("Collided with the lower side");
				MirrorX = alea;
				MirrorY = 0;
			}



		isColliding = false;

	}

}
