using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameZoneManager : MonoBehaviour
{
    [Header("GameZone Manager Parameters")]
    [SerializeField] InitialParameters initialParameters;
    [Space(16)]
    [Header("Stalker Manager Parameters")]
    [SerializeField] float spawnFirstStalkerTime;
    [SerializeField] private int attackStalkerCount = 5;
    [SerializeField] private GameObject currentStalkerParent;
    [SerializeField] private GameObject rightMarkArea;
    [SerializeField] private GameObject leftMarkArea;
    [SerializeField] private GameObject currentFollowerParent;
    [SerializeField] private GameObject firstMarkArea;
    [SerializeField] private GameObject secondMarkArea;


    public UnityEvent onFollowerSpawn;

    #region Stalker Private parameters
    private float spawnTimerCounter = 0;
    private List<StalkerEnemy> currentStalkerList;
    private DestinationWayPoint [] possiblesDestinationWayPoints;
    private bool isOnAttack;
    public int stalkerOnTheDoorQuantity;
    [HideInInspector]public GameManager gameManager;

    #endregion

    private void Awake()
    {
        currentStalkerList = new List<StalkerEnemy>();
        possiblesDestinationWayPoints = GetComponentsInChildren<DestinationWayPoint>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {

    }

    void Update()
    {
        spawnTimerCounter += Time.deltaTime;
        
        if (spawnTimerCounter >= spawnFirstStalkerTime && !isOnAttack) { SpawnStalkerSystem(); }
    }

    private void SpawnStalkerSystem()
    {
        spawnTimerCounter = 0;
        spawnFirstStalkerTime = Random.Range(initialParameters.timeBetweenPossiblesAttacks.x, initialParameters.timeBetweenPossiblesAttacks.y);
        if (Random.Range(0, 100) <= initialParameters.stalkerSpawnProbability && currentStalkerList.Count <= initialParameters.maxStalkerCount) 
        {
            float randomX = Random.Range(leftMarkArea.transform.position.x, rightMarkArea.transform.position.x);
            float randomY = this.transform.position.y;
            float randomZ = Random.Range(leftMarkArea.transform.position.z, rightMarkArea.transform.position.z);
            Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

            StalkerEnemy enemy = Instantiate<StalkerEnemy>(initialParameters.CompleteStalkerEnemyOptionList[Random.Range(0, initialParameters.CompleteStalkerEnemyOptionList.Length)]);
            enemy.transform.position = spawnPosition;
            enemy.transform.SetParent(currentStalkerParent.transform);
            int wayPointIndex = Random.Range(0, possiblesDestinationWayPoints.Length);
            //enemy.SetParentID(this);
            //enemy.SetStalkerDestination(possiblesDestinationWayPoints[wayPointIndex]);
            currentStalkerList.Add(enemy);
        }
    }

    public void ResetInGameZone()
    {
        spawnTimerCounter = 0;
        spawnFirstStalkerTime = Random.Range(0, initialParameters.timeBetweenPossiblesAttacks.y / 2);
        isOnAttack = false;





        //foreach (StalkerEnemy enemy in FindObjectsOfType<StalkerEnemy>())
        //{
        //    GameObject.Destroy(enemy.gameObject);

        //}

        //foreach (FollowerEnemy enemy in FindObjectsOfType<FollowerEnemy>())
        //{
        //    GameObject.Destroy(enemy.gameObject);
        //}

        //foreach(ElectricalPanel panel in electricalPanels)
        //{
        //    panel.BreakerOnAndOff(true);
        //}

        //foreach(ArrowScript arrow in FindObjectsOfType<ArrowScript>())
        //{
        //    if(arrow != null) { Destroy(arrow.gameObject); }
        //}

        currentStalkerList.Clear();

        //gameManager.UIManager("Lights on. They are gone");
        stalkerOnTheDoorQuantity = 0;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            stalkerOnTheDoorQuantity++;
            Debug.Log(stalkerOnTheDoorQuantity);
            if(stalkerOnTheDoorQuantity >= attackStalkerCount && !isOnAttack) { onFollowerSpawn?.Invoke(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
