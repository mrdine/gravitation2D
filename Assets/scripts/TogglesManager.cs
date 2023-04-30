using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglesManager : MonoBehaviour
{
    // Dicionário que mapeia o nome do GameObject para o Toggle
    private Toggle[] toggles;

    public Toggle spawnAstroToggle;
    public Toggle spawnStarToggle;
    public Toggle removeToggle;

    void Start()
    {
        // Encontra todos os toggles na cena e adiciona ao dicionário
        toggles = FindObjectsOfType<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setOffAllToggles()
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }
    }

    public void setOffAllTheOthersToggles(Toggle toIgnore)
    {
        foreach (Toggle toggle in toggles)
        {
            if (toggle != toIgnore)
            {
                toggle.isOn = false;
            }

        }
    }

    public bool isSomeToggleOn() {
        foreach (Toggle toggle in toggles)
        {
            if(toggle.isOn) {
                return true;
            }
        }
        return false;
    }

    public Toggle getRemoveToggle() {
        return removeToggle;
    }

    public Toggle[] getToggles() {
        return toggles;
    }
}
