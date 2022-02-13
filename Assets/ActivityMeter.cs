using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityMeter : MonoBehaviour
{
    public static ActivityMeter TheActivityMeter;


    private float rotationAngle = -55;
    [SerializeField] private int newIndex = 5;

    private AudioSource audioSource;


    private void Awake()
    {
        TheActivityMeter = this;
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeActivityMeter(newIndex);
        }

        ActivityManager();   
    }

    private void ActivityManager()
    {
        audioSource.pitch = 0.5f + ((rotationAngle + 55) / 110);

        if (rotationAngle > 45)
        {
            //Debug.Log("Red Alert");
        } else if(rotationAngle > 25)
        {
            //Debug.Log("Yellow Alert");
        } else if(rotationAngle <= 25)
        {
            //Debug.Log("Normal");
        }

        Debug.Log(rotationAngle);
        
    }

    public void ChangeActivityMeter(float intensity)
    {
        if (rotationAngle >= 65)
        {
            Debug.Log("DEATH");
            GameManager.TheGameManager.GameOver();
        }
        
        if (rotationAngle < -55)
        {
            transform.localRotation = Quaternion.Euler(0, -55, 0);
            return;
        }
        transform.Rotate(Vector3.up * intensity);
        rotationAngle += intensity;
    }






    //- 65 == 0;
    // 65 == 30;
}
