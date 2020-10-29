﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    Rigidbody rgBody = null;

    private Animator anim;

    public delegate void DropHive(Vector3 pos);
    public static event DropHive DroppedHive;

    private void Start()
    {
        rgBody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DroppedHive?.Invoke(transform.position + (transform.forward * 10));
        }
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        anim.SetFloat("speed", translation);

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Hazard"))
        {
            anim.SetTrigger("died");
        }
        else
        {
            anim.SetTrigger("twitchLeftEar");
        }
    }
}
