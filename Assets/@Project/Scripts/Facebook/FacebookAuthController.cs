using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookAuthController : MonoBehaviour
{
    private AdjustController adjustController;
    private LocalStorageManager localStorageManager;

    [Header("Json Read Write System")]
    private const string skinsFileName = "OwnedSkinLists.json";
    private const string usedSkinFileName = "UsedSkin.json";
    private const string powerUpsLevelFileName = "PowerUpsLevel.json";
    private const string currenciesAndHighscoreFileName = "CurrenciesAndHighscore.json";

    string persistentDataPath;

    void Awake()
    {
        persistentDataPath = Application.persistentDataPath + "/";
        localStorageManager = new LocalStorageManager();

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }


    private void Start()
    {
        adjustController = AdjustController.Instance;
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Facebook-Login"))
        {
            this.gameObject.SetActive(false);
            Debug.Log("Gue dinonaktifkan dong");
        }

    }

    public void Login()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            FB.API("/me?fields=id,name,email", HttpMethod.GET, GetFacebookInfo, new Dictionary<string, string>() { });
            //Adjust Event
            adjustController.CallingAdjustEvent("1f52c7");
            Debug.Log("Facebook Login Berhasil");
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    public void GetFacebookInfo(IResult result)
    {
        if (result.Error == null)
        {
            Debug.Log("Facebook Id : "+result.ResultDictionary["id"].ToString());
            Debug.Log("Facebook Name : "+result.ResultDictionary["name"].ToString());
            PlayerPrefs.SetString("Facebook-Login", "User-" +result.ResultDictionary["id"].ToString());


            //Read all Firebase Data
            string userId = PlayerPrefs.GetString("Facebook-Login");


            //Check apakah Player sudah ada pada Firebase
            StartCoroutine(FireBaseDataBase.UserExistInFirebaseDatabse(userId, returnedValue =>
            {
                bool result = returnedValue;
                if (result == false)
                {
                    Debug.Log("Id ini tidak ada (First Time Syncing)");
                    return;
                }
            }));


            ///--------------------SAVING DATA FROM FIREBASE INTO LOCAL STORAGE-------------------------------------
            ///

            //Save Skins
            StartCoroutine(FireBaseDataBase.LoadSkinsData(userId, returnedValue =>
            {
                List<SaveSkin> saveSkins = returnedValue;

                if (saveSkins.Count != 0)
                {
                    JsonReadWriteSystem.SaveListToJsonOverwrite<SaveSkin>(saveSkins, persistentDataPath + skinsFileName, true);
                }
            }));


            //Used Skin
            StartCoroutine(FireBaseDataBase.LoadUsedSkinsData(userId, returnedValue =>
            {
                UsedSkin usedSkin = returnedValue;

                if (usedSkin.idPerahu != "")
                {
                    JsonReadWriteSystem.SaveToJsonOverwrite<UsedSkin>(usedSkin, persistentDataPath + usedSkinFileName, true);
                }
            }));

            //Power Ups Level
            StartCoroutine(FireBaseDataBase.LoadPowerUpsData(userId, returnedValue =>
            {
                PowerUpsLevel powerUpsLevel = returnedValue;

                if (powerUpsLevel.levelMagnet != 0)
                {
                    JsonReadWriteSystem.SaveToJsonOverwrite<PowerUpsLevel>(powerUpsLevel, persistentDataPath + powerUpsLevelFileName, true);
                }
            }));

            //Currencies and Highscore
            StartCoroutine(FireBaseDataBase.LoadCurrenciesAndHighscoreData(userId, returnedValue =>
            {
                CurrenciesandHighscore currenciesandHighscore = returnedValue;

                if (currenciesandHighscore.coins != 0)
                {
                    JsonReadWriteSystem.SaveToJsonOverwrite<CurrenciesandHighscore>(currenciesandHighscore, persistentDataPath + currenciesandHighscore, true);
                }
            }));
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
}



