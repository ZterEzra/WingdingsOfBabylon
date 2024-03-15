using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    //Direction Movement & Jump
    private float dirX = 0f;
    private float walkspeed = 7f;
    private float jumpspeed = 7f;

    [SerializeField] private LayerMask jumpableGround;
    
    //Sprites
    private SpriteRenderer sprite;
    private Animator anim;

    private enum MovementState { idle,running, jumping, falling }
    
    //Ocupada para verificar Collide con Terrain
    private BoxCollider2D coll; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * walkspeed, rb.velocity.y);
        
        if (Input.GetButton("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
        }
        
        UpdateAnimationState();
    }

    //Se encarga de actualizar las animaciones del personaje dependiendo de su movimiento en X
    private void UpdateAnimationState()
    {
        MovementState state;
        //Movement on X
        if (dirX > 0f)
        {
            state = MovementState.running;
            //Sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            //Sprite.flipX = false;
        }
        else
        {
            state = MovementState.idle;
        }
        //Movement on Y (Jumping)
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y > -.1f)
        {
            state = MovementState.falling;
        }

        //anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .01f,jumpableGround);
    }
}
