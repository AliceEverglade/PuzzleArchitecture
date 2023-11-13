using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
