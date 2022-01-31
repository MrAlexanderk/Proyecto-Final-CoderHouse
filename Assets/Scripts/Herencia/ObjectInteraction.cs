using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectInteraction : MonoBehaviour
{
    [Header("Player Interaction Settings")]
    [SerializeField] protected float distanceInteraction = 1.5f;
    [SerializeField] protected Image action;

    protected bool CheckPlayerInteractionDistance()
    {
        RaycastHit hitObject;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitObject, distanceInteraction))
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * distanceInteraction, Color.yellow);
            return true;
        } else
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * distanceInteraction, Color.yellow);
            return false;
        }
    }


    private void OnMouseEnter()
    {
        //action.enabled = true;
    }

    private void OnMouseOver()
    {
        if (CheckPlayerInteractionDistance() && action.enabled == false)
        {
            action.enabled = true;
        } else if(!CheckPlayerInteractionDistance() && action.enabled == true)
        {
            action.enabled = false;
        }
    }

    private void OnMouseExit()
    {
        action.enabled = false;
    }



}
