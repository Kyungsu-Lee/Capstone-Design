using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour {
    public GameObject projectile;
    public float resetSpeed = 0.025f;
    public Rigidbody2D conRig;
    public Transform cam;

    private float resetSpeedSqr;
    private SpringJoint2D spring;
    private ProjectileDragging proscr;

	// Use this for initialization
	void Start () {
        resetSpeedSqr = resetSpeed * resetSpeed;
        spring = projectile.GetComponent<SpringJoint2D>();
	}
	
	// Update is called once per frame
	void Update () { 
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        
        if(spring == null && projectile.GetComponent<Rigidbody2D>().velocity.sqrMagnitude < resetSpeedSqr)
        {
            Reset();
        }
        */
	}

    void OnTriggerExit2D(Collider2D other)
    {

        if(other.GetComponent<Rigidbody2D>() == projectile.GetComponent<Rigidbody2D>())
        {

            Reset();
        }
    }

    public void Reset()
    {
        Done_DestroyByContact.comboCount = 0;
     
        GameObject obj = GameObject.FindGameObjectWithTag("Fire");
        if (obj != null)
        {
            proscr = obj.GetComponent<ProjectileDragging>();
            obj.tag = "Untagged";
            proscr.spring = projectile.AddComponent<SpringJoint2D>();
            proscr.spring.connectedBody = conRig;
            proscr.spring.distance = 1;
            proscr.spring.frequency = 5;
            proscr.spring.autoConfigureDistance = false;
        }
        ProjectileDragging.camFollow = false;
        projectile.transform.position = new Vector3(-0.17f, -0.13f, -0.3f);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
        projectile.GetComponent<Rigidbody2D>().isKinematic = true;
 

        ProjectileDragging.prevLoc = -99;

        cam.position = new Vector3(0f,0f,-15.34f);
    }
}
