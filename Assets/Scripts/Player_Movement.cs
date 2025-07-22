using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPlatformerMove : MonoBehaviour
{
    //Movement Controls
    private float moveX; //horizontal
    private float moveY; //vertical
    [SerializeField] float moveSpeed;
    [SerializeField] int jumpMax;
    [SerializeField] float jumpHeight;
    [SerializeField] int jumpCount;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject groundCheck;
    private Vector3 moveDir;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    public LayerMask mask;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemisEnemies;
    public float attackRange;
    public int Melee_damage;

    private enum PlayerState
    {
        Free,
        DodgeRoll,
        Attack,
        Death
    }

    private PlayerState myState = PlayerState.Free;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        //This is using if-else statements, but you can also use switch statements
        //Since we're using RigidBody, put all movement in FixedUpdate
        if (myState == PlayerState.Free)
        {
            PlayerFree();
        }

        else if (myState == PlayerState.DodgeRoll)
        {
            PlayerDodgeroll();
        }

        else if (myState == PlayerState.Attack)
        {
            PlayerAttack();
        }

        else
        {
            PlayerDeath();
        }

    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

    }

    void PlayerFree()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        playerRB.velocity = new Vector2(moveY * moveSpeed, playerRB.velocity.y);
        playerRB.velocity = new Vector2(moveX * moveSpeed, playerRB.velocity.x);  
    }

    void PlayerDodgeroll()
    {

    }

    void PlayerAttack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
              
            }
            timeBtwAttack = startTimeBtwAttack;
           } 
            else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void PlayerDeath()
    {

    }
}