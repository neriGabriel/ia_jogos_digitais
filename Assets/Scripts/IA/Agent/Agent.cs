﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [Header("Agent")]
    public NavMeshAgent m_Agent;
    public NavMeshAgent m_Target;

    [Header("Wander")]
    public float m_WanderRadius   = 10.0f;
    public float m_WanderDistance = 10.0f;
    public float m_Jitter = 1.0f;
    private Vector3 m_WanderTarget = Vector3.zero;

    [Header("Pursuit")]
    public float m_PursuitRadius = 4.0f;

    [Header("Line of Sight")]
    public float m_MaxAngle = 2.0f;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    public void wander() 
    {
        m_WanderTarget += new Vector3(
                Random.Range(-1.0f, 1.0f) * m_Jitter,    
                0.0f,
                Random.Range(-1.0f, 1.0f) * m_Jitter);
        m_WanderTarget = m_WanderTarget.normalized * m_WanderRadius;
        Vector3 targetLocal = m_WanderTarget + new Vector3(0, 0, m_WanderDistance);
        Vector3 targetWorld = transform.InverseTransformVector(targetLocal);
        m_Agent.SetDestination(targetWorld);
    }

    public void pursuit(Vector3 target) {
        Vector3 direction = target - m_Agent.transform.position;
        float lookAhed = direction.magnitude / (m_Target.speed + m_Agent.speed);
        float rate = Mathf.Clamp01(direction.magnitude / m_PursuitRadius);
        m_Agent.SetDestination(m_Target.transform.position + (m_Target.transform.forward * lookAhed) * rate);
    }

    public bool CanSeeTarget() {
        // POSIÇÃO DO ALGO ATÉ MIM
        Vector3 direction = transform.position - m_Target.transform.position;
        // ANGULO QUE O ALVO CONSEGUE VER
        float lookingAngle = Vector3.Angle(m_Target.transform.forward, direction);
        // RETORNO O VALOR BOOLEANO REFERENTE AO ANGULO
        return lookingAngle < m_MaxAngle;
    }


    public void OnTriggerEnter(Collider other)
    {       
        Destroy(other.gameObject);
    }

    void Update()
    {
        
    }

}
