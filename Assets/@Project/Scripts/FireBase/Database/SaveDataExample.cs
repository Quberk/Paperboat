using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataExample : MonoBehaviour
{
    LocalStorageManager localStorageManager = new LocalStorageManager();
    // Start is called before the first frame update
    void Start()
    {

        localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(100, 100, 100, 100);
        localStorageManager.SaveSkinIntoLocalStorage("A0002", true);
        localStorageManager.SavePowerUpsLevelIntoLocalStorage(2, 2, 3, 1);
        localStorageManager.SaveUsedSkinIntoLocalStorage("A0002", "A0002");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
