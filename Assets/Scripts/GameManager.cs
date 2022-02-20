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
    [SerializeField] private Text informationText;
    [SerializeField] private Text centralTextInformation;

    private MannequinHead[] allTheExistingHeads;
    private int headsFoundIndex = 0;

    public static GameManager TheGameManager;
    public static event UnityAction OnGameOver;

    private void Awake()
    {
        TheGameManager = this;
        allTheExistingHeads = FindObjectsOfType<MannequinHead>();

        foreach (var head in allTheCurrentHeads)
        {
            head.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            NewHeadOut();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameWinningStage();
        }
    }



    public void NewHeadOut()
    {
        informationText.text = $"One head down. Press Left Click to pick it up.";
        StartCoroutine(ClearTheText(informationText, 3));
        if(allTheExistingHeads[headsFoundIndex].isOnTheGround && headsFoundIndex != 3) allTheExistingHeads[headsFoundIndex + 1].OnHeadDown();
        allTheExistingHeads[headsFoundIndex].OnHeadDown();
        
    }

    private IEnumerator ClearTheText(Text currentText, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        currentText.text = "";
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


    private void CheckHeadsCounts(int headsCount)
    {
        if (headsCount >= 4) GameWinningStage();
    }

    public void GameWinningStage()
    {
        //El jugador ha ganado.
        Debug.Log("GANADOR");
        PlayerManager.ThePlayer.isActive = false;

        foreach(Light lights in FindObjectsOfType<Light>())
        {
            lights.enabled = false;
        }

        
        StartCoroutine(FinalTextAppear());
    }

    private IEnumerator FinalTextAppear()
    {
        string finalText = "Sometimes, you just die. \n Even when you win." ;

        for(int i = 0; i < finalText.Length; i++)
        {
            centralTextInformation.text += finalText[i];
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(3);
        centralTextInformation.enabled = false;
        GameOver();
    }

}
