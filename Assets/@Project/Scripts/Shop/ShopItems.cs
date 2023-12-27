using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
    private LocalStorageManager localStorageManager = new LocalStorageManager();

    [SerializeField] private ShopController shopController;

    [SerializeField] private ShopController.ItemType itemType;
    [SerializeField] private int itemAmount;
    [SerializeField] private TransactionCurrencyTypes currencyTypes;
    [SerializeField] private int price;

    [SerializeField] private Text priceTxt;

    [Header("Power Ups Level Price")]
    private const int powerUpslevel2Price = 1;
    private const int powerUpslevel3Price = 12;
    private const int powerUpslevel4Price = 36;
    private const int powerUpslevel5Price = 5000;


    private void Start()
    {
        priceTxt.text = price.ToString();
    }

    public void Buy()
    {
        int level = 1;

        switch (itemType)
        {
            case ShopController.ItemType.magnetLvl:
                level = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelMagnet;
                break;

            case ShopController.ItemType.boostUpLvl:
                level = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelParasut;
                break;

            case ShopController.ItemType.layarLvl:
                level = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelLayar;
                break;

            case ShopController.ItemType.paperPlaneLvl:
                level = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelPesawat;
                break;

            default:
                break;
        }

        if (itemType == ShopController.ItemType.magnetLvl || itemType == ShopController.ItemType.boostUpLvl ||
            itemType == ShopController.ItemType.layarLvl || itemType == ShopController.ItemType.paperPlaneLvl)
            price = PowerUpsPrice(level + 1);

        if (price == 0)
            return;

       
        shopController.BuyItem(itemType, itemAmount, price, currencyTypes);
        Debug.Log("sampai sini ja (Shop Items)");
    }

    int PowerUpsPrice(int level)
    {
        int returnedValue = 0;

        switch (level)
        {
            case 2:
                returnedValue = powerUpslevel2Price;
                break;
            case 3:
                returnedValue = powerUpslevel3Price;
                break;
            case 4:
                returnedValue = powerUpslevel4Price;
                break;
            case 5:
                returnedValue = powerUpslevel5Price;
                break;
            default:
                break;
        }

        return returnedValue;
    }
}
