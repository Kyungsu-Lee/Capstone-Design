using UnityEngine;

using System.Collections;

public class MiniMapCam : MonoBehaviour
{

    public GameObject tank;

    public GUITexture redRect;

    public Camera miniCam;

    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        Vector3 viewPortPos = miniCam.WorldToViewportPoint(tank.transform.position);

        redRect.transform.position = viewPortPos;
    }

}


