using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
  [SerializeField]
  GameManager gameManager;
  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Enemy")
    {
      StartCoroutine(gameManager.GameOver());
    }
  }
}