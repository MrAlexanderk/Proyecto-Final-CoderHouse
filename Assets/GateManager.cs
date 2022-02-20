using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    OpenAndCloseTheGate();
        //}
    }

    public void OpenAndCloseTheGate()
    {
        isOpen = !isOpen;
        StartCoroutine(Gate(isOpen));
    }


    private IEnumerator Gate(bool open)
    {
        if (open)
        {
            while (transform.position.y < 1.75f)
            {
                transform.position += Mathf.Lerp(0, 0.05f, 1) * Vector3.up;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (transform.position.y > -0.5f)
            {
                transform.position -= Mathf.Lerp(0, 0.05f, 1) * Vector3.up;
                yield return new WaitForEndOfFrame();
            }
        }

    }



    // Open = -0.5;
    // Close = 1.75;

}
