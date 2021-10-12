using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimianimation : MonoBehaviour
{
    public float deadZone = 5f;
    private Transform player;
    private mimiinSight enemySight;
    private UnityEngine.AI.NavMeshAgent nav;
    private Animator anim;
    private HashIDs hash;
    private AnimatorSetup animSetup;
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Enemy").transform;
        enemySight = GetComponent<mimiinSight>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        //nav.updateRotation = false;
        animSetup = new AnimatorSetup(anim, hash);

        anim.SetLayerWeight(1, 1f);
        anim.SetLayerWeight(2, 1f);
        deadZone *= Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {
        NavAnimSetup();
    }
    void OnAnimatorMove()
    {
        //nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }

    void NavAnimSetup()
    {
        float speed;
        float angle;

        if (enemySight.playerInSight)
        {
            speed = 0f;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);

        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
            if (Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }

        }
        animSetup.Setup(speed, angle);
    }
    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
        {
            return 0f;
        }
        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;
        return angle;
    }
}
