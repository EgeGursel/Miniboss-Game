using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    Transform groundCheck;
    public LayerMask collidableLayer;
    public float speed = 2.4f;
    public float distance = 1.6f;
    private float distanceToPlayer;
    private Transform player;
    private Rigidbody2D rb;
    Boss boss;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        groundCheck = animator.gameObject.transform.GetChild(0);
        boss = animator.GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        distanceToPlayer = Vector2.Distance(player.position, rb.position);
        if (Physics2D.OverlapCircle(groundCheck.position, 0.1f, collidableLayer))
        {
            if (distanceToPlayer >= distance)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            else if (distanceToPlayer < distance)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
