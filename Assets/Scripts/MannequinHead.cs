using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MannequinHead : ObjectInteraction
{
    [SerializeField] private CameraDisplayInformation initialParameters;
    [SerializeField] private int maxNoDamageTime = 5;
    [SerializeField] private LayerMask player;
    [SerializeField] private float jumpIntensity = 2.5f;
    [SerializeField] private float timeBetweenJumping = 3;
    [SerializeField] private Image actionHand;

    private GameObject parentObject;
    private GameObject target;
    private Transform lookTarget;
    private Text information;
    private float counter = 0;
    private PostProcessVolume volume;
    private ChromaticAberration aberration;
    private Bloom bloom;

    private Rigidbody headRigidbody;

    private float actionCounter = 0;

    private bool isOnTheGround = false;
    private float damageCounter = 0;

    void Start()
    {
        headRigidbody = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerManager>().gameObject;
        information = GetComponentInParent<BoxCollider>().gameObject.GetComponentInChildren<Text>();

        volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out aberration);
        ChangeChestText();
    }


    void Update()
    {    
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeChestText();
        }
       
    }

    private void ChangeChestText()
    {
        int index = Random.Range(0, initialParameters.CompleteMannequinTexts.Length);
        information.text = initialParameters.CompleteMannequinTexts[index];
    }

    private void OnMouseOver()
    {

        if (CheckPlayerInteractionDistance() && isOnTheGround && actionHand.enabled == false)
        {
            // && action.enabled == false 
            Debug.Log("Hand");
            actionHand.enabled = true;
        }
        else if (!CheckPlayerInteractionDistance() && actionHand.enabled == true)
        {
            Debug.Log("No Hand");
            actionHand.enabled = false;
        }


        if (damageCounter < maxNoDamageTime)
        {
            damageCounter += Time.deltaTime;
            return;
        }


        ActivityMeter.TheActivityMeter.ChangeActivityMeter(0.1f);
        Gramophone.CountDownMultiplier += 1.2f * Time.deltaTime;
        aberrationAndBloom();



    }

    private void OnMouseDown()
    {
        if (isOnTheGround)
        {
            Debug.Log("HeadDown");
            GameManager.TheGameManager.TakeAHead();
        }
        
    }

    public void aberrationAndBloom()
    {
        if(bloom.intensity.value < 40) bloom.intensity.value += 2f * Time.deltaTime;
        if(aberration.intensity.value < 1) aberration.intensity.value += Time.deltaTime;

        actionCounter += Time.deltaTime;
        if(actionCounter >= 1)
        {
            actionCounter = 0;
            ChangeChestText();
        }
    }


    private void OnMouseExit()
    {
        Gramophone.CountDownMultiplier = 1;
        damageCounter = 0;
        StartCoroutine(CoolDown());

        if (CheckPlayerInteractionDistance())
        {
            actionHand.enabled = true;
        }
    }

    private void OnMouseEnter()
    {
        StopCoroutine(CoolDown());
        if (CheckPlayerInteractionDistance())
        {
            actionHand.enabled = true;
        }
    }
    private IEnumerator CoolDown()
    {

        while (bloom.intensity.value > 0)
        {
            bloom.intensity.value -= 10 * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        while (aberration.intensity.value > 0)
        {
            aberration.intensity.value -= 10 * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void OnHeadDown()
    {
        headRigidbody.isKinematic = false;
        headRigidbody.AddForce(Vector3.up * jumpIntensity, ForceMode.Impulse);
        isOnTheGround = true;
        StartCoroutine(HeadJumping());
    }


    private IEnumerator HeadJumping()
    {
        while (GetComponent<MeshRenderer>().enabled)
        {
            headRigidbody.AddForce((Vector3.up * jumpIntensity) + 
                (Vector3.forward * Random.Range(-jumpIntensity, jumpIntensity)) +
                (Vector3.right * Random.Range(-jumpIntensity, jumpIntensity)), ForceMode.Impulse);

            yield return new WaitForSeconds(timeBetweenJumping);
        }
    }

}
