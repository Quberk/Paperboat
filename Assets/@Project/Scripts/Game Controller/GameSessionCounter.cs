using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionCounter : MonoBehaviour
{
    public static GameSessionCounter gameSessionCounter;
    //private YodoAdsController yodoAdsController;
    private AdMobController adMobController;
    private int gameSessions;

    private void Awake()
    {
        if (gameSessionCounter == null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
            gameSessionCounter = this;
        }

        else if (gameSessionCounter != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //yodoAdsController = FindObjectOfType<YodoAdsController>();
        adMobController = FindObjectOfType<AdMobController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameSessions >= 3)
        {
            adMobController.ShowInterstitialAd();
            gameSessions = 0;
        }

        Debug.Log(gameSessions);
    }

    public void AddingGameSession()
    {
        gameSessions ++;
    }
}
