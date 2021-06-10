using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    
    public BeeState state;
    private Vector3 target;
    private Vector2 direction;
    protected int laps = 0;
    public float waitTime = 4;
    private float saveTime;
    private int time;
   

    public enum BeeState
    {
        Roam,
        SeekFlower,
        GoToHive,
        Waiting,
        GoToBank
    }
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-5, 5), 0);

    }

    void FixedUpdate()
    {
        direction = (target - transform.position);

        switch (state)
        {
            case BeeState.Waiting:
                WaitingLogic();
                break;
            case BeeState.Roam:
                RoamLogic();
                break;
            case BeeState.SeekFlower:
                SeekFlowerLogic();
                break;
            case BeeState.GoToHive:
                GoToHiveLogic();
                break;
            case BeeState.GoToBank:
                GoToBankLogic();
                break;

        }

    }

    private void WaitingLogic()
    {
        if(saveTime < Time.time)
        {

            if (laps < 5)
            {
                state = BeeState.SeekFlower;

                GameObject[] flowerArray = GameObject.FindGameObjectsWithTag("Flower");

                int flowerIndex = (Random.Range(0, flowerArray.Length));

                if (flowerArray.Length == 0)
                {
                    state = BeeState.Roam;
                }
                else
                {
                    target = flowerArray[flowerIndex].transform.position;
                }
            }
            else
            {
                GameObject hive = GameObject.FindGameObjectWithTag("Hive");
                state = BeeState.GoToHive;
                target = hive.transform.position;
                laps = 0;
            }
            
          

        }
    }
    private void SeekFlowerLogic()
    {
        direction = (target - transform.position);

        if (direction.magnitude < speed * Time.fixedDeltaTime)
        {
            state = BeeState.Waiting;
            saveTime = waitTime + Time.time;
            rb.velocity = Vector2.zero;
            laps++;
           
        }
        else
        {
            direction.Normalize();
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
       

        
    }
    private void GoToHiveLogic()
    {
        direction = (target - transform.position);
        

        if (direction.magnitude < speed * Time.fixedDeltaTime)
        {
            state = BeeState.Waiting;
            saveTime = waitTime + Time.time;
            rb.velocity = Vector2.zero;
            
        }
        else
        {
            direction.Normalize();
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }
    private void RoamLogic()
    {
        direction = (target - transform.position);
        

        if (direction.magnitude < speed * Time.fixedDeltaTime)
        {
            target = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-5, 5), 0);

        }
        else
        {
            direction.Normalize();
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }

    private void GoToBankLogic()
    {
        direction = (target - transform.position);

        GameObject bank = GameObject.FindGameObjectWithTag("Bank");
        state = BeeState.GoToBank;
        target = bank.transform.position;
       

        if (direction.magnitude < speed * Time.fixedDeltaTime)
        {
            state = BeeState.Waiting;
            saveTime = waitTime + Time.time;
            rb.velocity = Vector2.zero;

        }
        else
        {
            direction.Normalize();
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }
}
