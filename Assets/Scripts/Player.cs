using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    public GameManager manager;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Sprite dyingSprite;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpPower = 0.5f;
    [SerializeField] private float groundRayLenght = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D= GetComponent<BoxCollider2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!manager.GameStarted) { return; }
        Jump();
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit =  Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + groundRayLenght, groundLayer);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
            animator.SetBool("Jumping", false);
        }
        else { rayColor = Color.red; animator.SetBool("Jumping", true); }
        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + groundRayLenght), rayColor);

        if (raycastHit.collider != null && !raycastHit.collider.GetComponent<Platform>().startingPlatform && (raycastHit.collider.GetComponent<Platform>().playerLanded == false)) 
        {
            manager.PlaySoundEffect(1);
            raycastHit.collider.GetComponent<Platform>().playerLanded = true;
            manager.SummonPlatform();
            manager.UpdateScore(1); 
            Destroy(raycastHit.collider.gameObject, 4);
            
        }

        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("GAME OVER");
        animator.enabled= false;
        boxCollider2D.enabled = false;
        spriteRenderer.sprite = dyingSprite;
        rb.velocity = Vector2.up * (jumpPower + 7);

        /*    Destroy(gameObject);*/
        manager.ChangeGameState(1);
    }

    private void Jump()
    {
        if (!IsGrounded()) { return; }

        

        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        {
            manager.PlaySoundEffect(0);
            rb.velocity = Vector2.up * jumpPower;
        }
    }
}
