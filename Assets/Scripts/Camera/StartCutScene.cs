using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutScene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float startSize;
    [SerializeField] float zoomSpeed;
    float delta;

    private void Start()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 11, -10);

        GetComponent<Camera>().orthographicSize = startSize;
    }

    private void Update()
    {

        delta = 3f - GetComponent<Camera>().orthographicSize;
        delta *= Time.deltaTime * zoomSpeed;
        GetComponent<Camera>().orthographicSize += delta;

        if (GetComponent<Camera>().orthographicSize <= 4f)
            SetActiveGameCamera();
    }

    void SetActiveGameCamera()
    {
        GetComponent<CameraController>().enabled = true;
        this.enabled = false;
    }

}
