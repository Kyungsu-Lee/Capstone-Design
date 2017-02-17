using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour {

    public float maxStretch = 3.0f;
    public LineRenderer catapultLineFront;
    public LineRenderer catapultLineBack;
    public LineRenderer catapultLineFrontPlus;
    public LineRenderer catapultLineBackPlus;

    public AudioSource audioSrc;
    public AudioClip chargeSound;
    public AudioClip fireSound;

    public static float prevLoc = -99f;
    public static bool camFollow;
    public SpringJoint2D tmpSpring;
    public SpringJoint2D spring;

    private Transform catapult;
    private Ray rayToMouse;
    private Ray leftCatapultToProjectile;
    private float maxStretchSqr;
    private float circleRadius;
    public static bool clickedOn;
    private Vector2 prevVelocity;
    private GameObject reseter;




    void Awake()
    {
        spring = GetComponent<SpringJoint2D>(); 
        catapult = spring.connectedBody.transform;
       
    }
    // Use this for initialization
    void Start () {
        reseter = GameObject.FindGameObjectWithTag("Boundary");
        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
        maxStretchSqr = maxStretch * maxStretch;
        CircleCollider2D circle = GetComponent<Collider2D>() as CircleCollider2D;
        circleRadius = circle.radius;
    }
	
	// Update is called once per frame
	void Update () {
        if (clickedOn)
            Dragging();

        if(spring != null)
        {
            if (!catapultLineFront.enabled)
            {        
                catapultLineFront.enabled = true;
                catapultLineBack.enabled = true;
            }

            if (!GetComponent<Rigidbody2D>().isKinematic && prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude)
            {
                Destroy(spring);
                GetComponent<Rigidbody2D>().velocity = prevVelocity/2.3f;
            }
            if (!clickedOn)
            {          
                prevVelocity = GetComponent<Rigidbody2D>().velocity;
            }
            LineRendererUpdate();
        }
        else
        {

            if (gameObject.tag == "Untagged")
                gameObject.tag = "Fire";

            if (spring == null && transform.position.y < prevLoc)
            {
                // gameObject.tag = "Untagged";
                reseter.GetComponent<Reseter>().Reset();
                return;
            }


            prevLoc = transform.position.y;
            catapultLineFront.enabled = false;
            catapultLineBack.enabled = false;
        }

    }

    void LineRendererSetup()
    {
        catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
        catapultLineBack.SetPosition(0, catapultLineBack.transform.position);

        catapultLineFront.sortingLayerName = "foreground";
        catapultLineBack.sortingLayerName = "foreground";

        catapultLineFront.sortingOrder = 99;
        catapultLineBack.sortingOrder = 99;
    }

    void OnMouseDown()
    {
        audioSrc.clip = chargeSound;
        audioSrc.Play();

        spring.enabled = false;
        clickedOn = true;
    }

    void OnMouseUp()
    {
        audioSrc.clip = fireSound;
        audioSrc.Play();

        spring.enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        clickedOn = false;
        camFollow = true;
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

        if(catapultToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = -0.3f;
        transform.position = mouseWorldPoint;
    }

    void LineRendererUpdate()
    {
        Vector2 captapultToProjectile = transform.position - catapultLineFront.transform.position;
        leftCatapultToProjectile.direction = captapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(captapultToProjectile.magnitude + circleRadius);
        holdPoint.z = -0.2f;

        catapultLineFrontPlus.SetPosition(1, -holdPoint);
        catapultLineBackPlus.SetPosition(1, -holdPoint);
        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);
    }
}
