using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleRemove : MonoBehaviour
{
    private Toggle toggle;
    private TogglesManager toggleManager;

    public Image imageButton;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggleManager = FindObjectOfType<TogglesManager>();

    }

    public void OnToggleValueChanged()
    {
        if (toggle.isOn)
        {
            toggleManager.setOffAllTheOthersToggles(toggle);
            imageButton.color = Color.gray;
        }
        else
        {
            imageButton.color = Color.white;
        }

    }

}
