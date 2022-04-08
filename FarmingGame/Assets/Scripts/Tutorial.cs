using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    public GameObject gameCanvas;

    public GameObject tutorialCanvas;

    public GameObject continueButton;

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
        continueButton.SetActive(false);
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
            tutorialText.text =
                "Now Harvest That Plant By Clicking" +
                '\n' +
                "The 'Harvest' Button In The Top Left" +
                '\n' +
                "Then Clicking On The Plant That Is Done";
        }
        else if (currentStep == 4)
        {
            tutorialText.text =
                "Now Buy More Plots By Clicking On" +
                '\n' +
                "The Hoe Icon Button" +
                '\n' +
                "Then Clicking On The Plot You Want";
        }
        else if (currentStep == 5)
        {
            tutorialText.text =
                "Your'e Done With The Tutorial" +
                '\n' +
                "Use Fertilizer To Speed Grow Time";

            continueButton.SetActive(true);
        }
    }

    public void HideTutorial()
    {
        if (currentStep == 0)
        {
            currentStep++;
        }
        else
        {
            gameCanvas.SetActive(true);
            tutorialCanvas.SetActive(false);
            Debug.Log("Clicked");
        }
    }

    public void LoadNextFarm(string sceneToLoad)
    {
        SceneManager.LoadScene (sceneToLoad);
    }
}
