using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    int time = 0;
    static int[] colorCount = {7, 6, 2};
    static int[] saveCount = {7, 6, 2}; //const、リセット、記憶しておくやつ
    static int saveLevel = 0; // 猶予
    static int level = 0; // レベル
    static int[] blueVec; // 青コマの配列
    static int[] yellowVec; // 黄コマの配列
    static int[] redVec; //赤コマの配列
    GameObject[] bluePawn;
    GameObject[] yellowPawn;
    GameObject[] redPawn;
    static string[] colorName = {"青", "黄", "赤"}; // 色名
    int runCount = 1;
    static int curCount = 0;
    public Text blueText;
    public Text yellowText;
    public Text redText;
    public Text runText;
    // public Text curText;
    public Text victoryText;
    public Text turnText;
    public GameObject retryButton;
    public GameObject bluePrefab;
    public GameObject yellowPrefab;
    public GameObject redPrefab;
    public GameObject yellow;
    public Material[] materialSet;
    public MeshRenderer[] m;
    bool isOver = false;
    bool isTurn = true;
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

    public static int[] getBlueVec() {
        return blueVec;
    }

    public static int[] getYellowVec() {
        return yellowVec;
    }

    public static int[] getRedVec() {
        return redVec;
    }

    void runEnemy() {
        int[] vec = new int[3];
        for (int i = 0; i < 3; i++) vec[i] = colorCount[i];
        System.Array.Sort(vec);
        int type = -1;
        if (vec[0] == 0 && vec[1] == 0 && vec[2] > 1) type = 1;
        else if (vec[0] == 0 && vec[1] == 1 && vec[2] > 1) type = 2;
        else if (vec[0] == 1 && vec[1] == 1 && vec[2] > 1) type = 1;
        else type = 3;
        int cnt = 0;
        int ind = 0;
        switch(type){
            case 1:
                for (int i = 0; i < 3; i++) {
                    if (colorCount[i] > 1) {
                        ind = i;
                        cnt = colorCount[i] - 1;
                        colorCount[i] = 1;
                    }
                }
                for (int i = 0; i < cnt; i++) {
                    int tmp = Random.Range(0, saveCount[ind]);
                    switch(ind) {
                        case 0:
                            while (blueVec[tmp] == 0) {
                                tmp = (tmp + 1) % saveCount[ind];
                            }
                            blueVec[tmp] = 0;
                            break;
                        case 1:
                            while (yellowVec[tmp] == 0) {
                                tmp = (tmp + 1) % saveCount[ind];
                            }
                            yellowVec[tmp] = 0;
                            break;
                        case 2:
                            while (redVec[tmp] == 0) {
                                tmp = (tmp + 1) % saveCount[ind];
                            }
                            redVec[tmp] = 0;
                            break;
                    }
                }
                break;
            
            case 2:
                for (int i = 0; i < 3; i++) {
                    if (colorCount[i] > 1) {
                        ind = i;
                        cnt = colorCount[i];
                        colorCount[i] = 0;
                    }
                }
                for (int i = 0; i < cnt; i++) {
                    int tmp = Random.Range(0, saveCount[ind]);
                    switch(ind) {
                        case 0:
                            while (blueVec[tmp] == 0) {
                                tmp = (tmp + 1) % saveCount[ind];
                            }
                            blueVec[tmp] = 0;
                            break;
                        case 1:
                            while (yellowVec[tmp] == 0) {
                                tmp = (tmp + 1) % saveCount[ind];
                            }
                            yellowVec[tmp] = 0;
                            break;
                        case 2:
                            while (redVec[tmp] == 0) {
                                tmp = (tmp + 1) % saveCount[ind];
                            }
                            redVec[tmp] = 0;
                            break;
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
                            ind = index;
                        }
                    }
                }
                if (xor == 0 || diff == -1 || level > 0) {
                    if (!(xor == 0 || diff == -1)) level--;
                    ind = Random.Range(0, 3);
                    while (colorCount[ind] == 0) {
                        ind = (ind + 1) % 3;
                    }
                    cnt = Random.Range(0, colorCount[ind]) + 1;
                    colorCount[ind] -= cnt;
                    for (int i = 0; i < cnt; i++) {
                        int tmp = Random.Range(0, saveCount[ind]);
                        switch(ind) {
                            case 0:
                                while (blueVec[tmp] == 0) {
                                    tmp = (tmp + 1) % saveCount[ind];
                                }
                                blueVec[tmp] = 0;
                                break;
                            case 1:
                                while (yellowVec[tmp] == 0) {
                                    tmp = (tmp + 1) % saveCount[ind];
                                }
                                yellowVec[tmp] = 0;
                                break;
                            case 2:
                                while (redVec[tmp] == 0) {
                                    tmp = (tmp + 1) % saveCount[ind];
                                }
                                redVec[tmp] = 0;
                                break;
                        }
                    }
                } else {
                    cnt = colorCount[ind] - diff;
                    colorCount[ind] -= cnt;
                    for (int i = 0; i < cnt; i++) {
                        int tmp = Random.Range(0, saveCount[ind]);
                        switch(ind) {
                            case 0:
                                while (blueVec[tmp] == 0) {
                                    tmp = (tmp + 1) % saveCount[ind];
                                }
                                blueVec[tmp] = 0;
                                break;
                            case 1:
                                while (yellowVec[tmp] == 0) {
                                    tmp = (tmp + 1) % saveCount[ind];
                                }
                                yellowVec[tmp] = 0;
                                break;
                            case 2:
                                while (redVec[tmp] == 0) {
                                    tmp = (tmp + 1) % saveCount[ind];
                                }
                                redVec[tmp] = 0;
                                break;
                        }
                    }
                }
                break;
        }
        if (colorCount[0] + colorCount[1] + colorCount[2] == 0) return;
        while (colorCount[curCount] == 0) {
            curCount = (curCount + 1) % 3;
        }
        fucMaterial();
    }

    public static int getCurCount() {
        return curCount;
    }

    void fucMaterial() {
        for (int i = 0; i < 2; i++) {
            m[i].material = materialSet[curCount];
        }
    }
    // Update is called once per frame
    void Update()
    {


        // if (Input.GetMouseButtonUp(0))
        // {
        //     Vector3 mousePosition = Input.mousePosition;
        //     if (300 <= mousePosition.y && mousePosition.y <= 550) {
        //         if (180 <= mousePosition.x && mousePosition.x < 450) curCount = 0;
        //         if (450 <= mousePosition.x && mousePosition.x < 750) curCount = 1;
        //         if (750 <= mousePosition.x && mousePosition.x < 1020) curCount = 2;
        //         fucMaterial();
        //     }
        // }
        

        
        if (isTurn) {
            turnText.text = "ターン：あなた";
        } else {
            turnText.text = "ターン：ＣＰＵ";
        }
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
        if (Input.GetKeyDown(KeyCode.Return)) {
            run();
        }
        if (colorCount[0] == 0 && colorCount[1] == 0 && colorCount[2] == 0) isOver = true;
        if (!isOver) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                curCount--;
                if (curCount < 0) curCount = 2;
                while (colorCount[curCount] == 0) {
                    curCount = (curCount + 2) % 3;
                }
                fucMaterial();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                curCount++;
                if (curCount > 2) curCount = 0;
                while (colorCount[curCount] == 0) {
                    curCount = (curCount + 1) % 3;
                }
                fucMaterial();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                runCount++;
                if (runCount > colorCount[curCount]) runCount = colorCount[curCount];
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                runCount--;
                if (runCount < 1) runCount = 1;
            }
            
            while (colorCount[curCount] == 0) {
                curCount = (curCount + 1) % 3;
            }
            
            if (runCount > colorCount[curCount]) runCount = colorCount[curCount];
            if (!isTurn) {
                if (countDown > 0) {
                    countDown--;
                } else {
                    runEnemy();
                    isTurn = true;
                }
            }
            blueText.text = colorCount[0].ToString() + "本";
            yellowText.text = colorCount[1].ToString() + "本";
            redText.text = colorCount[2].ToString() + "本";
            runText.text = runCount.ToString();
            // curText.text = colorName[curCount];
        } else {
            blueText.text = "0本";
            yellowText.text = "0本";
            redText.text = "0本";
            runText.text = "0";
            retryButton.SetActive(true);
            yellow.SetActive(false);
            if (isTurn) victoryText.text = "You win!!";
            else victoryText.text = "You lose...";
            victoryText.enabled = true;
        }
        
        
    }

    public void minusButton() {
        if (!isTurn || isOver) return; 
        runCount--;
        if (runCount < 1) runCount = 1;
    }

    public void plusButton() {
        if (!isTurn || isOver) return; 
        runCount++;
    }

    public void curPlusButton() {
        if (!isTurn || isOver) return; 
        curCount = (curCount + 1) % 3;
    }

    public void upButton() {
        if (!isTurn || isOver) return; 
        if (colorCount[(curCount + 1) % 3] != 0) curCount = (curCount + 1) % 3;
        fucMaterial();
    }

    public void downButton() {
        if (!isTurn || isOver) return; 
        if (colorCount[(curCount + 2) % 3] != 0) curCount = (curCount + 2) % 3;
        fucMaterial();
    }

    public void run() {
        if (!isTurn || isOver) return; 
        isTurn = false;
        countDown = 30 + Random.Range(0, 60);
        for (int i = 0; i < runCount; i++) {
            int tmp = Random.Range(0, saveCount[curCount]);
            switch(curCount) {
                case 0:
                    while (blueVec[tmp] == 0) {
                        tmp = (tmp + 1) % saveCount[curCount];
                    }
                    blueVec[tmp] = 0;
                    break;
                case 1:
                    while (yellowVec[tmp] == 0) {
                        tmp = (tmp + 1) % saveCount[curCount];
                    }
                    yellowVec[tmp] = 0;
                    break;
                case 2:
                    while (redVec[tmp] == 0) {
                        tmp = (tmp + 1) % saveCount[curCount];
                    }
                    redVec[tmp] = 0;
                    break;
            }
        }
        colorCount[curCount] -= runCount;
        if (colorCount[curCount] < 0) colorCount[curCount] = 0;
        if (colorCount[0] == 0 && colorCount[1] == 0 && colorCount[2] == 0) return;
        while (colorCount[curCount] == 0) {
            curCount = (curCount + 1) % 3;
        }
    }

    public void blueButton() {
        if (colorCount[0] != 0) {
            curCount = 0;
        }
        fucMaterial();
    }

    public void yellowButton() {
        if (colorCount[1] != 0) {
            curCount = 1;
        }
        fucMaterial();
    }

    public void redButton() {
        if (colorCount[2] != 0) {
            curCount = 2;
        }
        fucMaterial();
    }


    public void retry() {
        level = saveLevel;
        runCount = 1;
        curCount = 0;
        fucMaterial();
        isOver = false;
        isTurn = true;
        retryButton.SetActive(false);
        yellow.SetActive(true);
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
