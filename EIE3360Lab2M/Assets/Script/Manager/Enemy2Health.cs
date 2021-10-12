using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2Health : MonoBehaviour {

    public static int Health;


    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        Health = 100;
    }


    void Update()
    {
        text.text = "Enemy2: " + Health;
    }
}
