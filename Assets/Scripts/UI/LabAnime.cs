using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class LabAnime : MonoBehaviour
{
    public DOTweenAnimation[] dOTweenAnimations;
    public Text lab_info;

    public float animeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Play(string info){
        lab_info.text = info;
        foreach (DOTweenAnimation item in dOTweenAnimations)
        {
            item.DORestart();
        }

        yield return new WaitForSeconds(animeTime);
        lab_info.text = "";
    }
}
