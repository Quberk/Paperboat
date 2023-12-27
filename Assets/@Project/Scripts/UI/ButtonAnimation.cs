using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        myAnim.SetTrigger("Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        myAnim.SetTrigger("UnPressed");
    }
}
