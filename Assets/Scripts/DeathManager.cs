using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private AudioClip footSteps;
    [SerializeField] private AudioClip openCrate;
    [SerializeField] private AudioClip jumpScare;
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
        MannequinHead bloom = FindObjectOfType<MannequinHead>();
        audioSource.PlayOneShot(openCrate);
        jillAnim.SetTrigger("Death");
        yield return new WaitForSeconds(2);
        while (Vector3.Distance(target.transform.position, transform.position) > 1.5f)
        {
            transform.LookAt(target.transform.position + Vector3.down);
            transform.position += transform.forward * Time.deltaTime * speed;

            bloom.aberrationAndBloom();

            yield return new WaitForEndOfFrame();
        }
        jillAnim.SetTrigger("Stop");
        yield return new WaitForSeconds(Random.Range(1, 4));
        audioSource.volume = 1;
        audioSource.PlayOneShot(jumpScare);
    }

}
