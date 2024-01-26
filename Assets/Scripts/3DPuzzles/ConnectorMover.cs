using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using EasyButtons;

public class ConnectorMover : MonoBehaviour
{
    [Range(0.1f, 100)]
    [SerializeField] private float movePercentage = 50;

    enum moveDir
    {
        Left,
        Right,
        Up,
        Down,
        Forward,
        Backward,
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Moves the connection point in the set chosen direction (relative to itself)
    [Button]
    private void Move(moveDir direction)
    {
        float distance = movePercentage / 100;

        switch (direction)
        {
            case moveDir.Left:
                transform.position += new Vector3(transform.parent.GetComponent<BoxCollider>().bounds.size.x * -distance, 0, 0);
                break;
            case moveDir.Right:
                transform.position += new Vector3(transform.parent.GetComponent<BoxCollider>().bounds.size.x * distance, 0, 0);
                break;
            case moveDir.Up:
                transform.position += new Vector3(0, transform.parent.GetComponent<BoxCollider>().bounds.size.y * distance, 0);
                break;
            case moveDir.Down:
                transform.position += new Vector3(0, transform.parent.GetComponent<BoxCollider>().bounds.size.y * -distance, 0);
                break;
            case moveDir.Forward:
                transform.position += new Vector3 (0, 0, transform.parent.GetComponent<BoxCollider>().bounds.size.z * distance);
                break;
            case moveDir.Backward:
                transform.position += new Vector3(0, 0, transform.parent.GetComponent<BoxCollider>().bounds.size.z * -distance);
                break;
        }
    }
}
