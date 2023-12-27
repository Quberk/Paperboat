using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using System.Collections;



public static class FireBaseDataBase
{
    private static DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

    private const string skinsFileName = "OwnedSkinLists.json";
    private const string usedSkinFileName = "UsedSkin.json";
    private const string powerUpsLevelFileName = "PowerUpsLevel.json";
    private const string currenciesAndHighscoreFileName = "CurrenciesAndHighscore.json";


    ///--------------------------------------------------SKINS-------------------------------------------------
    public static void SaveSkinsData(string userId)
    {
        string contentToSave = ReadFile(GetPath(skinsFileName));

        List<SaveSkin> data = JsonHelper.FromJson<SaveSkin>(contentToSave).ToList<SaveSkin>();
        string content = JsonHelper.ToJson<SaveSkin>(data.ToArray());

        reference.Child("Users").Child(userId).Child("Skins").SetRawJsonValueAsync(content);
    }

    public static IEnumerator LoadSkinsData(string userId, System.Action<List<SaveSkin>> callback)
    {
        List<SaveSkin> saveSkin = new List<SaveSkin>();
        var task = reference.Child("Users").Child(userId).Child("Skins").GetValueAsync().ContinueWith((mainTask) =>
        {

            if (mainTask.IsFaulted)
            {
                Debug.LogError("An error...");
                return;
            }

            DataSnapshot result = mainTask.Result;
            saveSkin = JsonHelper.FromJson<SaveSkin>(result.GetRawJsonValue().ToString()).ToList<SaveSkin>();
            callback(saveSkin);
        });

        yield return new WaitUntil(() => task.IsCompleted);
    }

    ///--------------------------------------------------USED SKINS-------------------------------------------------
    public static void SaveUsedSkinData(string userId)
    {
        string contentToSave = ReadFile(GetPath(usedSkinFileName));

        UsedSkin data = JsonUtility.FromJson<UsedSkin>(contentToSave);
        string content = JsonUtility.ToJson(data);

        reference.Child("Users").Child(userId).Child("Used-Skins").SetRawJsonValueAsync(content);
    }

    public static IEnumerator LoadUsedSkinsData(string userId, System.Action<UsedSkin> callback)
    {
        UsedSkin usedSkin = new UsedSkin("", "");
        var task = reference.Child("Users").Child(userId).Child("Used-Skins").GetValueAsync().ContinueWith((mainTask) =>
        {
            
            if (mainTask.IsFaulted)
            {
                Debug.LogError("An error...");
                return;
            }

            DataSnapshot result = mainTask.Result;
            usedSkin = JsonUtility.FromJson<UsedSkin>(result.GetRawJsonValue().ToString());
            callback(usedSkin);
        });

        yield return new WaitUntil(() => task.IsCompleted);
    }

    ///--------------------------------------------------POWER UPS LEVEL-------------------------------------------------
    public static void SavePowerUpsLevelData(string userId)
    {
        string contentToSave = ReadFile(GetPath(powerUpsLevelFileName));

        PowerUpsLevel data = JsonUtility.FromJson<PowerUpsLevel>(contentToSave);
        string content = JsonUtility.ToJson(data);

        reference.Child("Users").Child(userId).Child("PowerUps-Level").SetRawJsonValueAsync(content);
    }

    public static IEnumerator LoadPowerUpsData(string userId, System.Action<PowerUpsLevel> callback)
    {
        PowerUpsLevel powerUpsLevel = new PowerUpsLevel(0, 0, 0, 0);
        var task = reference.Child("Users").Child(userId).Child("PowerUps-Level").GetValueAsync().ContinueWith((mainTask) =>
        {

            if (mainTask.IsFaulted)
            {
                Debug.LogError("An error...");
                return;
            }

            DataSnapshot result = mainTask.Result;
            powerUpsLevel = JsonUtility.FromJson<PowerUpsLevel>(result.GetRawJsonValue().ToString());
            callback(powerUpsLevel);
        });

        yield return new WaitUntil(() => task.IsCompleted);
    }

    ///--------------------------------------------------CURRENCIES AND HIGHSCORE-------------------------------------------------

    public static void SaveCurrenciesAndHighscoreData(string userId)
    {
        string contentToSave = ReadFile(GetPath(currenciesAndHighscoreFileName));

        CurrenciesandHighscore data = JsonUtility.FromJson<CurrenciesandHighscore>(contentToSave);
        string content = JsonUtility.ToJson(data);

        reference.Child("Users").Child(userId).Child("Currency-and-Highscore").SetRawJsonValueAsync(content);
    }

    public static IEnumerator LoadCurrenciesAndHighscoreData(string userId, System.Action<CurrenciesandHighscore> callback)
    {
        CurrenciesandHighscore currenciesandHighscore = new CurrenciesandHighscore(0, 0, 0, 0);
        var task = reference.Child("Users").Child(userId).Child("Currency-and-Highscore").GetValueAsync().ContinueWith((mainTask) =>
        {

            if (mainTask.IsFaulted)
            {
                Debug.LogError("An error...");
                return;
            }

            DataSnapshot result = mainTask.Result;
            currenciesandHighscore = JsonUtility.FromJson<CurrenciesandHighscore>(result.GetRawJsonValue().ToString());
            callback(currenciesandHighscore);
        });

        yield return new WaitUntil(() => task.IsCompleted);
    }

    ///--------------------------------------------------GENERIC UTILITIES-------------------------------------------------

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

    private static string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public static IEnumerator UserExistInFirebaseDatabse(string userId, System.Action<bool> callback)
    {
        bool result;
        var task = reference.Child("Users").Child(userId).GetValueAsync().ContinueWith((mainTask) =>
        {
            DataSnapshot snapshot = mainTask.Result;

            if (snapshot.Exists)
            {
                result = true;
            }

            else
            {
                result = false;
            }

            callback(result);
        });

        yield return new WaitUntil(() => task.IsCompleted);
    }

}
