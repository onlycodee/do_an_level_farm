using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(5f)] float m_moveSpeed = 10f;
    [SerializeField, Min(10f)] float m_rotationSpeed = 120f;
    [SerializeField] Joystick joystick;
    [SerializeField] bool useJoyStick = false;

    Rigidbody m_rigidBody;
    Animator m_animator;
    PlayerController m_playerController;

    Vector3 m_moveInput;
    float m_targetRotation;
    bool m_canMove = true;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_playerController = GetComponent<PlayerController>();
        m_targetRotation = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isFinished) return;
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (m_playerController.IsWorking() || !m_canMove) return;
        if (useJoyStick)
        {
            m_moveInput = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        } else
        {
            m_moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }
        m_moveInput = Vector3.ClampMagnitude(m_moveInput, 1f);
        m_playerController.SetAnimSpeed(m_moveInput.magnitude);
        if (!Mathf.Approximately(m_moveInput.x, Mathf.Epsilon) || !Mathf.Approximately(m_moveInput.z, Mathf.Epsilon))
        {
            m_rigidBody.MovePosition(transform.position + m_moveInput * m_moveSpeed * Time.deltaTime);
            m_targetRotation = Mathf.Atan2(m_moveInput.x, m_moveInput.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, m_targetRotation, 0f), m_rotationSpeed * Time.deltaTime);
        }
    }
    public void SetMovingState(bool canMove)
    {
        m_canMove = canMove;
        GetComponent<PlayerController>().ResetToIdleAnim();
    }
}
