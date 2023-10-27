using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SimulationHandler simHandler;
    [SerializeField] private GameObject mouseUI;
    [SerializeField] private Vector3 mouseUIOffset;
    private TMP_Text mouseUIText;
    // Start is called before the first frame update
    void Start()
    {
        mouseUIText = mouseUI.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseUI.transform.position = Input.mousePosition + mouseUIOffset;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, out hit))
        {
            PixelData data = hit.transform.gameObject.GetComponent<PixelDataHolder>().data;
            mouseUI.SetActive(true);
            mouseUIText.text = 
                $"<b>{data.properties.MaterialName}</b> ({data.position.x},{data.position.y})\n" +
                $"Hydration: {data.Hydration * 100}% \n";
            mouseUIText.text += data.ReactionProgress > 0 ? $"Reaction Progress: {data.ReactionProgress * 100}%" : $"";
            /*Debug.Log($"this is: {data.properties.MaterialName} + " +
                $"at x: {data.position.x}, y: {data.position.y}");*/
        }
        else
        {
            mouseUI.SetActive(false);
        }
    }
}
