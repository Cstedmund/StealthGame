using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimiinSight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public bool alive;
    public Vector3 personalLastSighting;
    public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
    public int currentenemy = 0;
    public Collider who;

    private UnityEngine.AI.NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private GameObject[] player;
   // private Animator playerAnim;
    private EnemyHealth playerHealth;
    private HashIDs hash;


    // Use this for initialization
    void Awake()
    {   
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Enemy");
        //who = player[0].GetComponent<Collider>();
         //Debug.Log(player[0]+""+player[1]+""+player[2]);
        //playerAnim = player.GetComponent<Animator>();
         playerHealth = player[currentenemy].GetComponent<EnemyHealth>(); 

        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        personalLastSighting = resetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player[currentenemy].GetComponent<EnemyHealth>();

        if (playerHealth.health > 0f)
        {
            alive = true;
           // Debug.Log("Update");
            anim.SetBool(hash.playerInSightBool, playerInSight);
        }
        else { anim.SetBool(hash.playerInSightBool, false);
            if (currentenemy < 2 )
            {
                alive = false;
                //playerInSight = false;
                if (!playerInSight || !alive)
                {
                    currentenemy += 1;
                    Debug.Log(currentenemy);
                    
                }
            }
         }
    }

    void OnTriggerStay(Collider other)
    {
      //  Debug.Log(other.gameObject.name);
        if (other.gameObject.name.StartsWith("char_robotGuard"))
        {
            //Debug.Log("Iamin");
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            //Debug.Log(angle);
            if (angle < fieldOfViewAngle * 0.5f)
            {
                Debug.Log("Intheangle");
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    //Debug.Log("Inthefirst"+ hit.collider.gameObject.name);
                    if (hit.collider.gameObject.name.StartsWith("char_robotGuard"))
                    {
                        playerInSight = true;
                        
                       // who= hit.collider;
                        //Debug.Log(who);
                    }
                }
            }


            //int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
           // int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;

            //if (playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState)
            //{
            //    if (CalculatePathLength(player.transform.position) <= col.radius)
                    personalLastSighting = player[currentenemy].transform.position;
            //}
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.StartsWith("char_robotGuard"))
        { playerInSight = false; }
    }
    float CalculatePathLength(Vector3 targetPosition)
    {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;


        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }
        float pathLength = 0;
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}
