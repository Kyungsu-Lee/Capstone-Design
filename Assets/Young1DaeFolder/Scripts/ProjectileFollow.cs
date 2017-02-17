using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFollow : MonoBehaviour {

    public Transform projectile;
    public Transform farLeft;
    public Transform farRight;

    void Start()
    {
        Screen.SetResolution(1440, 2560, true);
    }

    void Update()
    {
        if (ProjectileDragging.camFollow)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = projectile.position.x;
            newPosition.y = projectile.position.y;
            newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x, farRight.position.x); // keeps value in specific range
            transform.position = newPosition;
        }
    }
}
