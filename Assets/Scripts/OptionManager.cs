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
    public Text blueText;
    public Text yellowText;
    public Text redText;
    public GameObject[] Pawn;
    public GameObject fade;
    bool fadeOut = false;
    bool fadeIn = true;
    float fadeCount = 1.0f;

    int curCount = 0;
    const int pawnCount = 16;

    void Start()
    {
        fade.SetActive(true);
        for(int i = 0; i < 3; i++) {
            if(i == curCount) {
                Pawn[i].SendMessage("toBig");
            }else {
                Pawn[i].SendMessage("toSmall");
            }
        }
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
    }









    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////




    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    private void RequestBanner()
    {
        #if UNITY_IOS
            string adUnitId = AdmobVariableScript.GetIPHONE_BANNER();
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.TopRight);
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }








    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////



    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////
    ////////    ////////    ////////    ////////    ////////    ////////

    public int GetColorCount(int idx) {
        return curColorCount[idx];
    }

    public void UpdateText() {
        blueText.text = curColorCount[0].ToString();
        yellowText.text = curColorCount[1].ToString();
        redText.text = curColorCount[2].ToString();
    }

    void Update()
    {
        if(fadeIn) {
            fadeCount -= Time.deltaTime * 2;
            blueText.text = curColorCount[0].ToString();
            yellowText.text = curColorCount[1].ToString();
            redText.text = curColorCount[2].ToString();
            fade.GetComponent<Image>().color = new Color((float)51.0f/255.0f, (float)51.0f/255.0f, (float)51.0f/255.0f, Mathf.Max(0.0f, fadeCount));
            if(fadeCount < 0.0f) {
                fadeCount = 0.0f;
                fade.SetActive(false);
                fadeIn = false;
            }
            return;
        }
        if(fadeOut) {
            fadeCount += Time.deltaTime * 2;
            fade.GetComponent<Image>().color = new Color((float)51.0f/255.0f, (float)51.0f/255.0f, (float)51.0f/255.0f, Mathf.Min(1.0f, fadeCount));
            if(fadeCount > 1.1f) {
                fadeCount = 1.0f;
                SceneManager.LoadScene("Start");
            }
            return;
        }
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
        fade.SetActive(true);
        fadeOut = true;
        bannerView.Destroy();
    }

    // public void BlueButton() {
    //     curCount = 0;
    //     for(int i = 0; i < 3; i++) {
    //         if(i == curCount) {
    //             Pawn[i].SendMessage("toBig");
    //         }else {
    //             Pawn[i].SendMessage("toSmall");
    //         }
    //     }
    // }

    // public void YellowButton() {
    //     curCount = 1;
    //     for(int i = 0; i < 3; i++) {
    //         if(i == curCount) {
    //             Pawn[i].SendMessage("toBig");
    //         }else {
    //             Pawn[i].SendMessage("toSmall");
    //         }
    //     }
    // }

    // public void RedButton() {
    //     curCount = 2;
    //     for(int i = 0; i < 3; i++) {
    //         if(i == curCount) {
    //             Pawn[i].SendMessage("toBig");
    //         }else {
    //             Pawn[i].SendMessage("toSmall");
    //         }
    //     }
    // }

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
