using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleSystem : ObjectInteraction
{
    [Header("Candle Settings")]
    [SerializeField] private CandleColor thisCandleColor;
    [SerializeField] private bool isOn = false;

    private enum CandleColor { White, Red, Blue, Yellow}

    private GameObject thisLight;
    private void Awake()
    {
        thisLight = GetComponentInChildren<CandleComponent>().gameObject;
        thisLight.SetActive(false);
    }




    private void OnMouseDown()
    {
        if (CheckPlayerInteractionDistance() && !isOn)
        {
            isOn = true;
            TurnOnTheCandle();
        }
    }


    private void TurnOnTheCandle()
    { //Solo ocurre una vez.
        thisLight.gameObject.SetActive(true);
        

        switch (thisCandleColor)
        {
            case CandleColor.White:
                break;
            case CandleColor.Red:
                break;
            case CandleColor.Blue:
                break;
            case CandleColor.Yellow:
                break;
            default:
                break;
        }
        //Destroy
    }

}
