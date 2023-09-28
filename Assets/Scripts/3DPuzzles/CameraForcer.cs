using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForcer : MonoBehaviour
{
    private GameObject parentCamera;

    // Start is called before the first frame update
    void Start()
    {
        parentCamera = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parentCamera.transform.position;
        transform.rotation = parentCamera.transform.rotation;
    }
}
