using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string userId;
    public Skins skinsData;
    public UsedSkin usedSkinData;
    public PowerUpsLevel powerUpsLevelData;
    public CurrenciesandHighscore currenciesandHighscoreData;

    public UserData(string userId, Skins skinsData, UsedSkin usedSkinData, PowerUpsLevel powerUpsLevelData, CurrenciesandHighscore currenciesandHighscoreData) {
        this.userId = userId;
        this.skinsData = skinsData;
        this.usedSkinData = usedSkinData;
        this.powerUpsLevelData = powerUpsLevelData;
        this.currenciesandHighscoreData = currenciesandHighscoreData;
    }

}
