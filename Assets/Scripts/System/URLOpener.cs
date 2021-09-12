using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class URLOpener : MonoBehaviour
{
    // Start is called before the first frame update
    public static URLOpener instance;
    void Start()
    {
        instance = this;
    }

    public void OpenURL(string url ){
        
#if !UNITY_EDITOR
        OpenTab(url); 
#else
        Application.OpenURL(url);
#endif
    }

    [DllImport("__Internal")]
    private static extern void OpenTab(string url);
}
