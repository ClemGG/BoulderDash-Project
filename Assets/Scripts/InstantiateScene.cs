using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateScene : MonoBehaviour {

	[SerializeField] private ObjectCorrespondence objectcorrespondence;
	[SerializeField] private char[,] MapSize;
	[SerializeField] private string charToInstantiate;
	[SerializeField] private Vector3 ZeroPosition = Vector3.zero;
	[SerializeField] private Vector3 SpawnPosition;
	[SerializeField] private float gridSize = 0.16f;





	void Start(){
		SpawnPosition = ZeroPosition;
	}



	public void InstantiateNewMap (int TableWidth, int TableHeight, string map) {

		MapSize = new char[TableHeight, TableWidth];
		int MapWidth = 0;
		char c;

		for (int y = 0; y < TableHeight; y++) {
			for (int x = 0; x < TableWidth; x++) {


				c = map [MapWidth + x];
				//print (c);
				MapSize [y, x] = c;
				//print ("Line "+(y+1)+" , Column  "+(x+1)+" : "+MapSize[y,x]);

			}

			MapWidth += TableWidth;
	
		}



		for (int y = 0; y < TableHeight; y++) {
			for (int x = 0; x < TableWidth; x++) {

	
				c = MapSize [TableHeight-1-y, x];
					//print (c);
					Transform prefab = objectcorrespondence.ReturnReceivedChar (c);
					//print(prefab.name);
					objectcorrespondence.InstantiateObject (prefab, SpawnPosition);
					SpawnPosition = new Vector3 (SpawnPosition.x + gridSize, SpawnPosition.y, SpawnPosition.z);

			}

			SpawnPosition = new Vector3 (ZeroPosition.x, SpawnPosition.y + gridSize, SpawnPosition.z);

		}

	}

	
}
