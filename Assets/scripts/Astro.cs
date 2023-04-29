using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astro : MonoBehaviour
{
    public int maxDistance;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
     void Update()
    {
        destroyIfOutOfBorder();
    }

    private void destroyIfOutOfBorder() {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if( x < -maxDistance || x > maxDistance || y < -maxDistance || y > maxDistance) {
            Destroy(gameObject);
        }
    }

}
