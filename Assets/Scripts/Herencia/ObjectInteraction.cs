using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectInteraction : MonoBehaviour
{
    [Header("Player Interaction Settings")]
    [SerializeField] protected float distanceInteraction = 1.5f;


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



}
