using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public TapType testTapType;
    public List<VideoStopData> videoStopDatas;
    public GameObject rawImage;
    // Start is called before the first frame update
    private float stopTime;

    void Start(){
        rawImage.SetActive(false);
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"rat.mov");
        videoPlayer.Prepare();
    }
    
    
    [ContextMenu("TestPlay")]
    public void TestPlay(){
        PlayVideo(testTapType);
    }

    public void PlayFullVideo(){
        rawImage.SetActive(true);
        videoPlayer.Play();
    }

    public void PlayVideo(TapType tapType){
        Debug.Log("Play");
        rawImage.SetActive(true);
        videoPlayer.Play();
        stopTime = GetStopTime(tapType);
        StartCoroutine(Play());
    }

    public void SkipVideo(){
        videoPlayer.Stop();
        
    }

    IEnumerator Play(){
        yield return new WaitForSeconds(stopTime);
        videoPlayer.Pause();
    }

    public float GetIntervelTime(TapType tapType){
        if(tapType == videoStopDatas[0].tapType){
            return GetStopTime(tapType);
        }
        else{
            TapType tapType1 = (TapType)( (int)tapType -1);
            float time1 = GetStopTime(tapType1);  // 不太好擴充的寫法
            float time2 = GetStopTime(tapType);
            return time2 - time1;
        }
    }

    public float GetStopTime(TapType tapType){
        foreach (VideoStopData data in videoStopDatas)
        {
            if(data.tapType == tapType){
                return data.time;
            }
        }
        Debug.Log($"找不到{tapType}的時間點");
        return 0f;
    }
}

[System.Serializable]
public class VideoStopData{
    public TapType tapType;
    public float time;
}