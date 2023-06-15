using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ShadableObject : MonoBehaviour, IShadable
{
    [SerializeField] private float angle;
    [SerializeField] private float range;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask detectMask;

    private float scaleX;
    private float scaleZ;


    private void Start()
    {
        scaleX = transform.lossyScale.x;
        scaleZ = transform.lossyScale.z;
    }

    private void Update()
    {
        Shade();
    }

    public void Shade()
    {
        makeShadow();
    }

    private void makeShadow()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Color tmp = Color.black;
        tmp.a = tmp.a / 5;

        Handles.color = tmp;

        Vector3 vec = (player.transform.position - transform.position).normalized;

        Handles.DrawSolidArc(transform.position, Vector3.up, -vec, -angle * 0.5f, range);
        Handles.DrawSolidArc(transform.position, Vector3.up, -vec, angle * 0.5f, range);

    }
}
