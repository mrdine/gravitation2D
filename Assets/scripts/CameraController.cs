using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 1.0f; // Velocidade de zoom da câmera

    public float cameraSpeed = 3.0f; // Velocidade de movimento da câmera
    public float minZoom = 1.0f; // Zoom mínimo permitido
    public float maxZoom = 10.0f; // Zoom máximo permitido

    public float maxDistance = 10.0f; // Maxima distancia que a camera pode se mover

    private Vector3 lastMousePosition;

    private TogglesManager togglesManager;

    private Camera cam; // Referência para a câmera

    private bool isZoomingInMobile = false;

    public Vector2 lastTouchPosition { get; private set; }
    public Vector2 firstTouchPosition { get; private set; }
    public Vector3 initialPosition { get; private set; }

    void Start()
    {
        // Obtém a referência para a câmera
        cam = GetComponent<Camera>();

        togglesManager = FindObjectOfType<TogglesManager>();
    }

    void Update()
    {
        if (!togglesManager.isSomeToggleOn())
        {
            controlZoom();
            controlZoomMobile();
            //moveInDesktop();
            moveInMobile();
        }

    }

    private void controlZoom()
    {
        // Obtém o valor do scroll do mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Calcula o novo valor de zoom
        float newZoom = cam.orthographicSize - scroll * zoomSpeed;

        // Limita o valor de zoom dentro do intervalo permitido
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        // Atualiza o valor de zoom da câmera
        cam.orthographicSize = newZoom;
    }

    private void controlZoomMobile()
    {
        // Verifica se há dois toques na tela
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Calcula a distância entre os dois toques
            Vector2 touchDeltaPosition1 = touch1.position - touch1.deltaPosition;
            Vector2 touchDeltaPosition2 = touch2.position - touch2.deltaPosition;
            float previousTouchDeltaMagnitude = (touchDeltaPosition1 - touchDeltaPosition2).magnitude;
            float touchDeltaMagnitude = (touch1.position - touch2.position).magnitude;

            // Calcula o novo valor de zoom baseado na distância entre os dois toques
            float newZoom = cam.orthographicSize + (previousTouchDeltaMagnitude - touchDeltaMagnitude) * zoomSpeed;

            // Limita o valor de zoom dentro do intervalo permitido
            newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

            // Atualiza o valor de zoom da câmera
            cam.orthographicSize = newZoom;

            isZoomingInMobile = true;
        }
        else if(Input.touchCount == 0)
        {
            isZoomingInMobile = false;
        }
    }

    private void moveInDesktop()
    {

        // Verifica se o botão esquerdo do mouse foi pressionado
        if (Input.GetMouseButtonDown(0))
        {
            // Armazena a posição do mouse
            lastMousePosition = Input.mousePosition;
        }

        // Verifica se o botão esquerdo do mouse está sendo pressionado e movido
        if (Input.GetMouseButton(0))
        {
            // Calcula a diferença de posição entre o mouse atual e o último frame
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // Converte a posição do mouse em um ponto no mundo
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Move a câmera para a nova posição
            transform.position -= delta * Time.deltaTime * cameraSpeed;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -maxDistance, maxDistance), Mathf.Clamp(transform.position.y, -maxDistance, maxDistance), -10f);

            // Atualiza a posição do último mouse
            lastMousePosition = Input.mousePosition;
        }
    }

    private void moveInMobile()
    {
        if (!isZoomingInMobile)
        {
            // Verifica se há apenas um toque na tela
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    // Armazena a posição do toque
                    lastTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    // Calcula a diferença de posição entre o toque atual e o último frame
                    Vector3 delta = touch.position - lastTouchPosition;

                    // Converte a posição do toque em um ponto no mundo
                    Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    // Move a câmera para a nova posição
                    transform.position -= delta * Time.deltaTime * cameraSpeed;
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -maxDistance, maxDistance), Mathf.Clamp(transform.position.y, -maxDistance, maxDistance), -10f);

                    // Atualiza a posição do último toque
                    lastTouchPosition = touch.position;
                }
            }
            else if (Input.touchCount == 2)
            {
                // Mantém a posição no primeiro toque
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    // Armazena a posição do primeiro toque
                    firstTouchPosition = touch.position;
                    initialPosition = transform.position;
                }

                // Verifica se o primeiro toque foi solto
                if (touch.phase == TouchPhase.Ended)
                {
                    firstTouchPosition = Vector2.zero;
                }
                else
                {
                    // Mantém a posição no primeiro toque
                    transform.position = initialPosition + (Vector3)((firstTouchPosition - touch.position) * cameraSpeed * Time.deltaTime);
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -maxDistance, maxDistance), Mathf.Clamp(transform.position.y, -maxDistance, maxDistance), -10f);
                }
            }
        }
    }

}
