using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting:MonoBehaviour
{
    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
    public Vector3 updatedPosition = new Vector3(1000f, 1000f, 1000f);

    public float lightHighIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;
    public float musicFadeSpeed = 1f;

    public bool discover; //control

    private AlarmLight alarm; 
    private Light mainLight; 
    private AudioSource panicAudio; 
    private AudioSource[] sirens;

    public float timerOut = 0.0f;
    public int secondsOut;

    void Awake()
    {
        alarm = GameObject.FindGameObjectWithTag(Tags.alarmLight).GetComponent<AlarmLight>();
        mainLight = GameObject.FindGameObjectWithTag(Tags.mainLight).GetComponent<Light>();
        panicAudio = transform.Find("secondaryMusic").GetComponent<AudioSource>();
        GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.siren);
     
        sirens = new AudioSource[sirenGameObjects.Length];
     
        for (int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
        }

        discover = false;
    }
    void Update()
    {
        // Switch the alarms and fade the music.
        SwitchAlarms();
        MusicFading();

        if (!discover) {
            timerOut += Time.deltaTime;
            secondsOut = (int)(timerOut % 60);

            if (secondsOut >= 5)
            {
                if (updatedPosition == position)
                {
                    position = resetPosition;
                    timerOut = 0.0f;
                    secondsOut = 0;
                }
            }
        }
        else
        {
            timerOut = 0.0f;
            secondsOut = 0;
        }
        
    }

    void SwitchAlarms()
    {
        alarm.alarmOn = position != resetPosition;
        float newIntensity;
        if (position != resetPosition)
        {
            updatedPosition = position;
            newIntensity = lightLowIntensity;
        }
        else
            newIntensity = lightHighIntensity;

        mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

        for (int i = 0; i < sirens.Length; i++)
        {
            if (position != resetPosition && !sirens[i].isPlaying) sirens[i].Play();
            else if (position == resetPosition) sirens[i].Stop();
        }
    }

    void MusicFading()
    {
        if (position != resetPosition)
        {
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0.8f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}
