using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuButtons : MonoBehaviour {

	[SerializeField] private GameObject panel1;
	[SerializeField] private GameObject panel2;

	[SerializeField] public int lvlNum = 0;
	[SerializeField] public string lvlName = "";


	void Start(){
		panel1.SetActive (true);
		panel2.SetActive (false);
	}


	public void Play () {
		panel2.SetActive (true);
		panel1.SetActive (false);
	}
	

	public void Quit () {
		Application.Quit ();
	}


	public void ReturnToMainMenu () {
		panel1.SetActive (true);
		panel2.SetActive (false);
	}

	public void LoadSelectedLevel () {


		//createthenewscene = new CreateTheNewScene ();

		lvlName = string.Format("{0} {1}", lvlName, EventSystem.current.currentSelectedGameObject.name);
		//Debug.Log ("lvlName: " + lvlName);
		lvlNum = int.Parse (lvlName);
		Debug.Log (lvlNum);


		SceneManager.LoadScene("BD_lvl_"+lvlNum);


	}


//	void OnLevelWasLoaded()
//	{
//		if (SceneManager.GetActiveScene ().name == "BD_main_menu") {
//			
//			panel1 = GameObject.Find ("Main Panel");
//			panel2 = GameObject.Find ("Level Selection Panel");
//
//		}
//
//	}

}
