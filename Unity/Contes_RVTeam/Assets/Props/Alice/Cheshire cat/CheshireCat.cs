﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Valve.VR.InteractionSystem;

public class CheshireCat : MonoBehaviour {

    [SerializeField]
    Transform spawnPointsParent;
    int previousChoice = 0;
    [SerializeField]
    Transform cat;

    [Header("Eyes")]
    [SerializeField]
    Transform leftEye;
    [SerializeField]
    Transform rightEye;
    [SerializeField]
    BoxCollider leftLimits, rightLimits;
    float leftMinX;
    float leftMaxX;
    float rightMinX;
    float rightMaxX;

    [Space]
    [SerializeField]
    float playerDistance = 2;
    [SerializeField]
    List<VideoClip> startVideos = new List<VideoClip>();
    [SerializeField]
    VideoClip[] randomVideos;

    VideoPlayer playerA;
    VideoPlayer playerB;
    bool useA = true;
    int lastVideo = -1;
    bool aPlays;
    bool bPlays;
    bool aPrepared;
    bool bPrepared;

    Vector3 Target { get { return Player.instance.headCollider.transform.position; } }


    private void Awake()
    {
        leftMinX = cat.InverseTransformPoint(leftLimits.bounds.min).x;
        leftMaxX = cat.InverseTransformPoint(leftLimits.bounds.max).x;
        rightMinX = cat.InverseTransformPoint(rightLimits.bounds.min).x;
        rightMaxX = cat.InverseTransformPoint(rightLimits.bounds.max).x;

        // Setup video players.
        playerA = GetComponentsInChildren<VideoPlayer>()[0];
        playerB = GetComponentsInChildren<VideoPlayer>()[1];
        playerA.loopPointReached += delegate { aPlays = false; };
        playerA.prepareCompleted += delegate
        {
            aPrepared = true;
        };
        playerB.loopPointReached += delegate { bPlays = false; };
        playerB.prepareCompleted += delegate
        {
            bPrepared = true;
        };
    }

    private void Start()
    {
        Teleport();

        // Start the video at the first frame.
        playerA.clip = startVideos[0];
        startVideos.RemoveAt(0);
        playerA.Play();
        playerA.Pause();
        playerA.frame = 1;
        playerA.Prepare();

        Potion.ScaledNormal += Talk;
    }

    public void Teleport()
    {
        previousChoice = Utilities.ExclusiveRange(0, spawnPointsParent.childCount, previousChoice);
        cat.SetParent(spawnPointsParent.GetChild(previousChoice), false);
    }

    private void Update()
    {
        LookAt(Player.instance.headCollider.transform.position);
        if (startVideos.Count > 1 && Vector3.Distance(Target, cat.position) < playerDistance)
            SayStartSentence();
    }

    void SayStartSentence()
    {
        StartCoroutine(PlayStartVideos());
    }

    void Talk()
    {
        StartCoroutine(PlayRandomVideo());
    }

    void LookAt(Vector3 target)
    {
        if (Vector3.Dot((target - cat.position).normalized, cat.forward) < 0)
            return;

        float dotLeft = Vector3.Dot((target - leftLimits.transform.position).normalized, cat.right);
        float dotRight = Vector3.Dot((target - rightLimits.transform.position).normalized, cat.right);

        float newLeftX = Mathf.Lerp(leftMinX, leftMaxX, 1 - ((dotLeft + 1) * .5f));
        float newRightX = Mathf.Lerp(rightMinX, rightMaxX, 1 - ((dotRight + 1) * .5f));

        leftEye.localPosition = leftEye.localPosition.SetX(newLeftX);
        rightEye.localPosition = rightEye.localPosition.SetX(newRightX);
    }

    IEnumerator PlayStartVideos()
    {
        PlayPreparedClip();
        PrepareClip(startVideos[0]);
        startVideos.RemoveAt(0);
        while (aPlays || bPlays || !aPrepared || !bPrepared)
            yield return null;
        yield return new WaitForSeconds(1);

        PlayPreparedClip();
        PrepareClip(startVideos[0]);
        startVideos.RemoveAt(0);
        while (aPlays || bPlays || !aPrepared || !bPrepared)
            yield return null;
        yield return new WaitForSeconds(1);

        PlayPreparedClip();
        while (aPlays || bPlays)
            yield return null;

        lastVideo = Utilities.ExclusiveRange(0, randomVideos.Length, lastVideo);
        PrepareClip(randomVideos[lastVideo]);
    }

    IEnumerator PlayRandomVideo()
    {
        PlayPreparedClip();

        lastVideo = Utilities.ExclusiveRange(0, randomVideos.Length, lastVideo);
        PrepareClip(randomVideos[lastVideo]);
        while (aPlays || bPlays || !aPrepared || !bPrepared)
            yield return null;
    }

    void PrepareClip(VideoClip clip)
    {
        if (!useA)
        {
            bPrepared = false;
            playerB.clip = clip;
            playerB.Prepare();
        }
        else
        {
            aPrepared = false;
            playerA.clip = clip;
            playerA.Prepare();
        }
    }

    void PlayPreparedClip()
    {
        if (useA)
        {
            playerA.targetMaterialRenderer.gameObject.SetActive(true);
            playerA.Play();
            aPlays = true;
            playerB.targetMaterialRenderer.gameObject.SetActive(false);
        }
        else
        {
            playerB.targetMaterialRenderer.gameObject.SetActive(true);
            playerB.Play();
            bPlays = true;
            playerA.targetMaterialRenderer.gameObject.SetActive(false);
        }
        useA = !useA;
    }
}
