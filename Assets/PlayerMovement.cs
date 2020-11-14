using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gravity = -0.1f;
    public float jumpForce = 1f;
    public float moveForce = 1f;
    public float groundMaxSpeed = 1f;
    public float airModifier = 0.005f;


    private BoxCollider2D collider;
    private Rigidbody2D rb;
    
    public Vector2 vel = new Vector2(0, 0);
    public Vector2 acc;

    public bool onGround = false;
    public bool wasOnGround = false;
    // Start is called before the first frame update
    void Start()
    {
        acc = new Vector2(0, gravity);
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y - collider.bounds.extents.y), new Vector2(0,vel.y));
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - collider.bounds.extents.y), new Vector3(0, vel.y));
        if (hit.distance < vel.magnitude)
        {
        }
        Debug.Log(hit.distance + " help " + vel.magnitude);
        if (!onGround&&wasOnGround)
        {
            acc.y = gravity;
        }
        if(onGround&&!wasOnGround)
        {
            acc.y = 0;
            vel.y = 0;
        }
        transform.position += new Vector3(vel.x,vel.y);
        vel += acc;
        wasOnGround = onGround;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0.5 && onGround)
        {
            vel.y = jumpForce;
        }
        if (onGround)
        {
            vel.x = Input.GetAxis("Horizontal") * moveForce;
        }
        else
        {
            vel.x = Input.GetAxis("Horizontal") * moveForce* airModifier;
        }
        /*  bad movement
        acc.x = Input.GetAxisRaw("Horizontal") * moveForce;
        if (onGround)
        {
            if (vel.x > groundMaxSpeed)
                vel.x = groundMaxSpeed;
            if (vel.x < -groundMaxSpeed)
                vel.x = -groundMaxSpeed;
        }
        else
        {
            float airModifier = 1.2f;
            if (vel.x > groundMaxSpeed*airModifier)
                vel.x = groundMaxSpeed * airModifier;
            if (vel.x < -groundMaxSpeed * airModifier)
                vel.x = -groundMaxSpeed * airModifier;
        }
        */
        rb.MovePosition(transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }
}
