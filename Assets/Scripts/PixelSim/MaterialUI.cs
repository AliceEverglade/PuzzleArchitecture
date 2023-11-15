using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MaterialUI : MonoBehaviour
{
    private PlayerManager manager;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image displayImage;
    [SerializeField] private TMP_Text displayName;
    MaterialData data;
    [SerializeField] private bool selected;

    public void SetSelectedData()
    {
        manager.SetSelectedData(data);
    }

    public void SetUIElements(PlayerManager player, MaterialData materialData,Color color, string name) // might change color to sprite
    {
        data = materialData;
        manager = player;
        displayImage.color = color;
        displayName.text = name;
    }

    public void CheckIfSelected(MaterialData checkData)
    {
        if(data == checkData)
        {
            selected = true;
            backgroundImage.color = Color.magenta;
        }
        else
        {
            selected = false;
            backgroundImage.color = Color.white;
        }
    }
}
