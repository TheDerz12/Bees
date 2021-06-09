using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 direction;
    public float moveSpeed;

    private Vector3 mousePosition;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) { 

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

        
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        direction = (mousePosition - transform.position);

       
        if (direction.magnitude < Time.fixedDeltaTime * moveSpeed)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        { 
            rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            direction.Normalize();
        }

        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }

 
    
}
