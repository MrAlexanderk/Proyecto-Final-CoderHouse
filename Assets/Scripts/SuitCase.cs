using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitCase : ObjectInteraction
{
    [SerializeField] float interactionTime = 1;
    private bool isOpen = false;
    private bool isActive = true;





    private void OnMouseDown()
    {
        if (CheckPlayerInteractionDistance() && isActive)
        {
            OpenAndCloseSuitCase(isOpen);
        }
    }


    private void OpenAndCloseSuitCase(bool action)
    {
        isOpen = !isOpen;
        if (action)
        {
            Debug.Log("Close");
            //transform.localRotation = Quaternion.Euler(new Vector3(100, 0, 0));
            //transform.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(new Vector3(100, 0, 0)), interactionTime);
            StartCoroutine(CloseSuitcase());
        }
        else
        {
            Debug.Log("Open");
            StartCoroutine(OpenSuitcase());
            //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }


    private IEnumerator CloseSuitcase()
    {
        int index = 0;
        isActive = false;
        while (index < 100)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(index, 0, 0));
            index++;
            yield return new WaitForSeconds(interactionTime / 60);
        }
        isActive = true;
        yield return new WaitForSeconds(interactionTime / 60);
    }
    private IEnumerator OpenSuitcase()
    {
        int index = 100;
        isActive = false;
        while (index > 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(index, 0, 0));
            index--;
            yield return new WaitForSeconds(interactionTime / 100);
        }
        isActive = true;
        yield return new WaitForSeconds(interactionTime / 100);
    }
}
