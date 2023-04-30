using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstroLauncher : MonoBehaviour
{
    public GameObject astroPrefab; // Prefab do Astro que será instanciado

    private bool isDragging = false; // Flag para indicar se o mouse está sendo arrastado

    private Vector2? pressPosition; // Posição inicial onde o mouse foi pressionado
    private Vector2 dragVector; // Vetor de arrasto
    private RectTransform buttonRect;

    public LineRenderer lineRenderer;

    private Toggle toggle;

    public float forceMultiplier;

    public Image imageButton;

    private TogglesManager toggleManager;

    void Start()
    {
        // Pega o RectTransform do botão atual
        buttonRect = GetComponent<RectTransform>();

        toggle = GetComponent<Toggle>();

        toggleManager = FindObjectOfType<TogglesManager>();

    }
    void Update()
    {
        tryLaunchAstro();
        drawLine();

    }

    public void OnToggleValueChanged()
    {
        if(toggle.isOn) {
            toggleManager.setOffAllTheOthersToggles(toggle);
            imageButton.color = Color.gray;
        } else {
            imageButton.color = Color.white;
        }

        // Resetar variaveis de estado
        isDragging = false;
        pressPosition = null;

    }


    private void tryLaunchAstro()
    {
        if (toggle.isOn && !RectTransformUtility.RectangleContainsScreenPoint(buttonRect, Input.mousePosition))
        {
            if (Input.GetMouseButtonDown(0) && pressPosition == null)
            {
                isDragging = true;
                pressPosition = Input.mousePosition;
            }

            else if (isDragging)
            {
                dragVector = (Vector2)Input.mousePosition - pressPosition.GetValueOrDefault();
            }

            if (isDragging && Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                pressPosition = null;
                // Instancia o Astro na posição final do arrasto
                Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject astroInstance = Instantiate(astroPrefab, spawnPosition, Quaternion.identity);

                float force = dragVector.magnitude * forceMultiplier;

                // Aplica a força no Astro de acordo com o vetor de arrasto
                astroInstance.GetComponent<Rigidbody2D>().AddForce(-dragVector.normalized * force, ForceMode2D.Impulse);
            }
        }
    }

    private void drawLine()
    {
        if (isDragging)
        {
            lineRenderer.positionCount = 2;
            Vector3 start = Camera.main.ScreenToWorldPoint(pressPosition.GetValueOrDefault());
            Vector3 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, new Vector3(start.x, start.y, 0f));
            lineRenderer.SetPosition(1, new Vector3(end.x, end.y, 0f));

            lineRenderer.enabled = true;
        }

        else
        {
            lineRenderer.enabled = false;
        }
    }


}
