using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astro : MonoBehaviour
{
    public int maxDistance;
    private float rotationSpeed = 30f; // velocidade da rotação em graus por segundo
    public float minRotationSpeed = 30f;
    public float maxRotationSpeed = 1000f;

    public Color trailColor = Color.white;
    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;
    private TogglesManager togglesManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.material.SetColor("_Color", trailColor);
        togglesManager = FindObjectOfType<TogglesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        destroyIfOutOfBorder();
        rotate();
    }

    private void destroyIfOutOfBorder()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if (x < -maxDistance || x > maxDistance || y < -maxDistance || y > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void rotate()
    {
        // obtenha a velocidade atual do Astro (pode ser um Rigidbody2D, por exemplo)
        float currentSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;

        // calcule o novo rotationSpeed proporcional à velocidade atual
        float newRotationSpeed = Mathf.Clamp(currentSpeed * 100f, minRotationSpeed, maxRotationSpeed);

        // defina o rotationSpeed com o novo valor
        rotationSpeed = newRotationSpeed;
        transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        if (togglesManager.getRemoveToggle().isOn)
        {
            Destroy(gameObject);
        }

    }

}
