using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager m_Manager;
    public float m_Speed;
    private bool m_Turning;
    
    private void Start() {
        m_Speed = Random.Range(m_Manager.m_MinSpeed, m_Manager.m_MaxSpeed);
        var target = Random.insideUnitSphere * m_Manager.m_Radius;
        transform.rotation = Quaternion.LookRotation(target);
    }
    
    private void Update() {
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
        foreach(GameObject boid in m_Manager.m_Boids) 
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

                    var flock = boid.GetComponent<Flock>();
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
}
