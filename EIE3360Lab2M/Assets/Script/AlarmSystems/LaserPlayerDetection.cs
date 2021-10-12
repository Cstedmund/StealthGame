using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserPlayerDetection : MonoBehaviour
{
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;

    void Awake()
    {
        // Setting up references.
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }
    void OnTriggerStay(Collider other)
    {
        if (GetComponent<Renderer>().enabled)
            if (other.gameObject == player)
                lastPlayerSighting.position = other.transform.position;
    }
}
