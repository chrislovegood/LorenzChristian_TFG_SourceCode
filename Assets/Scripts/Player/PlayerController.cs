using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    public Vector2 lastMotionVector;
    Animator animator;

    public bool moving;
    public bool frozen = false;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = 0;
        float vertical = 0;

        if(!frozen)
        {
            horizontal =  Input.GetAxisRaw("Horizontal");
            vertical =  Input.GetAxisRaw("Vertical");
        }

        motionVector = new Vector2 (
            horizontal, 
            vertical
        );

        animator.SetFloat("horizontal",  horizontal);
        animator.SetFloat("vertical",  vertical);

        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        if(moving && !frozen)
        {
            lastMotionVector = new Vector2(
                horizontal,
                vertical
            ).normalized;

            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical); 
        }
        
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2d.velocity = motionVector * speed;
    }

    public void setFrozen(int isFrozen){
        frozen = isFrozen == 1 ? true : false;
    }
}


