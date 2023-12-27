using System.Collections;
using System.Collections.Generic;

public class CurrencyTransactionManager
{
    LocalStorageManager localStorageManager = new LocalStorageManager();

    public bool SpendCurrency(int price, TransactionCurrencyTypes currencyType)
    {
        switch (currencyType)
        {
            case (TransactionCurrencyTypes.Coins):
                if (price <= localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().coins)
                {
                    CurrenciesandHighscore currenciesandHighscore = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();
                    int coin = currenciesandHighscore.coins - price;

                    //Mengurangi jumlah Coin yang disave pada Local Storage
                    localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscore.highScore, coin, currenciesandHighscore.paperClips,
                        currenciesandHighscore.layar);

                    return true;
                }
                break;

            case (TransactionCurrencyTypes.PaperClip):
                if (price <= localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().paperClips)
                {
                    CurrenciesandHighscore currenciesandHighscore = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();
                    int paperClip = currenciesandHighscore.paperClips - price;

                    //Mengurangi jumlah Paper Clip yang disave pada Local Storage
                    localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscore.highScore, currenciesandHighscore.coins,
                        paperClip, currenciesandHighscore.layar);

                    return true;
                }
                break;

            case (TransactionCurrencyTypes.Layar):
                if (price <= localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().layar)
                {
                    CurrenciesandHighscore currenciesandHighscore = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();
                    int layar = currenciesandHighscore.layar - price;

                    //Mengurangi jumlah Layar yang disave pada Local Storage
                    localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscore.highScore, currenciesandHighscore.coins,
                        currenciesandHighscore.paperClips, layar);

                    return true;
                }
                break;

            default:
                break;

        }
        return false;
    }

    public void EarnCurrency(int amount, TransactionCurrencyTypes currencyType)
    {
        switch (currencyType)
        {
            case (TransactionCurrencyTypes.Coins):
                CurrenciesandHighscore currenciesandHighscore = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();
                int coins = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().coins + amount;

                localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscore.highScore, coins, 
                    currenciesandHighscore.paperClips, currenciesandHighscore.layar);

                break;

            case (TransactionCurrencyTypes.PaperClip):
                CurrenciesandHighscore currenciesandHighscoreNew = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();
                int paperClips = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().paperClips + amount;

                localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscoreNew.highScore, currenciesandHighscoreNew.coins,
                    paperClips, currenciesandHighscoreNew.layar);

                break;

            case (TransactionCurrencyTypes.Layar):
                CurrenciesandHighscore currenciesandHighscoreNew1 = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();
                int layar = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().layar + amount;

                localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscoreNew1.highScore, currenciesandHighscoreNew1.coins,
                    currenciesandHighscoreNew1.paperClips, layar);

                break;

            default:
                break;

        }
        return;
    }

    public void InputHighScore(int highScore)
    {
        CurrenciesandHighscore currenciesandHighscore = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();

        localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(highScore, currenciesandHighscore.coins,
        currenciesandHighscore.paperClips, currenciesandHighscore.layar);
    }

    public int GetHighScore()
    {
        CurrenciesandHighscore currenciesandHighscore = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();
        int highScore = currenciesandHighscore.highScore;
        return highScore;
    }

    public void ChangeCurrencyValue(int amount, TransactionCurrencyTypes currencyType)
    {
        switch (currencyType)
        {
            case (TransactionCurrencyTypes.Coins):
                CurrenciesandHighscore currenciesandHighscore = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();

                localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscore.highScore, amount,
                    currenciesandHighscore.paperClips, currenciesandHighscore.layar);

                break;

            case (TransactionCurrencyTypes.PaperClip):
                CurrenciesandHighscore currenciesandHighscoreNew = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();

                localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscoreNew.highScore, currenciesandHighscoreNew.coins,
                    amount, currenciesandHighscoreNew.layar);

                break;

            case (TransactionCurrencyTypes.Layar):
                CurrenciesandHighscore currenciesandHighscoreNew1 = localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage();

                localStorageManager.SaveCurrenciesAndHighscoreIntoLocalStorage(currenciesandHighscoreNew1.highScore, currenciesandHighscoreNew1.coins,
                    currenciesandHighscoreNew1.paperClips, amount);

                break;

            default:
                break;

        }
        return;
    }
}
