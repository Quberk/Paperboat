using UnityEngine;

public class MagnetController : MonoBehaviour
{
    [SerializeField] private GameObject targetCoin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin")) other.GetComponent<CoinController>().MoveIntoTarget(targetCoin);
    }
}
