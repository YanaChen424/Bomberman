using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    //list of the sprites in animation
    public Sprite[] animationSprites;
    //defult value,changes according to animation time in unity
    public float animationTime = 0.25f;
    private int animationFrame;

    public bool loop = true;
    public bool idle = true;

    private void Awake()
    {
        //get sprite renderer component from unity
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled=false;
    }
    void Start()
    {
        //invoke NextFrame function to change to the next sprite
        //invoke every animationtime
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }
    private void NextFrame()
    {
        //when we get to the next frame increase index
        animationFrame++;
        //loop back in the end of the animation
        if(loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        if(idle)
        {
            spriteRenderer.sprite=idleSprite;
        //make the animation
        }else if(animationFrame>=0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite=animationSprites[animationFrame];
        }
    }
}
