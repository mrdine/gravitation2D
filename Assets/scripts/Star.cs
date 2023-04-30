using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float mass;

    public float lightSpeed = 1.0f;
    public float lightRangeAmplitude = 2.3f;
    private float lightRangeOffset;
    public int maxDistance;
    private Rigidbody2D rb;

    private Light lightComponent;
    private TogglesManager togglesManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lightComponent = GetComponent<Light>();
        lightRangeOffset = lightComponent.range;
        togglesManager = FindObjectOfType<TogglesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        destroyIfOutOfBorder();
        lightEffect();
    }

    private void lightEffect() {
        float range = Mathf.Sin(Time.time * lightSpeed) * lightRangeAmplitude + lightRangeOffset;
        lightComponent.range = range;
    }

    private void destroyIfOutOfBorder() {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if( x < -maxDistance || x > maxDistance || y < -maxDistance || y > maxDistance) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Astro"))
        {
            Destroy(collision.gameObject);
        }
    }

     private void OnMouseDown()
    {
        if(togglesManager.getRemoveToggle().isOn) {
            Destroy(gameObject);
        }
       
    }

    
}
