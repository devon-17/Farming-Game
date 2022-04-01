using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotManager : MonoBehaviour
{
    public static PlotManager instance;
    bool isPlanted = false;
    public SpriteRenderer plant;
    BoxCollider2D plantCollider;
    [HideInInspector] public int plantStage = 0;
    float timer;
    [HideInInspector] public Plant selectedPlant; // scriptable obj
    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;
    SpriteRenderer plot;
    private bool isDry = true;
    public Sprite drySprite;
    public Sprite normalSprite;
    public Sprite notBoughtSprite;
    public Sprite[] toolIcons;
    float speed = 1f;
    public bool isPlotBought = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        plot = GetComponent<SpriteRenderer>();

        if (!isPlotBought)
        {
            plot.sprite = notBoughtSprite;
        }
        else
        {
            plot.sprite = drySprite;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted && !isDry)
        {
            timer -= speed * Time.deltaTime;

            if (timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                timer = selectedPlant.timeBetweenStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlanted)
        {
            // plant is on last stage and is done planting AKA isPlanting false
            if (plantStage == selectedPlant.plantStages.Length - 1 && !FarmManager.instance.isPlantSelected && !FarmManager.instance.isToolSelected)
            {
                Harvest();
            }
        }
        // if isPlanting is true and you have enough money
        else if (FarmManager.instance.isPlantSelected && FarmManager.instance.selectedPlant.plant.buyPrice <= FarmManager.instance.money && isPlotBought)
        {
            Plant(FarmManager.instance.selectedPlant.plant);
        }

        // if there is a tool selected AKA bool true
        if (FarmManager.instance.isToolSelected)
        {
            // setting var for switch or param
            switch (FarmManager.instance.selectedTool)
            {
                // if int = 1
                case 1:
                    if (isPlotBought)
                    {
                        isDry = false;
                        plot.sprite = normalSprite;
                        if (isPlanted)
                        {
                            UpdatePlant();
                        }
                    }
                    break;

                // if int = 2
                case 2:
                    if (FarmManager.instance.money >= 10 && isPlotBought)
                    {
                        FarmManager.instance.Transaction(-10);
                        if (speed < 2) speed += 0.2f;
                    }
                    break;

                // if int  = 3
                case 3:
                    if (FarmManager.instance.money >= 50 && !isPlotBought)
                    {
                        isPlotBought = true;
                        FarmManager.instance.Transaction(-50);
                        plot.sprite = drySprite;
                    }

                    break;

                default:
                    break;
            }
        }
    }

    void OnMouseOver()
    {
        if (FarmManager.instance.isPlantSelected)
        {
            // if there is a plant done or you dont have enough money
            if (isPlanted || FarmManager.instance.selectedPlant.plant.buyPrice > FarmManager.instance.money || !isPlotBought)
            {
                // cant buy
                plot.color = unavailableColor;
                CursorHover();
            }
            else
            {
                // can buy
                plot.color = availableColor;
                CursorHover();
            }
        }
        if (FarmManager.instance.isToolSelected)
        {
            switch (FarmManager.instance.selectedTool)
            {
                case 1:
                case 2:
                    if (isPlotBought && FarmManager.instance.money >= (FarmManager.instance.selectedTool - 1) * 10)
                    {
                        plot.color = availableColor;
                        ToolCursorHover();
                    }
                    else
                    {
                        plot.color = unavailableColor;
                        ToolCursorHover();
                    }
                    break;
                case 3:
                    if (!isPlotBought && FarmManager.instance.money >= 50)
                    {
                        plot.color = availableColor;
                        ToolCursorHover();
                    }
                    else
                    {
                        plot.color = unavailableColor;
                        ToolCursorHover();
                    }
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
            }
        }
    }

    void OnMouseExit()
    {
        plot.color = Color.white;
        FarmManager.instance.cursor.gameObject.SetActive(false);
        Cursor.visible = true;
    }

    public void Harvest()
    {
        Debug.Log("Harvested " + plant.name);
        plant.gameObject.SetActive(false);
        isPlanted = false;
        isDry = true;
        plot.sprite = drySprite;
        FarmManager.instance.Transaction(selectedPlant.sellPrice); // + money for the sell price
        speed = 1f;
    }

    public void Plant(Plant newPlant)
    {
        selectedPlant = newPlant;
        Debug.Log("Planted " + plant.name);
        plantStage = 0;
        isPlanted = true;

        FarmManager.instance.Transaction(-selectedPlant.buyPrice); // - money for the buy price

        UpdatePlant();
        timer = selectedPlant.timeBetweenStages;
        plant.gameObject.SetActive(true);
    }

    public void UpdatePlant()
    {
        // if there is a plant planted on a dry plot
        if (isDry)
        {
            // set sprite to plot for dry planted sprite
            plant.sprite = selectedPlant.plantedWhileDry;
        }
        else
        {
            plant.sprite = selectedPlant.plantStages[plantStage]; // updating sprite to current int
        }

        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2); // putting pivot in the center
    }

    public void CursorHover()
    {
        if (!FarmManager.instance.cursor)
            return;

        FarmManager.instance.cursor.gameObject.SetActive(true);
        FarmManager.instance.cursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 1f);
        FarmManager.instance.cursor.sprite = FarmManager.instance.selectedPlant.plant.icon;
        FarmManager.instance.cursor.transform.position = Input.mousePosition;
        Cursor.visible = false;
    }

    public void ToolCursorHover()
    {
        if (!FarmManager.instance.cursor)
            return;

        FarmManager.instance.cursor.gameObject.SetActive(true);
        FarmManager.instance.cursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 1f);
        FarmManager.instance.cursor.transform.position = Input.mousePosition;
        Cursor.visible = false;

        if (FarmManager.instance.isToolSelected)
        {
            switch (FarmManager.instance.selectedTool)
            {
                case 1:
                    FarmManager.instance.cursor.sprite = toolIcons[0];
                    break;
                case 2:
                    FarmManager.instance.cursor.sprite = toolIcons[1];
                    break;
                case 3:
                    FarmManager.instance.cursor.sprite = toolIcons[2];
                    break;
            }
        }
    }
}
