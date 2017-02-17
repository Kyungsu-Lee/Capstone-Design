using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorDetection : MonoBehaviour {

	public MeshRenderer WebCamTextureRenderer;
	public int deviceNumber;
	private WebCamTexture _webcamTexture;

	private const int imWidth = 1280;
	private const int imHeight = 720;

	public Sprite image;

	// Use this for initialization
	void Start () {

		WebCamDevice[] devices = WebCamTexture.devices;

		// initialized the webcam texture by the specific device number
		_webcamTexture = new WebCamTexture(devices[deviceNumber].name, imWidth, imHeight);
		// assign webcam texture to the meshrenderer for display
		WebCamTextureRenderer.material.mainTexture = _webcamTexture;

		// Play the video source
		_webcamTexture.Play();
	}

	// Update is called once per frame
	void Update () {

	}

	bool compareTwoColors(WebCamTexture one, Texture2D another, int pixelRange, float subRange)
	{
		Point one_mid 		= new Point (one.width / 2, 	one.height / 2);
		Point another_mid 	= new Point (another.width / 2, another.height / 2);

		bool flag = true;

		for (int i = 0; pixelRange * i <= Mathf.Min (one_mid.x, another_mid.x); i++) {
			for (int j = 0; j * pixelRange <= Mathf.Min (one_mid.y, another_mid.y); j++) {
				flag &= isClose (
					one.GetPixel 	 (one_mid.x + pixelRange * i, one_mid.y + j * pixelRange), 
					another.GetPixel (another_mid.x + pixelRange * i, another_mid.y + j * pixelRange),
					subRange
				);
			}
		}

		return flag;
	}

	bool compareTwoColorsTexture2D(Texture2D one, Texture2D another, int pixelRange, float subRange)
	{
		Point one_mid 		= new Point (one.width / 2, 	one.height / 2);
		Point another_mid 	= new Point (another.width / 2, another.height / 2);

		bool flag = true;

		for (int i = 0; pixelRange * i <= Mathf.Min (one_mid.x, another_mid.x); i++) {
			for (int j = 0; j * pixelRange <= Mathf.Min (one_mid.y, another_mid.y); j++) {
				flag &= isClose (
					one.GetPixel 	 (one_mid.x + pixelRange * i, one_mid.y + j * pixelRange), 
					another.GetPixel (another_mid.x + pixelRange * i, another_mid.y + j * pixelRange),
					subRange
				);
			}
		}

		return flag;
	}

	float compareTwoColorsByPercent(WebCamTexture one, Texture2D another, int pixelRange, float subRange)
	{
		Point one_mid 		= new Point (one.width / 2, 	one.height / 2);
		Point another_mid 	= new Point (another.width / 2, another.height / 2);

		float total 	= 0;
		float correct 	= 0;

		for (int i = 0; pixelRange * i <= Mathf.Min (one_mid.x, another_mid.x); i++) {
			for (int j = 0; j * pixelRange <= Mathf.Min (one_mid.y, another_mid.y); j++) {
				total++;
				if 
					(
						isClose 
						(
							one.GetPixel (one_mid.x + pixelRange * i, one_mid.y + j * pixelRange), 
							another.GetPixel (another_mid.x + pixelRange * i, another_mid.y + j * pixelRange),
							subRange
						)
					)
				{
					correct++;
				}
			}
		}

		return correct/total;
	}


	float compareTwoColorsByPercentSub(WebCamTexture one, Texture2D another, int pixelRange)
	{
		float subRange = 
			one.GetPixel (one.width / 2, one.height / 2).r + one.GetPixel (one.width / 2, one.height / 2).g + one.GetPixel (one.width / 2, one.height / 2).b -
			(another.GetPixel (another.width / 2, another.height / 2).r + another.GetPixel (another.width / 2, another.height / 2).g + another.GetPixel (another.width / 2, another.height / 2).b);

		subRange *= 255/3.0f;

		return compareTwoColorsByPercent (one, another, pixelRange, subRange);
	}

	public float imageCheck(int pixelRange, float subRange)
	{
		return compareTwoColorsByPercent (_webcamTexture, image.texture, pixelRange, subRange);
	}


	bool isClose(Color32 one, Color32 another, float subRange)
	{
		bool flag = true;


		flag &= Mathf.Abs (one.r - another.r) <= subRange;
		flag &= Mathf.Abs (one.g - another.g) <= subRange;
		flag &= Mathf.Abs (one.b - another.b) <= subRange;

		return flag;
	}

    public void close()
    {
        _webcamTexture.Stop();
    }

	public bool IsPlaying {
		get {
			if (_webcamTexture == null || image == null) {
				//Debug.Log ("ss");
				return false;
			}

			return _webcamTexture.isPlaying;
		}
	}

	class Point
	{
		public int x {
			get;
			set;
		}

		public int y {
			get;
			set;
		}

		public Point()
		{

		}

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}


	}
}
