using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlockManager : MonoBehaviour
{
    [Header("Boid")]
    public float m_MinSpeed;
    public float m_MaxSpeed;
    public GameObject m_BoidPrefab;
    public int m_BoidNumber;
    public float m_MinRotationSpeed;
    public float m_MaxRotationSpeed;
    
    public static List<Bot> m_ListBot = new List<Bot>();

    
    [Header("Environment")]
    public float m_Radius;
    public static GameObject[] m_Boids;
    public Vector3 m_Target;
    public float Range => m_Radius * 0.9f;

    [Header("Flocking")]
    public float m_Distance;
    public bool m_UseFlocking;

    [Header("Agent")]
    public NavMeshAgent mAgent;
    


    public void Start() {
        var parent = new GameObject($"{m_BoidPrefab.name}Parent");
        m_Boids = new GameObject[m_BoidNumber];
        for(int i =0; i < m_BoidNumber; i++) 
        {
            var position = transform.position + Random.insideUnitSphere * Range;
            
            m_Boids[i] = Instantiate(m_BoidPrefab, position, Quaternion.identity);
            m_Boids[i].GetComponent<Bot>().m_Manager = this;
            m_Boids[i].GetComponent<Bot>().m_Agent = mAgent;
            m_Boids[i].GetComponent<Bot>().m_index = i;
            m_Boids[i].transform.parent = parent.transform;
            
            m_ListBot.Add(m_Boids[i].GetComponent<Bot>());
        }

        m_Target = Random.insideUnitSphere * Range;
    }

    public void Update() {
        if(Random.Range(0.0f,1.0f) < 0.01f) 
            m_Target = transform.position +  Random.insideUnitSphere * Range;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_Target, 0.5f);
    }


    public static List<Bot> getListBot() {
        return m_ListBot;
    }

    public static Bot getTarget() {
        Vector3 menor = new Vector3(999,999,999);
        Bot newBot = null;

        //EM DETERMINADO MOMENTO O BOT SOME.

        foreach(Bot objBot in getListBot()) {
            //VER COM O PROFESSOR SE TEM UM JEITO MELHOR DE COMPARAR
             if(objBot.transform.position.x < menor.x && objBot.transform.position.z < menor.z){
                 newBot = objBot;
                 menor = objBot.transform.position;
            }
        }
        return newBot.GetComponent<Bot>();
    }

}
