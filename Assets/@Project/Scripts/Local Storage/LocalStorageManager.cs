using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalStorageManager
{
    private const string skinsFileName = "OwnedSkinLists.json";
    private const string usedSkinFileName = "UsedSkin.json";
    private const string powerUpsLevelFileName = "PowerUpsLevel.json";
    private const string currenciesAndHighscoreFileName = "CurrenciesAndHighscore.json";

    ///--------------------------------------------------SKINS-------------------------------------------------
    public void SaveSkinIntoLocalStorage(string id, bool pesawatUnlocked)
    {
        SaveSkin saveSkin = new SaveSkin(id, pesawatUnlocked);
        JsonReadWriteSystem.SaveToJsonAddNewItem<SaveSkin>(saveSkin, skinsFileName);

        //Input Data into Firebase
        if (PlayerPrefs.HasKey("Facebook-Login"))
            //FireBaseDataBase.SaveSkinsData(PlayerPrefs.GetString("Facebook-Login"));
            FireBaseDataBase.SaveSkinsData("User-1114840726126417");
    }

    public void SaveAllSkinsIntoLocalStorage(List<SaveSkin> content)
    {
        JsonReadWriteSystem.SaveToJson<SaveSkin>(content, skinsFileName);
    }

    public List<SaveSkin> LoadSkinFromLocalStorage()
    {
        List<SaveSkin> skins = new List<SaveSkin>();
        return skins = JsonReadWriteSystem.ReadListFromJson<SaveSkin>(skinsFileName);
    }

    public void DeleteSkinFromLocalStorage()
    {
        JsonReadWriteSystem.DeleteFile(skinsFileName);
    }

    ///--------------------------------------------------USED SKIN-------------------------------------------------
    public void SaveUsedSkinIntoLocalStorage(string idPerahu, string idPesawat)
    {
        ///KALAU YANG MAU DI SAVE HANYA SALAH SATU PARAMETER MAKA PARAMETER YG KOSONG DIINPUTKAN DENGAN PARAMETER LAMA
        if (idPerahu == null || idPerahu == "")
            idPerahu = LoadUsedSkinFromLocalStorage().idPerahu;
        if (idPesawat == null || idPesawat == "")
            idPesawat = LoadUsedSkinFromLocalStorage().idPesawat;

        UsedSkin usedSkin = new UsedSkin(idPerahu, idPesawat);
        Debug.Log("Id Pesawatnya : " + usedSkin.idPesawat + " (Local Storage)");
        JsonReadWriteSystem.SaveToJsonOverwrite<UsedSkin>(usedSkin, usedSkinFileName);

        //Input Data into Firebase
        if (PlayerPrefs.HasKey("Facebook-Login"))
            //FireBaseDataBase.SaveUsedSkinData(PlayerPrefs.GetString("Facebook-Login"));
            FireBaseDataBase.SaveUsedSkinData("User-1114840726126417");
    }

    public UsedSkin LoadUsedSkinFromLocalStorage()
    {
        UsedSkin usedSkin;
        return usedSkin = JsonReadWriteSystem.ReadFromJson<UsedSkin>(usedSkinFileName);
    }

    ///--------------------------------------------------POWER UPS LEVEL-------------------------------------------------
    public void SavePowerUpsLevelIntoLocalStorage(int levelPesawat, int levelParasut, int levelMagnet, int levelLayar)
    {
        ///KALAU YANG MAU DI SAVE HANYA SALAH SATU PARAMETER MAKA PARAMETER YG KOSONG DIINPUTKAN DENGAN PARAMETER LAMA
        if (levelPesawat == 0)
            levelPesawat = LoadPowerUpsLevelFromLocalStorage().levelPesawat;
        if (levelParasut == 0)
            levelParasut = LoadPowerUpsLevelFromLocalStorage().levelParasut;
        if (levelMagnet == 0)
            levelMagnet = LoadPowerUpsLevelFromLocalStorage().levelMagnet;
        if (levelLayar == 0)
            levelMagnet = LoadPowerUpsLevelFromLocalStorage().levelLayar;

        PowerUpsLevel powerUpsLevel = new PowerUpsLevel(levelPesawat, levelParasut, levelMagnet, levelLayar);
        JsonReadWriteSystem.SaveToJsonOverwrite<PowerUpsLevel>(powerUpsLevel, powerUpsLevelFileName);

        //Input Data into Firebase
        if (PlayerPrefs.HasKey("Facebook-Login"))
            //FireBaseDataBase.SavePowerUpsLevelData(PlayerPrefs.GetString("Facebook-Login"));
            FireBaseDataBase.SavePowerUpsLevelData("User-1114840726126417");
    }

    public PowerUpsLevel LoadPowerUpsLevelFromLocalStorage()
    {
        PowerUpsLevel powerUpsLevel;
        return powerUpsLevel = JsonReadWriteSystem.ReadFromJson<PowerUpsLevel>(powerUpsLevelFileName);
    }

    ///--------------------------------------------------CURRENCIES AND HIGHSCORE-------------------------------------------------
    public void SaveCurrenciesAndHighscoreIntoLocalStorage(int highscore, int coins, int paperClips, int layar)
    {
        CurrenciesandHighscore currenciesandHighscore = new CurrenciesandHighscore(highscore, coins, paperClips, layar);
        JsonReadWriteSystem.SaveToJsonOverwrite<CurrenciesandHighscore>(currenciesandHighscore, currenciesAndHighscoreFileName);

        //Input Data into Firebase
        if (PlayerPrefs.HasKey("Facebook-Login"))
            //FireBaseDataBase.SaveCurrenciesAndHighscoreData(PlayerPrefs.GetString("Facebook-Login"));
            FireBaseDataBase.SaveCurrenciesAndHighscoreData("User-1114840726126417");
    }

    public CurrenciesandHighscore LoadCurrenciesAndHighscoreFromLocalStorage()
    {
        CurrenciesandHighscore currenciesandHighscore;
        return currenciesandHighscore = JsonReadWriteSystem.ReadFromJson<CurrenciesandHighscore>(currenciesAndHighscoreFileName);
    }
}
