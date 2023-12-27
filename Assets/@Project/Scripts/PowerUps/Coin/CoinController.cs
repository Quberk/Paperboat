using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private GameObject impactGfx;
    [SerializeField]
    private GameObject soundFx;
    [SerializeField]
    private Text coinScore;
    private GameController gameController;

    [SerializeField]
    private Animator coinAnim;

    [Header("Coin Get Pulled")]
    private bool getPulled = false;
    private GameObject pullingTarget;


    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        if (coinScore == null)
        coinScore = GameObject.Find("Coin_Text").GetComponent<Text>();

        coinAnim.Play("Idle", 0, Random.Range(0f, 0.5f));

        getPulled = false;
    }

    public void OnObjectSpawn()
    {
        gameController = FindObjectOfType<GameController>();

        if (coinScore == null)
            coinScore = GameObject.Find("Coin_Text").GetComponent<Text>();

        coinAnim.Play("Idle", 0, Random.Range(0f, 0.5f));

        getPulled = false;

        //Menambah jumlah koin/skyCoin di Scene
        if (gameObject.CompareTag("Coin"))
            gameController.coins++;
        else
            gameController.skyCoins++;
    }

    private void FixedUpdate()
    {
        if (getPulled)
        {
            transform.position = Vector3.MoveTowards(transform.position, pullingTarget.transform.position, 20f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            gameController.CoinCounter();
            coinScore.text = gameController.GetCoinCount().ToString();
            Instantiate(impactGfx, transform.position, Quaternion.identity);
            Instantiate(soundFx, transform.position, Quaternion.identity);

            //Destroy(gameObject);
            gameObject.SetActive(false);

            //Mengurangi jumlah koin/skyCoin di Scene
            if (gameObject.CompareTag("Coin"))
                gameController.coins--;
            else
                gameController.skyCoins--;

            return;
        }
    }

    public void MoveIntoTarget(GameObject target)
    {
        getPulled = true;
        pullingTarget = target;
    }

}
