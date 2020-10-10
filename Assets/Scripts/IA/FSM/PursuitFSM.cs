using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitFSM : StateMachineBehaviour
{
    private AgentAi mAgentAi;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mAgentAi = animator.gameObject.GetComponent<AgentAi>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        mAgentAi.mAgent.pursuit(mAgentAi.mAgent.m_Agent.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
