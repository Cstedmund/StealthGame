using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public float health = 600f;
    public float resetAfterDeathTime = 5f;
    public AudioClip deathClip;
    public Collider myself;
    public mimiscript mimiscript;
    public mimishoot mimishoot;
    public mimiinSight mimisi;
    public mimianimation mimian;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    private float timer;
    private bool playerDead;
	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        myself = GetComponent<Collider>();
        mimian = GetComponent<mimianimation>();
        mimiscript = GetComponent<mimiscript>();
        mimishoot = GetComponent<mimishoot>();
        mimisi = GetComponent<mimiinSight>();
        playerMovement = GetComponent<PlayerMovement>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        //Debug.Log("myself is "+myself.name);
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0f)
        { if (!playerDead)
                PlayerDying();
            else {
                PlayerDead();
                //LevelReset();
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
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }
    void PlayerDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState) { anim.SetBool(hash.deadBool, false); }
        anim.SetFloat(hash.speedFloat, 0f);
        if (myself.name == "char_ethan")
        { playerMovement.enabled = false;
           
        }
        if (myself.name == "Mimi")
        { mimiscript.enabled = false;
            mimishoot.enabled = false;
            mimisi.enabled = false;
            mimian.enabled = false;
        }
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
        if (myself.name == "Mimi")
        {
            MimiHealth.Health = (int)health;
        }
        if (myself.name == "char_ethan")
        {
            EthanHealthManager.Health = (int)health;
        }
        
    }
}
