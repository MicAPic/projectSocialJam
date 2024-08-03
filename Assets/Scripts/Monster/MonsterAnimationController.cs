using UnityEngine;

namespace Monster
{
    public class MonsterAnimationController : MonoBehaviour
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
    }
}
