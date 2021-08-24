using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainControllerScript : MonoBehaviour
{
    int time = 0;
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
    public Material[] materialSet;
    public MeshRenderer[] m;
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
        {-3.0f, 0.0f, 3.0f, -2.0f, 2.0f, -3.0f, 0.0f, 3.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // 8
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


    private Camera mainCamera;
    private Vector3 currentPosition = Vector3.zero;


    void makePawn(int type, int cnt) {
        for (int i = 0; i < cnt; i++) {
            if (type == 0) {
                bluePawn[i] = Instantiate(bluePrefab, new Vector3((type - 1) * 15 + pawnPositionX[cnt-1, i], 1.5f, pawnPositionZ[cnt-1, i] + 5.0f), Quaternion.identity) as GameObject;
            } else if (type == 1) {
                yellowPawn[i] = Instantiate(yellowPrefab, new Vector3((type - 1) * 15 + pawnPositionX[cnt-1, i], 1.5f, pawnPositionZ[cnt-1, i] + 5.0f), Quaternion.identity) as GameObject;
            } else if (type == 2) {
                redPawn[i] = Instantiate(redPrefab, new Vector3((type - 1) * 15 + pawnPositionX[cnt-1, i], 1.5f, pawnPositionZ[cnt-1, i] + 5.0f), Quaternion.identity) as GameObject;
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
            stackIndex.Push(saveStackIndex.Pop());
            stackCount.Push(saveStackCount.Pop());
        }
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
        for (int i = 0; i < 2; i++) {
            m[i].material = materialSet[playerColorIndex];
        }
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

    void Start()
    {
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
    }

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
        endJudge();
        isPretend = true;
    }

    void Update()
    {
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
                        currentPosition.y = 0.6f;
                        if (-27.0f <= currentPosition.x && currentPosition.x < -8.0f && -2.0f <= currentPosition.z && currentPosition.z <= 18.0f) {
                            if (colorCount[0] != 0) {
                                playerColorIndex = 0;
                            }
                            fucMaterial();
                        } else if (-8.0f <= currentPosition.x && currentPosition.x < 8.0f && -3.0f <= currentPosition.z && currentPosition.z <= 15.0f) {
                            if (colorCount[1] != 0) {
                                playerColorIndex = 1;
                            }
                            fucMaterial();
                        } else if (8.0f <= currentPosition.x && currentPosition.x <= 27.0f && -2.0f <= currentPosition.z && currentPosition.z <= 18.0f) {
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
        SceneManager.LoadScene("Start");
    }
}
