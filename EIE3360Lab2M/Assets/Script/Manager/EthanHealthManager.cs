using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EthanHealthManager : MonoBehaviour {

    public static int Health;


    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        Health = 600;
    }


    void Update()
    {
        text.text = "Ethan: " + Health;
    }
}
