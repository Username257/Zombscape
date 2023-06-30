using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventoryExtraButtonAnim : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Start()
    {
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
