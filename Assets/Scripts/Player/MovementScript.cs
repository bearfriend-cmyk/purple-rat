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
    private bool isDashing = false;
    private bool dash_CD = false;


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
        Dash();
        
        
    }

   

    void ProcessInputs()
    {




        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isDashing) { moveSpeed = max_speed; }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        { if (!isDashing) { moveSpeed = default_speed; } }

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

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !dash_CD)
        {
            dash_CD = true;
            isDashing = true;
            CD_TIME = 0.15f;

            last_speed = moveSpeed;

            moveSpeed = dash_speed;
            

            StartCoroutine(Dash_Time());
            CD_TIME = 3f;

            StartCoroutine(Cooldown());
            StartCoroutine(Dash_Cooldown());
        }
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CD_TIME);
        CD_TIME = 0f;
    }


    public IEnumerator Dash_Time()
    {
        yield return new WaitForSeconds(CD_TIME);
        isDashing = false;
        moveSpeed = last_speed;

    }

    public IEnumerator Dash_Cooldown()
    {
        yield return new WaitForSeconds(CD_TIME);
        dash_CD = false;
    }

    void Flip()
    {
        
        Vector3 scale = transform.localScale;
        if (facingLeft) { scale.x = -3; } else { scale.x = 3; }
        transform.localScale = scale;
        //facingLeft = false;
    }
}