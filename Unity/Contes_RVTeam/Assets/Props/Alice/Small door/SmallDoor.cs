﻿using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmallDoor : MonoBehaviour {


    [SerializeField]
    Transform scepterParent;
    [SerializeField]
    Vector3 scepterPosition, scepterRotation;

    Animator animator;

    [SerializeField]
    Collider playerTrigger;

    [SerializeField]
    float delayBeforeTransition = 4;
    float openTime = 0;

    public AudioSource audioSource;

    public Transform Spawnpoint;

    bool open = false;

    public static SmallDoor instance;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerTrigger.enabled = false;
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (open)
            return;
        // Try to get the scepter if close, else try to get the player's head.
        Scepter scepter = other.GetComponentInParent<Scepter>();
        if (scepter && scepter.ScaledDown)
        {
            scepter.UseAsKey();
            scepter.transform.parent = scepterParent;
            scepter.transform.localPosition = scepterPosition;
            scepter.transform.localEulerAngles = scepterRotation;
            scepter.transform.localScale = Vector3.one;
            Open();
        }
    }

    bool sceneStarted = false;
    private void OnTriggerStay(Collider other)
    {
        if (open && other.CompareTag("HeadCollider") && Time.time - openTime > delayBeforeTransition && !sceneStarted)
        {
            InteractionManagerAlice.instance.LaunchNextScene();
            sceneStarted = true;
        }
    }

    public void Open()
    {
        animator.SetTrigger("open");
        playerTrigger.enabled = true;
        audioSource.Play();
        open = true;
        openTime = Time.time;
    }

}
