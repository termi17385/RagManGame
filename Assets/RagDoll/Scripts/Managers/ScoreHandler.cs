public static class ScoreHandler
{
	private static int hScore;
	public static string ReturnEditedHScore() => $"{hScore:000000000}";
	
	/// <summary> Takes in a score then returns it edited
	/// as well as sets the highscore </summary>
	public static string ReturnEditedScore(int _score)
	{
		if(_score >= hScore) hScore = _score;
		return $"{_score:000000000}";
	}
}
