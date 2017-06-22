using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

class GridMove : MonoBehaviour {
	[SerializeField] private float moveSpeed = 2f;
	[SerializeField] private float gridSize = 0.16f;
	[SerializeField] private enum Orientation {
		Horizontal,
		Vertical
	};
	[SerializeField] private Orientation gridOrientation = Orientation.Horizontal;
	[SerializeField] private bool allowDiagonals = false;
	[SerializeField] private bool correctDiagonalSpeed = true;
	[SerializeField] private Vector2 input;
	[SerializeField] private bool isMoving = false;
	[SerializeField] private Vector3 startPosition;
	[SerializeField] private Vector3 endPosition;
	[SerializeField] private float t;
	[SerializeField] private float factor;


	public Vector2 InitPos;

	public void Start()
	{
		InitPos.x = transform.position.x;
		InitPos.y = transform.position.y;
	}

	public void Update() {
		if (!isMoving) {
			input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if (!allowDiagonals) {
				if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
					input.y = 0;
				} else {
					input.x = 0;
				}
			}

			if (input != Vector2.zero) {
				StartCoroutine(move(transform));
			}
		}
	}

	public IEnumerator move(Transform transform) {
		isMoving = true;
		startPosition = transform.position;
		t = 0;

		if(gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
				startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
		} else {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
				startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
		}

		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.7071f;
		} else {
			factor = 1f;
		}

		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed/gridSize) * factor;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}
}