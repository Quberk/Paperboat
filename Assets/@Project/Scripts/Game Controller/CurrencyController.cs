using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour
{
    [SerializeField] private Text coinsAmountText;
    [SerializeField] private Text paperClipsAmountText;
    [SerializeField] private Text layarPerahuAmountText;

    [SerializeField] private Text highScoreText;

    LocalStorageManager localStorageManager = new LocalStorageManager();
    CurrencyTransactionManager currencyTransactionManager = new CurrencyTransactionManager();

   // FireBaseDataBase fireBaseDatabase;

    // Start is called before the first frame update
    void Start()
    {
        //fireBaseDatabase = FireBaseDataBase.Instance;

        /*if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 100);
            PlayerPrefs.SetInt("PaperClip", 5);
            PlayerPrefs.SetInt("LayarPerahu", 10);
            PlayerPrefs.SetInt("Highscore", 0);
        }*/

        //Set the amount on UI
        coinsAmountText.text = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().coins.ToString();
        paperClipsAmountText.text = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().paperClips.ToString();
        layarPerahuAmountText.text = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().layar.ToString();
        highScoreText.text = (localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().highScore.ToString()) + "m";

        /*coinsAmountText.text = PlayerPrefs.GetInt("Coins").ToString();
        paperClipsAmountText.text = PlayerPrefs.GetInt("PaperClip").ToString();
        layarPerahuAmountText.text = PlayerPrefs.GetInt("LayarPerahu").ToString();
        highScoreText.text = (PlayerPrefs.GetInt("Highscore").ToString()) + "m";*/
    }

    #region Public Function
    public void AddCoins(int coin)
    {
        UpdateCoins(coin);
    }

    public void SubtractCoins(int coin)
    {
        UpdateCoins(-coin);
    }

    public void AddPaperClip(int paperClip)
    {
        UpdatePaperClips(paperClip);
    }

    public void SubtractPaperClip(int paperClip)
    {
        UpdatePaperClips(-paperClip);
    }

    public void AddLayar(int layar)
    {
        UpdateLayar(layar);
    }

    public void SubtractLayar(int layar)
    {
        UpdateLayar(-layar);
    }
    #endregion

    #region Update Data
    public void UpdateHighScore(int highScore)
    {
        currencyTransactionManager.InputHighScore(highScore);
        highScoreText.text = highScore.ToString() + "m";

    }

    void UpdateCoins(int coinAmount)
    {
        currencyTransactionManager.EarnCurrency(coinAmount, TransactionCurrencyTypes.Coins);
        coinsAmountText.text = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().coins.ToString();
    }

    void UpdatePaperClips(int paperClipAmount)
    {
        currencyTransactionManager.EarnCurrency(paperClipAmount, TransactionCurrencyTypes.PaperClip);
        paperClipsAmountText.text = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().paperClips.ToString();
    }

    void UpdateLayar(int layarAmount)
    {
        currencyTransactionManager.EarnCurrency(layarAmount, TransactionCurrencyTypes.Layar);
        layarPerahuAmountText.text = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().layar.ToString();
    }
    #endregion

}
