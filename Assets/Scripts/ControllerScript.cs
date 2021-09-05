using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class ControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private BannerView bannerView;
    static int level = 0;
    public GameObject game;
    public GameObject gameEnglish;
    public GameObject startText;
    public GameObject optionText;
    public GameObject level1Text;
    public GameObject level2Text;
    public GameObject level3Text;
    public GameObject fade;
    bool fadeOut = false;
    bool fadeIn = true;
    float fadeCount = 1.0f;
    bool optionFlag = false;
    void Start() {
        fade.SetActive(true);
        game.SetActive(true);
        gameEnglish.SetActive(true);
        startText.SetActive(true);
        level1Text.SetActive(false);
        level2Text.SetActive(false);
        level3Text.SetActive(false);
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
    }

    private void RequestBanner()
    {
        #if UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomRight);
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }


    // Update is called once per frame
    public GameObject popUp;
    public GameObject popUpFilter;
    public GameObject popUpClose;
    void Update()
    {
        if (fadeIn) {
            fadeCount -= Time.deltaTime * 2;
            fade.GetComponent<Image>().color = new Color((float)50.0f/255.0f, (float)50.0f/255.0f, (float)50.0f/255.0f, Mathf.Max(0.0f, fadeCount));
            if (fadeCount < 0.0f) {
                fadeCount = 0.0f;
                fade.SetActive(false);
                fadeIn = false;
            }
            return;
        }
        if (fadeOut) {
            fadeCount += Time.deltaTime * 2;
            fade.GetComponent<Image>().color = new Color((float)50.0f/255.0f, (float)50.0f/255.0f, (float)50.0f/255.0f, Mathf.Min(1.0f, fadeCount));
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

    public void optionButton(){
        fade.SetActive(true);
        fadeOut = true;
        optionFlag = true;
        bannerView.Destroy();
    }
    
    public void startButton(){
        game.SetActive(false);
        gameEnglish.SetActive(false);
        startText.SetActive(false);
        level1Text.SetActive(true);
        level2Text.SetActive(true);
        level3Text.SetActive(true);
    }

    public void level1() {
        level = 2;
        fade.SetActive(true);
        fadeOut = true;
        bannerView.Destroy();
    }

    public void level2() {
        level = 1;
        fade.SetActive(true);
        fadeOut = true;
        bannerView.Destroy();
    }

    public void level3() {
        level = 0;
        fade.SetActive(true);
        fadeOut = true;
        bannerView.Destroy();
    }

    public void popUpOnButton() {
        popUp.SetActive(true);
        popUpFilter.SetActive(true);
        popUpClose.SetActive(true);
		popUp.GetComponent<Animator>().SetBool("PopUpOpen", false);
        popUpFilter.GetComponent<Animator>().SetBool("PopUpFilterOpen", false);
        popUpClose.GetComponent<Animator>().SetBool("PopUpCloseOpen", false);
    }

    public void popUpOffButton() {
		popUp.GetComponent<Animator>().SetBool("PopUpOpen", true);
        popUpFilter.GetComponent<Animator>().SetBool("PopUpFilterOpen", true);
        popUpClose.GetComponent<Animator>().SetBool("PopUpCloseOpen", true);
    }

    public static int getLevel() {
        return level;
    }

}
