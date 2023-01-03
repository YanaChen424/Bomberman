using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    Vector2 direction= Vector2.down;
    public float Speed;

    //player defult keys
    public KeyCode inputUp=KeyCode.UpArrow;
    public KeyCode inputDown=KeyCode.DownArrow;
    public KeyCode inputLeft=KeyCode.LeftArrow;
    public KeyCode inputRight=KeyCode.RightArrow;

    //using "sons" in script
    public AnimatedSprite spriteRendererUp;
    public AnimatedSprite spriteRendererDown;
    public AnimatedSprite spriteRendererLeft;
    public AnimatedSprite spriteRendererRight;
    public AnimatedSprite spriteRendererDeath;

    private AnimatedSprite activeSpriteRenderer;
    //using in enemy script
    public static float xPos;
    public static float yPos;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //idle sprite down
        activeSpriteRenderer=spriteRendererDown;
    }
    void Start()
    {
        spriteRendererDeath.enabled = false;
    }


    void Update()
    {
        ////using in enemy script
        xPos = transform.position.x;
        yPos = transform.position.y;

        //call to function according to direction
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up,spriteRendererUp);
        }else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down,spriteRendererDown);
        }else if(Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left,spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right,spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero,activeSpriteRenderer);
        }
    }
    //change player position every physical update
    private void FixedUpdate()
    {
        Vector2 position=rigidbody.position;
        Vector2 translation= direction*Speed*Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }
    //change animation according to direction
    private void SetDirection(Vector2 newDirection,AnimatedSprite spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle=direction == Vector2.zero;
    }
    //death by explosion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }
    //death by enemy
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Enemy")
        {
            DeathSequence();
        }
    }
    //death animation unable
    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled=false;
        spriteRendererDown.enabled=false;
        spriteRendererLeft.enabled=false;
        spriteRendererRight.enabled=false;
        spriteRendererDeath.enabled=true;

        Invoke(nameof(OnDeathSequenceEnded),1.25f);
    }
    //check if the player won
    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<BaseGameManager>().CheckWinState();
    }
}
