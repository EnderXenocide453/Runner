namespace GameManagement
{
    public class ScoreHandler
    {
        public int HighScore { get; private set; }

        public ScoreHandler(int highScore)
        {
            HighScore = highScore;
        }

        public void SetHighScore(int score)
        {
            if (score <= HighScore)
                return;
                
            HighScore = score;
        }
    }
}
