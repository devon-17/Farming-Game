using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Water Button").GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowToolTip_Static("Water Plots Before Planting");
        transform.Find("Water Button").GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideToolTip_Static();

    }
}
