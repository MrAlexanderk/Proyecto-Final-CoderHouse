using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextCameraDisplay : ObjectInteraction
{
    [SerializeField] CameraDisplayInformation camInformation;
    public static event UnityAction<int> OnCameraChangeDisplay;
    private int currentDisplay = 0;

    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        if (CheckPlayerInteractionDistance())
        {
            OnCameraChangeDisplayHandler();
        }
    }

    private void Awake()
    {

    }


    private void OnCameraChangeDisplayHandler()
    {
        int newCameraDisplay = currentDisplay + 1;

        if(newCameraDisplay < camInformation.CompleteCameraDisplayRender.Length)
        {
            currentDisplay = newCameraDisplay;
        
        } else
        {
            currentDisplay = 0;
        }

        Debug.Log("Changed " + newCameraDisplay);
        OnCameraChangeDisplay?.Invoke(currentDisplay);
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
