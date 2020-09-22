using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pursuit : MonoBehaviour
{
    private NavMeshAgent m_Target;
    public NavMeshAgent m_Agent;

    [Header("Pursuit")]
    public float m_PursuitRadius = 4.0f;

    private void Awake()
    {
        m_Target = GetComponent<NavMeshAgent>();
    }


    public void pursuit(Vector3 target) {
        Vector3 direction = target - m_Target.transform.position;
        float lookAhed = direction.magnitude / (m_Agent.speed + m_Target.speed);
        float rate = Mathf.Clamp01(direction.magnitude / m_PursuitRadius);
        m_Target.SetDestination(m_Agent.transform.position + (m_Agent.transform.forward * lookAhed) * rate);
    }

    void Update()
    {
        pursuit(m_Agent.transform.position);
    }
}
