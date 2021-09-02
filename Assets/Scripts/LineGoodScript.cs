using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGoodScript : MonoBehaviour
{
    public int colorType;
    public Material defaultColor;

    void Start () {
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        if (colorType < 3) {
            renderer.SetVertexCount(6);
            renderer.SetPosition(0, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 8.0f));
            renderer.SetPosition(1, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 8.0f));
            renderer.SetPosition(2, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, -6.0f));
            renderer.SetPosition(3, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, -6.0f));
            renderer.SetPosition(4, new Vector3(7.0f + 14f * (colorType - 1), 0.6f, 8.0f));
            renderer.SetPosition(5, new Vector3(-7.0f + 14f * (colorType - 1), 0.6f, 8.0f));
            renderer.material = defaultColor;
            renderer.SetWidth(0.2f, 0.2f);
        }
        
    }

    void Update()
    {
    }
    
}
