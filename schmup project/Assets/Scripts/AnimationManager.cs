using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    public void Die()
    {
        animator.SetTrigger("isDead");
    }

}
