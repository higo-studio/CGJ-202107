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
  public TMPro.TMP_Text gameResult;
  public CubeController controller;

  void GameOver(ResultType type)
  {
      Debug.Log($"GameOver, {type}");
      switch(type) {
        case ResultType.CannotBreath:
          gameResult.text = "You were blown away by a sandstorm";
          break;
        case ResultType.Drop:
          gameResult.text = "You fell off a cliff and died";
          break;
        case ResultType.Succeed:
          gameResult.text = "Successfully reach the small cave";
          break;
      }
      gameOver.SetActive(true);
      Time.timeScale = 0.0f;
      controller.enabled = false;
  }
}