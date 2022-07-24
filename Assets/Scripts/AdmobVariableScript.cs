using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobVariableScript : MonoBehaviour
{
    public static string IPHONE_BANNER = "ca-app-pub-7126478757168182/6947319284"; //アナザー環境
    // public static string IPHONE_BANNER = "ca-app-pub-7126478757168182/4440444590"; //本番環境
    // public static string IPHONE_BANNER = "ca-app-pub-3940256099942544/2934735716"; //テスト環境

    public static string IPHONE_INTERSTITIAL = "ca-app-pub-7126478757168182/8727304814"; //アナザー環境
    // public static string IPHONE_INTERSTITIAL = "ca-app-pub-7126478757168182/6404262951"; //本番環境
    // public static string IPHONE_INTERSTITIAL = "ca-app-pub-3940256099942544/4411468910"; //テスト環境

    void Start () {
        
    }

    void Update()
    {
        
    }

    public static string GetIPHONE_BANNER() {
        return IPHONE_BANNER;
    }
    public static string GetIPHONE_INTERSTITIAL() {
        return IPHONE_INTERSTITIAL;
    }
    
}
