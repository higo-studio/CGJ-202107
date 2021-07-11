using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class UIManagerEvent : MonoBehaviour
{
  public GameObject gameOver;
  public CubeController controller;

  void GameOver(ResultType type)
  {
      Debug.Log($"GameOver, {type}");
      gameOver.SetActive(true);
      Time.timeScale = 0.0f;
      controller.enabled = false;
  }
}