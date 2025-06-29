using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Update speed parameter based on agent velocity
        if (agent != null && animator != null)
        {
            animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
        }
    }

    public void TriggerAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("attack");
        }
    }

    public void TriggerDamage()
    {
        if (animator != null)
        {
            animator.SetTrigger("damage");
        }
    }
}
