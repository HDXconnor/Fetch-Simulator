using UnityEngine;
using System.Collections;

public class ScoreManager : DogScript{
	bool gameScoring;
	int score;
	
	void Start () {
		gameScoring = false;
		score = 0;
	}

	void Update () {
		if (gameScoring) {
			score+=5;
		}
	}

	public void StartScoring(){
		gameScoring = true;
	}

	public int GetScore(){
		return score;
	}

	public void ResetScore(){
		score = 0;
	}

	public void RemoveFromScore(int penalty){
		score -= penalty;
	}

	public void AddToScore(int points){
		score += points;
	}
}