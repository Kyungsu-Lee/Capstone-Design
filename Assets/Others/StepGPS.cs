using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepGPS : MonoBehaviour {

  
    public Button StepStart;
  
    public Button noGps;

	bool gpsInit = false;
	LocationInfo currentGPSPosition;

	void Start()
	{
		
	}

	bool flag = false;

	void Update()
	{
		if (!gpsInit)
		{
			//Starting the Location service before querying location
			Input.location.Start (0.5f);

			//Checks if the GPS is enabled by the user (-> Allow location )
			if (Input.location.isEnabledByUser) {

				if (Input.location.status == LocationServiceStatus.Initializing && !flag)
					flag = true;


				if (flag) {
					//GPS 안켜져 있으면
					if (Input.location.status == LocationServiceStatus.Failed) {
                        noGps.gameObject.SetActive(true);
                        //text_m.text = "GPS not available";
					//GPS 켜져 있으면
					} else {
						gpsInit = true;
						//RetrieveGPSData 함수 실ㅇ
						InvokeRepeating ("RetrieveGPSData", 0.0001f, 3);
						Input.location.Stop ();
					}
				}

			} else {
                noGps.gameObject.SetActive(true);
                //text_m.text = "GPS not available";
                //btn01.gameObject.SetActive (false);
                //btn9R.gameObject.SetActive (false);
                //btnNTILT.gameObject.SetActive (false);
            }
		}
	}

	void RetrieveGPSData()
	{
		//읽어들인 위도와 경도 input
		currentGPSPosition = Input.location.lastData;
		string gpsString = "::" + currentGPSPosition.latitude + "//" + currentGPSPosition.longitude;
		//text_m.text = gpsString;
		//영일대 gps 
		if (currentGPSPosition.latitude > 36.06100 && currentGPSPosition.latitude < 36.06225 && currentGPSPosition.longitude > 129.38201 && currentGPSPosition.longitude < 129.38396) {
			StepStart.gameObject.SetActive (false);
			noGps.gameObject.SetActive (true);
		} 
		//구룡포 gps
		else if (currentGPSPosition.latitude > 35.98941 && currentGPSPosition.latitude < 35.99127 && currentGPSPosition.longitude > 129.56053 && currentGPSPosition.longitude < 129.56282) {
			StepStart.gameObject.SetActive (true);
			noGps.gameObject.SetActive (false);
		} 
		// 뉴타운 ILT gps
		else if (currentGPSPosition.latitude > 22.57800 && currentGPSPosition.latitude < 22.58268 && currentGPSPosition.longitude > 88.47841 && currentGPSPosition.longitude < 88.48235) {
            //영일대만 시작
            StepStart.gameObject.SetActive(false);
            noGps.gameObject.SetActive(true); 
            //btn01.gameObject.SetActive (false);
            //btn9R.gameObject.SetActive (false);
            //btnNTILT.gameObject.SetActive (true);
        }
        else if (currentGPSPosition.latitude > 22.51310 && currentGPSPosition.latitude < 22.51630 && currentGPSPosition.longitude > 88.32301 && currentGPSPosition.longitude < 88.32617)
        {
            //구룡포 시작
            StepStart.gameObject.SetActive(true);
            noGps.gameObject.SetActive(false);
        }
        // 그 어디도 아닐 때
        else {
            StepStart.gameObject.SetActive(false);
            noGps.gameObject.SetActive(true);
            
		}
	}
}
