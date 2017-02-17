using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    float gravity = -3.0f;
    Vector3 velocity;

	public bool flag = true;

    Controller2D controller;
    void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    void Update()
    {
		if (flag) {
			velocity.y += gravity * Time.deltaTime;
			controller.Move (velocity * Time.deltaTime);
		}
    }
}
