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

    int curCount = 0;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            curCount--;
            if (curCount < 0) curCount = 0;
            for (int i = 0; i < 3; i++) {
                if (i == curCount) {
                    Pawn[i].SendMessage("toBig");
                } else {
                    Pawn[i].SendMessage("toSmall");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            curCount++;
            if (curCount > 2) curCount = 2;
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
                    if (blueCount > 9) blueCount = 9;
                    break;
                case 1:
                    yellowCount++;
                    if (yellowCount > 9) yellowCount = 9;
                    break;
                case 2:
                    redCount++;
                    if (redCount > 9) redCount = 9;
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


        
        


        blueText.text = blueCount.ToString() + "本";
        yellowText.text = yellowCount.ToString() + "本";
        redText.text = redCount.ToString() + "本";
    }

    public void bluePlusButton() {
        blueCount++;
        if (blueCount > 9) blueCount = 9;
        if (blueCount < 1) blueCount = 1; 
    }

    public void blueMinusButton() {
        blueCount--;
        if (blueCount > 9) blueCount = 9;
        if (blueCount < 1) blueCount = 1; 
    }

    public void yellowPlusButton() {
        yellowCount++;
        if (yellowCount > 9) yellowCount = 9;
        if (yellowCount < 1) yellowCount = 1; 
    }

    public void yellowMinusButton() {
        yellowCount--;
        if (yellowCount > 9) yellowCount = 9;
        if (yellowCount < 1) yellowCount = 1; 
    }

    public void redPlusButton() {
        redCount++;
        if (redCount > 9) redCount = 9;
        if (redCount < 1) redCount = 1; 
    }

    public void redMinusButton() {
        redCount--;
        if (redCount > 9) redCount = 9;
        if (redCount < 1) redCount = 1;
    }

    public void backButton() {
        SceneManager.LoadScene("Start");
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
            blueCount = Random.Range(0, 9) + 1;
            yellowCount = Random.Range(0, 9) + 1;
            redCount = Random.Range(0, 9) + 1;
            if ((int)(blueCount ^ (yellowCount ^ redCount)) != 0) break;
        }
        
    }
}
