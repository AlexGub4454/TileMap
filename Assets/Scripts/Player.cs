using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour
{ 

   

[SerializeField] float jumpVelocity = 5;
    [SerializeField] float speed=5;
    [SerializeField] float climdingSpeed = 5f;
 
    Rigidbody2D rigidbody;
    bool isAlive = true;
    Animator playerAnimator;
    CapsuleCollider2D collider;
    CircleCollider2D boxCollider;
    float gravityScaleAtStart;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<CircleCollider2D>();
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = rigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Climbing();
        Jump();
        Run();
        FlipSprite();
        Die();


    }
    private void Die()
    {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) return;
        
        playerAnimator.SetTrigger("Dying");
        rigidbody.velocity = new Vector2(10, 25);
       // rigidbody.isKinematic = true;
        isAlive = false;
        FindObjectOfType<SceneController>().TakeLife();
    }
    private void Climbing()
    {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { 
            playerAnimator.SetBool("Climbing", false);
            rigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        rigidbody.gravityScale = 0f;
        float crossThrow = CrossPlatformInputManager.GetAxis("Vertical");
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, crossThrow * climdingSpeed);
        bool playerHorSpeed = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("Climbing", playerHorSpeed);
    }
    void Run()
    {
      float crossThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(crossThrow*speed, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity;
        bool playerHorSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        
            playerAnimator.SetBool("Running", playerHorSpeed);
      
    }

    private void Jump()
    {
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 velocityJump = new Vector2(0, jumpVelocity);
            rigidbody.velocity += velocityJump;
        }
    }
    private void FlipSprite()
    {
        bool playerHorSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHorSpeed)
        {
            transform.localScale = new Vector2(3f*Mathf.Sign(rigidbody.velocity.x), 3f);
        }
    }
}
