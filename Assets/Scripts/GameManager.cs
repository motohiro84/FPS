using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  int maxScore = 99999999;
  [SerializeField]
  Text scoreText;
  [SerializeField]
  FirstPersonAIO firstPerson;
  [SerializeField]
  FirstPersonGunController gunCOntroller;
  [SerializeField]
  Text centerText;
  [SerializeField]
  float waitTime = 2;
  int score = 0;
  public int Score
  {
    set
    {
      score = Mathf.Clamp(value, 0, maxScore);
      scoreText.text = score.ToString("D8");
    }
    get
    {
      return score;
    }
  }
    void Start()
    {
    InitGame();
    StartCoroutine(GameStart());
    }
  void InitGame()
  {
    Score = 0;
    firstPerson.playerCanMove = false;
    firstPerson.enableCameraMovement = true;
    gunCOntroller.shootEnabled = false;
  }
  public IEnumerator GameStart()
  {
    yield return new WaitForSeconds(waitTime);
    centerText.enabled = true;
    centerText.text = "3";
    yield return new WaitForSeconds(1);
    centerText.text = "2";
    yield return new WaitForSeconds(1);
    centerText.text = "1";
    yield return new WaitForSeconds(1);
    centerText.text = "GO!!";
    firstPerson.playerCanMove = true;
    firstPerson.enableCameraMovement = true;
    gunCOntroller.shootEnabled = true;
    yield return new WaitForSeconds(1);
    centerText.text = "";
    centerText.enabled = false;
  }
    public void GameOver(string resultMessage)
    {
        DataSender.resultMessage = resultMessage;
        SceneManager.LoadScene("Result");
    }

}