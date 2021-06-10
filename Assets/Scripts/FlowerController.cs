using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    public float timeToGrow;
    public Sprite[] growingAnimation;
    public SpriteRenderer srenderer;
    public GameObject UIcontrollerscriptcomponent;
    bool grown;
    float growthStage;
    float growthCounter;
    float growthRate;

    // Start is called before the first frame update
    void Start()
    {
        grown = false;
        growthRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!grown) {
            if (growthCounter >= timeToGrow)
            {
                grown = true;
                UIcontroller mainscript = UIcontrollerscriptcomponent.GetComponent<UIcontroller>();
                mainscript.Heal();
                srenderer.sprite = growingAnimation[growingAnimation.Length - 1];
            }
            else
            {
                srenderer.sprite = growingAnimation[Mathf.FloorToInt(growthCounter / timeToGrow * (growingAnimation.Length - 2))];
            }
            growthCounter += growthRate * Time.deltaTime;
        }
    }

    public void StartOver()
    {
        grown = false;
        growthCounter = 0;
        //Debug.Log("started over!");
    }

    public void useFertilizer()
    {
        growthRate = 2f;
    }

    public void usePesticide()
    {
        growthRate = 3f;
    }
}
