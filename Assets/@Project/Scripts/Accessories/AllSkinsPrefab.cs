using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSkinsPrefab : MonoBehaviour
{
    public LimitedSkin[] limitedSkins;
    public DefaultSkin[] defaultSkins;
}

public abstract class Skins
{
    public abstract string id { get; set; }
    public abstract string perahuName { get; set; }
    public abstract string perahuDescription { get; set; }
    public abstract GameObject accPerahuModel { get; set; }
    public abstract GameObject accPesawatModel { get; set; }
    public abstract string pesawatName { get; set; }
    public abstract string pesawatDescription { get; set; }
    public abstract GameObject playerPerahuModel { get; set; }
    public abstract GameObject playerPesawatModel { get; set; }
    public abstract TransactionCurrencyTypes perahuPriceType { get; set; }
    public abstract TransactionCurrencyTypes pesawatPriceType { get; set; }
    public abstract int perahuPrice { get; set; }
    public abstract int pesawatPrice { get; set; }
}


[System.Serializable]
public class DefaultSkin : Skins
{
    public string Id;
    public string PerahuName;
    public string PerahuDescription;
    public GameObject AccPerahuModel;
    public GameObject AccPesawatModel;
    public string PesawatName;
    public string PesawatDescription;
    public GameObject PlayerPerahuModel;
    public GameObject PlayerPesawatModel;
    public TransactionCurrencyTypes PerahuPriceType;
    public TransactionCurrencyTypes PesawatPriceType;
    public int PerahuPrice;
    public int PesawatPrice;

    public override string id { get { return Id; } set { Id = value; } }
    public override string perahuName { get { return PerahuName; } set { PerahuName = value; } }
    public override string perahuDescription { get { return PerahuDescription; } set { PerahuDescription = value; } }
    public override GameObject accPerahuModel { get { return AccPerahuModel; } set { AccPerahuModel = value; } }
    public override GameObject accPesawatModel { get { return AccPesawatModel; } set { AccPesawatModel = value; } }
    public override string pesawatName { get { return PesawatName; } set { PesawatName = value; } }
    public override string pesawatDescription { get { return PesawatDescription; } set { PesawatDescription = value; } }
    public override GameObject playerPerahuModel { get { return PlayerPerahuModel; } set { PlayerPerahuModel = value; } }
    public override GameObject playerPesawatModel { get { return PlayerPesawatModel; } set { PlayerPesawatModel = value; } }
    public override TransactionCurrencyTypes perahuPriceType { get { return PerahuPriceType; } set { PerahuPriceType = value; } }
    public override TransactionCurrencyTypes pesawatPriceType { get { return PesawatPriceType; } set { PesawatPriceType = value; } }
    public override int perahuPrice { get { return PerahuPrice; } set { PerahuPrice = value; } }
    public override int pesawatPrice { get { return PesawatPrice; } set { PesawatPrice = value; } }
}


[System.Serializable]
public class LimitedSkin : Skins
{
    public string Id;
    public string PerahuName;
    public string PerahuDescription;
    public GameObject AccPerahuModel;
    public GameObject AccPesawatModel;
    public string PesawatName;
    public string PesawatDescription;
    public GameObject PlayerPerahuModel;
    public GameObject PlayerPesawatModel;
    public TransactionCurrencyTypes PerahuPriceType;
    public TransactionCurrencyTypes PesawatPriceType;
    public int PerahuPrice;
    public int PesawatPrice;

    public override string id { get { return Id; } set { Id = value; } }
    public override string perahuName { get { return PerahuName; } set { PerahuName = value; } }
    public override string perahuDescription { get { return PerahuDescription; } set { PerahuDescription = value; } }
    public override GameObject accPerahuModel { get { return AccPerahuModel; } set { AccPerahuModel = value; } }
    public override GameObject accPesawatModel { get { return AccPesawatModel; } set { AccPesawatModel = value; } }
    public override string pesawatName { get { return PesawatName; } set { PesawatName = value; } }
    public override string pesawatDescription { get { return PesawatDescription; } set { PesawatDescription = value; } }
    public override GameObject playerPerahuModel { get { return PlayerPerahuModel; } set { PlayerPerahuModel = value; } }
    public override GameObject playerPesawatModel { get { return PlayerPesawatModel; } set { PlayerPesawatModel = value; } }
    public override TransactionCurrencyTypes perahuPriceType { get { return PerahuPriceType; } set { PerahuPriceType = value; } }
    public override TransactionCurrencyTypes pesawatPriceType { get { return PesawatPriceType; } set { PesawatPriceType = value; } }
    public override int perahuPrice { get { return PerahuPrice; } set { PerahuPrice = value; } }
    public override int pesawatPrice { get { return PesawatPrice; } set { PesawatPrice = value; } }

    [Header("Limited Edition Time Until")]
    public int date;
    public Month month;
    public Year year;
}

public enum Month
{
    January,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}

public enum Year
{
    a2022,
    a2023,
    a2024,
    a2025,
    a2026
}
