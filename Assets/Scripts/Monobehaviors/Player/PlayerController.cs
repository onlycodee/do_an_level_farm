using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator m_animator;

    int hoeParam = Animator.StringToHash("Hoe");
    int wateringParam = Animator.StringToHash("Watering");
    int seedParam = Animator.StringToHash("Seed");
    int m_animSpeedParam = Animator.StringToHash("MoveSpeed");

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ResetAllTriggers();
            m_animator.SetTrigger(hoeParam);
        } else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ResetAllTriggers();
            m_animator.SetTrigger(wateringParam);
        } else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ResetAllTriggers();
            m_animator.SetTrigger(seedParam);
        }
    }

    private void ResetAllTriggers()
    {
        foreach (var trigger in m_animator.parameters)
        {
            m_animator.ResetTrigger(trigger.name);
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
