using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    Vector3 start;
    Vector3 end;
    Vector3 vecStart = new Vector3(1, 1, 1);
    Vector3 vecEnd = new Vector3(2, 2, 2);
    bool moving = false;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 2, 0, Space.World);
        if (moving) {
            float locationTime = (Time.time - timer) / 0.1f;
            transform.localScale = Vector3.Lerp(vecStart, vecEnd, locationTime);
            transform.position = Vector3.Lerp(start, end, locationTime);
            if (locationTime >= 1.0f) moving = false;
        }
    }

    void toSmall() {
        if (transform.localScale != new Vector3(1, 1, 1)) {
            moving = true;
            vecStart = new Vector3(2, 2, 2);
            vecEnd = new Vector3(1, 1, 1);
            start = new Vector3(transform.position.x, 3.5f, transform.position.z);
            end = new Vector3(transform.position.x, 2.0f, transform.position.z);
            timer = Time.time;
        }
    }

    void toBig() {
        if (transform.localScale != new Vector3(2, 2, 2)) {
            moving = true;
            vecStart = new Vector3(1, 1, 1);
            vecEnd = new Vector3(2, 2, 2);
            start = new Vector3(transform.position.x, 2.0f, transform.position.z);
            end = new Vector3(transform.position.x, 3.5f, transform.position.z);
            timer = Time.time;
        }
    }
}
