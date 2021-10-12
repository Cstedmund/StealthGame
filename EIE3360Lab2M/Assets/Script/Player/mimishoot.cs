using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimishoot : MonoBehaviour
{
    public float maximumDamage = 120f;
    public float minimumDamage = 45f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private Animator anim;
    private HashIDs hash;
    private LineRenderer laserShotLine;
    private Light laserShotLight;
    private SphereCollider col;
    private GameObject[] player;
   // private Collider who;
    private EnemyHealth playerHealth;
    private int currentenemy=0;
    private bool shooting;
    private bool alive;
    private float scaledDamage;

    // Use this for initialization
    void Awake()
    {
        
        anim = GetComponent<Animator>();
        laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectsWithTag("Enemy");
        playerHealth = player[0].gameObject.GetComponent<EnemyHealth>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        laserShotLine.enabled = false;
        laserShotLight.intensity = 0f;
        scaledDamage = maximumDamage - minimumDamage;
    }

    // Update is called once per frame
    void Update()
    {
        currentenemy = GetComponent<mimiinSight>().currentenemy;
        //who = GetComponent<mimiinSight>().who;
        //Debug.Log(currentenemy);
        alive = GetComponent<mimiinSight>().alive;
        playerHealth = player[currentenemy].GetComponent<EnemyHealth>();
        //Debug.Log(who.name);
        float shot = anim.GetFloat(hash.shotFloat);
        Debug.Log(playerHealth.health);
        if (shot > 0.5f && !shooting&&alive )
        {
            Shoot();

        }
        if (shot < 0.5f || !alive )
        {
            shooting = false;
            laserShotLine.enabled = false;
        }
        laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
    }
    void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(hash.aimWeightFloat);
        anim.SetIKPosition(AvatarIKGoal.RightHand, player[currentenemy].transform.position + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }
    void Shoot()
    {
        shooting = true;
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player[currentenemy].transform.position)) / col.radius;
        float damage = scaledDamage * fractionalDistance + minimumDamage;
        playerHealth.TakeDamage(damage);
        ShotEffects();
    }

    void ShotEffects()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, player[currentenemy].transform.position + Vector3.up * 1.5f);
        laserShotLine.enabled = true;
        laserShotLight.intensity = flashIntensity;
        AudioSource.PlayClipAtPoint(shotClip, laserShotLight.transform.position);
    }

}
