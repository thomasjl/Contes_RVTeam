using System.Collections.Generic;
using UnityEngine;

public class CheshireSound : MonoBehaviour {

    [SerializeField]
    AudioClip firstAudio;

    [SerializeField]
    List<AudioClip> listAudio;
    int lastChoice = 0;

    new AudioSource audio;

    bool playedFirstAudio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        PlayerScaleManager.ScaledNormal += Talk;
    }

    public void Talk()
    {
        if (audio.isPlaying)
            return;
        lastChoice = Utilities.ExclusiveRange(0, listAudio.Count - 1, lastChoice);
        if (!playedFirstAudio)
        {
            audio.clip = firstAudio;
            playedFirstAudio = true;
        }
        else
            audio.clip = listAudio[lastChoice];
        audio.Play();
        Table.Instance.AddPotion();
    }
}
