using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButtonAnim : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Normal");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger("Highlighted");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.SetTrigger("Pressed");
        anim.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.enabled = true;
        anim.SetTrigger("Normal");
    }
}
