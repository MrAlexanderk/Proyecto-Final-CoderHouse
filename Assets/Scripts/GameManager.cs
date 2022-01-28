using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Configuration")]
    [SerializeField] private GameSettings configuration;

    
    [SerializeField] private Transform leftSpawnAreaCorner;
    [SerializeField] private Transform rightSpawnAreaCorner;


    [SerializeField] private GameObject[] completePhasePoints;
    [SerializeField] private GameObject parentReference;



    private enum AllGameStages { Hiding, WaitingToMove, AlreadyMoving}

    private AllGameStages currentGameStage = AllGameStages.Hiding;
    private float timerCounter;
    private float attackTimeCount;

    private bool isOnTheScene = false;

    public static GameManager TheGameManager;



    private void Awake()
    {
        TheGameManager = this;
        attackTimeCount = Random.Range(configuration.AttackTimeCountRange.x, configuration.AttackTimeCountRange.y);
    }






    private void Update()
    {
        //TimerCounterDown();
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartAttackSystem();
        }
    }



    private void TimerCounterDown()
    {   //Esta función se ocupa exclusivamente de contar el tiempo entre ataques.
        timerCounter += Time.deltaTime * configuration.StalkerAttackMultiplier;

        if(timerCounter <= attackTimeCount && currentGameStage == AllGameStages.AlreadyMoving) { return; }
        
        timerCounter = 0;

        
    }



    private void StartAttackSystem()
    {   //Este método de encarga de instanciar al enemigo cuando se está realizando un ataque.
        isOnTheScene = true;
        float randomX = Random.Range(leftSpawnAreaCorner.transform.position.x, rightSpawnAreaCorner.transform.position.x);
        float randomY = this.transform.position.y;
        float randomZ = Random.Range(leftSpawnAreaCorner.transform.position.z, rightSpawnAreaCorner.transform.position.z);
        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        StalkerEnemy gonzalez = Instantiate<StalkerEnemy>(configuration.GonzalezPrefab);
        gonzalez.transform.position = spawnPosition;
        gonzalez.transform.SetParent(parentReference.transform);
        gonzalez.SetStalkerDestination(completePhasePoints[1]);

    }






    //private Animator crateAnimator;
    //private AudioSource crateAudioSource;
    //private enum ShakeIntensity { Low, Medium, Hard, Death}



    //private void Awake()
    //{
    //    TheGameManager = this;




    //    crateAnimator = GetComponent<Animator>();
    //    crateAudioSource = GetComponent<AudioSource>();
    //}


    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ShakeTheCrate(ShakeIntensity.Low);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        StopTheCrate();
    //    }
    //}


    //private void ShakeTheCrate(ShakeIntensity shake)
    //{
    //    crateAnimator.SetBool("isShaking", true);
    //    crateAudioSource.Play();

    //    switch (shake)
    //    {
    //        case ShakeIntensity.Low:
    //            Debug.Log("Move");
    //            crateAnimator.SetTrigger("Move");
    //            break;
    //        case ShakeIntensity.Medium:
    //            break;
    //        case ShakeIntensity.Hard:
    //            break;
    //        case ShakeIntensity.Death:
    //            break;
    //        default:
    //            break;
    //    }
    //    //The Crate is moving
    //}


    //private void StopTheCrate()
    //{
    //    crateAnimator.SetBool("isShaking", false);
    //    crateAudioSource.Stop();
    //}


    //public static void KillThePlayer()
    //{
    //    foreach (var light in FindObjectsOfType<Light>())
    //    {
    //        light.enabled = false;
    //    }
    //}

}
