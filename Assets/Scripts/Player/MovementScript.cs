using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.CompilerServices;
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
    private float default_speed;
    private float max_speed;
    private bool busy = false;
    private bool running = false;


    private float CD_TIME;

    private float dash_speed;
    private float last_speed;


    //private bool facingUp;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        default_speed = moveSpeed;
        max_speed = default_speed * 1.85f;
        dash_speed = default_speed * 5f;
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




        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            running = !running;

            if (!running)
            { { moveSpeed = default_speed; } }
            else
            { { moveSpeed = max_speed; } }
        }

    

        newMoveDirection.x = (Input.GetAxisRaw("Horizontal") * moveSpeed);
        newMoveDirection.y = (Input.GetAxisRaw("Vertical") * moveSpeed);

        newMoveDirection.Normalize();


        //Vector2(transform.position + (newMoveDirection * Time.deltaTime) * moveSpeed);

        rb.velocity = new Vector2(newMoveDirection.x * moveSpeed, newMoveDirection.y * moveSpeed);

        anim.speed = (moveSpeed/default_speed);

    }

    void Animate()
    {   //can't change this stuff w/o access to ur animator
        anim.SetFloat("MoveX", lastMoveDirection.x);
        anim.SetFloat("MoveY", lastMoveDirection.y);
        anim.SetFloat("MoveMagnitude", lastMoveDirection.magnitude);
       

        if (rb.velocity != Vector2.zero)
        {
            anim.SetFloat("LastMoveX", lastMoveDirection.x);
            anim.SetFloat("LastMoveY", lastMoveDirection.y);
        }

    }


    void DirectionDetection()
    {


        if (anim.GetFloat("MoveX") == -1)
        { facingLeft = true; }

        if (anim.GetFloat("MoveX") == 1)
        { facingLeft = false; }




        // if (newMoveDirection.y > lastMoveDirection.y) facingUp = true;
        // else facingUp = false;
    }

   
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CD_TIME);
        CD_TIME = 0f;
        if (busy) { busy = false; }
    }


 
    void Flip()
    {
        
        Vector3 scale = transform.localScale;
        if (facingLeft) { scale.x = -3; } else { scale.x = 3; }
        transform.localScale = scale;
        //facingLeft = false;
    }
}