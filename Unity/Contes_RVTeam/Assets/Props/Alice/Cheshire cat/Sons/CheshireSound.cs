using System.Collections.Generic;
using UnityEngine;

public class CheshireSound : MonoBehaviour {

    [SerializeField]
    AudioClip firstAudio;

    [SerializeField]
    List<AudioClip> listAudio;

    new AudioSource audio;

    bool playedFirstAudio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        PlayerScaleManager.ScaledNormal += Talk;
    }

    public void Talk()
    {
        if (!playedFirstAudio)
            audio.clip = firstAudio;
        else
            audio.clip = listAudio[Random.Range(0, 15)];
        audio.Play();
        Table.Instance.AddPotion();
    }
}
