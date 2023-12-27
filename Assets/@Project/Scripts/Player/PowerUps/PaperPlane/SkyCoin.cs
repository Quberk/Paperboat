using UnityEngine;

public class SkyCoin : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject skyCoinObject;
    // Start is called before the first frame update
    void Start()
    {
        skyCoinObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        skyCoinObject.SetActive(false);
    }

    public void ActivateSkyCoinObject()
    {
        skyCoinObject.SetActive(true);
    }

    public void DeactivateSkyCoinObject()
    {
        skyCoinObject.SetActive(false);
    }
}
