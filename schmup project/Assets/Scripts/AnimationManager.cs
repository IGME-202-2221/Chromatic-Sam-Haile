using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator playerAnimator;

    // When player dies
    public void Die()
    {
        playerAnimator.SetTrigger("isDead");
    }
}
