using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] public GameObject cameraZero;
    [SerializeField] public GameObject cameraOne;
    private int currentCamera = 0;
    private List<GameObject> Cameras;
    void Start() {
        Cameras = new List<GameObject>() {
            cameraZero,
            cameraOne
        };
    }
    void Update()
    {
        // Press the space key to start coroutine
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cameras[currentCamera].SetActive(false);
            currentCamera = (currentCamera + 1) % Cameras.Count;
            Cameras[currentCamera].SetActive(true);
        }
    }
}
