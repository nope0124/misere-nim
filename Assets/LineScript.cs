using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start () {
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        // 線の幅
        renderer.SetWidth(0.2f, 0.2f);
        // 頂点の数
        renderer.SetVertexCount(12);
        // 頂点を設定
        renderer.SetPosition(0, new Vector3(7.5f, 0.6f, 12.5f));
        renderer.SetPosition(1, new Vector3(7.5f, 0.6f, -2.5f));
        renderer.SetPosition(2, new Vector3(22.5f, 0.6f, -2.5f));
        renderer.SetPosition(3, new Vector3(22.5f, 0.6f, 12.5f));
        renderer.SetPosition(4, new Vector3(7.5f, 0.6f, 12.5f));
        renderer.SetPosition(5, new Vector3(-7.5f, 0.6f, 12.5f));
        renderer.SetPosition(6, new Vector3(-7.5f, 0.6f, -2.5f));
        renderer.SetPosition(7, new Vector3(7.5f, 0.6f, -2.5f));
        renderer.SetPosition(8, new Vector3(-7.5f, 0.6f, -2.5f));
        renderer.SetPosition(9, new Vector3(-22.5f, 0.6f, -2.5f));
        renderer.SetPosition(10, new Vector3(-22.5f, 0.6f, 12.5f));
        renderer.SetPosition(11, new Vector3(-7.5f, 0.6f, 12.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
