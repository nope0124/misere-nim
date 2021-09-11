using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using GoogleMobileAds.Api;


public class MainControllerScript : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private int showingCount = 0;
    bool showingFlag = false;
    bool retryFlag = false;
    bool hadRetryFlag = false;
    public string loadEventParam; // 読込イベント名(遷移元で渡されるゴロ)
    int enemyCurCount = 0;
    int enemyCurIndex = 0;
    int[] decreaseAlpha = {0, 0, 0};
    int[] decreaseCount = {0, 0, 0};
    static int[] colorCount = {7, 6, 2};
    static int[] saveCount = {7, 6, 2}; //const、リセット、記憶しておくやつ
    static int saveLevel = 0; // 猶予
    static int level = 0; // レベル
    static int[] blueVec; // 青コマが存在するかの配列
    static int[] yellowVec; // 黄コマが存在するかの配列
    static int[] redVec; //赤コマが存在するかの配列
    GameObject[] bluePawn;
    GameObject[] yellowPawn;
    GameObject[] redPawn;
    static string[] colorName = {"青", "黄", "赤"}; // 色名
    int runCount = 1;
    static int playerColorIndex = 0;
    static int enemyColorIndex = 0;
    static bool isGameOver = false;
    static bool isMyTurn = true;
    bool isPretend = true;
    public Text blueText;
    public Text yellowText;
    public Text redText;
    public Text runText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public Text thinkText;
    public Image loadingImage;
    public Text[] decreaseText; 
    public GameObject retryButton;
    public GameObject prevButton;
    public GameObject nextButton;
    public GameObject bluePrefab;
    public GameObject yellowPrefab;
    public GameObject redPrefab;
    public Image pawnDisplay;
    public Sprite[] colorSprite;
    Stack<int> stackIndex = new Stack<int>();
    Stack<int> stackCount = new Stack<int>();
    Stack<int> stackLevel = new Stack<int>();
    Stack<int> saveStackIndex = new Stack<int>();
    Stack<int> saveStackCount = new Stack<int>();
    Stack<int> saveStackLevel = new Stack<int>();
    float[,] pawnPositionX = {
        {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 1
        {-2.5f, 2.5f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 2
        {-3.0f, 0.0f, 3.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 3
        {-2.5f, 2.5f, -2.5f, 2.5f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 4
        {-3.0f, 3.0f, 0.0f, -3.0f, 3.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 5
        {-4.0f, 0.0f, 4.0f, -2.0f, 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 6
        {-2.0f, 2.0f, -4.0f, 0.0f, 4.0f, -2.0f, 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 7
        {-4.0f, 0.0f, 4.0f, -2.0f, 2.0f, -4.0f, 0.0f, 4.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 8
        {-4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 9
        {-5.4f, -1.8f, 1.8f, 5.4f, -3.6f, 0.0f, 3.6f, -1.8f, 1.8f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 10
        {-4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, -2.0f, 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 11
        {-4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 12
        {-5.4f, -1.8f, 1.8f, 5.4f, -4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, -4.0f, 0.0f, 4.0f, 0.0f, 0.0f, 0.0f}, // 13
        {-4.0f, 0.0f, 4.0f, -5.4f, -1.8f, 1.8f, 5.4f, -5.4f, -1.8f, 1.8f, 5.4f, -4.0f, 0.0f, 4.0f, 0.0f, 0.0f}, // 14
        {-5.4f, -1.8f, 1.8f, 5.4f, -5.4f, -1.8f, 1.8f, 5.4f, -5.4f, -1.8f, 1.8f, 5.4f, -4.0f, 0.0f, 4.0f, 0.0f}, // 15
        {-5.4f, -1.8f, 1.8f, 5.4f, -5.4f, -1.8f, 1.8f, 5.4f, -5.4f, -1.8f, 1.8f, 5.4f, -5.4f, -1.8f, 1.8f, 5.4f}, // 16
    };
    float[,] pawnPositionZ = {
        {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 1
        {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 2
        {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 3
        {2.5f, 2.5f, -2.5f, -2.5f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 4
        {3.0f, 3.0f, 0.0f, -3.0f, -3.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 5
        {4.0f, 4.0f, 4.0f, 0.0f, 0.0f, -4.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 6
        {4.0f, 4.0f, 0.0f, 0.0f, 0.0f, -4.0f, -4.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 7
        {4.0f, 4.0f, 4.0f, 0.0f, 0.0f, -4.0f, -4.0f, -4.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 8
        {4.0f, 4.0f, 4.0f, 0.0f, 0.0f, 0.0f, -4.0f, -4.0f, -4.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 9
        {5.4f, 5.4f, 5.4f, 5.4f, 1.8f, 1.8f, 1.8f, -1.8f, -1.8f, -5.4f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 10
        {5.4f, 5.4f, 5.4f, 1.8f, 1.8f, 1.8f, -1.8f, -1.8f, -1.8f, -5.4f, -5.4f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 11
        {5.4f, 5.4f, 5.4f, 1.8f, 1.8f, 1.8f, -1.8f, -1.8f, -1.8f, -5.4f, -5.4f, -5.4f, 0.0f, 0.0f, 0.0f, 0.0f}, // 12
        {5.4f, 5.4f, 5.4f, 5.4f, 1.8f, 1.8f, 1.8f, -1.8f, -1.8f, -1.8f, -5.4f, -5.4f, -5.4f, 0.0f, 0.0f, 0.0f}, // 13
        {5.4f, 5.4f, 5.4f, 1.8f, 1.8f, 1.8f, 1.8f, -1.8f, -1.8f, -1.8f, -1.8f, -5.4f, -5.4f, -5.4f, 0.0f, 0.0f}, // 14
        {5.4f, 5.4f, 5.4f, 5.4f, 1.8f, 1.8f, 1.8f, 1.8f, -1.8f, -1.8f, -1.8f, -1.8f, -5.4f, -5.4f, -5.4f, 0.0f}, // 15
        {5.4f, 5.4f, 5.4f, 5.4f, 1.8f, 1.8f, 1.8f, 1.8f, -1.8f, -1.8f, -1.8f, -1.8f, -5.4f, -5.4f, -5.4f, -5.4f}, // 16
    };
    int countDown = 0;
    GameObject[,] gotPawnPlayer = new GameObject[48, 16];
    GameObject[,] gotPawnEnemy = new GameObject[48, 16];
    int gotPawnIndex = 0;
    private Camera mainCamera;
    private Vector3 currentPosition = Vector3.zero;


    void makePawn(int type, int cnt) {
        for (int i = 0; i < cnt; i++) {
            if (type == 0) {
                bluePawn[i] = Instantiate(bluePrefab, new Vector3((type - 1) * 14 + pawnPositionX[cnt-1, i], 1.5f, pawnPositionZ[cnt-1, i] + 5.0f), Quaternion.identity) as GameObject;
            } else if (type == 1) {
                yellowPawn[i] = Instantiate(yellowPrefab, new Vector3((type - 1) * 14 + pawnPositionX[cnt-1, i], 1.5f, pawnPositionZ[cnt-1, i] + 5.0f), Quaternion.identity) as GameObject;
            } else if (type == 2) {
                redPawn[i] = Instantiate(redPrefab, new Vector3((type - 1) * 14 + pawnPositionX[cnt-1, i], 1.5f, pawnPositionZ[cnt-1, i] + 5.0f), Quaternion.identity) as GameObject;
            }
        }
    }

    public static int[] getBlueVec() {
        return blueVec;
    }

    public static int[] getYellowVec() {
        return yellowVec;
    }

    public static int[] getRedVec() {
        return redVec;
    }

    public static int getPlayerColorIndex() {
        return playerColorIndex;
    }

    public static int getEnemyColorIndex() {
        return enemyColorIndex;
    }

    public static bool getIsMyTurn() {
        return isMyTurn;
    }

    public static bool getIsGameOver() {
        return isGameOver;
    }
    // void displayGotPawn(int curIndex, int curCount, int type) {
    //     if (type == 0) {
    //         for (int i = 0; i < curCount; i++) {
    //             switch (curIndex) {
    //                 case 0:
    //                     gotPawnPlayer[gotPawnIndex, i] = Instantiate(bluePrefab, new Vector3(-24 - 3 * gotPawnIndex, 0.0f, 0 - i * 3), Quaternion.identity) as GameObject;
    //                     break;
    //                 case 1:
    //                     gotPawnPlayer[gotPawnIndex, i] = Instantiate(yellowPrefab, new Vector3(-24 - 3 * gotPawnIndex, 0.0f, 0 - i * 3), Quaternion.identity) as GameObject;
    //                     break;
    //                 case 2:
    //                     gotPawnPlayer[gotPawnIndex, i] = Instantiate(redPrefab, new Vector3(-24 - 3 * gotPawnIndex, 0.0f, 0 - i * 3), Quaternion.identity) as GameObject;
    //                     break;
    //             }
    //             gotPawnPlayer[gotPawnIndex, i].SetActive(true);
    //         }
    //     } else if (type == 1) {
    //         for (int i = 0; i < curCount; i++) {
    //             switch (curIndex) {
    //                 case 0:
    //                     gotPawnEnemy[gotPawnIndex, i] = Instantiate(bluePrefab, new Vector3(24 + 3 * gotPawnIndex, 0.0f, 10 + i * 3), Quaternion.identity) as GameObject;
    //                     break;
    //                 case 1:
    //                     gotPawnEnemy[gotPawnIndex, i] = Instantiate(yellowPrefab, new Vector3(24 + 3 * gotPawnIndex, 0.0f, 10 + i * 3), Quaternion.identity) as GameObject;
    //                     break;
    //                 case 2:
    //                     gotPawnEnemy[gotPawnIndex, i] = Instantiate(redPrefab, new Vector3(24 + 3 * gotPawnIndex, 0.0f, 10 + i * 3), Quaternion.identity) as GameObject;
    //                     break;
    //             }
    //             gotPawnEnemy[gotPawnIndex, i].SetActive(true);
    //         }
    //     } else if (type == 2) {
    //         for (int i = 0; i < 16; i++) {
    //             if (gotPawnPlayer[gotPawnIndex, i] != null) {
    //                 gotPawnPlayer[gotPawnIndex, i].SetActive(false);
    //                 gotPawnPlayer[gotPawnIndex, i] = null;
    //             }
    //             if (gotPawnEnemy[gotPawnIndex, i] != null) {
    //                 gotPawnEnemy[gotPawnIndex, i].SetActive(false);
    //                 gotPawnEnemy[gotPawnIndex, i] = null;
    //             }
    //         }
    //     }
        
    // }

    public void prevFunc() {
        if (!isMyTurn || isGameOver) return;
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < stackCount.Peek(); j++) {
                if (stackIndex.Peek() == 0) {
                    blueVec[colorCount[0] + j] = 1;
                } else if (stackIndex.Peek() == 1) {
                    yellowVec[colorCount[1] + j] = 1;
                } else if (stackIndex.Peek() == 2) {
                    redVec[colorCount[2] + j] = 1;
                }
            }
            colorCount[stackIndex.Peek()] += stackCount.Peek();
            saveStackIndex.Push(stackIndex.Pop());
            saveStackCount.Push(stackCount.Pop());
        }
        level += stackLevel.Peek();
        saveStackLevel.Push(stackLevel.Pop());
        // gotPawnIndex--;
        // displayGotPawn(-1, -1, 2);
    }

    public void nextFunc() {
        if (!isMyTurn || isGameOver) return;
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < saveStackCount.Peek(); j++) {
                if (saveStackIndex.Peek() == 0) {
                    blueVec[colorCount[0] - j - 1] = 0;
                } else if (saveStackIndex.Peek() == 1) {
                    yellowVec[colorCount[1] - j - 1] = 0;
                } else if (saveStackIndex.Peek() == 2) {
                    redVec[colorCount[2] - j - 1] = 0;
                }
            }
            colorCount[saveStackIndex.Peek()] -= saveStackCount.Peek();
            // displayGotPawn(saveStackIndex.Peek(), saveStackCount.Peek(), i);
            stackIndex.Push(saveStackIndex.Pop());
            stackCount.Push(saveStackCount.Pop());
        }
        // gotPawnIndex++;
        level -= saveStackLevel.Peek();
        stackLevel.Push(saveStackLevel.Pop());
        while (colorCount[playerColorIndex] == 0) {
            playerColorIndex = (playerColorIndex + 1) % 3;
        }
        fucMaterial();
        if (runCount > colorCount[playerColorIndex]) runCount = colorCount[playerColorIndex];
        
    }

    void endJudge() {
        if (colorCount[0] + colorCount[1] + colorCount[2] == 0) {
            isGameOver = true;
            showingFlag = true;
            return;
        }
        while (colorCount[playerColorIndex] == 0) {
            playerColorIndex = (playerColorIndex + 1) % 3;
        }
        fucMaterial();
        while (colorCount[enemyColorIndex] == 0) {
            enemyColorIndex = (enemyColorIndex + 1) % 3;
        }
        if (runCount > colorCount[playerColorIndex]) runCount = colorCount[playerColorIndex];
        isMyTurn = !isMyTurn;
        return;
    }

    void fucMaterial() {
        pawnDisplay.sprite = colorSprite[playerColorIndex];
    }

    void displayDecrease() {
        for (int i = 0; i < 3; i++) {
            if (decreaseAlpha[i] > 0) {
                decreaseAlpha[i] -= 17;
            } else {
                decreaseAlpha[i] = 0;
            }
        }
        for (int i = 0; i < 3; i++) {
            decreaseText[i].text = "-" + decreaseCount[i].ToString();
            decreaseText[i].color = new Color(1.0f, 0.0f, 0.0f, decreaseAlpha[i] / 15.0f);
        }
    }

    void init() {
        // for (int i = 0; i < 48; i++) {
        //     for (int j = 0; j < 16; j++) {
        //         if (gotPawnPlayer[i, j] != null) {
        //             gotPawnPlayer[i, j].SetActive(false);
        //             gotPawnPlayer[i, j] = null;
        //         }
        //         if (gotPawnEnemy[i, j] != null) {
        //             gotPawnEnemy[i, j].SetActive(false);
        //             gotPawnEnemy[i, j] = null;
        //         }
                
        //     }
        // }
        // gotPawnIndex = 0;
        stackIndex.Clear();
        stackCount.Clear();
        stackLevel.Clear();
        saveStackIndex.Clear();
        saveStackCount.Clear();
        saveStackLevel.Clear();
        level = saveLevel;
        runCount = 1;
        playerColorIndex = 0;
        enemyColorIndex = 0;
        fucMaterial();
        isGameOver = false;
        isMyTurn = true;
        retryButton.SetActive(false);
    }

    public GameObject fade;
    bool fadeOut = false;
    bool fadeIn = true;
    float fadeCount = 1.0f;

    void Start()
    {
        fade.SetActive(true);
        mainCamera = Camera.main;
        level = saveLevel = ControllerScript.getLevel(); //レベル取得
        colorCount[0] = saveCount[0] = NumberScript.getBlueCount(); //青の本数取得
        colorCount[1] = saveCount[1] = NumberScript.getYellowCount(); //黄の本数取得
        colorCount[2] = saveCount[2] = NumberScript.getRedCount(); //赤の本数取得
        //配列のサイズ変更
        System.Array.Resize(ref blueVec, saveCount[0]);
        System.Array.Resize(ref yellowVec, saveCount[1]);
        System.Array.Resize(ref redVec, saveCount[2]);
        System.Array.Resize(ref bluePawn, saveCount[0]);
        System.Array.Resize(ref yellowPawn, saveCount[1]);
        System.Array.Resize(ref redPawn, saveCount[2]);
        for (int i = 0; i < saveCount[0]; i++) blueVec[i] = 1;
        for (int i = 0; i < saveCount[1]; i++) yellowVec[i] = 1;
        for (int i = 0; i < saveCount[2]; i++) redVec[i] = 1;
        makePawn(0, saveCount[0]);
        makePawn(1, saveCount[1]);
        makePawn(2, saveCount[2]);
        init();
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        RequestInterstitial();
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

    private void RequestBanner()
    {
        #if UNITY_IOS
            string adUnitId = AdmobVariableScript.getIPHONE_BANNER();
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.BottomRight);
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





    void enemyPretendToThink() {
        if (enemyCurIndex == enemyColorIndex) {
            isPretend = false;
        } else {
            countDown = 40;
            isPretend = false;
            enemyColorIndex = enemyCurIndex;
        }
    }
    
    public void runPlayer() {
        if (!isMyTurn || isGameOver) return;
        saveStackIndex.Clear();
        saveStackCount.Clear();
        saveStackLevel.Clear();
        stackIndex.Push(playerColorIndex);
        stackCount.Push(runCount);
        countDown = 30 + Random.Range(0, 30);
        for (int i = 0; i < System.Math.Min(runCount, colorCount[playerColorIndex]); i++) {
            switch(playerColorIndex) {
                case 0:
                    blueVec[colorCount[0] - i - 1] = 0;
                    break;
                case 1:
                    yellowVec[colorCount[1] - i - 1] = 0;
                    break;
                case 2:
                    redVec[colorCount[2] - i - 1] = 0;
                    break;
            }
        }
        colorCount[playerColorIndex] -= runCount;
        if (colorCount[playerColorIndex] < 0) colorCount[playerColorIndex] = 0;
        decreaseAlpha[playerColorIndex] = 255;
        decreaseCount[playerColorIndex] = runCount;
        // displayGotPawn(playerColorIndex, runCount, 0);
        endJudge();
    }

    void runEnemyFirst() {
        bool usedLevel = false;
        int[] vec = new int[3];
        for (int i = 0; i < 3; i++) vec[i] = colorCount[i];
        System.Array.Sort(vec);
        int type = -1;
        if (vec[0] == 0 && vec[1] == 0 && vec[2] > 1) type = 1; // n 0 0
        else if (vec[0] == 0 && vec[1] == 1 && vec[2] > 1) type = 2; // n 1 0
        else if (vec[0] == 1 && vec[1] == 1 && vec[2] > 1) type = 1; // n 1 1
        else type = 3;
        switch(type){
            case 1:
                for (int i = 0; i < 3; i++) {
                    if (colorCount[i] > 1) {
                        enemyCurIndex = i;
                        enemyCurCount = colorCount[i] - 1;
                    }
                }
                break;
            
            case 2:
                for (int i = 0; i < 3; i++) {
                    if (colorCount[i] > 1) {
                        enemyCurIndex = i;
                        enemyCurCount = colorCount[i];
                    }
                }
                break;

            case 3:
                int diff = -1;
                int xor = (colorCount[0] ^ colorCount[1]) ^ colorCount[2];
                for (int index = 0; index < 3; index++) {
                    if (xor == 0) break;
                    for (int i = 0; i < colorCount[index]; i++) {
                        int d = (xor ^ colorCount[index]) ^ i;
                        if (d == 0) {
                            diff = i;
                            enemyCurIndex = index;
                        }
                    }
                }
                if (xor == 0 || diff == -1 || level > 0) {
                    if (!(xor == 0 || diff == -1)) {
                        level--;
                        usedLevel = true;
                    }
                    enemyCurIndex = Random.Range(0, 3);
                    while (colorCount[enemyCurIndex] == 0) {
                        enemyCurIndex = (enemyCurIndex + 1) % 3;
                    }
                    enemyCurCount = Random.Range(0, colorCount[enemyCurIndex]) + 1;
                } else {
                    enemyCurCount = colorCount[enemyCurIndex] - diff;
                    
                }
                break;
        }
        stackIndex.Push(enemyCurIndex);
        stackCount.Push(enemyCurCount);
        if (usedLevel) {
            stackLevel.Push(1);
        } else {
            stackLevel.Push(0);
        }
        
    }

    void runEnemySecond() {
        for (int i = 0; i < enemyCurCount; i++) {
            switch(enemyCurIndex) {
                case 0:
                    blueVec[colorCount[0] - i - 1] = 0;
                    break;
                case 1:
                    yellowVec[colorCount[1] - i - 1] = 0;
                    break;
                case 2:
                    redVec[colorCount[2] - i - 1] = 0;
                    break;
            }
        }
        colorCount[enemyCurIndex] -= enemyCurCount;
        decreaseAlpha[enemyCurIndex] = 255;
        decreaseCount[enemyCurIndex] = enemyCurCount;
        // displayGotPawn(enemyCurIndex, enemyCurCount, 1);
        endJudge();
        isPretend = true;
        // gotPawnIndex++;
    }

    void Update()
    {
        if (fadeIn) {
            fadeCount -= Time.deltaTime * 2;
            blueText.text = colorCount[0].ToString();
            yellowText.text = colorCount[1].ToString();
            redText.text = colorCount[2].ToString();
            runText.text = runCount.ToString();
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
                if (interstitial.IsLoaded() && showingCount > 0 && hadRetryFlag == false) {
                    interstitial.Show();
                    showingCount = 0;
                } else {
                    SceneManager.LoadScene("Start");
                }
            }
            return;
        }
        displayDecrease();
        for (int i = 0; i < saveCount[0]; i++) {
            if (blueVec[i] == 1) bluePawn[i].SetActive(true);
            else bluePawn[i].SetActive(false);
        }

        for (int i = 0; i < saveCount[1]; i++) {
            if (yellowVec[i] == 1) yellowPawn[i].SetActive(true);
            else yellowPawn[i].SetActive(false);
        }

        for (int i = 0; i < saveCount[2]; i++) {
            if (redVec[i] == 1) redPawn[i].SetActive(true);
            else redPawn[i].SetActive(false);
        }
        
        if (!isGameOver) {
            //戻るボタンの表示
            if (stackCount.Count <= 0) {
                prevButton.SetActive(false);
            } else {
                prevButton.SetActive(true);
            }
            if (saveStackCount.Count <= 0) {
                nextButton.SetActive(false);
            } else {
                nextButton.SetActive(true);
            }
            
            //キー受け付け
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                downButton();
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                upButton();
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                plusButton();
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                minusButton();
            } else if (Input.GetKeyDown(KeyCode.Return)) {
                runPlayer();
            }


            //自分のターンじゃなかったらテキストを表示させる
            if (isMyTurn) {
                thinkText.enabled = false;
                loadingImage.enabled = false;
                //クリック処理
                //コピペしただけで理解してない
                if (Input.GetMouseButton(0)) {
                    var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    var raycastHitList = Physics.RaycastAll(ray).ToList();
                    if (raycastHitList.Any()) {
                        var distance = Vector3.Distance(mainCamera.transform.position, raycastHitList.First().point);
                        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                        currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                        currentPosition.y = 0.5f;
                        if (-27.0f <= currentPosition.x && currentPosition.x < -7.5f && -2.0f <= currentPosition.z && currentPosition.z <= 18.0f) {
                            if (colorCount[0] != 0) {
                                playerColorIndex = 0;
                            }
                            fucMaterial();
                        } else if (-7.5f <= currentPosition.x && currentPosition.x < 7.5f && -3.0f <= currentPosition.z && currentPosition.z <= 15.0f) {
                            if (colorCount[1] != 0) {
                                playerColorIndex = 1;
                            }
                            fucMaterial();
                        } else if (7.5f <= currentPosition.x && currentPosition.x <= 27.0f && -2.0f <= currentPosition.z && currentPosition.z <= 18.0f) {
                            if (colorCount[2] != 0) {
                                playerColorIndex = 2;
                            }
                            fucMaterial();
                        }
                        if (runCount > colorCount[playerColorIndex]) runCount = colorCount[playerColorIndex];
                    }
                }
            } else {
                thinkText.enabled = true;
                loadingImage.enabled = true;
                if (countDown > 0) {
                    countDown--;
                } else {
                    if (isPretend == true) {
                        runEnemyFirst();
                        enemyPretendToThink();
                    } else {
                        runEnemySecond();
                    }
                }
            }
            blueText.text = colorCount[0].ToString();
            yellowText.text = colorCount[1].ToString();
            redText.text = colorCount[2].ToString();
            runText.text = runCount.ToString();
        } else {
            if (showingFlag == true) {
                showingCount++;
                showingFlag = false;
            }
            thinkText.enabled = false;
            loadingImage.enabled = false;
            blueText.text = "0";
            yellowText.text = "0";
            redText.text = "0";
            runText.text = "0";
            retryButton.SetActive(true);
            if (!isMyTurn) {
                // victoryText.GetComponent<Outline>().effectColor = new Color(1.0f, 0.0f, 0.0f);
                winText.enabled = true;
            } else {
                // victoryText.GetComponent<Outline>().effectColor = new Color(0.0f, 0.0f, 1.0f);
                loseText.enabled = true;
            }

        }
        
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

    private void RequestInterstitial()
    {
        // ★リリース時に自分のIDに変更する
        #if UNITY_IOS
            string adUnitId = AdmobVariableScript.getIPHONE_INTERSTITIAL();
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdDidRecordImpression += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    // シーン遷移処理
    private void LoadNextScene()
    {
        // SceneManager.LoadScene("Start");
    }

    private void OnDestroy()
    {
        // オブジェクトの破棄
        interstitial.Destroy();
    }

    // ---以下、イベントハンドラー
    
    // 広告の読み込み完了時
    public void HandleOnAdLoaded(object sender, System.EventArgs args)
    {
    }

    // 広告の読み込み失敗時
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // 次のシーンに遷移
        // LoadNextScene();
    }

    // 広告がデバイスの画面いっぱいに表示されたとき
    public void HandleOnAdOpened(object sender, System.EventArgs args)
    {
    }

    // 広告を閉じたとき
    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        // 次のシーンに遷移
        if (retryFlag == true) {
            retryFlag = false;
            RequestInterstitial();
        } else {
            LoadNextScene();
        }
    }
    
    // 別のアプリ（Google Play ストアなど）を起動した時
    public void HandleOnAdLeavingApplication(object sender, System.EventArgs args)
    {
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

    public void minusButton() {
        if (!isMyTurn || isGameOver) return; // 相手のターン、またはゲーム終了時は機能しない
        runCount--;
        if (runCount < 1) runCount = 1;
    }

    public void plusButton() {
        if (!isMyTurn || isGameOver) return; // 相手のターン、またはゲーム終了時は機能しない
        runCount++;
        if (runCount > colorCount[playerColorIndex]) runCount = colorCount[playerColorIndex];
    }

    public void curPlusButton() {
        if (!isMyTurn || isGameOver) return; 
        playerColorIndex = (playerColorIndex + 1) % 3;
    }

    public void upButton() {
        if (!isMyTurn || isGameOver) return; 
        playerColorIndex = (playerColorIndex + 1) % 3;
        while (colorCount[playerColorIndex] == 0) {
            playerColorIndex = (playerColorIndex + 1) % 3;
        }
        if (runCount > colorCount[playerColorIndex]) runCount = colorCount[playerColorIndex];
        fucMaterial();
    }

    public void downButton() {
        if (!isMyTurn || isGameOver) return;
        playerColorIndex = (playerColorIndex + 2) % 3;
        while (colorCount[playerColorIndex] == 0) {
            playerColorIndex = (playerColorIndex + 2) % 3;
        }
        if (runCount > colorCount[playerColorIndex]) runCount = colorCount[playerColorIndex];
        fucMaterial();
    }


    public void retry() {
        if (showingCount > 1) {
            retryFlag = true;
            hadRetryFlag = true;
            interstitial.Show();
            showingCount = 0;
            showingFlag = false;
        }
        init();
        winText.enabled = false;
        loseText.enabled = false;
        colorCount[0] = saveCount[0];
        colorCount[1] = saveCount[1];
        colorCount[2] = saveCount[2];
        for (int i = 0; i < saveCount[0]; i++) blueVec[i] = 1;
        for (int i = 0; i < saveCount[1]; i++) yellowVec[i] = 1;
        for (int i = 0; i < saveCount[2]; i++) redVec[i] = 1;
    }

    public void toTitle() {
        fade.SetActive(true);
        fadeOut = true;
        bannerView.Destroy();
        // // SceneManager.LoadScene("Start");
    }
}
