using System;
using Roro.Scripts.SettingImplementations;
using UnityCommon.Modules;
using UnityEngine;

namespace LevelManagement.PlatformManagement
{
    public class GravityController : MonoBehaviour
    {
        private Conditional m_GravitySwitchController;
        
        private GeneralSettings m_GeneralSettings;
        
        private int m_GravityDirection = 1;
        
        private void Awake()
        {
            m_GeneralSettings = GeneralSettings.Get();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_GravityDirection = -1;
                
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                m_GravityDirection = 1;
            }

            if (Math.Abs(m_GravityDirection * m_GeneralSettings.GravityMagnitude - Physics2D.gravity.y) > 0f)
            {
                SwitchGravity();
            }
        }

        private void SwitchGravity()
        {
            Physics2D.gravity = Vector3.Lerp(Physics2D.gravity,
                Vector3.up * (m_GravityDirection * m_GeneralSettings.GravityMagnitude) ,Time.deltaTime * m_GeneralSettings.GravityChangeSpeed);
        }
    }
}
