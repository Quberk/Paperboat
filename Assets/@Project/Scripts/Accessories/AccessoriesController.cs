using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AccessoriesController : MonoBehaviour
{
    /// <PENTING>
    /// Masukkan Urutan Skin sesuai karena nanti Skin yang ditampilkan mulai dari urutan pertama 
    /// </PENTING>
   
    [System.Serializable]
    public class SkinInAccessoriesDisplay
    {
        public string id;
        [HideInInspector] public string perahuName;
        [HideInInspector] public string perahuDescription;
        [HideInInspector] public GameObject accPerahuModel;
        [HideInInspector] public GameObject accPesawatModel;
        [HideInInspector] public string pesawatName;
        [HideInInspector] public string pesawatDescription;
        [HideInInspector] public GameObject playerPerahuModel;
        [HideInInspector] public GameObject playerPesawatModel;
        [HideInInspector] public TransactionCurrencyTypes perahuPriceType;
        [HideInInspector] public TransactionCurrencyTypes pesawatPriceType;
        [HideInInspector] public int perahuPrice;
        [HideInInspector] public int pesawatPrice;
        [HideInInspector] public int date;
        [HideInInspector] public Month month;
        [HideInInspector] public Year year;
        [HideInInspector] public bool pesawatUnlocked;
        [HideInInspector] public SkinStatus perahuStatus = SkinStatus.locked;
        [HideInInspector] public SkinStatus pesawatStatus = SkinStatus.locked;
    }

    public enum SkinStatus
    {
        locked,
        unlocked,
        used
    }

    #region Singleton

    public static AccessoriesController Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    //private JsonReadWriteSystem jsonReadWriteSystem;  

    private LocalStorageManager localStorageManager;
    private CurrencyTransactionManager currencyTransactionManager;

    [SerializeField] private List<SkinInAccessoriesDisplay> skinInAccessoriesDisplays;
    [SerializeField] private AllSkinsPrefab allSkinsPrefab;

    private string mode;

    [Header("Showen Stat in Accessories Shop")]
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject unlockBoatSkinFirst;
    [SerializeField] private GameObject paperClipLogo;
    [SerializeField] private GameObject coinLogo;
    [SerializeField] private Text priceTag;
    private string perahuUsedSkinId;
    private string pesawatUsedSkinId;
    private int perahuUsedSkinNumQueue;
    private int pesawatUsedSkinNumQueue;

    [Header("Skin Queue")]
    [SerializeField] private GameObject parentSkinQueue;
    [SerializeField] private GameObject parentBoatSkinQueue;
    [SerializeField] private GameObject parentPlaneSkinQueue;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject boatActiveBtn;
    [SerializeField] private GameObject planeActiveBtn;

    [Header("Gameplay Atribute")]
    [SerializeField] private GameObject perahuSkinPlacement;
    [SerializeField] private Vector3 perahuSkinPos;
    [SerializeField] private Vector3 perahuSkinRot;
    [SerializeField] private Vector3 perahuSkinSize;
    [SerializeField] private GameObject pesawatSkinPlacement;
    [SerializeField] private Vector3 pesawatSkinPos;
    [SerializeField] private Vector3 pesawatSkinRot;
    [SerializeField] private Vector3 pesawatSkinSize;

    private float xDistanceInParentSkinQueue = 1856f;
    private int skinQueueNumSelected = 0;
    private bool farLeftSkinQueue;
    private bool farRightSkinQueue;

    // Start is called before the first frame update
    public void StartAccessoriesController()
    {
        currencyTransactionManager = new CurrencyTransactionManager();
        localStorageManager = new LocalStorageManager();
        AddingSkinFromStorage(); //Adding Skin From storage to be merge with the already exist one.

        //Mengatur Button Acc
        selectButton.SetActive(false);
        buyButton.SetActive(false);

        skinQueueNumSelected = 0;

        ///MENELUSURI SEMUA SKINACCESSORIES DISPLAY
        for (int i = 0; i < skinInAccessoriesDisplays.Count; i++)
        {
            /// <PENTING>
            /// DEFAULT SKIN YANG MASUK DARI SKIN ACCESORIES DISPLAYS
            /// </PENTING>
            for (int j = 0; j < allSkinsPrefab.defaultSkins.Length; j++)
            {
                if (skinInAccessoriesDisplays[i].id == allSkinsPrefab.defaultSkins[j].id)
                {
                    ///<BOAT>
                    GameObject accBoatSkinPrefab = Instantiate(allSkinsPrefab.defaultSkins[j].AccPerahuModel);

                    //Set the Position, Scale and Rotation
                    accBoatSkinPrefab.transform.SetParent(parentBoatSkinQueue.transform);
                    accBoatSkinPrefab.transform.localPosition = new Vector3(53f + (i * 1856f), 0f, 0f);
                    accBoatSkinPrefab.transform.localRotation = Quaternion.Euler(-20f, 0f, 0f);
                    accBoatSkinPrefab.transform.localScale = new Vector3(233f, 215f, 165f);

                    ///<PLANE>
                    GameObject accPlaneSkinPrefab = Instantiate(allSkinsPrefab.defaultSkins[j].AccPesawatModel);

                    //Set the Position, Scale and Rotation
                    accPlaneSkinPrefab.transform.SetParent(parentPlaneSkinQueue.transform);
                    accPlaneSkinPrefab.transform.localPosition = new Vector3(53f + (i * 1856f), 0f, 0f);
                    accPlaneSkinPrefab.transform.localRotation = Quaternion.Euler(-20f, 0f, 0f);
                    accPlaneSkinPrefab.transform.localScale = new Vector3(211f, 194f, 150f);

                    ///POPULATING THE SKIN ACCESSORIES DISPLAYS
                    ///
                    skinInAccessoriesDisplays[i].perahuName = allSkinsPrefab.defaultSkins[j].perahuName;
                    skinInAccessoriesDisplays[i].perahuDescription = allSkinsPrefab.defaultSkins[j].perahuDescription;
                    skinInAccessoriesDisplays[i].accPerahuModel = allSkinsPrefab.defaultSkins[j].accPerahuModel;
                    skinInAccessoriesDisplays[i].accPesawatModel = allSkinsPrefab.defaultSkins[j].accPesawatModel;
                    skinInAccessoriesDisplays[i].pesawatName = allSkinsPrefab.defaultSkins[j].pesawatName;
                    skinInAccessoriesDisplays[i].pesawatDescription = allSkinsPrefab.defaultSkins[j].pesawatDescription;
                    skinInAccessoriesDisplays[i].playerPerahuModel = allSkinsPrefab.defaultSkins[j].playerPerahuModel;
                    skinInAccessoriesDisplays[i].playerPesawatModel = allSkinsPrefab.defaultSkins[j].playerPesawatModel;
                    skinInAccessoriesDisplays[i].perahuPriceType = allSkinsPrefab.defaultSkins[j].perahuPriceType;
                    skinInAccessoriesDisplays[i].pesawatPriceType = allSkinsPrefab.defaultSkins[j].pesawatPriceType;
                    skinInAccessoriesDisplays[i].perahuPrice = allSkinsPrefab.defaultSkins[j].perahuPrice;
                    skinInAccessoriesDisplays[i].pesawatPrice = allSkinsPrefab.defaultSkins[j].pesawatPrice;
                }
            }

            /// <PENTING>
            /// MENCARI LIMITED EDITION YANG MASUK DARI SKIN ACCESORIES DISPLAYS
            /// </PENTING>
            for (int j = 0; j < allSkinsPrefab.limitedSkins.Length; j++)
            {
                if (skinInAccessoriesDisplays[i].id == allSkinsPrefab.limitedSkins[j].id)
                {
                    ///<BOAT>
                    GameObject accBoatSkinPrefab = Instantiate(allSkinsPrefab.limitedSkins[j].AccPerahuModel);
                    accBoatSkinPrefab.transform.SetParent(parentBoatSkinQueue.transform);
                    accBoatSkinPrefab.transform.localPosition = new Vector3(53f + (i * 1856f), 0f, 0f);
                    accBoatSkinPrefab.transform.localRotation = Quaternion.Euler(-20f, 0f, 0f);
                    accBoatSkinPrefab.transform.localScale = new Vector3(233f, 215f, 165f);

                    ///<PLANE>
                    GameObject accPlaneSkinPrefab = Instantiate(allSkinsPrefab.limitedSkins[j].AccPesawatModel);
                    accPlaneSkinPrefab.transform.SetParent(parentPlaneSkinQueue.transform);
                    accPlaneSkinPrefab.transform.localPosition = new Vector3(53f + (i * 1856f), 0f, 0f);
                    accPlaneSkinPrefab.transform.localRotation = Quaternion.Euler(-20f, 0f, 0f);
                    accPlaneSkinPrefab.transform.localScale = new Vector3(211f, 194f, 150f);

                    ///POPULATING THE SKIN ACCESSORIES DISPLAYS
                    ///
                    skinInAccessoriesDisplays[i].perahuName = allSkinsPrefab.limitedSkins[j].perahuName;
                    skinInAccessoriesDisplays[i].perahuDescription = allSkinsPrefab.limitedSkins[j].perahuDescription;
                    skinInAccessoriesDisplays[i].accPerahuModel = allSkinsPrefab.limitedSkins[j].accPerahuModel;
                    skinInAccessoriesDisplays[i].accPesawatModel = allSkinsPrefab.limitedSkins[j].accPesawatModel;
                    skinInAccessoriesDisplays[i].pesawatName = allSkinsPrefab.limitedSkins[j].pesawatName;
                    skinInAccessoriesDisplays[i].pesawatDescription = allSkinsPrefab.limitedSkins[j].pesawatDescription;
                    skinInAccessoriesDisplays[i].playerPerahuModel = allSkinsPrefab.limitedSkins[j].playerPerahuModel;
                    skinInAccessoriesDisplays[i].playerPesawatModel = allSkinsPrefab.limitedSkins[j].playerPesawatModel;
                    skinInAccessoriesDisplays[i].perahuPriceType = allSkinsPrefab.limitedSkins[j].perahuPriceType;
                    skinInAccessoriesDisplays[i].pesawatPriceType = allSkinsPrefab.limitedSkins[j].pesawatPriceType;
                    skinInAccessoriesDisplays[i].perahuPrice = allSkinsPrefab.limitedSkins[j].perahuPrice;
                    skinInAccessoriesDisplays[i].pesawatPrice = allSkinsPrefab.limitedSkins[j].pesawatPrice;
                    //Limited Edition Expire
                    skinInAccessoriesDisplays[i].date = allSkinsPrefab.limitedSkins[j].date;
                    skinInAccessoriesDisplays[i].month = allSkinsPrefab.limitedSkins[j].month;
                    skinInAccessoriesDisplays[i].year = allSkinsPrefab.limitedSkins[j].year;
                }
            }
        }

        //Mengecek di Log semua Id yang telah ditangkap pada Skin Accessories Display
        for (int i = 0; i < skinInAccessoriesDisplays.Count; i++)
        {
            Debug.Log(skinInAccessoriesDisplays[i].id);
        }

        ///SET THE PLAYER SKIN
        SetPlayerPerahuModel(perahuUsedSkinId);
        SetPlayerPesawatModel(pesawatUsedSkinId);

        ///SET THE INITIAL DATA TO SHOW IN ACCESSORIES PANEL
        ResetTheAccessoriesPanel(); //Reset Skin Mode apabila modenya pesawat

    }

    // Update is called once per frame
    void Update()
    {
        #region Mengecek apakah sudah sampai paling kiri atau paling kanan
        //Jika sampai ujung kiri
        if (skinQueueNumSelected <= 0)
        {
            farLeftSkinQueue = true;

            //Mengatur Button dan Image Component dri Button
            previousButton.GetComponent<Button>().enabled = false;
            previousButton.GetComponent<Image>().enabled = false;
            
        }

        else if (farLeftSkinQueue == true && skinQueueNumSelected > 0)
        {
            farLeftSkinQueue = false;
     
            //Mengatur Button dan Image Component dri Button
            previousButton.GetComponent<Button>().enabled = true;
            previousButton.GetComponent<Image>().enabled = true;
        }   

        //Jika sampai pada ujung kanan
        if (skinQueueNumSelected >= (skinInAccessoriesDisplays.Count - 1))
        {
            farRightSkinQueue = true;

            //Mengatur Button dan Image Component dri Button
            nextButton.GetComponent<Button>().enabled = false;
            nextButton.GetComponent<Image>().enabled = false;
        }

        else if (farRightSkinQueue == true && skinQueueNumSelected < (skinInAccessoriesDisplays.Count - 1))
        {
            farRightSkinQueue = false;

            //Mengatur Button dan Image Component dri Button
            nextButton.GetComponent<Button>().enabled = true;
            nextButton.GetComponent<Image>().enabled = true;
        }
            
        #endregion

    }

    public GameObject GetPerahuSkins(string id)
    {
        GameObject skinPrefab = null;
        foreach(SkinInAccessoriesDisplay skin in skinInAccessoriesDisplays)
        {
            if (skin.id == id)
            {
                skinPrefab = skin.playerPerahuModel;
                break;
            }
        }

        return skinPrefab;
    }

    public GameObject GetPesawatSkins(string id)
    {
        GameObject skinPrefab = null;
        foreach (SkinInAccessoriesDisplay skin in skinInAccessoriesDisplays)
        {
            if (skin.id == id)
            {
                skinPrefab = skin.playerPesawatModel;
                break;
            }
        }

        return skinPrefab;
    }

    private void AddingSkinFromStorage()
    {
        List<SaveSkin> saveSkins = localStorageManager.LoadSkinFromLocalStorage();
        UsedSkin usedSkin = localStorageManager.LoadUsedSkinFromLocalStorage();

        foreach (SaveSkin saveSkin in saveSkins)
        {
            SkinInAccessoriesDisplay temp = new SkinInAccessoriesDisplay();
            temp.id = saveSkin.id;
            temp.pesawatUnlocked = saveSkin.pesawatUnlocked;
            temp.perahuStatus = SkinStatus.unlocked;

            if (saveSkin.pesawatUnlocked == true)
                temp.pesawatStatus = SkinStatus.unlocked; //Mengupdate status Pesawat

            bool existInTheLocalStorage = false;
            int numQueue = 0;

            foreach (SkinInAccessoriesDisplay skinInAccessoriesDisplay in skinInAccessoriesDisplays)
            {
                if (skinInAccessoriesDisplay.id == temp.id)
                {
                    if (temp.pesawatUnlocked == true)
                        skinInAccessoriesDisplay.pesawatStatus = SkinStatus.unlocked; //Mengupdate status Pesawat

                    existInTheLocalStorage = true;
                    skinInAccessoriesDisplay.perahuStatus = SkinStatus.unlocked;
                }

                if (skinInAccessoriesDisplay.id == usedSkin.idPerahu)
                {
                    skinInAccessoriesDisplay.perahuStatus = SkinStatus.used;
                    perahuUsedSkinNumQueue = numQueue;
                    perahuUsedSkinId = skinInAccessoriesDisplay.id;
                }

                if (skinInAccessoriesDisplay.id == usedSkin.idPesawat)
                {
                    skinInAccessoriesDisplay.pesawatStatus = SkinStatus.used;
                    pesawatUsedSkinNumQueue = numQueue;
                    pesawatUsedSkinId = skinInAccessoriesDisplay.id;
                }
                numQueue++;
            }

            if (existInTheLocalStorage == false)
                skinInAccessoriesDisplays.Add(temp);
        }

        return;
    }

    public void ResetTheAccessoriesPanel()
    {
        ResetSkinMode("Boat");
        ChangeTheDataShownInAccessoriesPanel();

    }

    ///---------------------------------------------------------------BUTTONS FUNCTIONALITY------------------------------------------------------------
    ///
    //Next ke order selanjutnya tapi tidak dapat di next apabila sudah mencapai ujung paling kanan
    public void NextSkin()
    {
        if (!farRightSkinQueue)
        {
            skinQueueNumSelected++;

            ResetSkinMode("Boat");

            Vector3 boatSkinPos = parentBoatSkinQueue.GetComponent<RectTransform>().localPosition;
            Vector3 planeSkinPos = parentPlaneSkinQueue.GetComponent<RectTransform>().localPosition;

            parentBoatSkinQueue.GetComponent<RectTransform>().localPosition = new Vector3(boatSkinPos.x - xDistanceInParentSkinQueue,
                                                                                    boatSkinPos.y, boatSkinPos.z);

            parentPlaneSkinQueue.GetComponent<RectTransform>().localPosition = new Vector3(planeSkinPos.x - xDistanceInParentSkinQueue,
                                                                                    planeSkinPos.y, planeSkinPos.z);

            ChangeTheDataShownInAccessoriesPanel();
        }

    }

    //Previous ke order sebelumnya tapi tidak dapat di next apabila sudah mencapai ujung paling kiri
    public void PreviousSkin()
    {
        if (!farLeftSkinQueue)
        {
            skinQueueNumSelected--;

            ResetSkinMode("Boat");

            Vector3 boatSkinPos = parentBoatSkinQueue.GetComponent<RectTransform>().localPosition;
            Vector3 planeSkinPos = parentPlaneSkinQueue.GetComponent<RectTransform>().localPosition;

            parentBoatSkinQueue.GetComponent<RectTransform>().localPosition = new Vector3(boatSkinPos.x + xDistanceInParentSkinQueue,
                                                                                    boatSkinPos.y, boatSkinPos.z);

            parentPlaneSkinQueue.GetComponent<RectTransform>().localPosition = new Vector3(planeSkinPos.x + xDistanceInParentSkinQueue,
                                                                                    planeSkinPos.y, planeSkinPos.z);

            ChangeTheDataShownInAccessoriesPanel();
        }
    }

    public void SelectSkin()
    {
        if (mode == "Boat")
        {
            skinInAccessoriesDisplays[perahuUsedSkinNumQueue].perahuStatus = SkinStatus.unlocked; //Mengganti status Skin yang terpakai sebelumnya
            perahuUsedSkinNumQueue = skinQueueNumSelected;

            skinInAccessoriesDisplays[skinQueueNumSelected].perahuStatus = SkinStatus.used;
            SetStatus(skinInAccessoriesDisplays[skinQueueNumSelected].id);
            SetPlayerPerahuModel(skinInAccessoriesDisplays[skinQueueNumSelected].id);
            localStorageManager.SaveUsedSkinIntoLocalStorage(skinInAccessoriesDisplays[skinQueueNumSelected].id, null); //Update ke Local Storage
        }

        else
        {
            skinInAccessoriesDisplays[pesawatUsedSkinNumQueue].pesawatStatus = SkinStatus.unlocked; //Mengganti status Skin yang terpakai sebelumnya
            pesawatUsedSkinNumQueue = skinQueueNumSelected;

            skinInAccessoriesDisplays[skinQueueNumSelected].pesawatStatus = SkinStatus.used;
            SetStatus(skinInAccessoriesDisplays[skinQueueNumSelected].id);
            SetPlayerPesawatModel(skinInAccessoriesDisplays[skinQueueNumSelected].id);
            localStorageManager.SaveUsedSkinIntoLocalStorage(null, skinInAccessoriesDisplays[skinQueueNumSelected].id);
        }
    }

    public void BuySkin()
    {
        //Mengecek apakah jumlah uang Memenuhi
        if (mode == "Boat" && currencyTransactionManager.SpendCurrency(skinInAccessoriesDisplays[skinQueueNumSelected].perahuPrice, 
            skinInAccessoriesDisplays[skinQueueNumSelected].perahuPriceType))
        {
            skinInAccessoriesDisplays[skinQueueNumSelected].perahuStatus = SkinStatus.unlocked;
            SetStatus(skinInAccessoriesDisplays[skinQueueNumSelected].id);
            localStorageManager.SaveSkinIntoLocalStorage(skinInAccessoriesDisplays[skinQueueNumSelected].id, false);
        }

        else if (currencyTransactionManager.SpendCurrency(skinInAccessoriesDisplays[skinQueueNumSelected].perahuPrice, 
            skinInAccessoriesDisplays[skinQueueNumSelected].perahuPriceType))
        {
            Debug.Log("benar masuk ke pembelian Pesawat");
            skinInAccessoriesDisplays[skinQueueNumSelected].pesawatStatus = SkinStatus.unlocked;
            SetStatus(skinInAccessoriesDisplays[skinQueueNumSelected].id);
            localStorageManager.SaveSkinIntoLocalStorage(skinInAccessoriesDisplays[skinQueueNumSelected].id, true);
        }
    }

    private void ChangeTheDataShownInAccessoriesPanel()
    {
        SetName(skinInAccessoriesDisplays[skinQueueNumSelected].id); //Set The Name in Acc
        SetDescription(skinInAccessoriesDisplays[skinQueueNumSelected].id); //Set The Description in Acc
        SetStatus(skinInAccessoriesDisplays[skinQueueNumSelected].id); //Set The Button according To the Status of the Skin
    }
    private void ChangeTheSkin(int i)
    {

    }

    public void ResetSkinMode(string mode)
    {
        Vector3 parentPos = parentSkinQueue.GetComponent<RectTransform>().localPosition;
        this.mode = mode;
        ChangeTheDataShownInAccessoriesPanel();

        switch (mode)
        {
            case "Boat":
                boatActiveBtn.SetActive(true);
                planeActiveBtn.SetActive(false);

                parentSkinQueue.GetComponent<RectTransform>().localPosition = new Vector3(parentPos.x,
                                                                                        0f, parentPos.z);

                
                break;

            case "Plane":
                boatActiveBtn.SetActive(false);
                planeActiveBtn.SetActive(true);

                parentSkinQueue.GetComponent<RectTransform>().localPosition = new Vector3(parentPos.x,
                                                                                        -1200f, parentPos.z);
                break;

            default:
                break;
        }
    }

    ///-----------------------------------------------------SET ALL THE ATRIBUTE OF ACCESSORIES PANEL-------------------------------------------------
    void SetName(string id)
    {
        //Mencari apakah ada yang sama Default Skin
        foreach(SkinInAccessoriesDisplay skinInAccessoriesDisplay in skinInAccessoriesDisplays)
        {
            //Skip saja jika Id tidak cocok
            if (skinInAccessoriesDisplay.id != id)
            {
                continue;
            }

            if (mode == "Boat")
                nameText.text = skinInAccessoriesDisplay.perahuName;
            else
                nameText.text = skinInAccessoriesDisplay.pesawatName;
        }
    }

    void SetDescription(string id)
    {
        //Mencari apakah ada yang sama Default Skin
        foreach (SkinInAccessoriesDisplay skinInAccessoriesDisplay in skinInAccessoriesDisplays)
        {
            //Skip saja jika Id tidak cocok
            if (skinInAccessoriesDisplay.id != id)
            {
                continue;
            }

            if (mode == "Boat")
                descriptionText.text = skinInAccessoriesDisplay.perahuDescription;
            else
                descriptionText.text = skinInAccessoriesDisplay.pesawatDescription;
        }
    }

    void SetStatus(string id)
    {
        //Mencari apakah ada yang sama Default Skin
        foreach (SkinInAccessoriesDisplay skinInAccessoriesDisplay in skinInAccessoriesDisplays)
        {
            //Skip saja jika Id tidak cocok
            if (skinInAccessoriesDisplay.id != id)
            {
                continue;
            }

            if (mode == "Boat")
            {
                switch (skinInAccessoriesDisplay.perahuStatus)
                {
                    case (SkinStatus.locked): //Belum terbeli
                        buyButton.SetActive(true);
                        selectButton.SetActive(false);
                        unlockBoatSkinFirst.SetActive(false);

                        priceTag.text = skinInAccessoriesDisplay.perahuPrice.ToString();
                        if (skinInAccessoriesDisplay.perahuPriceType == TransactionCurrencyTypes.Coins)
                        {
                            coinLogo.SetActive(true);
                            paperClipLogo.SetActive(false);
                        }
                        else if (skinInAccessoriesDisplay.perahuPriceType == TransactionCurrencyTypes.PaperClip)
                        {
                            coinLogo.SetActive(false);
                            paperClipLogo.SetActive(true);
                        }

                        break;

                    case (SkinStatus.unlocked): //Sudah Terbeli
                        buyButton.SetActive(false);
                        selectButton.SetActive(true);
                        unlockBoatSkinFirst.SetActive(false);
                        break;

                    case (SkinStatus.used): //Sedang Digunakan
                        buyButton.SetActive(false);
                        selectButton.SetActive(false);
                        unlockBoatSkinFirst.SetActive(false);
                        break;

                    default:
                        break;
                }
                return;
            }

            //Jika Mode Plane
            switch (skinInAccessoriesDisplay.pesawatStatus)
            {
                case (SkinStatus.locked): //Belum terbeli
                    buyButton.SetActive(true);
                    selectButton.SetActive(false);
                    unlockBoatSkinFirst.SetActive(false);

                    if (skinInAccessoriesDisplay.perahuStatus == SkinStatus.locked) //Perahu harus terbuka dahulu baru bisa membeli Pesawat
                    {
                        unlockBoatSkinFirst.SetActive(true);
                        buyButton.SetActive(false);
                    }

                    priceTag.text = skinInAccessoriesDisplay.pesawatPrice.ToString();

                    if (skinInAccessoriesDisplay.pesawatPriceType == TransactionCurrencyTypes.Coins)
                    {
                        coinLogo.SetActive(true);
                        paperClipLogo.SetActive(false);
                    }
                    else if (skinInAccessoriesDisplay.pesawatPriceType == TransactionCurrencyTypes.PaperClip)
                    {
                        coinLogo.SetActive(false);
                        paperClipLogo.SetActive(true);
                    }

                    break;

                case (SkinStatus.unlocked): //Sudah Terbeli
                    buyButton.SetActive(false);
                    selectButton.SetActive(true);
                    unlockBoatSkinFirst.SetActive(false);
                    break;

                case (SkinStatus.used): //Sedang Digunakan
                    buyButton.SetActive(false);
                    selectButton.SetActive(false);
                    unlockBoatSkinFirst.SetActive(false);
                    break;

                default:
                    break;
            }

        }
    }

    void SetAccPerahuModel(string id)
    {

    }

    public void SetAccPesawatModel(string id)
    {

    }

    public void SetPlayerPerahuModel(string id)
    {
        //Menghilangkan child Skin yang lama
        Debug.Log("Sampai sini masuk (Accessories Controller)");
        foreach (Transform child in perahuSkinPlacement.transform)
        {
            if (child.CompareTag("Skin_Gfx"))
            {
                Destroy(child.gameObject);
            }
        }

        //Mencari apakah ada yang sama Default Skin
        foreach (SkinInAccessoriesDisplay skinInAccessoriesDisplay in skinInAccessoriesDisplays)
        {
            //Skip saja jika Id tidak cocok
            if (skinInAccessoriesDisplay.id != id)
            {
                continue;
            }

            GameObject skin = Instantiate(skinInAccessoriesDisplay.playerPerahuModel, perahuSkinPlacement.transform);
            skin.transform.localPosition = perahuSkinPos;
            skin.transform.localRotation = Quaternion.Euler(perahuSkinRot);
            skin.transform.localScale = perahuSkinSize;
        }
    }

    public void SetPlayerPerahuModel(string id, GameObject perahuSkinPlacement)
    {
        //Menghilangkan child Skin yang lama
        for (int i = 0; i < perahuSkinPlacement.transform.childCount; i++)
        {
            Debug.Log("Sampai sini masuk (Accessories Controller)");
            Transform child = perahuSkinPlacement.transform.GetChild(i);

            if (child.CompareTag("Skin_Gfx"))
                Destroy(child.gameObject);
        }

        
        //Mencari apakah ada yang sama Default Skin
        foreach (SkinInAccessoriesDisplay skinInAccessoriesDisplay in skinInAccessoriesDisplays)
        {
            //Skip saja jika Id tidak cocok
            if (skinInAccessoriesDisplay.id != id)
            {
                continue;
            }

            GameObject skin = Instantiate(skinInAccessoriesDisplay.playerPerahuModel, perahuSkinPlacement.transform);
            skin.transform.localPosition = perahuSkinPos;
            skin.transform.localRotation = Quaternion.Euler(perahuSkinRot);
            skin.transform.localScale = perahuSkinSize;
        }
    }

    public void SetPlayerPesawatModel (string id)
    {
        //Menghilangkan child Skin yang lama
        foreach (Transform child in pesawatSkinPlacement.transform)
        {
            if (child.CompareTag("Skin_Gfx"))
            {
                Destroy(child.gameObject);
            }
        }

        //Mencari apakah ada yang sama Default Skin
        foreach (SkinInAccessoriesDisplay skinInAccessoriesDisplay in skinInAccessoriesDisplays)
        {
            //Skip saja jika Id tidak cocok
            if (skinInAccessoriesDisplay.id != id)
            {
                continue;
            }

            GameObject skin = Instantiate(skinInAccessoriesDisplay.playerPesawatModel, pesawatSkinPlacement.transform);
            skin.transform.localPosition = pesawatSkinPos;
            skin.transform.localRotation = Quaternion.Euler(pesawatSkinRot);
            skin.transform.localScale = pesawatSkinSize;
        }
    }

    public void SetPlayerPesawatModel(string id, GameObject pesawatSkinPlacement)
    {
        //Menghilangkan child Skin yang lama
        foreach (Transform child in pesawatSkinPlacement.transform)
        {
            if (child.CompareTag("Skin_Gfx"))
            {
                Destroy(child.gameObject);
            }
        }

        //Mencari apakah ada yang sama Default Skin
        foreach (SkinInAccessoriesDisplay skinInAccessoriesDisplay in skinInAccessoriesDisplays)
        {
            //Skip saja jika Id tidak cocok
            if (skinInAccessoriesDisplay.id != id)
            {
                continue;
            }

            GameObject skin = Instantiate(skinInAccessoriesDisplay.playerPesawatModel, pesawatSkinPlacement.transform);
            skin.transform.localPosition = pesawatSkinPos;
            skin.transform.localRotation = Quaternion.Euler(pesawatSkinRot);
            skin.transform.localScale = pesawatSkinSize;
        }
    }

    public void SetPerahuPrice(int price)
    {

    }

    public void SetPesawatPrice(int price)
    {

    }
}
