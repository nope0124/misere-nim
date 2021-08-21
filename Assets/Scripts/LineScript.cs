﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public int colorType;
    public Material playerFrameColor;
    public Material defaultColor;

    void Start () {
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        renderer.SetVertexCount(6);
        renderer.SetPosition(0, new Vector3(7.0f + 15f * (colorType - 1), 0.6f, 12.0f));
        renderer.SetPosition(1, new Vector3(-7.0f + 15f * (colorType - 1), 0.6f, 12.0f));
        renderer.SetPosition(2, new Vector3(-7.0f + 15f * (colorType - 1), 0.6f, -2.0f));
        renderer.SetPosition(3, new Vector3(7.0f + 15f * (colorType - 1), 0.6f, -2.0f));
        renderer.SetPosition(4, new Vector3(7.0f + 15f * (colorType - 1), 0.6f, 12.0f));
        renderer.SetPosition(5, new Vector3(-7.0f + 15f * (colorType - 1), 0.6f, 12.0f));
    }

    void Update()
    {
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        if (MainControllerScript.getIsGameOver() == false) {
            if (MainControllerScript.getIsMyTurn() == true) {
                if (MainControllerScript.getPlayerColorIndex() == colorType) {
                    renderer.material = playerFrameColor;
                    renderer.SetWidth(0.4f, 0.4f);
                } else {
                    renderer.material = defaultColor;
                    renderer.SetWidth(0.2f, 0.2f);
                }
            } else {
                if (MainControllerScript.getEnemyColorIndex() == colorType) {
                    renderer.material = defaultColor;
                    renderer.SetWidth(0.2f, 0.2f);
                } else {
                    renderer.material = defaultColor;
                    renderer.SetWidth(0.2f, 0.2f);
                }
            }
        } else {
            renderer.material = defaultColor;
            renderer.SetWidth(0.2f, 0.2f);
        }
        
    }
    
}
