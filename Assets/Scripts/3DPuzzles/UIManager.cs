using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    GameObject connectUIElement;
    public static event Action<bool> OnUIChange;

    private void Start()
    {
        connectUIElement = GameObject.Find("ConnectUI");
        connectUIElement.SetActive(false);
    }

    void OnEnable()
    {
        PieceConnect.ToggleConnectUI += ConnectUI;
    }

    void OnDisable()
    {
        PieceConnect.ToggleConnectUI -= ConnectUI;
    }

    void ConnectUI(bool toggle)
    {
        connectUIElement.SetActive(toggle);
    }

    bool GetUIState()
    {
        return connectUIElement.activeSelf;
    }
}