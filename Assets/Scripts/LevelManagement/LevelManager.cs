using System;
using Events;
using Roro.Scripts.GameManagement;
using Roro.Scripts.GameManagement.EventImplementations;
using Roro.Scripts.Sounds.Core;
using UnityCommon.Modules;
using UnityCommon.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        private GameManager m_GameManager;
        private BoolVariable m_GameIsRunning;
        private SoundManager m_SoundManager;
        
        private SoundDatabase m_SoundDatabase;
        
        private void Awake()
        {
            m_GameManager = GameManager.Instance;
            m_SoundManager = SoundManager.Instance;
            
            m_SoundDatabase = SoundDatabase.Get();
            
            m_GameIsRunning = Variable.Get<BoolVariable>("GameIsRunning");
            
            GEM.AddListener<LevelFailEvent>(OnLevelFail);
            GEM.AddListener<LevelWinEvent>(OnLevelWin);
            
            StartLevel();
        }

        private void OnLevelFail(LevelFailEvent evt)
        {
            EndLevel(false);

            Conditional.Wait(1.5f).Do(RestartLevel);
        }
        
        private void OnLevelWin(LevelWinEvent evt)
        {
            EndLevel(true);
        }

        private void EndLevel(bool result)
        {
            using var evt = LevelEndEvent.Get(result);
            evt.SendGlobal();
            
            m_GameIsRunning.Value = false;

            var sound = result ? m_SoundDatabase.LevelWinSound : m_SoundDatabase.LevelFailSound;
            m_SoundManager.PlayOneShot(sound);
        }
        
        private void RestartLevel()
        {
            // write a code to reload the scene 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }

        private void StartLevel()
        {
            m_GameIsRunning.Value = true;
            
            using var evt = LevelStartEvent.Get();
            evt.SendGlobal();
            
            m_SoundManager.PlayOneShot(m_SoundDatabase.LevelStartSound);
        }

        private void OnDestroy()
        {
            GEM.RemoveListener<LevelFailEvent>(OnLevelFail);
            GEM.RemoveListener<LevelWinEvent>(OnLevelWin);
        }
    }
}
