using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemManager : MonoBehaviour
{
    [SerializeField] CameraDisplayInformation camInformation;

    private enum CameraDisplay { FrontDoor, Hall, Off}

    private CameraDisplay currentCameraDisplay;


    private Material cameraDisplay;

    private Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        StartParameters();
    }

    private void StartParameters()
    {
        NextCameraDisplay.OnCameraChangeDisplay += ChangeDisplayCameraHandler;
        materials = GetComponent<MeshRenderer>().materials;


        currentCameraDisplay = CameraDisplay.Off;

        materials[1].mainTexture = camInformation.CompleteCameraDisplayRender[0];
    }





    private void ChangeDisplayCameraHandler(int newCameraDisplay)
    {
        Debug.Log("Changing");
        currentCameraDisplay = (CameraDisplay)newCameraDisplay;
        materials[1].mainTexture = camInformation.CompleteCameraDisplayRender[newCameraDisplay];
        Debug.Log($"The Camera is " + camInformation.CompleteCameraDisplayRender[newCameraDisplay].name);
    }










    void Update()
    {
        
    }
}
