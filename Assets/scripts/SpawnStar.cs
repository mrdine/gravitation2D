using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnStar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject starPrefab;
    private RectTransform buttonRect;

    private Toggle toggle;
    private TogglesManager toggleManager;

    public Image imageButton;

    private  bool canSpawn = false;
    void Start()
    {
        buttonRect = GetComponent<RectTransform>();
        toggle = GetComponent<Toggle>();
        toggleManager = FindObjectOfType<TogglesManager>();

    }

    // Update is called once per frame
    void Update()
    {
        trySpawnStar();
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

    public void trySpawnStar()
    {
       
        if (toggle.isOn && Input.GetMouseButtonDown(0) && !isMousePositionInSomeToggleButton())
        {
            canSpawn = true;
        }

        if (canSpawn && Input.GetMouseButtonUp(0) && !isMousePositionInSomeToggleButton())
        {
            Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject astroInstance = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            toggle.isOn = false;
            canSpawn = false;
        }
    }

    private bool isMousePositionInSomeToggleButton() {
    foreach (Toggle toggle in toggleManager.getToggles()) {
        RectTransform toggleRect = toggle.GetComponent<RectTransform>();
        Vector2 localMousePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(toggleRect, Input.mousePosition, Camera.main, out localMousePos)) {
            if (toggleRect.rect.Contains(localMousePos)) {
                return true;
            }
        }
    }
    return false;
}
}
