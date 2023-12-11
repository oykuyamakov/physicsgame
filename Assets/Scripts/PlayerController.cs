using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Roro.Scripts.GameManagement.EventImplementations;
using Roro.Scripts.SettingImplementations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private GeneralSettings m_GeneralSettings;
   private int m_BodyDirection = 1;

   private void Awake()
   {
      m_GeneralSettings = GeneralSettings.Get();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.A) && m_BodyDirection == 1)
      {
         m_BodyDirection = -1;
         transform.rotation = Quaternion.Euler(0, 0, -90);


      }
      else if (Input.GetKeyDown(KeyCode.D) && m_BodyDirection == -1)
      {
         m_BodyDirection = 1;
         
         transform.rotation = Quaternion.Euler(0, 0, 0);
      }
      
   }
   
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
      {

         using var evt = LevelFailEvent.Get();
         evt.SendGlobal();
      }
   }
}
