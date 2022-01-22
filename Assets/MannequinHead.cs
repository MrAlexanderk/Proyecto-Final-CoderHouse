using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinHead : MonoBehaviour
{

    private GameObject target;
    private Transform lookTarget;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //lookTarget.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }
}
