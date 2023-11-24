using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Responsible for handling camera movement (via keyboard controls).
/// </summary>

public class CameraMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Vector3 move = new Vector3(0, 0, 0);
    private Vector2 rotate = new Vector2(0, 0);

    [SerializeField] private float drag = 2f;
    [SerializeField] private float speed = 10f;

    [Range(1000, 3000)]
    [SerializeField] private float mouseSensitivity = 2000f;

    private float xInput;
    private float yInput;
    private float zInput;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        MouseLook();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void GetInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal (X)");
    
        zInput = Input.GetAxisRaw("Horizontal (Z)");
    }

    private void Movement()
    {
        move = transform.forward * zInput + transform.right * xInput;
        rigidbody.AddForce(move.normalized * speed, ForceMode.Force);
        rigidbody.drag = drag; 
    }

    private void MouseLook()
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