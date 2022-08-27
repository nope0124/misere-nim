using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class OptionManager : MonoBehaviour
{
    private BannerView bannerView;
    public static int[] curColorCount = {7, 6, 2};
    [SerializeField] Text[] colorText;
    [SerializeField] GameObject[] Pawn;
    [SerializeField] GameObject eventSystem;

    int curCount = 0;
    const int pawnCount = 16;

    void Start()
    {
        for(int i = 0; i < 3; i++) {
            if(i == curCount) {
                Pawn[i].SendMessage("toBig");
            }else {
                Pawn[i].SendMessage("toSmall");
            }
        }
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    private void RequestBanner()
    {
        #if UNITY_IOS
            string adUnitId = AdmobVariableScript.GetIPHONE_BANNER();
        #else
            string adUnitId = "unexpected_platform";
        #endif

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.TopRight);
        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    public int GetColorCount(int idx) {
        return curColorCount[idx];
    }

    public void UpdateText() {
        for(int i = 0; i < 3; i++) {
            colorText[i].text = curColorCount[i].ToString();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            curCount = (curCount + 2) % 3;
            for(int i = 0; i < 3; i++) {
                if(i == curCount) {
                    Pawn[i].SendMessage("toBig");
                }else {
                    Pawn[i].SendMessage("toSmall");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            curCount = (curCount + 1) % 3;
            for(int i = 0; i < 3; i++) {
                if(i == curCount) {
                    Pawn[i].SendMessage("toBig");
                }else {
                    Pawn[i].SendMessage("toSmall");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            curColorCount[curCount]++;
            if(curColorCount[curCount] > pawnCount) curColorCount[curCount] = pawnCount;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            curColorCount[curCount]--;
            if(curColorCount[curCount] < 1) curColorCount[curCount] = 1;
        }

        UpdateText();
    }

    public void RemoveCountPlusButton(int idx) {
        int num = +1;
        curCount = idx;
        curColorCount[idx] += num;
        if(curColorCount[idx] > pawnCount) {
            curColorCount[idx] = pawnCount;
        }else if(curColorCount[idx] < 1) {
            curColorCount[idx] = 1;
        }
        Pawn[idx].SendMessage("toBig");
        Pawn[(idx+1)%3].SendMessage("toSmall");
        Pawn[(idx+2)%3].SendMessage("toSmall");
    }

    public void RemoveCountMinusButton(int idx) {
        int num = -1;
        curCount = idx;
        curColorCount[idx] += num;
        if(curColorCount[idx] > pawnCount) {
            curColorCount[idx] = pawnCount;
        }else if(curColorCount[idx] < 1) {
            curColorCount[idx] = 1;
        }
        Pawn[idx].SendMessage("toBig");
        Pawn[(idx+1)%3].SendMessage("toSmall");
        Pawn[(idx+2)%3].SendMessage("toSmall");
    }


    public void ToTitle() {
        bannerView.Destroy();
        eventSystem.SetActive(false);
        FadeManager.Instance.LoadScene(0.5f, "Start");
    }



    public void RandomButton() {
        while(true) {
            for(int i = 0; i < 3; i++) {
                if(Random.Range(0, 100) < 85) {
                    curColorCount[i] = Random.Range(0, 9) + 1;
                }else {
                    curColorCount[i] = Random.Range(9, pawnCount) + 1;
                }
            }
            if((int)(curColorCount[0] ^ (curColorCount[1] ^ curColorCount[2])) != 0) break;
        }
    }
}
