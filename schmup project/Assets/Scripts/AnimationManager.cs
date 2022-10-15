using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator circleAnimator;

    // When player dies
    public void Die()
    {
        playerAnimator.SetTrigger("isDead");
    }

    // When circle dies
    // not done yet
    public void circleDie()
    {
        circleAnimator.SetTrigger("circleDead");
    }

}
