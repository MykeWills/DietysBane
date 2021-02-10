using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class StreamVideo : MonoBehaviour {

    [Header("HomeScreen")]
    public RawImage startVideo;
    public RawImage campaignVideo;
    public RawImage multiplayerVideo;
    public RawImage hordeVideo;
    public VideoClip fenixClip;
    [Space]


    AudioSource audioSrc;
    VideoPlayer videoPlayer;



	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = null;

    }
	 public IEnumerator StartVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
       
        startVideo.texture = videoPlayer.texture;
        campaignVideo.texture = videoPlayer.texture;
        multiplayerVideo.texture = videoPlayer.texture;
        hordeVideo.texture = videoPlayer.texture;

        videoPlayer.Play();
        audioSrc.Play();
    }

    public void PlayStartVideo()
    {
        videoPlayer.clip = fenixClip;
        StartCoroutine(StartVideo());
    }
    public void PlayCampaignVideo()
    {
        videoPlayer.clip = fenixClip;
        StartCoroutine(StartVideo());
    }
    public void PlayMultiplayerVideo()
    {
        videoPlayer.clip = fenixClip;
        StartCoroutine(StartVideo());
    }
    public void PlayHordeVideo()
    {
        videoPlayer.clip = fenixClip;
        StartCoroutine(StartVideo());
    }

}
