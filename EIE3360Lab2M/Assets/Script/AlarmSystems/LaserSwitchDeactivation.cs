using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
public class LaserSwitchDeactivation : MonoBehaviour
{
    public GameObject laser;
    public Material unlockedMat;

    private GameObject player; 
    void Awake()
    {

        player = GameObject.FindGameObjectWithTag(Tags.player);
    }
    void OnTriggerStay(Collider other)
    {
  
        if (other.gameObject == player)
            if (Input.GetButton("Switch") || CnInputManager.GetButton("Switch"))
                LaserDeactivation();
    }
    void LaserDeactivation()
    {
        laser.SetActive(false);
        Renderer screen = transform.Find("prop_switchUnit_screen").GetComponent<Renderer>(); 
        screen.material = unlockedMat;
        GetComponent<AudioSource>().Play();
    }
}
