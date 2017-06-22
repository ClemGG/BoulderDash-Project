using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

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
	[SerializeField] private bool isFalling = true;
	public Vector2 InitPos;
	[SerializeField] private Animator anim;
	[SerializeField] private string nameOfThisGameObject;

	public void Start()
	{
		InitPos.x = transform.position.x;
		InitPos.y = transform.position.y;
		nameOfThisGameObject = transform.name;
	}

	public void Update() {
		if (!isMoving) {
			
			input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
			if (!allowDiagonals) {
				if (Mathf.Abs (input.x) > Mathf.Abs (input.y)) {
					input.y = 0;
				} else {
					input.x = 0;
				}
			} 

			if (input != Vector2.zero && isFalling) {
				if (nameOfThisGameObject == "Rock")
					anim.Play ("fall");
				StartCoroutine (move (transform));
			} else {
				if (nameOfThisGameObject == "Rock")
					anim.Play ("idle");
			}
		}
	}

	public IEnumerator move(Transform transform) {

		if (isFalling) {
			

			isMoving = true;
			startPosition = transform.position;
			t = 0;


			endPosition = new Vector3 (startPosition.x,
				startPosition.y - gridSize,
				startPosition.z);



			if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
				factor = 0.7071f;
			} else {
				factor = 1f;
			}

			while (t < 1f) {
				t += Time.deltaTime * (moveSpeed / gridSize) * factor;
				transform.position = Vector3.Lerp (startPosition, endPosition, t);
				yield return null;
			}

			isMoving = false;
			yield return 0;
		}
	}


	void OnTriggerEnter2D(Collider2D col){
		//string name = col.transform.name;
		//Debug.Log ("The object's collider triggered with : " + name);

		if (nameOfThisGameObject == "Rock")
			anim.Play ("idle");
		
			isFalling = false;


	
	}

	void OnTriggerExit2D(Collider2D col){

			isFalling = true;
	
	}
}
