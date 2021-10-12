using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minihealth : MonoBehaviour
{
    public float health = 10000f;
    public float resetAfterDeathTime = 5f;
    //public AudioClip deathClip;

    private Animator anim;
    private mimiscript playerMovement;
    private HashIDs hash;
    private float timer;
    private bool playerDead;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<mimiscript>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            if (!playerDead)
                PlayerDying();
            else
            {
                PlayerDead();
                LevelReset();
            }
        }
    }
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    void PlayerDying()
    {
        playerDead = true;
        anim.SetBool(hash.deadBool, playerDead);
       // AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }
    void PlayerDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState) { anim.SetBool(hash.deadBool, false); }
        anim.SetFloat(hash.speedFloat, 0f);
        playerMovement.enabled = false;
        GetComponent<AudioSource>().Stop();

    }
    void LevelReset()
    {
        timer += Time.deltaTime;
        if (timer >= resetAfterDeathTime) health = 100;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
