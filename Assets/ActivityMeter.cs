using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityMeter : MonoBehaviour
{
    public static ActivityMeter TheActivityMeter;

    private float rotationAngle = -55;
    [SerializeField] private int newIndex = 5;

    private AudioSource audioSource;
    private bool isHeadOut = false;

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
    }

    public void ChangeActivityMeter(float intensity)
    {
        if (rotationAngle >= 65)
        {
            Debug.Log("DEATH");
            GameManager.TheGameManager.GameOver();
        }
        else if (rotationAngle >= 50 && !isHeadOut)
        {
            
            CrateManager.theCrateManager.ShakeTheCrate();
            if(!isHeadOut) GameManager.TheGameManager.NewHeadOut();
            isHeadOut = true;
        }
        else if (rotationAngle >= 0)
        {
            CrateManager.theCrateManager.OpenAndCloseCrate(true);
        }
        else if (rotationAngle < -30)
        {
            CrateManager.theCrateManager.OpenAndCloseCrate(true);
        }
        else if (rotationAngle < -55)
        {
            CrateManager.theCrateManager.StopTheCrate();
            transform.localRotation = Quaternion.Euler(0, -55, 0);
            return;
        }
        transform.Rotate(Vector3.up * intensity);
        rotationAngle += intensity;
    }






    //- 65 == 0;
    // 65 == 30;
}
