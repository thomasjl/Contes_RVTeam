using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheshireSound : MonoBehaviour {

    public AudioClip firstAudio;

    public List<AudioClip> listAudio;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Invoke("LaunchFirstAudio", 45);
    }

    public void LaunchFirstAudio()
    {
        audioSource.clip = firstAudio;
        audioSource.volume = 0.25f;
        audioSource.Play();
        

        StartCoroutine(LaunchRandomAudio());
    }

    IEnumerator LaunchRandomAudio()
    {
        yield return new WaitForSeconds(Random.Range(15, 20));
        audioSource.volume = 1f;

        while (true)
        {
            audioSource.clip=listAudio[Random.Range(0,15)];
            audioSource.Play();
            yield return new WaitForSeconds(Random.Range(7,10));
        }
    }
}
