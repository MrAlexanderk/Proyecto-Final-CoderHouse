using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzalezManager : BasicAI
{
    [SerializeField] private float enemySpeed = 3.5f;
    [SerializeField, Range(0, 100)] private int enemyAttackProbability = 100;  //Probabilidad de que el enemigo se mueva o deje de moverse al ocurrir un timer.
    [SerializeField] private float attackTimeMoment = 10;
    [SerializeField] private Vector2 timeRange = new Vector2(10, 20);          //Valor entre los cuales puede ocurrir el evento de mover/no mover
    [SerializeField] private Vector2 waitTimeRange = new Vector2(10, 20);      //Valor entre los cuales Gonzalez esperar? antes de abrir la puerta y continuar.
    [SerializeField] private GateManager[] completeGateCollection;
    [SerializeField] private float maxWaitingTime = 5;

    private bool isCounting = false;

    private float counter = 0;
    private float attackCounter;
    private int destinationIndex = 1;

    private Transform target;
    float escapeCounter = 0;


    private void Awake()
    {
        StartParameters();
    }

    void Start()
    {
        target = GameManager.TheGameManager.completePhasePoints[1].transform;
        SetTheMovement(false);
    }

    void Update()
    {
        if(isCounting) GonzalezOnTheMove();
        if (!navMeshAgent.hasPath) StartCoroutine(GonzalezOnTheSpot());
    }

    private void SetDestinationPoints()
    {
        if (destinationIndex == GameManager.TheGameManager.completePhasePoints.Length) 
        { 
            Debug.Log("The Game Is Over");
            GameManager.TheGameManager.GameOver();
            return; 
        }
        target = GameManager.TheGameManager.completePhasePoints[destinationIndex].transform;
        navMeshAgent.SetDestination(target.position);
        destinationIndex++;
    }

    private void GonzalezOnTheMove()
    {
        targetDistance = (target.position - this.transform.position).magnitude;
        attackCounter += Time.deltaTime;

        if (attackCounter >= attackTimeMoment)
        {
            int randomIndex;
            if (isMoving) { randomIndex = Random.Range(0, 10); } else { randomIndex = Random.Range(0, 100); }

            attackCounter = 0;
            attackTimeMoment = Random.Range(timeRange.x, timeRange.y);

            if (randomIndex <= enemyAttackProbability) { SetTheMovement(!isMoving); }
        }
    }

    private IEnumerator GonzalezOnTheSpot()
    {   
        isCounting = false;

        if((destinationIndex == 3 && completeGateCollection[0].isOpen) || (destinationIndex == 5 && completeGateCollection[1].isOpen) || (destinationIndex == 7 && completeGateCollection[2].isOpen))
        {
            
            escapeCounter += Time.deltaTime;

            if(escapeCounter >= maxWaitingTime) 
            {
                destinationIndex = 0;
                isCounting = false;
                SetDestinationPoints();
                animatorController.SetBool("Walking", true);
                navMeshAgent.speed = enemySpeed;

                GameManager.TheGameManager.NewHeadOut();
                isCounting = true;


            }
            
            yield break;
        }

        if(destinationIndex == 0) 
        {
            isCounting = true;
            yield return new WaitForSeconds(20);
        }

        escapeCounter = 0;


        SetDestinationPoints();
        SetTheMovement(false);
        int waitTimeIndex = (int) Random.Range(waitTimeRange.x, waitTimeRange.y);
        yield return new WaitForSeconds(waitTimeIndex);
        isCounting = true;
    }

    private void SetTheMovement(bool setAction)
    {
        attackCounter = 0;
        isMoving = setAction;
        animatorController.SetBool("Walking", setAction);
        switch (setAction)
        {
            case true:
                navMeshAgent.speed = enemySpeed;
                break;
            case false:
                navMeshAgent.speed = 0;
                break;
        }
    }
}
