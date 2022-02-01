using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private AudioClip footSteps;
    [SerializeField] private AudioClip openCrate;

    private AudioSource audioSource;
    private PlayerManager target;


    private void Awake()
    {
        GameManager.OnGameOver += PlayerDeath;
        audioSource = GetComponent<AudioSource>();
        target = FindObjectOfType<PlayerManager>();
    }





    private void PlayerDeath()
    {
        audioSource.PlayOneShot(openCrate);
        StartCoroutine(Death());

    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        audioSource.Play();
        while (Vector3.Distance(target.transform.position, transform.position) > 0.1f)
        {
            transform.LookAt(target.transform.position);
            transform.position += transform.forward * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}
