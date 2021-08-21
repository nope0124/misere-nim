using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    public Text victoryText;
    public Text thinkText;
    public Image loadingImage;
    public Text[] decreaseText; 
    public GameObject retryButton;
    public GameObject bluePrefab;
    public GameObject yellowPrefab;
    public GameObject redPrefab;
    public GameObject yellow;
    public Material[] materialSet;
    public MeshRenderer[] m;
    
    int countDown = 0;

    void makePawn(int type, int cnt) {
        switch(cnt) {
            case 1:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15, 1.5f, 5), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15, 1.5f, 5), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15, 1.5f, 5), Quaternion.identity) as GameObject;
                }
                break;
            case 2:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[1] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[1] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[1] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                }
                break;
            case 3:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[1] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[2] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[1] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[2] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[1] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[2] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject; 
                }
                break;
            case 4:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 7.5f), Quaternion.identity) as GameObject;
                    bluePawn[1] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 7.5f), Quaternion.identity) as GameObject;
                    bluePawn[2] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 2.5f), Quaternion.identity) as GameObject;
                    bluePawn[3] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 2.5f), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 7.5f), Quaternion.identity) as GameObject;
                    yellowPawn[1] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 7.5f), Quaternion.identity) as GameObject;
                    yellowPawn[2] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 2.5f), Quaternion.identity) as GameObject;
                    yellowPawn[3] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 2.5f), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 7.5f), Quaternion.identity) as GameObject;
                    redPawn[1] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 7.5f), Quaternion.identity) as GameObject;
                    redPawn[2] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 2.5f), Quaternion.identity) as GameObject;
                    redPawn[3] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 2.5f), Quaternion.identity) as GameObject;
                }
                break;
            case 5:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 4, 1.5f, 9), Quaternion.identity) as GameObject;
                    bluePawn[1] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 4, 1.5f, 9), Quaternion.identity) as GameObject;
                    bluePawn[2] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 4, 1.5f, 1), Quaternion.identity) as GameObject;
                    bluePawn[3] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 4, 1.5f, 1), Quaternion.identity) as GameObject;
                    bluePawn[4] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 4, 1.5f, 9), Quaternion.identity) as GameObject;
                    yellowPawn[1] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 4, 1.5f, 9), Quaternion.identity) as GameObject;
                    yellowPawn[2] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 4, 1.5f, 1), Quaternion.identity) as GameObject;
                    yellowPawn[3] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 4, 1.5f, 1), Quaternion.identity) as GameObject;
                    yellowPawn[4] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 4, 1.5f, 9), Quaternion.identity) as GameObject;
                    redPawn[1] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 4, 1.5f, 9), Quaternion.identity) as GameObject;
                    redPawn[2] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 4, 1.5f, 1), Quaternion.identity) as GameObject;
                    redPawn[3] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 4, 1.5f, 1), Quaternion.identity) as GameObject;
                    redPawn[4] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject;
                }
                break;
            case 6:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[1] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[2] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[3] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[4] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[5] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[1] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[2] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[3] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[4] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[5] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[1] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[2] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[3] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[4] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[5] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                }
                break;
            case 7:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[1] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[2] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[3] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 0, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[4] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[5] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 0), Quaternion.identity) as GameObject;
                    bluePawn[6] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 0), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[1] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[2] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[3] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 0, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[4] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[5] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 0), Quaternion.identity) as GameObject;
                    yellowPawn[6] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 0), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[1] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[2] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[3] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 0, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[4] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[5] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 0), Quaternion.identity) as GameObject;
                    redPawn[6] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 0), Quaternion.identity) as GameObject; 
                }
                break;
            case 8:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[1] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[2] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[3] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[4] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[5] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 0), Quaternion.identity) as GameObject;
                    bluePawn[6] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                    bluePawn[7] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 0), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[1] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[2] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[3] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[4] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[5] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 0), Quaternion.identity) as GameObject;
                    yellowPawn[6] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                    yellowPawn[7] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 0), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[1] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[2] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[3] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[4] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 2.5f, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[5] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 0), Quaternion.identity) as GameObject;
                    redPawn[6] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                    redPawn[7] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 0), Quaternion.identity) as GameObject;
                }
                
                break;
            case 9:
                if (type == 0) {
                    bluePawn[0] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[1] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[2] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    bluePawn[3] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[4] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[5] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    bluePawn[6] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 0), Quaternion.identity) as GameObject;
                    bluePawn[7] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                    bluePawn[8] = Instantiate(bluePrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 0), Quaternion.identity) as GameObject;
                } else if (type == 1) {
                    yellowPawn[0] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[1] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[2] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    yellowPawn[3] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[4] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[5] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    yellowPawn[6] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 0), Quaternion.identity) as GameObject;
                    yellowPawn[7] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                    yellowPawn[8] = Instantiate(yellowPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 0), Quaternion.identity) as GameObject;
                } else if (type == 2) {
                    redPawn[0] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[1] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[2] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 10), Quaternion.identity) as GameObject;
                    redPawn[3] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[4] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[5] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 5), Quaternion.identity) as GameObject;
                    redPawn[6] = Instantiate(redPrefab, new Vector3(type * 15 - 15 - 5, 1.5f, 0), Quaternion.identity) as GameObject;
                    redPawn[7] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 0, 1.5f, 0), Quaternion.identity) as GameObject;
                    redPawn[8] = Instantiate(redPrefab, new Vector3(type * 15 - 15 + 5, 1.5f, 0), Quaternion.identity) as GameObject; 
                }
                break;
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

    void Start()
    {
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
        countDown = 40;
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
                    if (!(xor == 0 || diff == -1)) level--;
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
            if (isMyTurn) {
                thinkText.enabled = false;
                loadingImage.enabled = false;
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
            yellow.SetActive(false);
            if (!isMyTurn) victoryText.text = "You win!!";
            else victoryText.text = "You lose...";
            victoryText.enabled = true;
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

    public void blueButton() {
        if (colorCount[0] != 0) {
            playerColorIndex = 0;
        }
        fucMaterial();
    }

    public void yellowButton() {
        if (colorCount[1] != 0) {
            playerColorIndex = 1;
        }
        fucMaterial();
    }

    public void redButton() {
        if (colorCount[2] != 0) {
            playerColorIndex = 2;
        }
        fucMaterial();
    }


    public void retry() {
        level = saveLevel;
        runCount = 1;
        playerColorIndex = 0;
        fucMaterial();
        isGameOver = false;
        isMyTurn = true;
        retryButton.SetActive(false);
        // yellow.SetActive(true);
        victoryText.enabled = false;
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
