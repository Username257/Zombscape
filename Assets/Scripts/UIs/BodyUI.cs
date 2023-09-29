using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class BodyUI : MonoBehaviour
{
    [SerializeField] Image RArmImg;
    [SerializeField] Image LArmImg;
    [SerializeField] Image RLegImg;
    [SerializeField] Image LLegImg;
    [SerializeField] Image NeckImg;
    [SerializeField] Sprite scar;
    [SerializeField] Sprite bleeding;
    [SerializeField] Sprite bited;
    [SerializeField] Sprite bandaged;
    [SerializeField] Sprite none;
    PlayerGetInjured playerGetInjured;
    PlayerEater eater;
    enum Body { Neck, RArm, LArm, RLeg, LLeg, None}

    private void Init()
    {
        playerGetInjured = GameObject.FindWithTag("Player").GetComponent<PlayerGetInjured>();
        eater = GameObject.FindWithTag("Player").GetComponent<PlayerEater>();

        NeckImg.sprite = none;
        RArmImg.sprite = none;
        LArmImg.sprite = none;
        RLegImg.sprite = none;
        LLegImg.sprite = none;
    }
    private void OnEnable()
    {
        if (playerGetInjured != null && eater != null)
        {
            playerGetInjured.OnGetInjured += ChangeImage;
            playerGetInjured.OnGetBited += ChangeImage;
            eater.OnHealed += ChangeImage;
        }

    }

    private void OnDisable()
    {
        if (playerGetInjured != null && eater != null)
        {
            playerGetInjured.OnGetInjured -= ChangeImage;
            playerGetInjured.OnGetBited -= ChangeImage;
            eater.OnHealed -= ChangeImage;
        }
            
    }

    public void ChangeImage(int body)
    {
        Body whichBody;

        switch (body)
        {
            case 0:
                whichBody = Body.Neck;
                break;
            case 1:
                whichBody = Body.RArm;
                break;
            case 2:
                whichBody = Body.LArm;
                break;
            case 3:
                whichBody = Body.RLeg;
                break;
            case 4:
                whichBody = Body.LLeg;
                break;
            default:
                whichBody = Body.None;
                break;
        }

        if (whichBody == Body.Neck)
        {
            if (GameManager.Data.Neck == DataManager.State.Scar)
                NeckImg.sprite = scar;
            else if (GameManager.Data.Neck == DataManager.State.Bleeding)
                NeckImg.sprite = bleeding;
            else if (GameManager.Data.Neck == DataManager.State.Bited)
                NeckImg.sprite = bited;
            else if (GameManager.Data.Neck == DataManager.State.Bandaged)
                NeckImg.sprite = bandaged;
            else
                NeckImg.sprite = none;
        }

        if (whichBody == Body.RArm)
        {
            if (GameManager.Data.RArm == DataManager.State.Scar)
                RArmImg.sprite = scar;
            else if (GameManager.Data.RArm == DataManager.State.Bleeding)
                RArmImg.sprite = bleeding;
            else if (GameManager.Data.RArm == DataManager.State.Bited)
                RArmImg.sprite = bited;
            else if (GameManager.Data.RArm == DataManager.State.Bandaged)
                RArmImg.sprite = bandaged;
            else
                RArmImg.sprite = none;
        }

        if (whichBody == Body.LArm)
        {
            if (GameManager.Data.LArm == DataManager.State.Scar)
                LArmImg.sprite = scar;
            else if (GameManager.Data.LArm == DataManager.State.Bleeding)
                LArmImg.sprite = bleeding;
            else if (GameManager.Data.LArm == DataManager.State.Bited)
                LArmImg.sprite = bited;
            else if (GameManager.Data.LArm == DataManager.State.Bandaged)
                LArmImg.sprite = bandaged;
            else
                LArmImg.sprite = none;
        }

        if (whichBody == Body.RLeg)
        {
            if (GameManager.Data.RLeg == DataManager.State.Scar)
                RLegImg.sprite = scar;
            else if (GameManager.Data.RLeg == DataManager.State.Bleeding)
                RLegImg.sprite = bleeding;
            else if (GameManager.Data.RLeg == DataManager.State.Bited)
                RLegImg.sprite = bited;
            else if (GameManager.Data.RLeg == DataManager.State.Bandaged)
                RLegImg.sprite = bandaged;
            else
                RLegImg.sprite = none;
        }

        if (whichBody == Body.LLeg)
        {
            if (GameManager.Data.LLeg == DataManager.State.Scar)
                LLegImg.sprite = scar;
            else if (GameManager.Data.LLeg == DataManager.State.Bleeding)
                LLegImg.sprite = bleeding;
            else if (GameManager.Data.LLeg == DataManager.State.Bited)
                 LLegImg.sprite = bited;
            else if (GameManager.Data.LLeg == DataManager.State.Bandaged)
                LLegImg.sprite = bandaged;
            else
                LLegImg.sprite = none;
        }
    }

}
