using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] LayerMask detectMask;
    [SerializeField, Range(0f, 360f)] float angle;
    [SerializeField] private LayerMask obstacleMask;
    private void Update()
    {
        Look();
    }

    private void Look()
    {
        //일단 원형 범위 안의 것들을 감지
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, detectMask);
     
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            //범위에서 부채꼴 범위 안에 있는 지(각도 만큼)
            if (Vector3.Dot(-transform.forward, dirTarget) < Mathf.Cos(angle * range * Mathf.Deg2Rad))
            {
               
            }
            else
            {
                
            }

        }

    }
    private void OnDrawGizmosSelected()
    {
        Color tmp = Color.white;
        tmp.a = tmp.a / 20;

        Handles.color = tmp;
        Handles.DrawSolidArc(transform.position, transform.up, transform.forward, -angle, range);
        Handles.DrawSolidArc(transform.position, transform.up, transform.forward, angle, range);
    }
}
