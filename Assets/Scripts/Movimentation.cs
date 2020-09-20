using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentation : MonoBehaviour
{
    public float m_Speed = 5.0f;
    public float m_RotationSpeed = 120.0f;
    public float m_currentSpeed = 0.0f;
    
    void Update()
    {
        float movement = Input.GetAxis("Vertical") * m_Speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * m_RotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, movement);

        m_currentSpeed = movement;

        transform.Rotate(0, rotation, 0);
    }
}
