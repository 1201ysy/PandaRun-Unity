using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    private BoxCollider2D bc;
    [SerializeField] private float jumpForce = 20f;


    private float slideStartTime;
    private float slideDuration = 0.8f;
    private bool isGrounded = false;
    private bool isSliding = false;

    private float slidePosY = -2.86f;
    private Vector2 slideBoxColliderOffset = new Vector2(0.18f, -0.27f);
    private Vector2 slideBoxColliderSize = new Vector2(2.27f, 1.47f);

    private float initPosY;

    private Vector2 initBoxColliderOffset;
    private Vector2 initBoxColliderSize;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver == false)
        {
            if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
            {
                Jump();
            }
            else if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
            {
                SlideBegin();
            }

            if (isSliding && Time.time - slideStartTime >= slideDuration)
            {
                SlideEnd();
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && !isSliding)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void Jump()
    {
        isGrounded = false;
        rb.velocity = Vector2.up * jumpForce;
        animator.SetBool("isJumping", true);
    }

    private void SlideBegin()
    {
        isSliding = true;
        isGrounded = false;
        slideStartTime = Time.time;
        initPosY = transform.position.y;
        transform.position = new Vector3(transform.position.x, slidePosY, transform.position.z);

        initBoxColliderOffset = bc.offset;
        initBoxColliderSize = bc.size;

        bc.offset = slideBoxColliderOffset;
        bc.size = slideBoxColliderSize;
        animator.SetBool("isSliding", true);
    }

    private void SlideEnd()
    {
        isSliding = false;
        isGrounded = true;
        transform.position = new Vector3(transform.position.x, initPosY, transform.position.z);


        bc.offset = initBoxColliderOffset;
        bc.size = initBoxColliderSize;
        animator.SetBool("isSliding", false);

    }

    public void SetGameOver()
    {
        Jump();
        animator.enabled = false;
        bc.enabled = false;
        Destroy(gameObject, 2f);
    }
}
