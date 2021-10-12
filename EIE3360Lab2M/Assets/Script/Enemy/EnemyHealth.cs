using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float resetAfterDeathTime = 5f;
    //public AudioClip deathClip;
    public Collider myself;
    private Animator anim;
    private EnemyAI playerMovement;
    private EnemyShooting playerShoot;
    private EnemyAnimation playeranimation;
    private EnemySight playersight;
    private HashIDs hash;
    private float timer;
    private bool playerDead;
    // Use this for initialization
    void Awake()
    {
        myself = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<EnemyAI>();
        playeranimation= GetComponent<EnemyAnimation>();
        playerShoot = GetComponent<EnemyShooting>();
        playersight = GetComponent<EnemySight>();
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
        //AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }
    void PlayerDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState) { anim.SetBool(hash.deadBool, false); }
        anim.SetFloat(hash.speedFloat, 0f);
        playerMovement.enabled = false;
        playersight.enabled = false;
        playerShoot.enabled = false;
        playeranimation.enabled = false;

    }
   
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health > 100) { health = 100; }
        if (health < 0) { health = 0; }
        if (myself.name == "char_robotGuard_001")
        {
            Enemy1Health.Health = (int)health;
        }
        if (myself.name == "char_robotGuard_002")
        {
            Enemy2Health.Health = (int)health;
        }
        if (myself.name == "char_robotGuard_003")
        {
            Enemy3Health.Health = (int)health;
        }
        
    }
}
