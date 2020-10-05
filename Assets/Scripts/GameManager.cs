using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  [SerializeField]
  int maxScore = 999999;
  [SerializeField]
  int maxKill = 100;
  [SerializeField]
  Canvas mainCanvas;
  [SerializeField]
  Canvas titleCanvas;
  [SerializeField]
  Text scoreText;
  [SerializeField]
  Text killText;
  [SerializeField]
  FirstPersonAIO firstPerson;
  [SerializeField]
  FirstPersonGunController gunController;
  [SerializeField]
  Text centerText;
  [SerializeField]
  float waitTime = 2;
  [SerializeField]
  EnemySpawner[] spawners;

  int score = 0;
  int kill = 0;
  bool gameOver = false;
  bool gameClear = false;

  public int Score
  {
    set
    {
      score = Mathf.Clamp(value, 0, maxScore);

      scoreText.text = score.ToString("D6");
    }
    get
    {
      return score;
    }
  }

  public int Kill
  {
    set
    {
      kill = value;

      killText.text = kill.ToString("D3") + "/" + maxKill.ToString();

      if(kill >= maxKill)
      {
        StartCoroutine(GameClear());
      }
    }
    get
    {
      return kill;
    }
  }

  void Start()
    {
    InitGame();
    }

  void InitGame()
  {
    Score = 0;
    Kill = 0;
    firstPerson.playerCanMove = false;
    firstPerson.enableCameraMovement = false;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    gunController.shootEnabled = false;
    StartCoroutine(GameStart());
  }

  public void StartGameByButton()
  {
  }

  public IEnumerator GameStart()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    firstPerson.enableCameraMovement = true;
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
    gunController.shootEnabled = true;
    SetSpawners(true);
    yield return new WaitForSeconds(1);
    centerText.text = "";
    centerText.enabled = false;
  }

  public IEnumerator GameOver()
  {
    if (!gameOver)
    {
      gameOver = true;
      firstPerson.playerCanMove = false;
      firstPerson.enableCameraMovement = true;
      gunController.shootEnabled = false;
      SetSpawners(false);
      centerText.enabled = true;
      centerText.text = "Game Over";
      StopEnemies();
      yield return new WaitForSeconds(waitTime);
      centerText.text = "";
      centerText.enabled = false;
      gameOver = false;
      yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    else
    {
      yield return null;
    }
  }

  public IEnumerator GameClear()
  {
    if (!gameClear)
    {
      gameClear = true;
      firstPerson.playerCanMove = false;
      firstPerson.enableCameraMovement = true;
      gunController.shootEnabled = false;
      SetSpawners(false);
      centerText.enabled = true;
      centerText.text = "Game Clear!!";
      StopEnemies();
      yield return new WaitForSeconds(waitTime);
      centerText.text = "";
      centerText.enabled = false;
      gameClear = false;
      yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    else
    {
      yield return null;
    }
  }

  void StopEnemies()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    foreach (GameObject enemy in enemies)
    {
      EnemyController controller = enemy.GetComponent<EnemyController>();
      controller.moveEnabled = false;
    }
  }

  void SetSpawners(bool isEnable)
  {
    foreach(EnemySpawner spawner in spawners)
    {
      spawner.spawnEnabled = isEnable;
    }
  }

}