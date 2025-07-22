using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    Animator anim;
    private Vector3 lastMoveDirection;
    private Vector3 newMoveDirection;
    private bool facingLeft;
    //private bool facingUp;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        lastMoveDirection = newMoveDirection;

        newMoveDirection = transform.position;
        DirectionDetection();
        ProcessInputs();
        Animate();
        Flip();

        
    }

    void ProcessInputs()
    {
    
        newMoveDirection.x = (Input.GetAxisRaw("Horizontal") * moveSpeed);
        newMoveDirection.y = (Input.GetAxisRaw("Vertical") * moveSpeed);

        newMoveDirection.Normalize();


        //Vector2(transform.position + (newMoveDirection * Time.deltaTime) * moveSpeed);

        rb.velocity = new Vector2(newMoveDirection.x * moveSpeed, newMoveDirection.y * moveSpeed);
    }

    void Animate()
    {   //can't change this stuff w/o access to ur animator
        anim.SetFloat("MoveX", lastMoveDirection.x);
        anim.SetFloat("MoveY", lastMoveDirection.y);
        anim.SetFloat("MoveMagnitude", lastMoveDirection.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);

        

    }


    void DirectionDetection()
    {


        if (anim.GetFloat("MoveX") == -1)
        { facingLeft = true; }

        if (anim.GetFloat("MoveX") == 1 || anim.GetFloat("MoveX") == 0)
        { facingLeft = false; }




        // if (newMoveDirection.y > lastMoveDirection.y) facingUp = true;
        // else facingUp = false;
    }

    void Flip()
    {
        
        Vector3 scale = transform.localScale;
        if (facingLeft) { scale.x = -3; } else { scale.x = 3; }
        transform.localScale = scale;
        //facingLeft = false;
    }
}