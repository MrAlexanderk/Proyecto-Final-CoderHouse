using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Configuration")]
    [SerializeField] private GameSettings configuration;

    
    [SerializeField] private Transform leftSpawnAreaCorner;
    [SerializeField] private Transform rightSpawnAreaCorner;


    [SerializeField] public GameObject[] completePhasePoints;
    [SerializeField] private GameObject parentReference;
    [SerializeField] private Light lightSystem;


    private enum AllGameStages { Hiding, WaitingToMove, AlreadyMoving}

    private AllGameStages currentGameStage = AllGameStages.Hiding;
    private float timerCounter;
    private float attackTimeCount;

    private bool isOnTheScene = false;

    public static GameManager TheGameManager;

    public static event UnityAction OnGameOver;
    public static event UnityAction OnGameReset;

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
    {   




    }



    private void StartAttackSystem()
    {   //Este m�todo de encarga de instanciar al enemigo cuando se est� realizando un ataque.
        isOnTheScene = true;
        float randomX = Random.Range(leftSpawnAreaCorner.transform.position.x, rightSpawnAreaCorner.transform.position.x);
        float randomY = this.transform.position.y;
        float randomZ = Random.Range(leftSpawnAreaCorner.transform.position.z, rightSpawnAreaCorner.transform.position.z);
        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        GonzalezManager gonzalez = Instantiate<GonzalezManager>(configuration.GonzalezPrefab);
        gonzalez.transform.position = spawnPosition;
        gonzalez.transform.SetParent(parentReference.transform);

    }


    public void GameOver()
    {
        OnGameOver?.Invoke();
        foreach(var light in FindObjectsOfType<Light>())
        {
            light.enabled = false;
        }

        lightSystem.GetComponent<LightManager>().enabled = true;


    }

    public void GameReset()
    {
        OnGameReset?.Invoke();
    }


   

}
