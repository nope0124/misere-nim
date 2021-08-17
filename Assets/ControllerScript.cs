using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    static int level = 0;
    public GameObject game;
    public GameObject startText;
    public GameObject optionText;
    public GameObject level1Text;
    public GameObject level2Text;
    public GameObject level3Text;
    void Start()
    {
        game.SetActive(true);
        startText.SetActive(true);
        level1Text.SetActive(false);
        level2Text.SetActive(false);
        level3Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void optionButton(){
        SceneManager.LoadScene("Option");
    }
    
    public void startButton(){
        game.SetActive(false);
        startText.SetActive(false);
        level1Text.SetActive(true);
        level2Text.SetActive(true);
        level3Text.SetActive(true);
    }

    public void level1() {
        level = 2;
        SceneManager.LoadScene("Main");
    }

    public void level2() {
        level = 1;
        SceneManager.LoadScene("Main");
    }

    public void level3() {
        level = 0;
        SceneManager.LoadScene("Main");
    }

    public static int getLevel() {
        return level;
    }

}
