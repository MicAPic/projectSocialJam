using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void SetAnimatorTakeDamage()
    {
        animator.SetBool("TakeDamage", true);
    }

    public void SetAnimatorDeath()
    {
        animator.SetBool("Death", true);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
