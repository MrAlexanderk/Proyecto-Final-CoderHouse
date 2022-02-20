using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleSystem : ObjectInteraction
{
    [Header("Candle Settings")]
    [SerializeField] private CandleColor thisCandleColor;
    [SerializeField] private bool isOn = false;
    [SerializeField] private int usingTime = 10;
    [SerializeField] private int useIntensity = 10;

    private enum CandleColor { White, Red, Blue, Yellow}

    private GameObject thisLight;
    private Light thisCandleLight;

    private static bool isACandleOn = false;
    private static int candlesUsed = 0;
    private void Awake()
    {
        thisLight = GetComponentInChildren<CandleComponent>().gameObject;
        thisLight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (CheckPlayerInteractionDistance() && !isOn && !isACandleOn)
        {
            isOn = true;
            isACandleOn = true;
            StartCoroutine(TurnOnTheCandles());
        }
    }


    private IEnumerator TurnOnTheCandles()
    {
        Debug.Log("Usando la vela " + this.name);
        thisLight.SetActive(true);
        yield return new WaitForSeconds(10);
        ActivityMeter.TheActivityMeter.ChangeActivityMeter(-useIntensity);
        isACandleOn = false;
        candlesUsed++;
        if(candlesUsed == 2)
        {
            GameManager.TheGameManager.NewHeadOut();
        }

        GetComponentInChildren<SphereCollider>().gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        Destroy(thisLight.gameObject);
        Destroy(GetComponent<MeshRenderer>());
    }


}
