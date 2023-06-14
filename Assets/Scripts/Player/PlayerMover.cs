using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private CharacterController controller;
    private Vector3 moveDir;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (moveDir.magnitude == 0)
        {
            return;
        }

        Vector3 moveVec = new Vector3(moveDir.x, 0, moveDir.y);
        controller.Move(moveVec * Time.deltaTime * moveSpeed);

        Quaternion lookRotation = Quaternion.LookRotation(moveVec);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
    }

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

}
