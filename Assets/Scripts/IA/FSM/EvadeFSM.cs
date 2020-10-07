using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeFSM : StateMachineBehaviour
{
    private BotAI mBotAI;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mBotAI = animator.gameObject.GetComponent<BotAI>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mBotAI.mBot.evade(mBotAI.mBot.m_Agent.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
