using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpotlightButton : MonoBehaviour
{
    [SerializeField] private GameObject spotlight;
    [SerializeField] private Image cursor;
    [SerializeField] private Image cursorAction;
    [SerializeField] private float verticalDistance;



    private BoxCollider lightZone;
    private Light lightSpot;

    private Vector3 actualPosition;

    private UnityEvent<bool> onLightOn;

    // Start is called before the first frame update
    void Start()
    {
        lightZone = spotlight.GetComponentInChildren<BoxCollider>();
        lightSpot = spotlight.GetComponent<Light>();
        actualPosition = lightZone.transform.position;
        onLightOn.AddListener(TurnTheLight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        lightSpot.intensity = 3;
        lightZone.transform.position = actualPosition + Vector3.up * verticalDistance;
    }

    private void OnMouseUp()
    {
        lightSpot.intensity = 0;
        lightZone.transform.position = actualPosition;
    }

    private void OnMouseEnter()
    {
        onLightOn?.Invoke(false);
    }
    private void OnMouseExit()
    {
        onLightOn?.Invoke(true);

    }


    private void TurnTheLight(bool isOn)
    {
        cursor.enabled = isOn;
        cursorAction.enabled = !isOn;
    }


}
