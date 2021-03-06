using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;
    public Camera uiCamera;
    public Text tooptipText;
    public RectTransform background;

    void Awake()
    {
        instance = this;
        HideToolTip();
    }

    void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    void ShowToolTip(string tooltipString)
    {
        Cursor.visible = false;
        gameObject.SetActive(true);

        tooptipText.text = tooltipString;
        Vector2 bgSize = new Vector2(tooptipText.preferredWidth + 4f * 2f, tooptipText.preferredHeight + 4f * 2f);
        background.sizeDelta = bgSize;
    }

    void HideToolTip()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    public static void ShowToolTip_Static(string showToolTipString)
    {
        instance.ShowToolTip(showToolTipString);
    }

    public static void HideToolTip_Static()
    {
        instance.HideToolTip();
    }
}
