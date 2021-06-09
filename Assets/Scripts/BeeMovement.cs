using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        rb.velocity = new Vector2(transform.position.x * speed, transform.position.y * speed);

    }
}
