using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// Responsible for handling UI for the puzzle game.
/// </summary>

public class UIManager : MonoBehaviour
{
    //Holds all UI elements with a key to locate them.
    [SerializeField] Dictionary<string, GameObject> UIElements = new Dictionary<string, GameObject>();

    //Handles UI changes
    public static event Action<bool> OnUIChange;

    private void Start()
    {
        UIElements.Add("ConnectUI", GameObject.Find("ConnectUI"));
        UIElements.Add("WinOrNoWinUI", GameObject.Find("WinOrNoWinUI"));
        
        //Turns off all UI
        foreach (KeyValuePair<string, GameObject> UIElement in UIElements)
        {
            UIElement.Value.SetActive(false);
        }
    }

    #region Enable Disable
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
    #endregion

    //Controls UI popups
    public void ConnectUI(bool toggle, string key, string text, Color? color)
    {
        UIElements[key].SetActive(toggle);
        if (text != null)
        {
            ChangeText(UIElements[key], text, color);
        }
    }

    //Changes text in UI popups
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