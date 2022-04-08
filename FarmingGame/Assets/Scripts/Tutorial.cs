using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    public GameObject gameCanvas;

    public GameObject tutorialCanvas;

    public Text tutorialText;

    public Text buttonText;

    public int currentStep;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentStep = 0;

        gameCanvas.SetActive(false);
        tutorialCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameCanvas.activeInHierarchy)
            {
                gameCanvas.SetActive(false);
                tutorialCanvas.SetActive(true);
            }
            else
            {
                gameCanvas.SetActive(false);
                tutorialCanvas.SetActive(true);
            }
        }

        if (currentStep == 0)
        {
            tutorialText.text = "Welcome to the tutorial!";
            buttonText.text = "Next";
        }
        else if (currentStep == 1)
        {
            buttonText.text = "Close & Try";
            tutorialText.text =
                "Water a Plot By Clicking" +
                '\n' +
                "The Water Icon Button" +
                '\n' +
                "Then Clicking On A Plot";
        }
        else if (currentStep == 2)
        {
            FarmManager.instance.CheckSelection();

            tutorialCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            tutorialText.text =
                "Now Plant Something By Clicking" +
                '\n' +
                "On The 'Buy' Button" +
                '\n' +
                "Of The Plant You Want" +
                '\n' +
                "Then Clicking That Same" +
                '\n' +
                "Watered Plot";
        }
        else if (currentStep == 3)
        {
            tutorialCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            tutorialText.text =
                "Now Plant Something By Clicking" +
                '\n' +
                "On The 'Buy' Button" +
                '\n' +
                "Of The Plant You Want";
        }
    }

    public void HideTutorial()
    {
        if (buttonText.text != "Next")
        {
            gameCanvas.SetActive(true);
            tutorialCanvas.SetActive(false);
        }
        else if (buttonText.text == "Next")
        {
            currentStep++;
        }
    }
}
