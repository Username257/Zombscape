using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetBited : MonoBehaviour
{
    [SerializeField] PlayerGetInjured playerGetInjured;

    private void Awake()
    {
        GetComponent<PlayerGetInjured>();
    }

    private void OnEnable()
    {
        playerGetInjured.OnGetBited += GetBited;
    }

    private void OnDisable()
    {
        playerGetInjured.OnGetBited -= GetBited;
    }

    public void GetBited(int body)
    {
        switch (body)
        {
            case 0:
                GameManager.Data.Neck = DataManager.State.Bited;
                break;
            case 1:
                GameManager.Data.RArm = DataManager.State.Bited;
                break;
            case 2:
                GameManager.Data.LArm = DataManager.State.Bited;
                break;
            case 3:
                GameManager.Data.RLeg = DataManager.State.Bited;
                break;
            case 4:
                GameManager.Data.LLeg = DataManager.State.Bited;
                break;
        }
    }
}
