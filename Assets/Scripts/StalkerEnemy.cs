using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StalkerEnemy : BasicAI
{
    [Header("Stalker Settings")]
    [SerializeField] private float enemyDamageResistance;
    [SerializeField] private float enemySpeed = 3.5f;
    [SerializeField, Range(0, 100)] private int enemyAttackProbability = 100; //Probabilidad de que el enemigo se mueva o deje de moverse al ocurrir un timer.
    [SerializeField] private float attackTimeMoment = 10;
    [SerializeField] private Vector2 timeRange;   //Valor entre los cuales puede ocurrir el evento de mover/no mover
    [Space(8)]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float groundDistance;
    [SerializeField] AudioClip cryAudio;

    private float counter = 0;
    private float attackCounter;

    private Transform target;
    private bool isOnLight;
    private bool isOnTheDoor;

    private enum EnemyState { MovingTowards, OnTheDoor, OnLight}
    private EnemyState currentEnemyState;
    private GameZoneManager parentID;

    private void Awake() { StartParameters(); }  //Se inicializan todos los parámetros comunes desde el BasicAI.


    void Start() //Se inicializan los parámetros particulares de cada tipo de enemigo.
    {
        navMeshAgent.speed = 0;
        navMeshAgent.SetDestination(target.position);
        currentEnemyState = EnemyState.MovingTowards;

        //GameManager.onGameReset += OnGameResetHandler;
    }

    void Update()
    {
        switch (currentEnemyState)
        {
            case EnemyState.MovingTowards:
                StalkerOnTheMove();
                break;
            case EnemyState.OnTheDoor:
                break;
            case EnemyState.OnLight:
                counter += Time.deltaTime;
                if (counter > enemyDamageResistance && counter < 10000) 
                {
                    parentID.stalkerOnTheDoorQuantity--;
                    Destroy(this.gameObject); 
                }
                break;
        }


        //if (isOnLight)
        //{
        //    counter += Time.deltaTime;

        //    if (counter > enemyDamageResistance && counter < 10000)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
        //else
        //{
        //    attackCounter += Time.deltaTime;
        //}
    }



    private void StalkerOnTheMove()
    {
        targetDistance = (target.position - this.transform.position).magnitude;
        attackCounter += Time.deltaTime;

        if (attackCounter >= attackTimeMoment)
        {
            int randomIndex;
            if (isMoving) { randomIndex = Random.Range(0, 10); } else { randomIndex = Random.Range(0, 101); }

            attackCounter = 0;
            attackTimeMoment = Random.Range(timeRange.x, timeRange.y);

            if (randomIndex <= enemyAttackProbability)
            {
                isMoving = !isMoving;
                animatorController.SetBool("Walking", !animatorController.GetBool("Walking"));
                if (isMoving) { navMeshAgent.speed = enemySpeed; } else { navMeshAgent.speed = 0; }
            }


        }
    }








    //private void PlayerOnTheDoor()
    //{
    //    isArrived = true;
    //    enemyAnimator.SetTrigger("getOnTheDoor");
    //}



    #region DETECTION ZONE
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightZone")) { EnemyOnTheLight(0, true); currentEnemyState = EnemyState.OnLight; }
        else if (other.CompareTag("Gate")) 
        { 
            isOnTheDoor = true;
            currentEnemyState = EnemyState.OnTheDoor;
            animatorController.SetBool("getOnTheDoor", true); 
            audioSource.PlayOneShot(cryAudio); 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightZone")) 
        { 
            EnemyOnTheLight(enemySpeed, false);
            if (!isOnTheDoor) { currentEnemyState = EnemyState.MovingTowards; } else { currentEnemyState = EnemyState.OnTheDoor; }
        }
        else if (other.CompareTag("Gate")) { isOnTheDoor = false; currentEnemyState = EnemyState.OnTheDoor; }
    }
    #endregion

    private void EnemyOnTheLight(float changeSpeed, bool lightStatus)
    {
        navMeshAgent.speed = changeSpeed;
        isOnLight = lightStatus;
        animatorController.SetBool("isOnTheLight", lightStatus);
        if (lightStatus) { counter = 0; } else { counter = 10000; }
    }

    public void SetStalkerDestination(GameObject destination)
    { target = destination.transform; }
}
