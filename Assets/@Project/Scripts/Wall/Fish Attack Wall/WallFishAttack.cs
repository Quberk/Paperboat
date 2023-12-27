using UnityEngine;

public class WallFishAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject ikanSpawner;
    [SerializeField]
    private GameObject[] ikanZPos;
    private float ikanXPosStart = 8.77f;
    private float ikanXPosFinish = 6.3f;
    private float ikanYPos = 1.47f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ikanZPos.Length; i++)
        {
            float xPos = Random.Range(ikanXPosStart, ikanXPosFinish);
            Instantiate(ikanSpawner, new Vector3(xPos, ikanYPos, ikanZPos[i].transform.position.z), Quaternion.Euler(0f, 0f, 0f));
        }

        Destroy(gameObject);
        
    }
}
