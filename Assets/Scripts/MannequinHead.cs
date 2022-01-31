using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MannequinHead : MonoBehaviour
{
    [SerializeField] private CameraDisplayInformation initialParameters;
    [SerializeField] private int maxNoDamageTime = 5;
    private GameObject parentObject;
    private GameObject target;
    private Transform lookTarget;
    private Text information;
    private float counter = 0;
    private PostProcessVolume volume;
    private ChromaticAberration aberration;
    private Bloom bloom;

    private float damageCounter = 0;

    void Start()
    {
        target = FindObjectOfType<PlayerManager>().gameObject;
        information = GetComponentInParent<BoxCollider>().gameObject.GetComponentInChildren<Text>();

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
            bloom.intensity.value += 1f;
            aberration.intensity.value += 0.1f;
            Debug.Log($"{bloom.intensity.value}, {aberration.intensity.value}, {volume.name}, {Camera.main.name}");
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

        Gramophone.CountDownMultiplier += 2 * Time.deltaTime;
        bloom.intensity.value += 5f * Time.deltaTime;
        aberration.intensity.value += 1f * Time.deltaTime;
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
