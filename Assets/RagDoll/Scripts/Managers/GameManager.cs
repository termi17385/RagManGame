using System;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private GameObject gameOverScreen;
	
	[NonSerialized] public float characterVelocity;
	[NonSerialized] public bool isPlaying = false;
	[NonSerialized] public bool dropped = false;
	
	private float timer = 3;
	private bool setupMenu = false;

	private void Awake() => InitializeOnAwake();
	private void InitializeOnAwake()
	{
		timer = 3;
		setupMenu = false;
		Time.timeScale = 1.0f;
		
		timerText.gameObject.SetActive(false);
		gameOverScreen.SetActive(false);
	}
	private void LateUpdate()
	{
		if(GameOver())
		{
			// activate and setup the gameover menu
			if(!setupMenu) SetupGameOverScreen(Mathf.RoundToInt(FindObjectOfType<RagScore>().currentScore));
			gameOverScreen.SetActive(true);
			Time.timeScale = .0f;
		}
	}
	private void SetupGameOverScreen(int _score = 0)
	{
		var screenTransform = gameOverScreen.transform.Find("ScoreText");
		var scoreText = screenTransform.Find("Score").GetComponent<TextMeshProUGUI>();
		var hScoreText = screenTransform.Find("hScore").GetComponent<TextMeshProUGUI>();

		scoreText.text = ScoreHandler.ReturnEditedScore(_score);
		hScoreText.text = ScoreHandler.ReturnEditedHScore();
		setupMenu = true;
	}
	
	/// <summary> GameOver method that handles detecting
	/// that the player has been dropped and stopped moving
	/// then sets off a timer </summary>
	private bool GameOver()
	{
		// checks that the player has stopped moving and was dropped
		if(characterVelocity <= 0.02f && dropped)
		{
			// engages the timer
			timerText.gameObject.SetActive(true);
			if(timer <= 0) {timer = 0; return true;} // if the timer is 0 return and make sure time stays at zero
			
			timer -= Time.deltaTime;
			timerText.text = $"{Mathf.Round(timer)}"; // round the timer and display as int
		}
		else timer = 3; // else resets the timer
		return false;
	}
}
