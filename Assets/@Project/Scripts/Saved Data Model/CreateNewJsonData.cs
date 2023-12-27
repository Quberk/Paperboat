using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreateNewJsonData
{
    private static LocalStorageManager localStorageManager = new LocalStorageManager();

    public static void CreateNewJson()
    {
        localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(0, 100, 0, 10);
        localStorageManager.SavePowerUpsLevelIntoLocalStorage(1, 1, 1, 1);
        localStorageManager.DeleteSkinFromLocalStorage();
        localStorageManager.SaveSkinIntoLocalStorage("A0001", true);
        localStorageManager.SaveUsedSkinIntoLocalStorage("A0001", "A0001");

    }


}
