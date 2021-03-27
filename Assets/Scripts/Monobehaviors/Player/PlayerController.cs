using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator m_animator;

    int hoeParam = Animator.StringToHash("Hoe");
    int wateringParam = Animator.StringToHash("Watering");
    int m_animSpeedParam = Animator.StringToHash("MoveSpeed");

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            m_animator.ResetTrigger(wateringParam);
            m_animator.SetTrigger(hoeParam);
        } else if (Input.GetKeyDown(KeyCode.I))
        {
            m_animator.ResetTrigger(hoeParam);
            m_animator.SetTrigger(wateringParam);
        }
    }

    public void SetAnimSpeed(float speed)
    {
        m_animator.SetFloat(m_animSpeedParam, speed);
    }

    public bool IsWorking()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).IsTag("Work");
    }
}
