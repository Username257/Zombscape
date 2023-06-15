using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float zoomSpeed;
    Vector3 pointVec;
    private float scrollScale;
    private float curSize;

    private void FixedUpdate()
    {
        ScampPlayer();

        if (curSize < 5)
            curSize = Mathf.Lerp(curSize, 5, 0.2f);
        if (curSize > 10)
            curSize = Mathf.Lerp(curSize, 10, 0.2f);

        Zoom();
        Camera.main.orthographicSize = curSize;
    }

    private void OnPointer(InputValue value)
    {
        pointVec = value.Get<Vector2>();
    }

    private void Zoom()
    {
        if (scrollScale == 0)
            return;

        curSize += scrollScale * zoomSpeed;
    }

    private void OnZoom(InputValue value)
    {
        scrollScale = value.Get<Vector2>().y;
    }

    private void ScampPlayer()
    {
        transform.position = player.transform.position + new Vector3(0, 11, -10);
    }
}
