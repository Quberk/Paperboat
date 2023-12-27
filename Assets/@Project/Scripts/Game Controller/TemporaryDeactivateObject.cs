using UnityEngine;

public class TemporaryDeactivateObject : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject[] objectToActivate;

    public void OnObjectSpawn()
    {
        for (int i = 0; i < objectToActivate.Length; i++)
        {
            objectToActivate[i].SetActive(true);
        }
    }

}
