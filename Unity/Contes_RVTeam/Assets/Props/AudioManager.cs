using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
  
}
