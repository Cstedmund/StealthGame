using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LiftTrigger : MonoBehaviour
{
    public float timeToDoorsClose = 2f; // Time since the player entered the lift before the doors close.
    public float timeToLiftStart = 3f;// Time since the player entered the lift before it starts to move.
    public float timeToEndLevel = 6f; // Time since the player entered the lift before the level ends.
    public float liftSpeed = 3f; // The speed at which the lift moves.
    private GameObject player; // Reference to the player.
    private HashIDs hash; // Reference to the HashIDs script.

    private ParticleSystem ps;

    private CameraMovement camMovement; // Reference to the camera movement script.
    private SceneFadeInOut sceneFadeInOut; // Reference to the SceneFadeInOut script.
    private LiftDoorsTracking liftDoorsTracking; // Reference to LiftDoorsTracking script.
    private bool playerInLift; // Whether the player is in the lift or not.
    private float timer; // Timer to determine when the lift moves and when the level ends.
    void Awake()
    {
        // Setting up references.
        player = GameObject.FindGameObjectWithTag(Tags.player);

        ps = player.GetComponent<ParticleSystem>();
        ps.Stop();

        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        camMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
        sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
        liftDoorsTracking = GetComponent<LiftDoorsTracking>();
    }
    void OnTriggerEnter(Collider other)
    {
        // If the colliding gameobject is the player...
        if (other.gameObject == player)
        {// ... the player is in the lift.
            playerInLift = true;
            ps.Play();
        }

    }
    void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger area...
        if (other.gameObject == player)
        {
            // ... reset the timer, the player is no longer in the lift and unparent the player from the lift.
            playerInLift = false;
            timer = 0;
            ps.Stop();
        }
    }
    void Update()
    {
        // If the player is in the lift...
        if (playerInLift)
            // ... activate the lift.
            LiftActivation();
        // If the timer is less than the time before the doors close...
        if (timer < timeToDoorsClose)
            // ... the inner doors should follow the outer doors.
            liftDoorsTracking.DoorFollowing();
        else
            // Otherwise the doors should close.
            liftDoorsTracking.CloseDoors();
    }
    void LiftActivation()
    {
        // Increment the timer by the amount of time since the last frame.
        timer += Time.deltaTime;
        // If the timer is greater than the amount of time before the lift should start...
    if (timer >= timeToLiftStart)
        {
            // ... stop the camera moving and parent the player to the lift.
            camMovement.enabled = false;
            player.transform.parent = transform;
            // Move the lift upwards.
            transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);
            // If the audio clip isn't playing...
            if (!GetComponent<AudioSource>().isPlaying)
                // ... play the clip.
                GetComponent<AudioSource>().Play();
            // If the timer is greater than the amount of time before the level should end...
            if (timer >= timeToEndLevel)
                // ... call the EndScene function.
                sceneFadeInOut.EndScene();
        }
    }
}