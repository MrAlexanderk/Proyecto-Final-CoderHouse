using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : ObjectInteraction
{
    [SerializeField] GateManager currentGate;
    [SerializeField, Range(1, 10)] int damageIntensity = 5;

    private GameObject button;
    private Material buttonMaterial;
    private bool isPressed = false;

    private void Awake()
    {
        button = GetComponentInChildren<CapsuleCollider>().gameObject;
        buttonMaterial = GetComponentInChildren<SphereCollider>().gameObject.GetComponent<MeshRenderer>().material;
        buttonMaterial.SetColor("_EmissionColor", Color.red);
    }

    private void OnMouseDown()
    {
        PressButton();
    }

    private void Update()
    {
        if (isPressed) ActivityMeter.TheActivityMeter.ChangeActivityMeter(Time.deltaTime * 0.5f);
    }


    private void PressButton()
    {
        isPressed = !isPressed;

        if (isPressed)
        {
            button.transform.localPosition = new Vector3(button.transform.localPosition.x, button.transform.localPosition.y, -0.4f);
            buttonMaterial.SetColor("_EmissionColor", Color.green);
            ActivityMeter.TheActivityMeter.ChangeActivityMeter(damageIntensity);
        }
        else
        {
            button.transform.localPosition = new Vector3(button.transform.localPosition.x, button.transform.localPosition.y, 0);
            buttonMaterial.SetColor("_EmissionColor", Color.red); 
            ActivityMeter.TheActivityMeter.ChangeActivityMeter(-damageIntensity);
        }

        currentGate.OpenAndCloseTheGate();
    }

    //0 en z sin apretar
    //-0.4 en z apretar

}
