using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController2 : MonoBehaviour
{
    public float jumpForce = 5.0f;
    public float speed = 1.0f;

    private bool jump;
    private bool grounded = true;
    private bool moving;

    private float moveDirection;

    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(_rigidBody2D.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        Debug.Log(moveDirection);

        if(jump)
        {
            anim.SetFloat("speed", 0.0f);//havadayken yurumesin
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.y, jumpForce);
            
            jump = false;
            //  grounded = false;
            
        }

        _rigidBody2D.velocity = new Vector2(speed*moveDirection, _rigidBody2D.velocity.y);
    }


    private void Update()
    {
        if(grounded &&(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)))
        {
            if(Input.GetKey(KeyCode.A))
            {
                moveDirection = -1.0f;
                _spriteRenderer.flipX = true;
                anim.SetFloat("speed", speed);
            }
            else
            {
                moveDirection = 1.0f;
                _spriteRenderer.flipX = false;
                anim.SetFloat("speed", speed);
            }

        }
        else if(grounded)
        {
            moveDirection = 0.0f;
            anim.SetFloat("speed", 0.0f);
        }


        if(grounded && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
            anim.SetBool("grounded", false);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("floor"))
        {
            grounded = true;
            //jump = false;
        }
    }


}
