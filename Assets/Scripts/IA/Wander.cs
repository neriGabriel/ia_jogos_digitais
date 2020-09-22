using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    [Header("Agent")]
    private NavMeshAgent m_Agent;

    [Header("Wander")]
    public float m_WanderRadius   = 10.0f;
    public float m_WanderDistance = 10.0f;
    public float m_Jitter = 1.0f;
    private Vector3 m_WanderTarget = Vector3.zero;

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

    void Update()
    {
        wander();
    }
}
