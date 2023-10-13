using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SimulationHandler simHandler;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, out hit))
        {
            PixelData data = hit.transform.gameObject.GetComponent<PixelDataHolder>().data;
            /*Debug.Log($"this is: {data.properties.MaterialName} + " +
                $"at x: {data.position.x}, y: {data.position.y}");*/
        }
    }
}
