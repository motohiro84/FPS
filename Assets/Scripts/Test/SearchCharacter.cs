using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{
  private MoveEnemy moveEnemy;
  void Start()
  {
    moveEnemy = GetComponentInParent<MoveEnemy>();
  }

  void OnTriggerStay(Collider col)
  {
    if (col.tag == "Player")
    {
      MoveEnemy.EnemyState state = moveEnemy.GetState();
      if (state != MoveEnemy.EnemyState.Chase)
      {
        Debug.Log("発見");
        moveEnemy.SetState(MoveEnemy.EnemyState.Chase, col.transform);
      }
    }
  }

  void OnTriggerExit(Collider col)
  {
    if (col.tag == "Player")
    {
      Debug.Log("見失う");
      moveEnemy.SetState(MoveEnemy.EnemyState.Wait);
    }
  }

}
