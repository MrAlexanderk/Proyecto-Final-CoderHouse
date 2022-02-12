using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MannequinHead : MonoBehaviour
{
    [SerializeField] private CameraDisplayInformation initialParameters;
    [SerializeField] private int maxNoDamageTime = 5;
    [SerializeField] private LayerMask player;


    private GameObject parentObject;
    private GameObject target;
    private Transform lookTarget;
    private Text information;
    private float counter = 0;
    private PostProcessVolume volume;
    private ChromaticAberration aberration;
    private Bloom bloom;


    private GameObject eyes;

    private float damageCounter = 0;

    void Start()
    {
        target = FindObjectOfType<PlayerManager>().gameObject;
        information = GetComponentInParent<BoxCollider>().gameObject.GetComponentInChildren<Text>();

        eyes = GetComponentInChildren<MeshRenderer>().gameObject;

        volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out aberration);

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
        if(damageCounter < maxNoDamageTime)
        {
            damageCounter += 1 * Time.deltaTime;
            return;
        }

        ActivityMeter.TheActivityMeter.ChangeActivityMeter(0.1f);
        Gramophone.CountDownMultiplier += 1.2f * Time.deltaTime;
        aberrationAndBloom();
    }

    public void aberrationAndBloom()
    {
        if(bloom.intensity.value < 40) bloom.intensity.value += 2f * Time.deltaTime;
        if(aberration.intensity.value < 1) aberration.intensity.value += Time.deltaTime;
    }


    private void OnMouseExit()
    {
        Gramophone.CountDownMultiplier = 1;
        damageCounter = 0;
        StartCoroutine(CoolDown());
    }

    private void OnMouseEnter()
    {
        StopCoroutine(CoolDown());
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

}
