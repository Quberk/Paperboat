using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveTimer : MonoBehaviour
{
    [SerializeField] GameController gameController;

    [SerializeField] private int timer;
    private int timerCounter;
    private float timerFloatCounter;

    private bool startCounting = false;

    [SerializeField] private GameObject revivePanel;
    [SerializeField] private GameObject postDeadPanel;
    [SerializeField] private Text highScorePostDeadPanel;

    [Header("Post Dead")]
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject newHighScoreFx;
    private GameSessionCounter gameSessionCounter;

    private UIController uIController;
    private CurrencyTransactionManager currencyTransactionManager = new CurrencyTransactionManager();

    // Start is called before the first frame update
    void Start()
    {
        timerCounter = timer;
        uIController = FindObjectOfType<UIController>();
        gameSessionCounter = FindObjectOfType<GameSessionCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounting)
        {
            TimerCounter();

            timerText.text = timerCounter.ToString() + " s";

            if (timerCounter <= 0)
            {
                timerFloatCounter = 0f;
                timerCounter = 0;

                revivePanel.SetActive(false);
                postDeadPanel.SetActive(true);

                uIController.PostDeadOpen();

                //Menambahkan jumlah session
                gameSessionCounter.AddingGameSession();

                highScorePostDeadPanel.text = currencyTransactionManager.GetHighScore().ToString();

                if (gameController.GetScore() == currencyTransactionManager.GetHighScore())
                {
                    newHighScoreFx.SetActive(true);
                }

                startCounting = false;
            }
        }
    }

    public void StartCounting()
    {
        revivePanel.SetActive(true);
        startCounting = true;
        timerFloatCounter = 0f;
        timerCounter = timer;

        newHighScoreFx.SetActive(false);
    }

    public void StopCounting()
    {
        startCounting = false;
        highScorePostDeadPanel.text = currencyTransactionManager.GetHighScore().ToString();

        uIController.PostDeadOpen();

        //Menambahkan jumlah session
        gameSessionCounter.AddingGameSession();

        if (gameController.GetScore() == currencyTransactionManager.GetHighScore())
        {
            newHighScoreFx.SetActive(true);
        }
    }

    void TimerCounter()
    {
        timerFloatCounter += Time.deltaTime;

        if (timerFloatCounter >= 1)
        {
            timerFloatCounter = 0f;
            timerCounter--;
        }
    }
}
