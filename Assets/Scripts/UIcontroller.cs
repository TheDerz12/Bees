using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // health bar things
    public GameObject healthBarMask;
    Vector2 healthStartingScale;
    int beehealth = 4;

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
    public GameObject plantButtonComponent;
    public GameObject originalPlant;
    Vector2 mousePosition = new Vector2();


    // Start is called before the first frame update
    void Start()
    {
        healthStartingScale = healthBarMask.transform.localScale;
        honey = startingMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if (shopOpen)
        {
            shopMenu.SetActive(true);
            if (honey < seedsPrice) {
                seedsButton.interactable = false;
            }
            if (honey < fertilizerPrice)
            {
                fertilizerButton.interactable = false;
            }
            if (honey < pesticidePrice)
            {
                pesticideButton.interactable = false;
            }
        }
        else {
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

        if (Input.GetMouseButton(0)) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (mousePosition.x > -5 && mousePosition.x < 3.5 && mousePosition.y > -5 && mousePosition.y < 3)
        {
            plantButtonComponent.SetActive(true);
            if (seeds > 0)
            {
                plantButton.interactable = true;
            }
            else {
                plantButton.interactable = false;
            }
        }
        else
        {
            plantButtonComponent.SetActive(false);
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
    }

    public void OpenShop() {
        shopOpen = true;
    }

    public void CloseShop() {
        shopOpen = false;
    }
}
