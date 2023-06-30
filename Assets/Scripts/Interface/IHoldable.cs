using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    void Hold(GameObject obj);
    void Release();
}
