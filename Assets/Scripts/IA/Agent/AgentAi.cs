﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAi : MonoBehaviour
{
    private Animator m_Anim;
    public Agent mAgent;

    private void Awake()
    {
       m_Anim = GetComponent<Animator>();
       mAgent = GetComponent<Agent>();
    }
    
    void Update()
    {
    }
}