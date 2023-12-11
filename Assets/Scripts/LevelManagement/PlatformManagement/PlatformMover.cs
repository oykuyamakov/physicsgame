using System;
using Roro.Scripts.GameManagement;
using Roro.Scripts.SettingImplementations;
using UnityCommon.Variables;
using UnityEngine;

namespace PlatformManager
{
    public class PlatformMover : MonoBehaviour
    {
        private GeneralSettings m_GeneralSettings;
        private GameManager m_GameManager;
        private BoolVariable m_GameIsRunning;

        private void Awake()
        {
            m_GeneralSettings = GeneralSettings.Get();
            m_GameManager = GameManager.Instance;
            
            m_GameIsRunning = Variable.Get<BoolVariable>("GameIsRunning");
        }

        private void Update()
        {
            if(!m_GameIsRunning.Value)
               return;

            transform.position += Vector3.left * (m_GeneralSettings.PlatformSpeed * Time.deltaTime);
        }
    }
}
