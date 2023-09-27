using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DebugMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector3 move = new Vector3(0, 0, 0);
    Vector2 rotate = new Vector2(0, 0);
    [SerializeField] float speed = 10f;
    [SerializeField] float mouseSensitivity = 10f;

    float xInput;
    float yInput;
    float zInput;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardMovement();
        VerticalMovement();
        MouseLook();
    }

    void KeyboardMovement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        move = transform.forward * zInput + transform.right * xInput;
        move.y = 0;
        rigidbody.velocity = move.normalized * speed;
    }

    void VerticalMovement()
    {
        yInput = 0;

        if (Input.GetKey(KeyCode.Space))
        {
            yInput = 1;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            yInput = -1;
        }

        rigidbody.velocity += new Vector3(0.0f, yInput * speed, 0.0f);
    }

    void MouseLook()
    {
        if (Input.GetMouseButton(1))
        {
            rotate.x += Input.GetAxis("Mouse X");
            rotate.y += Input.GetAxis("Mouse Y");

            transform.rotation = Quaternion.Euler(-rotate.y * mouseSensitivity, rotate.x * mouseSensitivity, 0.0f);
        }
    }
}