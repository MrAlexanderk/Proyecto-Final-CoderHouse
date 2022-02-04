using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityMeter : MonoBehaviour
{
    public static ActivityMeter TheActivityMeter;


    private int rotationAngle = -55;
    [SerializeField] private int newIndex = 5;



    private void Awake()
    {
        TheActivityMeter = this;
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
    }

    public void ChangeActivityMeter(int intensity)
    {
        if (rotationAngle >= 65) GameManager.TheGameManager.GameOver();
        if (rotationAngle <= -55)
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
