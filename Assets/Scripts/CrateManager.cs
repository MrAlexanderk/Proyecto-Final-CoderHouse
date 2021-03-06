using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour
{
    [SerializeField] private float interactionTime = 1;
    [SerializeField] private Vector2 activityTimeRange = new Vector2(15, 30);
    [SerializeField] private GameSettings configuration;

    private Animator crateAnimator;
    private AudioSource crateAudioSource;
    private enum ShakeIntensity { Low, Medium, Hard, Death }
    private bool isOpen = false;
    private bool isActive = false;
    private GameObject crateDoor;
    private AudioSource audioSource;

    //Contador del crate
    private float activityCounter = 0;
    private bool isActivityActive = false;
    private float activityTime = 0;
    public static float DangerProbability = 0.1f;
    public static CrateManager theCrateManager;

    private void Awake()
    {

        crateAnimator = GetComponent<Animator>();
        crateAudioSource = GetComponent<AudioSource>();
        crateDoor = GetComponentInChildren<SphereCollider>().gameObject;
        audioSource = GetComponent<AudioSource>();
        activityTime = Random.Range(activityTimeRange.x, activityTimeRange.y);
        GameManager.OnGameOver += StartOpenComplete;
        theCrateManager = this;

    }

    private void StartOpenComplete()
    {
        StartCoroutine(OpenComplete());
    }

    // Update is called once per frame
    void Update()
    {
        GameController();
        
    }

    private void GameController()
    {
        if(!isActivityActive) activityCounter += Time.deltaTime;
        if(activityCounter >= activityTime && !isActivityActive)
        {
            isActivityActive = true;
            activityCounter = 0;
        }
    }

    public void ShakeTheCrate()
    {
        crateAnimator.SetBool("isShaking", true);
        crateAudioSource.Play();
        crateAnimator.SetTrigger("Move");
    }


    public void StopTheCrate()
    {
        crateAnimator.SetBool("isShaking", false);
        crateAudioSource.Stop();
    }

    public void OpenAndCloseCrate(bool action)
    {
        isOpen = !isOpen;
        if (action)
        {
            audioSource.volume = 0.1f;
            StartCoroutine(CloseCrate());
        }
        else
        {
            audioSource.volume = 0.3f;
            StartCoroutine(OpenCrate());
        }

    }

    private IEnumerator CloseCrate()
    {
        float index = -10;
        isActive = false;
        while (index <= 0)
        {
            crateDoor.transform.localRotation = Quaternion.Euler(new Vector3(index, 0, 0));
            index += 0.5f;
            yield return new WaitForSeconds(interactionTime / 60);
        }
        isActive = true;
        yield return new WaitForSeconds(interactionTime / 60);
    }
    private IEnumerator OpenCrate()
    {
        float index = 0;
        isActive = false;
        while (index >= -10)
        {
            crateDoor.transform.localRotation = Quaternion.Euler(new Vector3(index, 0, 0));
            index -= 0.5f;
            yield return new WaitForSeconds(interactionTime / 100);
        }
        GameManager.TheGameManager.NewHeadOut();
        isActive = true;
        yield return new WaitForSeconds(interactionTime / 100);
    }

    private IEnumerator OpenComplete()
    {
        float index = 0;
        isActive = false;
        while (index >= -90)
        {
            crateDoor.transform.localRotation = Quaternion.Euler(new Vector3(index, 0, 0));
            index -= 0.5f;
            yield return new WaitForSeconds(interactionTime / 150);
        }
        isActive = true;
        yield return new WaitForSeconds(interactionTime / 150);


    }


    // 0 Cerrado.
    // -10 semi abierto.
}
