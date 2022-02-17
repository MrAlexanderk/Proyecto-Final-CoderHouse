using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Configuration")]
    [SerializeField] private GameSettings configuration;

    
    [SerializeField] private Transform leftSpawnAreaCorner;
    [SerializeField] private Transform rightSpawnAreaCorner;


    [SerializeField] public GameObject[] completePhasePoints;
    [SerializeField] private GameObject parentReference;
    [SerializeField] private Light lightSystem;



    [SerializeField] private GameObject[] allTheCurrentHeads;

    [SerializeField] private Image[] allHeadsCollected;







    private float timerCounter;
    private float attackTimeCount;

    private MannequinHead[] allTheExistingHeads;

    private int headsFoundIndex = 0;


    public static GameManager TheGameManager;

    public static event UnityAction OnGameOver;
    public static event UnityAction OnGameReset;

    private void Awake()
    {
        TheGameManager = this;
        OnGameReset += OnGameResetHandler;
        allTheExistingHeads = FindObjectsOfType<MannequinHead>();

        attackTimeCount = Random.Range(configuration.AttackTimeCountRange.x, configuration.AttackTimeCountRange.y);
        foreach (var head in allTheCurrentHeads)
        {
            head.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            allTheExistingHeads[headsFoundIndex].OnHeadDown();
        }
    }



    public void NewHeadOut()
    {
        allTheExistingHeads[headsFoundIndex].OnHeadDown();
        //Una cabeza ha ca�do. Ac�rcate a ella y pulsa {KeyCode.Mouse0} para asegurarla.
    }


    public void TakeAHead()
    {
        allTheExistingHeads[headsFoundIndex].GetComponent<MeshRenderer>().enabled = false;
        allTheExistingHeads[headsFoundIndex].GetComponent<CapsuleCollider>().enabled = false;
        
        foreach (var heads in allTheExistingHeads[headsFoundIndex].GetComponentsInChildren<MeshRenderer>())
        {
            heads.enabled = false;
        }

        allTheCurrentHeads[headsFoundIndex].GetComponent<MeshRenderer>().enabled = true;
        allHeadsCollected[headsFoundIndex].enabled = true;

        headsFoundIndex++;
        CheckHeadsCounts(headsFoundIndex);
    }


    private void StartAttackSystem()
    {   
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

        StartCoroutine(DeathTimerMenu());
    }


    private IEnumerator DeathTimerMenu()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }



    public void GameReset()
    {
        OnGameReset?.Invoke();
    }

    private void CheckHeadsCounts(int headsCount)
    {

        if (headsCount >= 4) GameWinningStage();
    }

    public void GameWinningStage()
    {
        //El jugador ha ganado.
        Debug.Log("GANADOR");
    }


    private void OnGameResetHandler()
    {
        foreach(var head in allTheCurrentHeads)
        {
            head.GetComponent<MeshRenderer>().enabled = false;
        }
    }
   

}
