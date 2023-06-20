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

    [SerializeField] LayerMask clickPass;

    private void OnInteract(InputValue value)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 1000) && hit.transform.tag == "Player" && value.isPressed)
            PlayerClicked?.Invoke();

        else if (Physics.Raycast(ray, out hit, 1000) && hit.transform.tag == "Item" && value.isPressed)
            ItemClicked?.Invoke();

        else if (Physics.Raycast(ray, out hit, 1000) && hit.transform.tag == "UI" && value.isPressed)
            Debug.Log("UI°¡ Å¬¸¯ µÊ");

        else if (Physics.Raycast(ray, out hit, 1000, clickPass))
        {
            GoAttack?.Invoke();
            StartCoroutine(ClickDelay());
        }

    }

    IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(1f);
    }
}
