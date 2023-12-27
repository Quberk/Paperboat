using UnityEngine;
using UnityEngine.UI;


public class ShopController : MonoBehaviour
{
    private CurrencyTransactionManager currencyTransactionManager;
    private LocalStorageManager localStorageManager;
    [SerializeField] private PowerUpsLevelController powerUpsLevelController;

    [SerializeField] private ShopItems[] shopItems;

    public enum ItemType
    {
        piggy,
        paperClip,
        coin,
        layar,
        magnetLvl,
        paperPlaneLvl,
        boostUpLvl,
        layarLvl
    }

    // Start is called before the first frame update
    void Start()
    {
        currencyTransactionManager = new CurrencyTransactionManager();
        localStorageManager = new LocalStorageManager();
    }

    public void BuyItem(ItemType itemType, int itemAmount, int price, TransactionCurrencyTypes currencyTypes)
    {

        switch (itemType)
        {
            ///ITEM IN SHOP
            case ItemType.piggy:
                //Nanti tambahkan Piggy
                break;
            case ItemType.coin:
                currencyTransactionManager.EarnCurrency(itemAmount, TransactionCurrencyTypes.Coins);
                break;
            case ItemType.layar:
                currencyTransactionManager.EarnCurrency(itemAmount, TransactionCurrencyTypes.Layar);
                break;
            case ItemType.paperClip:
                currencyTransactionManager.EarnCurrency(itemAmount, TransactionCurrencyTypes.PaperClip);
                break;

            /// UPGRADES IN SHOP
            case ItemType.magnetLvl:
                powerUpsLevelController.AddingLevel(ItemType.magnetLvl);
                break;
            case ItemType.boostUpLvl:
                powerUpsLevelController.AddingLevel(ItemType.boostUpLvl);
                break;
            case ItemType.layarLvl:
                powerUpsLevelController.AddingLevel(ItemType.layarLvl);
                break;
            case ItemType.paperPlaneLvl:
                powerUpsLevelController.AddingLevel(ItemType.paperPlaneLvl);
                break;
            default:
                break;
        }

        switch (currencyTypes)
        {
            case TransactionCurrencyTypes.Coins:
                if (price <= localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().coins)
                {
                    currencyTransactionManager.SpendCurrency(price, currencyTypes);
                }
                break;

            case TransactionCurrencyTypes.PaperClip:
                if (price <= localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().paperClips)
                {
                    currencyTransactionManager.SpendCurrency(price, currencyTypes);
                }
                break;

            case TransactionCurrencyTypes.none:
                break;

            default:
                break;
        }
    }
}
