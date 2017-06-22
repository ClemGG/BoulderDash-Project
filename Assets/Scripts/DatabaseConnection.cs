using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class DatabaseConnection : MonoBehaviour {

	[SerializeField] private string host;
	[SerializeField] private string username;
	[SerializeField] private string database;
	[SerializeField] private string password;
	private string connexionStatement;
	private MySqlConnection cn;

	[SerializeField] private InstantiateScene instantiatescene;
	private string str;

	public int TableWidth = 0; 
	public int TableHeight = 0;
	public string MapToLoad = "";

	void Awake () {
		
		str = string.Concat("Server=", host, ";DATABASE=", database, ";User ID=", username, ";Password=", password,";Pooling=true;Charset=utf8;");
	//	print (str);

		try
		{
			cn = new MySqlConnection(str);
			cn.Open();
			connexionStatement = cn.State.ToString ();
			print(connexionStatement);

			LoadMapFileText ();

		
			connexionStatement = cn.State.ToString ();
			print(connexionStatement);

		}
		catch(IOException e)
		{
			cn.Close ();
			connexionStatement = e.ToString ();
			print(connexionStatement);
		}
		cn.Close ();
	}


	void OnApplicationQuit () {

		if (cn != null && cn.State.ToString () != "Closed") {
			cn.Close ();
			connexionStatement = cn.State.ToString ();
			print(connexionStatement);
		}
	}


	void LoadMapFileText () {
		
		string lvlName = SceneManager.GetActiveScene ().name;
		string lvlNumber = string.Format("{0}", lvlName [lvlName.Length - 1] );
		int i = int.Parse(lvlNumber);

		string FilePath = string.Concat("Assets/MySQL/Maps/Map", i, ".txt");

		if (System.IO.File.Exists (FilePath)) {

			System.IO.File.OpenRead(FilePath);

			 TableWidth = int.Parse(System.IO.File.ReadAllLines (FilePath) [0]);
			 TableHeight = int.Parse(System.IO.File.ReadAllLines (FilePath) [1]);

			//print("TableWidth : " + TableWidth);
			//print("TableHeigh : " + TableHeight);
			string map = "";

			for(int x = 0; x <System.IO.File.ReadAllBytes (FilePath).Length; x++){
				char c = (char) System.IO.File.ReadAllBytes (FilePath) [x];
				if (!System.Char.IsNumber (c) && c != '\n') {
					//print(c);
					map = string.Concat (map, c);
				}
			}
			//print (map);
			InsertMapIntoTable (map, i);
			GetMapFromTable (i);

			instantiatescene.InstantiateNewMap (TableWidth, TableHeight, map);
			}

		else
			print ("ERROR : The map named 'Map"+i+"' does not exist.");
	}






		void InsertMapIntoTable (string mapToInsert, int numberOfMapToLoad) {
		string cmdSet = string.Concat(   "CALL SetMap('", mapToInsert, "', ", numberOfMapToLoad, ");"   );
		MySqlCommand commandToSetMap = new MySqlCommand (cmdSet, cn);

		try
		{
			commandToSetMap.ExecuteReader();
			connexionStatement = "Map registered successfully.";
			print(connexionStatement);

		}
		catch(IOException e)
		{
			commandToSetMap.Dispose ();
			cn.Close ();
			connexionStatement = e.ToString ();
			print(connexionStatement);
		}
		cn.Close ();

	}





	void GetMapFromTable (int numberOfMapToLoad) {

		cn.Open ();

		string cmdGet = string.Concat(   "CALL GetMap(", numberOfMapToLoad, ");"   );
		MySqlCommand commandToGetMap = new MySqlCommand (cmdGet, cn);
		MySqlDataReader MapReader = commandToGetMap.ExecuteReader ();

			while(MapReader.Read())
				{
				if (MapReader ["Map_To_Convert"].ToString () != "") {
					MapToLoad = MapReader ["Map_To_Convert"].ToString ();
				}
			}

		print (MapToLoad);
		MapReader.Close ();
		cn.Close ();


	}

}
