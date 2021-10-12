using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f; 
    private bool sceneStarting = true; 

    void Awake()
    {
        Debug.Log("faderEnter");
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
    }
    void Update()
    {
        if (sceneStarting)
            StartScene();
    }
    void FadeToClear()
    {

        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }
    void FadeToBlack()
    {
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
    }
    void StartScene()
    {
        FadeToClear();
        if (GetComponent<GUITexture>().color.a <= 0.05f)
        {
            GetComponent<GUITexture>().color = Color.clear;
            GetComponent<GUITexture>().enabled = false;
            sceneStarting = false;
        }
    }
    public void EndScene()
    {
        Debug.Log("faderExit");
        GetComponent<GUITexture>().enabled = true;
        FadeToBlack();
        if (GetComponent<GUITexture>().color.a >= 0.95f)
            SceneManager.LoadScene(0);
    }
}

