using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Evade : MonoBehaviour
{
    public NavMeshAgent m_Target;
    private NavMeshAgent m_Agent;

    private void Awake(){
        m_Agent = GetComponent<NavMeshAgent>();
    }

    public void evade(Vector3 target) {
        Vector3 direction = transform.position - target;
        float lookAhed = direction.magnitude / (m_Agent.speed + m_Target.speed);
        Vector3 position = (m_Target.transform.position + m_Target.transform.forward * lookAhed) - target;
        m_Agent.SetDestination(transform.position + position);
    }

    void Update()
    {
        evade(m_Target.transform.position);
    }
}
