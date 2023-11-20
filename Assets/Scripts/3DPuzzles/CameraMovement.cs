using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Responsible for handling camera movement (via keyboard controls).
/// </summary>

public class CameraMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector3 move = new Vector3(0, 0, 0);
    Vector2 rotate = new Vector2(0, 0);
    [SerializeField] float speed = 10f;

    [Range(1000, 3000)]
    [SerializeField] float mouseSensitivity = 2000f;

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
        float mouseX;
        float mouseY;

        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
            mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

            rotate.y += mouseX;
            rotate.x -= mouseY;
            rotate.x = Mathf.Clamp(rotate.x, -90f, 90f);

            transform.rotation = Quaternion.Euler(rotate.x, rotate.y, 0f);
        }
    }
}