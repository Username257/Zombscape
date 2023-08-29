using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomPositionGenerator : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] int layerMask;
    public Vector3 SetPosition()
    {
        Vector3 pos = new Vector3(Random.Range(-30, 163), 150f, Random.Range(-232, 98));

        if (Physics.Raycast(pos, Vector3.down, out hit, 500f))
        {
            if (hit.transform.gameObject.layer == 8)
            {
                return new Vector3(pos.x, 0f, pos.z);
            }
            else
                return Vector3.zero;
        }
        else
            return Vector3.zero;
    }
}
