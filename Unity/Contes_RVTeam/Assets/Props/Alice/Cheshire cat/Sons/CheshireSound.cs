using System.Collections.Generic;
using UnityEngine;

public class CheshireSound : MonoBehaviour {

    [SerializeField]
    AudioClip firstAudio;

    [SerializeField]
    List<AudioClip> listAudio;
    int lastChoice = 0;

    AudioSource Audio { get { return GetComponent<AudioSource>(); } }

    bool playedFirstAudio;

    private void Awake()
    {
        PlayerScaleManager.ScaledNormal += Talk;
    }

    public void Talk()
    {
        if (Audio.isPlaying)
            return;
        lastChoice = Utilities.ExclusiveRange(0, listAudio.Count - 1, lastChoice);
        if (!playedFirstAudio)
        {
            Audio.clip = firstAudio;
            playedFirstAudio = true;
        }
        else
            Audio.clip = listAudio[lastChoice];
        Audio.Play();
        Table.Instance.AddPotion();
    }
}
