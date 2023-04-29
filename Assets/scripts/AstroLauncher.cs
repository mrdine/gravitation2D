using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroLauncher : MonoBehaviour
{
    public GameObject astroPrefab; // Prefab do Astro que será instanciado

    private bool isDragging = false; // Flag para indicar se o mouse está sendo arrastado

    public bool canLaunch = false; // Flag para indicar se pode começar o processo de instanciar o astro
    private Vector2? pressPosition; // Posição inicial onde o mouse foi pressionado
    private Vector2 dragVector; // Vetor de arrasto
    private RectTransform buttonRect;

    public LineRenderer lineRenderer;

    public float forceMultiplier;

    void Start()
    {
        // Pega o RectTransform do botão atual
        buttonRect = GetComponent<RectTransform>();

        // Define as propriedades do LineRenderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.alignment = LineAlignment.TransformZ;
        lineRenderer.enabled = false;
    }
    void Update()
    {
        tryLaunchAstro();
        drawLine();
    }

    public void allowLaunchAstro()
    {
        canLaunch = true;
    }

    private void tryLaunchAstro()
    {
        if (canLaunch)
        {
            if (Input.GetMouseButtonDown(0) && pressPosition == null && !RectTransformUtility.RectangleContainsScreenPoint(buttonRect, Input.mousePosition))
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
                canLaunch = false;
                pressPosition = null;

                // Instancia o Astro na posição final do arrasto
                Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject astroInstance = Instantiate(astroPrefab, spawnPosition, Quaternion.identity);

                float force = dragVector.magnitude * forceMultiplier;
                Debug.Log(dragVector.magnitude);

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
