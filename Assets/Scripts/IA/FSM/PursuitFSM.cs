using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitFSM : StateMachineBehaviour
{
    private AgentAi mAgentAi;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mAgentAi = animator.gameObject.GetComponent<AgentAi>();
       

        mAgentAi.mAgent.m_Target = FlockManager.getTarget(mAgentAi.mAgent.m_Agent).m_Target;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        mAgentAi.mAgent.m_Target = FlockManager.getTarget(mAgentAi.mAgent.m_Agent).m_Target;
        mAgentAi.mAgent.pursuit(mAgentAi.mAgent.m_Agent.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
