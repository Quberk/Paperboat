[System.Serializable]

public class CurrenciesandHighscore
{
    public int highScore;
    public int coins;
    public int paperClips;
    public int layar;

    public CurrenciesandHighscore(int highScore, int coins, int paperClips, int layar)
    {
        this.highScore = highScore;
        this.coins = coins;
        this.paperClips = paperClips;
        this.layar = layar;
    }
}
