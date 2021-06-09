using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    private float frameTimer;
    private int frameIndex;
    public BeeState state;
    public Vector3 target;
    public Vector2 direction;
   

    public enum BeeState
    {
        Roam,
        SeekFlower,
        GoToHive
    }
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-5, 5), 0);

    }

    void Update()
    {
        direction = (target - transform.position);

        if (direction.magnitude < Time.fixedDeltaTime * speed)
        {
            if (state == BeeState.Roam)
            {
                target = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-5, 5), 0);
            }
            if(state == BeeState.SeekFlower)
            {
                target = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-5, 5), 0);
                GameObject[] flowerArray = GameObject.FindGameObjectsWithTag("Flower");
                if(flowerArray == null)
                {
                    state = BeeState.Roam;
                }
                int flowerIndex = (Random.Range(0, flowerArray.Length));

                target = flowerArray[flowerIndex].transform.position;
            }
            
        }
        else
        {
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            direction.Normalize();
        }

        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

    }

    void TransitionToState(BeeState newState)
    {
        frameTimer = 0.0f;
        frameIndex = 0;
        state = newState;
    }

    BeeState GetState()
    {

        return BeeState.Roam;
    }
}
