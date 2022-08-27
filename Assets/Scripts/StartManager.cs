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
    [SerializeField] GameObject titleLogo;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject levelButton;
    [SerializeField] GameObject popUp;
    [SerializeField] GameObject popUpFilter;
    [SerializeField] GameObject popUpClose;
    [SerializeField] GameObject eventSystem;

    public static bool isIDFA = false;

    void Start() {
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

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomRight);
        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }



    public void OptionButton(){
        bannerView.Destroy();
        eventSystem.SetActive(false);
        FadeManager.Instance.LoadScene(0.5f, "Option");
    }
    
    public void StartButton(){
        titleLogo.SetActive(false);
        startButton.SetActive(false);
        levelButton.SetActive(true);
    }

    public void SetLevel(int num) {
        level = 3 - num;
        bannerView.Destroy();
        eventSystem.SetActive(false);
        FadeManager.Instance.LoadScene(0.5f, "Main");
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
