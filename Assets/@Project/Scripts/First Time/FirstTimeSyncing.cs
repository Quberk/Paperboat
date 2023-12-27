using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FirstTimeSyncing : MonoBehaviour
{
    LocalStorageManager localStorageManager = new LocalStorageManager();
    [SerializeField] private AccessoriesController accessoriesController;

    [Header("Json Read Write System")]
    private const string skinsFileName = "OwnedSkinLists.json";
    private const string usedSkinFileName = "UsedSkin.json";
    private const string powerUpsLevelFileName = "PowerUpsLevel.json";
    private const string currenciesAndHighscoreFileName = "CurrenciesAndHighscore.json";

    string persistentDataPath;

    bool noDataOrNoInternet;
    int takenDataAmount;
    List<SaveSkin> saveSkins = new List<SaveSkin>();
    UsedSkin usedSkin = new UsedSkin("", "");
    PowerUpsLevel powerUpsLevel = new PowerUpsLevel(0, 0, 0, 0);
    CurrenciesandHighscore currenciesandHighscore = new CurrenciesandHighscore(0, 0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        persistentDataPath = Application.persistentDataPath + "/";

        //Syncing First
        if (PlayerPrefs.HasKey("Facebook-Login"))
            SyncingDataWithFirebase();

        if (PlayerPrefs.HasKey("First-Syncing"))
        {
            return;
        }

        PlayerPrefs.SetInt("First-Syncing", 1);
        CreateNewJsonData.CreateNewJson();
        accessoriesController.SetPlayerPerahuModel("A0001");
        accessoriesController.SetPlayerPesawatModel("A0001");

    }

    private void Update()
    {
        if (takenDataAmount == 4 || noDataOrNoInternet)
        {
            accessoriesController.StartAccessoriesController();
            Destroy(gameObject);
            if (usedSkin.idPerahu == "")
            {
                return;
            } 
        }
    }

    public void SyncingDataWithFirebase()
    {
        //Read all Firebase Data
        string userId = PlayerPrefs.GetString("Facebook-Login");//Id betulan
        //string userId = "User-1114840726126417";//Id lain
        //string userId = "User-11148407261264120";//Id tdk nyata

        //Check apakah Player sudah ada pada Firebase
        StartCoroutine(FireBaseDataBase.UserExistInFirebaseDatabse(userId, returnedValue =>
        {
            bool result = returnedValue;
            if (result == false)
            {
                Debug.Log("Id ini tidak ada (First Time Syncing)");
                noDataOrNoInternet = true;
            }
        }));
         

        ///--------------------SAVING DATA FROM FIREBASE INTO LOCAL STORAGE-------------------------------------
        ///

        //Save Skins
        StartCoroutine(FireBaseDataBase.LoadSkinsData(userId, returnedValue =>
        {
            saveSkins = returnedValue;

            if (saveSkins.Count != 0)
            {
                JsonReadWriteSystem.SaveListToJsonOverwrite<SaveSkin>(saveSkins, persistentDataPath + skinsFileName, true);
                takenDataAmount++;
            }
        }));


        //Used Skin
        StartCoroutine(FireBaseDataBase.LoadUsedSkinsData(userId, returnedValue =>
        {
            usedSkin = returnedValue;

            if (usedSkin.idPerahu != "")
            {
                JsonReadWriteSystem.SaveToJsonOverwrite<UsedSkin>(usedSkin, persistentDataPath+ usedSkinFileName, true);

                takenDataAmount++;
            }
        }));

        //Power Ups Level
        StartCoroutine(FireBaseDataBase.LoadPowerUpsData(userId, returnedValue =>
        {
            powerUpsLevel = returnedValue;

            if (powerUpsLevel.levelMagnet != 0)
            {
                JsonReadWriteSystem.SaveToJsonOverwrite<PowerUpsLevel>(powerUpsLevel, persistentDataPath + powerUpsLevelFileName, true);

                takenDataAmount++;
            }
        }));

        //Currencies and Highscore
        StartCoroutine(FireBaseDataBase.LoadCurrenciesAndHighscoreData(userId, returnedValue =>
        {
            currenciesandHighscore = returnedValue;

            if (currenciesandHighscore.coins != 0)
            {
                JsonReadWriteSystem.SaveToJsonOverwrite<CurrenciesandHighscore>(currenciesandHighscore, persistentDataPath + currenciesandHighscore, true);
                takenDataAmount++;
            }
        }));


    }

}
