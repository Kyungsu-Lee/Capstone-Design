using UnityEngine;
using System.Collections;

public class GuideDestroy : MonoBehaviour
{
    private GameObject objt;
    private bool flag;
    void Update()
    {
        if (flag && objt == null)
        {
            --Done_Mover.guideCount;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.tag == "Fire")
            return;
        else if(other.tag != "Guide" && other == objt.GetComponent<BoxCollider2D>())
        {
        
            --Done_Mover.guideCount;
            Destroy(gameObject);
        }

    }

    public void getObj(GameObject Obj)
    {
        flag = true;
        objt = Obj;
    }
}