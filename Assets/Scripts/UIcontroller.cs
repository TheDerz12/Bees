using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIcontroller : MonoBehaviour
{
    // shop things
    public float startingMoney;
    public float seedsPrice;
    public float fertilizerPrice;
    public float pesticidePrice;
    public Text seedsPriceText;
    public Text fertilizerPriceText;
    public Text pesticidePriceText;
    public Button seedsButton;
    public Button fertilizerButton;
    public Button pesticideButton;
    public GameObject shopMenu;
    bool shopOpen = false;

    // health bar/money things
    public float timeUpperBound;
    public float timeLowerBound;
    public float payAmount;
    public GameObject healthBarMask;
    Vector2 healthStartingScale;
    public int beehealth = 4;
    float paydayTimer;
    float payrate;

    // inventory things
    public Text honeyText;
    public Text seedsText;
    public Text fertilizersText;
    public Text pesticidesText;
    float honey = 0;
    float seeds = 0;
    float fertilizers = 0;
    float pesticides = 0;

    // for planting
    public Button plantButton;
    public Button fertilizeButton;
    public Button usePesticideButton;
    public GameObject plantButtonComponent;
    public GameObject originalPlant;
    public GameObject growhelpersButtonComponent;
    Vector2 mousePosition = new Vector2();
    GameObject selectedFlower;




    // Start is called before the first frame update
    void Start()
    {
        healthStartingScale = healthBarMask.transform.localScale;
        honey = startingMoney;
        plantButtonComponent.SetActive(false);
        growhelpersButtonComponent.SetActive(false);
        paydayTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (shopOpen)
        {
            shopMenu.SetActive(true);
            if (honey < seedsPrice)
            {
                seedsButton.interactable = false;
            }
            else {
                seedsButton.interactable = true;
            }
            if (honey < fertilizerPrice)
            {
                fertilizerButton.interactable = false;
            }
            else
            {
                fertilizerButton.interactable = true;
            }
            if (honey < pesticidePrice)
            {
                pesticideButton.interactable = false;
            }
            else
            {
                pesticideButton.interactable = true;
            }
        }
        else
        {
            shopMenu.SetActive(false);
        }

        Vector2 goalScale = new Vector2();
        goalScale.y = healthStartingScale.y;
        goalScale.x = healthStartingScale.y * (20 - beehealth) / 20f;
        healthBarMask.transform.localScale = goalScale;

        honeyText.text = ((int)honey).ToString("G");
        seedsText.text = ((int)seeds).ToString("G");
        fertilizersText.text = ((int)fertilizers).ToString("G");
        pesticidesText.text = ((int)pesticides).ToString("G");

        seedsPriceText.text = ((int)seedsPrice).ToString("G");
        fertilizerPriceText.text = ((int)fertilizerPrice).ToString("G");
        pesticidePriceText.text = ((int)pesticidePrice).ToString("G");

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);
            if (hit != null && hit.gameObject.CompareTag("Flower")) {
                GameObject selectedFlower = hit.gameObject;
                growhelpersButtonComponent.SetActive(true);
                
                plantButtonComponent.SetActive(false);
                //Debug.Log("hello");
            }
            else if (mousePosition.x > -5 && mousePosition.x < 3.5 && mousePosition.y > -5 && mousePosition.y < 3)
            {
                plantButtonComponent.SetActive(true);
                growhelpersButtonComponent.SetActive(false);
            }
            else
            {
                plantButtonComponent.SetActive(false);
                growhelpersButtonComponent.SetActive(false);
            }
        }
        if (seeds > 0)
        {
            plantButton.interactable = true;
        }
        else
        {
            plantButton.interactable = false;
        }
        if (fertilizers > 0)
        {
            fertilizeButton.interactable = true;
        }
        else
        {
            fertilizeButton.interactable = false;
        }
        if (pesticides > 0)
        {
            usePesticideButton.interactable = true;
        }
        else
        {
            usePesticideButton.interactable = false;
        }

        if (paydayTimer >= 100f)
        {
            Pay();
            paydayTimer = 0f;
        } else {
            payrate = 100f / ((20f - beehealth) * ((timeUpperBound - timeLowerBound) / 20f) + timeLowerBound);
            paydayTimer += payrate * Time.deltaTime;
            //Debug.Log("paydayTimer: " + paydayTimer.ToString());
            //Debug.Log("payrate: " + payrate.ToString());
        }
    }

    public void BuySeed()
    {
        seeds++;
        honey -= seedsPrice;

    }

    public void BuyFertilizer()
    {
        fertilizers++;
        honey -= fertilizerPrice;
    }

    public void BuyInsecticide() {
        pesticides++;
        honey -= pesticidePrice;
    }

    public void PlantSeed()
    {
        seeds -= 1;
        GameObject newPlant = Instantiate(originalPlant);
        newPlant.transform.position = mousePosition;
        FlowerController flowerScript = newPlant.GetComponent<FlowerController>();
        flowerScript.StartOver();
    }

    public void usePesticide() {
        pesticides -= 1;
        Hurt();
    }

    public void useFertilizer() {
        fertilizers -= 1;
    }


    public void OpenShop() {
        shopOpen = true;
    }

    public void CloseShop() {
        shopOpen = false;
    }

    public void Hurt()
    {
        beehealth--;
        if (beehealth < 0) {
            Application.Quit();
        }
    }

    public void Heal()
    {
        beehealth++;
        if (beehealth >= 20) {
            // you win the game
            return;
        }
    }

    public void Pay() {
        honey += payAmount;
        
    }
}
