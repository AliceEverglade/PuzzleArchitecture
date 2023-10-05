using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    Dictionary<string, GameObject> UIElements = new Dictionary<string, GameObject>();

    public static event Action<bool> OnUIChange;

    private void Start()
    {
        UIElements.Add("ConnectUI", GameObject.Find("ConnectUI"));
        UIElements.Add("WinOrNoWinUI", GameObject.Find("WinOrNoWinUI"));
        
        foreach (KeyValuePair<string, GameObject> UIElement in UIElements)
        {
            UIElement.Value.SetActive(false);
        }
    }

    void OnEnable()
    {
        PieceConnect.ToggleConnectUI += ConnectUI;
        ConnectionManager.WinOrLose += ConnectUI;
    }

    void OnDisable()
    {
        PieceConnect.ToggleConnectUI -= ConnectUI;
        ConnectionManager.WinOrLose -= ConnectUI;
    }

    public void ConnectUI(bool toggle, string key, string text, Color? color)
    {
        UIElements[key].SetActive(toggle);
        if (text != null)
        {
            ChangeText(UIElements[key], text, color);
        }
    }

    public void ChangeText(GameObject textObject, string text, Color? color)
    {
        textObject.GetComponent<TMP_Text>().text = text;
        if (color != null)
        {
            textObject.GetComponent<TMP_Text>().color = (Color)color;
        }
    }

    bool GetUIState(string key)
    {
        return UIElements[key].activeSelf;
    }
}