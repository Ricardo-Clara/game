using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackState : StateMachineBehaviour
{ 
    private NavMeshAgent navMeshAgent;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent = animator.GetComponent<NavMeshAgent>();
        if (animator.GetInteger("attack") == 0) {
            animator.SetInteger("attack", UnityEngine.Random.Range(1, 3));
        } else if (animator.GetInteger("attack") == 1) {
            animator.SetInteger("attack", UnityEngine.Random.Range(0, 3));
        } else if (animator.GetInteger("attack") == 2) {
            animator.SetInteger("attack", UnityEngine.Random.Range(0, 2));
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent.SetDestination(navMeshAgent.transform.position);
    }

}
