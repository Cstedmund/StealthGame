using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CCTVPlayerDetection : MonoBehaviour
{
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;
    Animator cctvAnimation;

    public GameObject cctv;
    //public GameObject cctvCollision;
    
    //Animator cctvColliAnimation;
    public float timer = 0.0f;
    public int seconds;
    public SceneFadeInOut sceneFadeInOut;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            cctv.GetComponent<Animator>().enabled = false;
            //cctvCollision.GetComponent<Animator>().enabled = false;
            lastPlayerSighting.discover = true;
        }
    }

    void OnTriggerStay(Collider other)
    {

        // If the colliding gameobject is the player...
        if (other.gameObject == player)
        {
            // ... raycast from the camera towards the player.
            Vector3 relPlayerPos = player.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, relPlayerPos, out hit))
                // If the raycast hits the player...
                if (hit.collider.gameObject == player)
                {
                    // ... set the last global sighting of the player to the player's position.
                    lastPlayerSighting.position = player.transform.position;
                    cctv.GetComponent<Animator>().enabled = false;

                    relPlayerPos.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(relPlayerPos);
                    cctv.transform.rotation = rotation;
                    //cctvCollision.transform.rotation = cctv.transform.rotation;
                    timer += Time.deltaTime;
                    seconds = (int)(timer % 60);
                    if (seconds >= 5)
                    {
                        sceneFadeInOut.EndScene();
                    }
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            cctv.GetComponent<Animator>().enabled = true;
            //cctvCollision.GetComponent<Animator>().enabled = true;
            timer = 0.0f;
            seconds = 0;
        }
        lastPlayerSighting.discover = false; //trigger reset alarm
    }

}
