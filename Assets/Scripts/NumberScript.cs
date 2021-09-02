using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NumberScript : MonoBehaviour
{
    static int blueCount = 7;
    static int yellowCount = 6;
    static int redCount = 2;
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
    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(true);
        for (int i = 0; i < 3; i++) {
            if (i == curCount) {
                Pawn[i].SendMessage("toBig");
            } else {
                Pawn[i].SendMessage("toSmall");
            }
        }
    }

    public static int getBlueCount() {
        return blueCount;
    }

    public static int getYellowCount() {
        return yellowCount;
    }

    public static int getRedCount() {
        return redCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn) {
            fadeCount -= Time.deltaTime * 2;
            blueText.text = blueCount.ToString();
            yellowText.text = yellowCount.ToString();
            redText.text = redCount.ToString();
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
                SceneManager.LoadScene("Start");
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            curCount = (curCount + 2) % 3;
            for (int i = 0; i < 3; i++) {
                if (i == curCount) {
                    Pawn[i].SendMessage("toBig");
                } else {
                    Pawn[i].SendMessage("toSmall");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            curCount = (curCount + 1) % 3;
            for (int i = 0; i < 3; i++) {
            if (i == curCount) {
                Pawn[i].SendMessage("toBig");
            } else {
                Pawn[i].SendMessage("toSmall");
            }
        }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            switch(curCount) {
                case 0:
                    blueCount++;
                    if (blueCount > pawnCount) blueCount = pawnCount;
                    break;
                case 1:
                    yellowCount++;
                    if (yellowCount > pawnCount) yellowCount = pawnCount;
                    break;
                case 2:
                    redCount++;
                    if (redCount > pawnCount) redCount = pawnCount;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            switch(curCount) {
                case 0:
                    blueCount--;
                    if (blueCount < 1) blueCount = 1;
                    break;
                case 1:
                    yellowCount--;
                    if (yellowCount < 1) yellowCount = 1;
                    break;
                case 2:
                    redCount--;
                    if (redCount < 1) redCount = 1;
                    break;
            }
        }


        
        


        blueText.text = blueCount.ToString();
        yellowText.text = yellowCount.ToString();
        redText.text = redCount.ToString();
    }

    public void bluePlusButton() {
        blueCount++;
        if (blueCount > pawnCount) blueCount = pawnCount;
        if (blueCount < 1) blueCount = 1;
        Pawn[0].SendMessage("toBig");
        Pawn[1].SendMessage("toSmall");
        Pawn[2].SendMessage("toSmall");
    }

    public void blueMinusButton() {
        blueCount--;
        if (blueCount > pawnCount) blueCount = pawnCount;
        if (blueCount < 1) blueCount = 1;
        Pawn[0].SendMessage("toBig");
        Pawn[1].SendMessage("toSmall");
        Pawn[2].SendMessage("toSmall");
    }

    public void yellowPlusButton() {
        yellowCount++;
        if (yellowCount > pawnCount) yellowCount = pawnCount;
        if (yellowCount < 1) yellowCount = 1;
        Pawn[1].SendMessage("toBig");
        Pawn[0].SendMessage("toSmall");
        Pawn[2].SendMessage("toSmall");
    }

    public void yellowMinusButton() {
        yellowCount--;
        if (yellowCount > pawnCount) yellowCount = pawnCount;
        if (yellowCount < 1) yellowCount = 1;
        Pawn[1].SendMessage("toBig");
        Pawn[0].SendMessage("toSmall");
        Pawn[2].SendMessage("toSmall");
    }

    public void redPlusButton() {
        redCount++;
        if (redCount > pawnCount) redCount = pawnCount;
        if (redCount < 1) redCount = 1;
        Pawn[2].SendMessage("toBig");
        Pawn[0].SendMessage("toSmall");
        Pawn[1].SendMessage("toSmall");
    }

    public void redMinusButton() {
        redCount--;
        if (redCount > pawnCount) redCount = pawnCount;
        if (redCount < 1) redCount = 1;
        Pawn[2].SendMessage("toBig");
        Pawn[0].SendMessage("toSmall");
        Pawn[1].SendMessage("toSmall");
    }

    public void toTitle() {
        fade.SetActive(true);
        fadeOut = true;
    }

    public void blueButton() {
        curCount = 0;
        for (int i = 0; i < 3; i++) {
            if (i == curCount) {
                Pawn[i].SendMessage("toBig");
            } else {
                Pawn[i].SendMessage("toSmall");
            }
        }
    }

    public void yellowButton() {
        curCount = 1;
        for (int i = 0; i < 3; i++) {
            if (i == curCount) {
                Pawn[i].SendMessage("toBig");
            } else {
                Pawn[i].SendMessage("toSmall");
            }
        }
    }

    public void redButton() {
        curCount = 2;
        for (int i = 0; i < 3; i++) {
            if (i == curCount) {
                Pawn[i].SendMessage("toBig");
            } else {
                Pawn[i].SendMessage("toSmall");
            }
        }
    }

    public void randomButton() {
        while (true) {
            if (Random.Range(0, 100) < 85) {
                blueCount = Random.Range(0, 9) + 1;
            } else {
                blueCount = Random.Range(9, pawnCount) + 1;
            }
            if (Random.Range(0, 100) < 85) {
                yellowCount = Random.Range(0, 9) + 1;
            } else {
                yellowCount = Random.Range(9, pawnCount) + 1;
            }
            if (Random.Range(0, 100) < 85) {
                redCount = Random.Range(0, 9) + 1;
            } else {
                redCount = Random.Range(9, pawnCount) + 1;
            }
            if ((int)(blueCount ^ (yellowCount ^ redCount)) != 0) break;
        }
        
    }
}
