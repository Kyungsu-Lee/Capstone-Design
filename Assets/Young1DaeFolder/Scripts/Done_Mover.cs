using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
    public static GameObject obj;
    public static int guideCount = 0;

    public float speed = 0.1f;
    public GameObject guide;

    private bool flag,aniFlag;
    private GuideDestroy guideDest;
    private GameObject temp;




    void Start ()
	{
        obj = gameObject;
     
        //GetComponent<Rigidbody>().velocity = -new * speed;
    }

    void Update()
    {
        transform.Translate(0, -speed, 0);      

        if (!flag && transform.position.y < 40 && transform.position.y > 20 && guideCount < 4)
        {
            flag = true;
            guideCount++;

            Vector3 spawnPosition = new Vector3(transform.position.x, 7.43f, -0.3f);
            temp = Instantiate(guide, spawnPosition, guide.transform.rotation);
            guideDest = temp.GetComponent<GuideDestroy>();
            guideDest.getObj(obj);

            gameObject.GetComponent<Done_DestroyByContact>().getGuide(temp);
        }


        if (temp != null && !aniFlag && transform.position.y < 20)
        {
            aniFlag = true;
            temp.GetComponent<Animator>().SetTrigger("color");
        }

        if (temp != null)
        {
            if (transform.position.y < 9)
            {
                Destroy(temp);
                --guideCount;
            }
        }

        
    }


    
}
