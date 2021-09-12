using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLinkBar : MonoBehaviour
{
    // Start is called before the first frame update
    public string url;

    public void OpenURL(){
        URLOpener.instance.OpenURL(url);
    }
}
