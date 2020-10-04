using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAi : MonoBehaviour
{
    private Animator m_Anim;

    private void Awake()
   {
       m_Anim = GetComponent<Animator>();
   }
    // Update is called once per frame
    void Update()
    {
       m_Anim.SetBool("isPursuiting", true);
    }
}
