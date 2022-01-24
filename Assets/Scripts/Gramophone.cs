using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gramophone : ObjectInteraction
{
    [SerializeField] private float maxHandCrank = 180f;
    [SerializeField] private float distortionPercentage = 0.4f;
    [SerializeField] private float chargingMultiplier = 10f;



    [SerializeField] private bool isCountingDown = true;
    private bool isPlaying = true;


    private float crankCountDown;

    private AudioSource audioSource;


    #region EDITOR SERIALIZED

    [SerializeField] Text info;
    [SerializeField] Image gramophoneImage;
    bool isActive = false;

    #endregion


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        crankCountDown = maxHandCrank;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCountingDown && isPlaying) { CrankCountDown(); }



        info.text = ((int) crankCountDown).ToString(); //JUST EDITOR
    }

    private void CrankCountDown()
    {
        if(crankCountDown <= 0) 
        {
            //El player muere.
            GameManager.KillThePlayer();
            return; 
        }
        crankCountDown -= Time.deltaTime;
        if(crankCountDown < maxHandCrank * distortionPercentage)
        {
            audioSource.pitch = crankCountDown / (maxHandCrank * distortionPercentage);
        } else 
        { 
            audioSource.pitch = 1; 
        }
    }

    private void MovingHandCrank()
    {
        if (!isActive) 
        { 
            gramophoneImage.enabled = true;
            isActive = true;
        }
        if(crankCountDown >= maxHandCrank) { return; }
        crankCountDown += chargingMultiplier * Time.deltaTime;
        gramophoneImage.fillAmount = crankCountDown / maxHandCrank;
    }



    private void OnMouseDrag()
    {
        if (CheckPlayerInteractionDistance())
        {
            MovingHandCrank();
        }
    }

    private void OnMouseUp()
    {
        if (isActive)
        {
            isActive = false;
            gramophoneImage.enabled = false;
        }
    }
}
