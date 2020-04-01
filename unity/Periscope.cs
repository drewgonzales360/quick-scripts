using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Periscope : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cinemachine;
    // Update is called once per frame
    public float mouseSensitivity = 100f;
    void FixedUpdate()
    {
        if (cinemachine.activeSelf) {
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;
            transform.localPosition = transform.localPosition + new Vector3(
                mouseX * mouseSensitivity * Time.deltaTime,
                mouseY * mouseSensitivity * Time.deltaTime,
                0
                );
        }
    }
}
