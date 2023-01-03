using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    /// <summary>
    /// makes the explosion animation
    /// </summary>
    public AnimatedSprite start;
    public AnimatedSprite middle;
    public AnimatedSprite end;

    public void SetActiveRenderer(AnimatedSprite renderer)
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }
    //rotate the explosion sprite
    public void SetDir(Vector2 direction)
    {
        float angle=Mathf.Atan2(direction.y,direction.x);
        transform.rotation = Quaternion.AngleAxis(angle*Mathf.Rad2Deg,Vector3.forward);
    }
    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }

}
