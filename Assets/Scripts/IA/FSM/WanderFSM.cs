﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WanderFSM : StateMachineBehaviour
{

    private AgentAi mAgentAi;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mAgentAi = animator.gameObject.GetComponent<AgentAi>();
        
        mAgentAi.mAgent.m_Target = FlockManager.getTarget(mAgentAi.mAgent.m_Agent).m_Target;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mAgentAi.mAgent.m_Target = FlockManager.getTarget(mAgentAi.mAgent.m_Agent).m_Target;
        mAgentAi.mAgent.wander();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
