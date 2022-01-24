using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{





    private Animator crateAnimator;
    private AudioSource crateAudioSource;
    private enum ShakeIntensity { Low, Medium, Hard, Death}


    public static GameManager TheGameManager;

    private void Awake()
    {
        TheGameManager = this;
        crateAnimator = GetComponent<Animator>();
        crateAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeTheCrate(ShakeIntensity.Low);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StopTheCrate();
        }
    }


    private void ShakeTheCrate(ShakeIntensity shake)
    {
        crateAnimator.SetBool("isShaking", true);
        crateAudioSource.Play();

        switch (shake)
        {
            case ShakeIntensity.Low:
                Debug.Log("Move");
                crateAnimator.SetTrigger("Move");
                break;
            case ShakeIntensity.Medium:
                break;
            case ShakeIntensity.Hard:
                break;
            case ShakeIntensity.Death:
                break;
            default:
                break;
        }
        //The Crate is moving
    }


    private void StopTheCrate()
    {
        crateAnimator.SetBool("isShaking", false);
        crateAudioSource.Stop();
    }


    public static void KillThePlayer()
    {
        foreach (var light in FindObjectsOfType<Light>())
        {
            light.enabled = false;
        }
    }

}
