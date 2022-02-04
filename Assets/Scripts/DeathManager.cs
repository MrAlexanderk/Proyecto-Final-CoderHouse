using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private AudioClip footSteps;
    [SerializeField] private AudioClip openCrate;
    [SerializeField] private float speed = 0.1f;
    private AudioSource audioSource;
    private PlayerManager target;
    private Animator jillAnim;

    public static DeathManager TheDeathManager;

    private void Awake()
    {
        DeathManager.TheDeathManager = this;
        GameManager.OnGameOver += PlayerDeath;
        jillAnim = GetComponent<Animator>();
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
        jillAnim.SetTrigger("Death");
        while (Vector3.Distance(target.transform.position, transform.position) > 0.1f)
        {
            transform.LookAt(target.transform.position + Vector3.down);
            transform.position += transform.forward * Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
    }

}
