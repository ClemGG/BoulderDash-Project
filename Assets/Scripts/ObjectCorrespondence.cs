using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCorrespondence : MonoBehaviour {

	[SerializeField] private Transform Background;
	[SerializeField] private Transform Wall;
	[SerializeField] private Transform Enemy;
	[SerializeField] private Transform Character;
	[SerializeField] private Transform BigDiamond;
	[SerializeField] private Transform LittleDiamond;
	[SerializeField] private Transform Rock;
	[SerializeField] private Transform Exit;
	[SerializeField] private Transform Dirt;

	Transform PrefabToSpawn;


	public Transform ReturnReceivedChar(char incomingChar)
	{



		switch(incomingChar)
		{
		case ' ':
			PrefabToSpawn = Background;
			break;
		case '|':
			PrefabToSpawn = Wall;
			break;
		case 'E':
			PrefabToSpawn = Enemy;
			break;
		case 'C':
			PrefabToSpawn = Character;
			break;
		case 'V':
			PrefabToSpawn = BigDiamond;
			break;
		case 'v':
			PrefabToSpawn = LittleDiamond;
			break;
		case 'O':
			PrefabToSpawn = Rock;
			break;
		case 'X':
			PrefabToSpawn = Exit;
			break;
		case 'D':
			PrefabToSpawn = Dirt;
			break;

		default:

			break;
		}

		return PrefabToSpawn;
	}


	public void InstantiateObject(Transform prefab, Vector3 SpawnPosition){

		if (prefab != Background) {
			Instantiate (Background, SpawnPosition, Quaternion.identity);
			Instantiate (prefab, SpawnPosition, Quaternion.identity);
		} 
		else {
			Instantiate (prefab, SpawnPosition, Quaternion.identity);
		}
	}

}
