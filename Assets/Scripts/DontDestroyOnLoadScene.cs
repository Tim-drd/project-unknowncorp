using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
   public GameObject[] objects;

   public static DontDestroyOnLoadScene instance;

   private void Awake()
   {
      if (instance != null)
      {
         Debug.LogWarning("Il y a plus d'une instance DontDestroyOnLoadScene dans la scène");
         return;
      }

      instance = this;

      foreach (var element in objects)
      {
         DontDestroyOnLoad(element);
      }
   }

   public void RemoveFromDontDestroyOnLoad()
   {
      foreach (var element in objects)
      {
         SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
      }
   }
}
