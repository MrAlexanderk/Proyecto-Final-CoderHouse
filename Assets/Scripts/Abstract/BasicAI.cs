using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BasicAI : MonoBehaviour
{
    [SerializeField] protected InitialParameters initialParameters;

    protected PlayerManager playerTarget;
    protected Animator animatorController;
    protected AudioSource audioSource;
    protected NavMeshAgent navMeshAgent;

    protected float targetDistance;
    protected bool isMoving;


    protected void StartParameters()
    {
        playerTarget = FindObjectOfType<PlayerManager>();  //Se localiza el player en la Escena.
        animatorController = GetComponentInChildren<Animator>();  //Se localiza el Animator del enemigo.
        audioSource = GetComponent<AudioSource>(); //Se localiza el AudioSource.
        navMeshAgent = GetComponent<NavMeshAgent>(); //Se localiza el navMeshAgent.
    }
    protected void OnGameResetHandler()
    {
        Debug.Log($"{this.name} was destroyed");
        GameObject.Destroy(this.gameObject);
    }

}
