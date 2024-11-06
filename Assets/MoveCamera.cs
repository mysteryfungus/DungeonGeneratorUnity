using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float panSpeed = 20f;
    public float zoomSpeed = 1f;

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            float h = -Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float v = -Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
            Camera.main.transform.Translate(h, v, 0);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            Camera.main.orthographicSize -= scroll * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2, 100);
        }
    }
}
