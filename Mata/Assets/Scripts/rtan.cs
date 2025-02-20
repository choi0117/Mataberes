using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rtan : MonoBehaviour
{
    public float speed = 5f; 
    public Camera mainCamera; 
    public Vector2 backgroundSize = new Vector2(50f, 50f); 
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool facingRight = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isNearGamebox = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        bool isMoving = moveInput.magnitude > 0;
        animator.SetBool("isRunning", isMoving);


        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
            facingRight = true;
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
            facingRight = false;
        }

        if (mainCamera != null)
        {
            float camHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float camHalfHeight = mainCamera.orthographicSize;

            float minX = -backgroundSize.x / 2 + camHalfWidth;
            float maxX = backgroundSize.x / 2 - camHalfWidth;
            float minY = -backgroundSize.y / 2 + camHalfHeight;
            float maxY = backgroundSize.y / 2 - camHalfHeight;

            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

            mainCamera.transform.position = new Vector3(clampedX, clampedY, mainCamera.transform.position.z);
        }

        if (isNearGamebox && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("mingame");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("gamebox"))
        {
            isNearGamebox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("gamebox"))
        {
            isNearGamebox = false;
        }
    }
}
