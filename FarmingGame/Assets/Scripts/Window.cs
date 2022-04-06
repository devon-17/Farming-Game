using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public Transform waterButton;

    public Transform fertilizerButton;

    public Transform plotButton;

    // Start is called before the first frame update
    void Start()
    {
        waterButton.GetComponent<Button_UI>().MouseOverOnceFunc = () =>
            Tooltip.ShowToolTip_Static("Water Plots Before Planting");
        waterButton.GetComponent<Button_UI>().MouseOutOnceFunc = () =>
            Tooltip.HideToolTip_Static();
        fertilizerButton.GetComponent<Button_UI>().MouseOverOnceFunc = () =>
            Tooltip.ShowToolTip_Static("Speeds Up Grow Time");
        fertilizerButton.GetComponent<Button_UI>().MouseOutOnceFunc = () =>
            Tooltip.HideToolTip_Static();
        plotButton.GetComponent<Button_UI>().MouseOverOnceFunc = () =>
            Tooltip.ShowToolTip_Static("Buy Plots to Grow Plants On Them");
        plotButton.GetComponent<Button_UI>().MouseOutOnceFunc = () =>
            Tooltip.HideToolTip_Static();
    }
}
