using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    public NavMeshAgent m_Target;
    public NavMeshAgent m_Agent;
    public int m_index;
    

    [Header("flock")]
    public FlockManager m_Manager;
    public float m_Speed;
    private bool m_Turning;
    private bool mIsStarted = false;

    [Header("Line of Sight")]
    public float m_MaxAngle = 2.0f;

    [Header("Hide")]
    public float m_HideRadius = 30.0f;
    public float m_HideDistance = 5.0f;
    public LayerMask m_HideLayer;


    private void Awake(){
        m_Target = GetComponent<NavMeshAgent>();
    }


    /* Flock */
    private void begin() {
        m_Speed = Random.Range(m_Manager.m_MinSpeed, m_Manager.m_MaxSpeed);
        var target = Random.insideUnitSphere * m_Manager.m_Radius;
        transform.rotation = Quaternion.LookRotation(target);
    }
    
    public void flock() {
        if(!mIsStarted) {
            begin();
            mIsStarted = true;
        }

        var hit = new RaycastHit();
        var direction  = Vector3.zero;
        m_Turning = false;

        if(Vector3.Distance(m_Manager.transform.position, transform.position) > m_Manager.m_Radius * 0.8f) 
        {
            m_Turning = true;
            direction = m_Manager.transform.position - transform.position;
        } 
        else if (Physics.Raycast(transform.position, transform.forward * 2.0f, out hit)) 
        {
            m_Turning = true; 
            direction = Vector3.Reflect(transform.forward, hit.normal);
        }

        if(m_Turning) 
        {
            var rotationSpeed = Random.Range(m_Manager.m_MinRotationSpeed, m_Manager.m_MaxRotationSpeed);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime
            );
        }
        else 
        {
            if(Random.Range(0.0f,1.0f) < 0.1f) 
                m_Speed = Random.Range(m_Manager.m_MinSpeed, m_Manager.m_MaxSpeed);
            if(Random.Range(0.0f,1.0f) < 0.25f) 
                ApplyRules();
        }

        transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);
    }

    private void ApplyRules() {
        if(!m_Manager.m_UseFlocking)
            return;

        float distance;
        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero;
        int groupSize = 0;
        float speed = 0.01f;
        foreach(GameObject boid in FlockManager.m_Boids) 
        {
            if(boid != this.gameObject) 
            {
                distance = Vector3.Distance(transform.position, boid.transform.position);
                if(distance <= m_Manager.m_Distance) 
                {
                    groupSize++;
                    center += boid.transform.position;  
                    if (distance <  1.0f) 
                        avoid += transform.position - boid.transform.position;

                    var flock = boid.GetComponent<Bot>();
                    speed += flock.m_Speed;
                } 
            }
        }

        if(groupSize > 0 )
        {
            center =  center/(float)groupSize + (m_Manager.m_Target - transform.position);
            m_Speed = speed/(float)groupSize;

            var direction = (center + avoid) - transform.position;
            if(direction != Vector3.zero) {
                var rotationSpeed = Random.Range(m_Manager.m_MinRotationSpeed, m_Manager.m_MaxRotationSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction),rotationSpeed * Time.deltaTime
                );
            }
                
        } 
    }
    /* End Flock */

    public void evade(Vector3 target) {
        Vector3 direction = transform.position - target;
        float lookAhed = direction.magnitude / (m_Target.speed + m_Agent.speed);
        Vector3 position = (m_Agent.transform.position + m_Agent.transform.forward * lookAhed) - target;
        m_Target.SetDestination(transform.position + position);
    }

    public bool TargetCanSeeMe() {
        // POSIÇÃO DO ALGO ATÉ MIM
        Vector3 direction = transform.position - m_Agent.transform.position;
        // ANGULO QUE O ALVO CONSEGUE VER
        float lookingAngle = Vector3.Angle(m_Agent.transform.forward, direction);
        // RETORNO O VALOR BOOLEANO REFERENTE AO ANGULO
        return lookingAngle < m_MaxAngle;
    }

    public void hide() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 
                              m_HideRadius,
                              m_HideLayer);
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < colliders.Length; i++) {
            Vector3 hideDir = colliders[i].transform.position - m_Target.transform.position;
            Vector3 hidePos = colliders[i].transform.position + hideDir.normalized * m_HideDistance;

            if (Vector3.Distance(transform.position, hidePos) < distance) {
                chosenSpot = hidePos;
                distance = Vector3.Distance(transform.position, hidePos);
            }
        }

        m_Target.SetDestination(chosenSpot);
    }

    public bool isAgentTooClose() {
        if(m_Agent != null) {
            return Vector3.Distance(m_Agent.transform.position, m_Target.transform.position) <= 50 ? true : false;
        }
        return false;
    }

    
}
