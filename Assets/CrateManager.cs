using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour
{
    [SerializeField] private float interactionTime = 1;

    private Animator crateAnimator;
    private AudioSource crateAudioSource;
    private enum ShakeIntensity { Low, Medium, Hard, Death }
    private bool isOpen = false;
    private bool isActive = false;
    private GameObject crateDoor;

    private void Awake()
    {




        crateAnimator = GetComponent<Animator>();
        crateAudioSource = GetComponent<AudioSource>();
        //crateDoor = GetComponentInParent<BoxCollider>().gameObject.GetComponentInChildren<SphereCollider>().gameObject;
        crateDoor = GetComponentInChildren<SphereCollider>().gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeTheCrate(ShakeIntensity.Low);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StopTheCrate();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            OpenAndCloseCrate(!isOpen);
        } 
        
    }


    private void ShakeTheCrate(ShakeIntensity shake)
    {
        crateAnimator.SetBool("isShaking", true);
        crateAudioSource.Play();

        switch (shake)
        {
            case ShakeIntensity.Low:
                Debug.Log("Move");
                crateAnimator.SetTrigger("Move");
                break;
            case ShakeIntensity.Medium:
                break;
            case ShakeIntensity.Hard:
                break;
            case ShakeIntensity.Death:
                break;
            default:
                break;
        }
        //The Crate is moving
    }


    private void StopTheCrate()
    {
        crateAnimator.SetBool("isShaking", false);
        crateAudioSource.Stop();
    }


    public static void KillThePlayer()
    {
        foreach (var light in FindObjectsOfType<Light>())
        {
            light.enabled = false;
        }
    }

    private void OpenAndCloseCrate(bool action)
    {
        isOpen = !isOpen;
        if (action)
        {
            StartCoroutine(CloseCrate());
        }
        else
        {
            StartCoroutine(OpenCrate());
        }

    }

    private IEnumerator CloseCrate()
    {
        int index = -10;
        isActive = false;
        while (index <= 0)
        {
            crateDoor.transform.localRotation = Quaternion.Euler(new Vector3(index, 0, 0));
            index++;
            yield return new WaitForSeconds(interactionTime / 60);
        }
        isActive = true;
        yield return new WaitForSeconds(interactionTime / 60);
    }
    private IEnumerator OpenCrate()
    {
        int index = 0;
        isActive = false;
        while (index >= -10)
        {
            crateDoor.transform.localRotation = Quaternion.Euler(new Vector3(index, 0, 0));
            index--;
            yield return new WaitForSeconds(interactionTime / 100);
        }
        isActive = true;
        yield return new WaitForSeconds(interactionTime / 100);
    }


    // 0 Cerrado.
    // -10 semi abierto.
}
