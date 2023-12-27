using UnityEngine;

public class LastTunnelCheck : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private GameObject cameraNormalTrigger;

    public void OnObjectSpawn()
    {
        cameraNormalTrigger.SetActive(false);
    }

    public void ItsTheLastTunnel()
    {
        cameraNormalTrigger.SetActive(true);
    }
}
