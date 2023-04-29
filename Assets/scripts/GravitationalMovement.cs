using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalMovement : MonoBehaviour
{
    public float gravity = 9.8f; // constante gravitacional
    public float mass = 1f; // massa do objeto
    public float maxVelocity; // velocidade maxima do objeto

    private Rigidbody2D rb;

    // Start é chamado antes da primeira atualização do frame
    void Start()
    {
        // Obtém o componente Rigidbody2D do objeto
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate é chamado em um intervalo de tempo fixo
    void FixedUpdate()
    {
        // Obtém todos os objetos do tipo Star no cenário
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");


        // Inicializa a força resultante
        Vector2 resultantForce = Vector2.zero;

        // Calcula a força resultante de cada objeto Star sobre este objeto Astro
        foreach (GameObject star in stars)
        {
            // Calcula a distancia entre este objeto Astro e o objeto Star atual
            float distance = Vector2.Distance(transform.position, star.transform.position);

            // Calcula a direção do objeto Star para este objeto Astro
            Vector2 direction = (star.transform.position - transform.position).normalized;

            // Calcula a força gravitacional entre os objetos
            float force = (gravity * mass * star.GetComponent<Star>().mass) / Mathf.Pow(distance, 2);

            // Soma a força resultante
            resultantForce += direction * force;
        }

        // Aplica a força resultante no objeto Astro
        rb.AddForce(resultantForce);

        // Limita a velocidade do objeto
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}
