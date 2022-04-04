using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoneyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("_money"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
