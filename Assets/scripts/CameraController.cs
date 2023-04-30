using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 1.0f; // Velocidade de zoom da câmera

    public float cameraSpeed = 3.0f; // Velocidade de movimento da câmera
    public float minZoom = 1.0f; // Zoom mínimo permitido
    public float maxZoom = 10.0f; // Zoom máximo permitido

    private Vector3 lastMousePosition;

    private TogglesManager togglesManager;

    private Camera cam; // Referência para a câmera

    void Start()
    {
        // Obtém a referência para a câmera
        cam = GetComponent<Camera>();

        togglesManager = FindObjectOfType<TogglesManager>();
    }

    void Update()
    {
        if(!togglesManager.isSomeToggleOn()) {
            controlZoom();
            move();
        }
        
    }

    private void controlZoom() {
        // Obtém o valor do scroll do mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Calcula o novo valor de zoom
        float newZoom = cam.orthographicSize - scroll * zoomSpeed;

        // Limita o valor de zoom dentro do intervalo permitido
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        // Atualiza o valor de zoom da câmera
        cam.orthographicSize = newZoom;
    }

    private void move() {
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
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            
            // Atualiza a posição do último mouse
            lastMousePosition = Input.mousePosition;
        }
    }
}
