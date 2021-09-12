using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapBoard : MonoBehaviour
{
    [SerializeField] private GameObject[] tapBarBoard;

    public void Init(){
        foreach (GameObject item in tapBarBoard)
        {
            
        }
    }

    public void TapBar(int id){
        if(id > tapBarBoard.Length){
            Debug.Log(id  + "超過範圍");
            return;
        }

        tapBarBoard[id].SetActive(true);
    }

    public void CancelTapBar(int id){
        if(id > tapBarBoard.Length){
            Debug.Log(id  + "超過範圍");
            return;
        }

        tapBarBoard[id].SetActive(false);
    }
}
