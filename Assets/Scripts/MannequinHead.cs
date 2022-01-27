using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MannequinHead : MonoBehaviour
{
    [SerializeField] private CameraDisplayInformation initialParameters;
    private GameObject parentObject;
    private GameObject target;
    private Transform lookTarget;
    private Text information;


    void Start()
    {
        target = FindObjectOfType<PlayerManager>().gameObject;

        information = GetComponentInParent<BoxCollider>().gameObject.GetComponentInChildren<Text>();

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


}
