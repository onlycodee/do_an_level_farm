using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(5f)] float m_moveSpeed = 10f;
    [SerializeField, Min(10f)] float m_rotationSpeed = 120f;
    Rigidbody m_rigidBody;
    Animator m_animator;
    int m_animSpeedParam;
    Vector3 m_moveInput;
    float m_targetRotation;
    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_animSpeedParam = Animator.StringToHash("Speed");
        m_targetRotation = 0;
    }
    private void Update()
    {
        m_moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        m_moveInput = Vector3.ClampMagnitude(m_moveInput, 1f);
        m_animator.SetFloat(m_animSpeedParam, m_moveInput.magnitude);
        if (!Mathf.Approximately(m_moveInput.x, Mathf.Epsilon) || !Mathf.Approximately(m_moveInput.z, Mathf.Epsilon))
        {
            m_rigidBody.MovePosition(transform.position + m_moveInput * m_moveSpeed * Time.deltaTime);
            m_targetRotation = Mathf.Atan2(m_moveInput.x, m_moveInput.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, m_targetRotation, 0f), m_rotationSpeed * Time.deltaTime);
        }
        //transform.rotation = Quaternion.Euler(new Vector3(0f, Mathf.Atan2(m_moveInput.x, m_moveInput.z)) * Mathf.Rad2Deg);
    }
}
