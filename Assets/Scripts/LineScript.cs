using System.Collections;
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
        renderer.SetPosition(0, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
        renderer.SetPosition(1, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
        renderer.SetPosition(2, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, -2.0f));
        renderer.SetPosition(3, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, -2.0f));
        renderer.SetPosition(4, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
        renderer.SetPosition(5, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
    }

    void Update()
    {
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        if (MainManager.GetIsGameOver() == false) {
            if (MainManager.GetIsMyTurn() == true) {
                if (MainManager.GetPlayerColorIndex() == colorType) {
                    renderer.SetPosition(0, new Vector3(7.0f + 14f * (colorType - 1), 0.8f, 12.0f));
                    renderer.SetPosition(1, new Vector3(-7.0f + 14f * (colorType - 1), 0.8f, 12.0f));
                    renderer.SetPosition(2, new Vector3(-7.0f + 14f * (colorType - 1), 0.8f, -2.0f));
                    renderer.SetPosition(3, new Vector3(7.0f + 14f * (colorType - 1), 0.8f, -2.0f));
                    renderer.SetPosition(4, new Vector3(7.0f + 14f * (colorType - 1), 0.8f, 12.0f));
                    renderer.SetPosition(5, new Vector3(-7.0f + 14f * (colorType - 1), 0.8f, 12.0f));
                    renderer.material = playerFrameColor;
                    renderer.SetWidth(0.4f, 0.4f);
                } else {
                    renderer.SetPosition(0, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
                    renderer.SetPosition(1, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
                    renderer.SetPosition(2, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, -2.0f));
                    renderer.SetPosition(3, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, -2.0f));
                    renderer.SetPosition(4, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
                    renderer.SetPosition(5, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
                    renderer.material = defaultColor;
                    renderer.SetWidth(0.2f, 0.2f);
                }
            } else {
                    renderer.SetPosition(0, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
                    renderer.SetPosition(1, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
                    renderer.SetPosition(2, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, -2.0f));
                    renderer.SetPosition(3, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, -2.0f));
                    renderer.SetPosition(4, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
                    renderer.SetPosition(5, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
                    renderer.material = defaultColor;
                    renderer.SetWidth(0.2f, 0.2f);
            }
        } else {
            renderer.SetPosition(0, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
            renderer.SetPosition(1, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
            renderer.SetPosition(2, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, -2.0f));
            renderer.SetPosition(3, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, -2.0f));
            renderer.SetPosition(4, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
            renderer.SetPosition(5, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 12.0f));
            renderer.material = defaultColor;
            renderer.SetWidth(0.2f, 0.2f);
        }
        
    }
    
}
