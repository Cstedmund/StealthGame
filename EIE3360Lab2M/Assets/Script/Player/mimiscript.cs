using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimiscript : MonoBehaviour {

    private EnemySight enemySight;
    private PlayerHealth playerHealth;
    private Animator anim;
    Transform player;
    Transform enemy;
    UnityEngine.AI.NavMeshAgent nav;
    private HashIDs hash;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemySight = GetComponent<EnemySight>();
        enemy = GameObject.FindGameObjectWithTag(Tags.enemy).transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);

    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        MovementManagement(h, v, sneak);
    }

    void MovementManagement(float horizontal, float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);
        if ((int)Vector3.Distance(transform.position, player.position)>1)
        {
            anim.SetFloat(hash.speedFloat, 5.5f, 0.1f, Time.deltaTime);

        }
        else anim.SetFloat(hash.speedFloat, 0);
    }
    void Update()
    {
        
        float dist = Vector3.Distance(transform.position, player.position);
        //Debug.Log(transform.position+","+ player.position +","+ dist);
        if ((int)dist>1)
        {
            nav.enabled = true;
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
    
}
