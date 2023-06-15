using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMouseInteractor : MonoBehaviour
{
    [SerializeField] Vector3 mouseWorldPos;
    public UnityEvent ItemClicked;
    public UnityEvent PlayerClicked;
    public UnityEvent GoAttack;

    private void OnInteract(InputValue value)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000) && value.isPressed)
        {
            Debug.Log($"{hit.point}");
            mouseWorldPos = hit.point;
            mouseWorldPos.y = 0;
        }

        if (Physics.Raycast(ray, out hit, 1000) && hit.transform.tag == "Player" && value.isPressed)
            PlayerClicked?.Invoke();

        else if (Physics.Raycast(ray, out hit, 1000) && hit.transform.tag == "Item" && value.isPressed)
            ItemClicked?.Invoke();

        else
            GoAttack?.Invoke();
        
        if (!value.isPressed)
            StartCoroutine(ClickDelay());

    }

    IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(3f);
    }
}
