using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class StartManager : MonoBehaviour
{
    
    private BannerView bannerView;
    static int level = 0;
    [SerializeField] GameObject game;
    [SerializeField] GameObject gameEnglish;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject optionText;
    [SerializeField] GameObject levelObject;
    [SerializeField] GameObject fade;
    [SerializeField] GameObject popUp;
    [SerializeField] GameObject popUpFilter;
    [SerializeField] GameObject popUpClose;


    bool fadeOut = false;
    bool fadeIn = true;
    float fadeCount = 1.0f;
    bool optionFlag = false;
    public static bool isIDFA = false;


    void Start() {
        fade.SetActive(true);
        game.SetActive(true);
        gameEnglish.SetActive(true);
        startText.SetActive(true);
        levelObject.SetActive(false);
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

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomRight);
        AdRequest request = new AdRequest.Builder().Build();

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

    
    void Update()
    {
        if (fadeIn) {
            fadeCount -= Time.deltaTime * 2;
            fade.GetComponent<Image>().color = new Color((float)51.0f/255.0f, (float)51.0f/255.0f, (float)51.0f/255.0f, Mathf.Max(0.0f, fadeCount));
            if (fadeCount < 0.0f) {
                fadeCount = 0.0f;
                fade.SetActive(false);
                fadeIn = false;
            }
            return;
        }
        if (fadeOut) {
            fadeCount += Time.deltaTime * 2;
            fade.GetComponent<Image>().color = new Color((float)51.0f/255.0f, (float)51.0f/255.0f, (float)51.0f/255.0f, Mathf.Min(1.0f, fadeCount));
            if (fadeCount > 1.1f) {
                fadeCount = 1.0f;
                if (optionFlag) {
                    SceneManager.LoadScene("Option");
                } else {
                    SceneManager.LoadScene("Main");
                }
                
            }
            return;
        }


    }

    public void OptionButton(){
        fade.SetActive(true);
        fadeOut = true;
        optionFlag = true;
        bannerView.Destroy();
    }
    
    public void StartButton(){
        game.SetActive(false);
        gameEnglish.SetActive(false);
        startText.SetActive(false);
        levelObject.SetActive(true);
    }

    public void SetLevel(int num) {
        level = 3 - num;
        fade.SetActive(true);
        fadeOut = true;
        bannerView.Destroy();
    }

    public void PopUpOnButton() {
        popUp.SetActive(true);
        popUpFilter.SetActive(true);
        popUpClose.SetActive(true);
		popUp.GetComponent<Animator>().SetBool("PopUpOpen", false);
        popUpFilter.GetComponent<Animator>().SetBool("PopUpFilterOpen", false);
        popUpClose.GetComponent<Animator>().SetBool("PopUpCloseOpen", false);
    }

    public void PopUpOffButton() {
		popUp.GetComponent<Animator>().SetBool("PopUpOpen", true);
        popUpFilter.GetComponent<Animator>().SetBool("PopUpFilterOpen", true);
        popUpClose.GetComponent<Animator>().SetBool("PopUpCloseOpen", true);
    }

    public int GetLevel() {
        return level;
    }

}
