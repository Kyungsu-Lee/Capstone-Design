using UnityEngine;
using System.Collections;


public class CamA : MonoBehaviour
{

    WebCamTexture texture;
    WebCamDevice[] cams;
    GameObject obj;

    // Use this for initialization
    void Start()
    {

        cams = WebCamTexture.devices;
        texture = new WebCamTexture();
        obj = GameObject.FindWithTag("CAMERA");
        obj.GetComponent<Renderer>().material.mainTexture = texture;

        for (int i = 0; i < cams.Length; i++)
        {
          texture.deviceName = cams[i].name;
           texture.Play();
        }

    }

    public void close()
    {
        if (texture != null && texture.isPlaying)
        {
            texture.Stop();
            texture = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}