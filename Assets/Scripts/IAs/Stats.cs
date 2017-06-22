using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour {

	[SerializeField] private GridMove gridmove;
	[SerializeField] private Animator anim;



	public int RemainingLives = 0;
	[SerializeField] private int TotalLives = 3;
	public int Score = 0;
	public int ScoreToWin = 1000;
	[SerializeField] private int ScoreTemp = 0;
	[SerializeField] private int PointsToGetExtraLife = 2000;
	public int HighScore;

	[SerializeField] private float Timer = 180;
	private float TimerOnAwake;


	void Awake()
	{
		Time.timeScale = 1;
	}



	void Start()
	{
		InvokeRepeating("AllotedTime", 1f, 1f);
		TimerOnAwake = Timer;
		HighScore = (PlayerPrefs.GetInt("HighScore"));

		if (RemainingLives == 0) {
			PlayerPrefs.SetInt ("RemainingLives", TotalLives);
			RemainingLives = TotalLives;
		}
	}


	void Update()
	{

		UpdateScore ();
			
		if (Input.GetKeyDown (KeyCode.X) && Time.timeScale == 0 && RemainingLives > 0) {
			
			Reset ();
		}
		if (Input.GetKeyDown (KeyCode.W) && RemainingLives <= 0) {
			
			ReturnToMainMenu ();
		}
	}






	void AllotedTime()
	{
		if(RemainingLives > 0)
		Timer -= 1;
		
		if (Timer <= 0)
		{
			Timer = 0;
			RemainingLives -= 1;
			Time.timeScale = 0;
		}
	}




	void UpdateScore()
	{
		if (ScoreTemp < Score && ScoreTemp < PointsToGetExtraLife) {
			ScoreTemp = Score;
		}

		if (ScoreTemp >= PointsToGetExtraLife) 
		{
			RemainingLives += 1;
			ScoreTemp -= PointsToGetExtraLife;
		}

		if (Score > HighScore)
		{
			HighScore = Score;
			PlayerPrefs.SetInt("HighScore", HighScore);
		}

		if (Input.GetKeyDown (KeyCode.R))
		{
			HighScore = 0;
			PlayerPrefs.SetInt("HighScore", HighScore);
		}
	}





	void OnGUI(){
		GUI.Box(new Rect(10, 25, 130, 20), "Lives : " + RemainingLives);
		GUI.Box(new Rect(10, 50, 130, 20), "Score : " + Score);
		GUI.Box(new Rect(150, 50, 130, 20), "Score : " + HighScore);
		GUI.Box(new Rect(10, 75, 130, 20), "Timer : " + Timer);

		if (Timer == 0 && RemainingLives > 0)
		{
			GUI.Box(new Rect(10, 250, 300, 40), " Time out! Press X to try again!");
		}

		if (RemainingLives == 0)
		{
			GUI.Box(new Rect(10, 300, 300, 40), " Game Over! Press W to return to the main menu!");
		}


	}


	public void Reset()
	{
		Time.timeScale = 1;
		Timer = TimerOnAwake;

		if (RemainingLives > 0) {
			
		
			PlayerPrefs.SetInt ("RemainingLives", RemainingLives);
			transform.position = new Vector2 (gridmove.InitPos.x, gridmove.InitPos.y);
			anim.Play ("idle");
			gridmove.enabled = true;
		

		}
	}

	public void ReturnToMainMenu(){
		
		RemainingLives = 0;
		if (Input.GetKeyDown (KeyCode.W)) {
			SceneManager.LoadScene ("BD_main_menu");
		}
	}







	public void Win()
	{
		SceneManager.LoadScene ("BD_main_menu");
	}


	void OnLevelWasLoaded()
	{
		if (RemainingLives == 0) {
			PlayerPrefs.SetInt ("RemainingLives", TotalLives);
			RemainingLives = TotalLives;
		}

	}
}
