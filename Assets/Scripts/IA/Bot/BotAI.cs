using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    private Animator m_Anim;
    public Bot mBot;

    private void Awake()
    {
       m_Anim = GetComponent<Animator>();
       mBot = GetComponent<Bot>();
    }
    // Update is called once per frame
    void Update()
    {
        if(mBot.TargetCanSeeMe()) {
            m_Anim.SetBool("isEvade", true);
        }
    }
}
