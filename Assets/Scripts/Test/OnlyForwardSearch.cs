using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OnlyForwardSearch : MonoBehaviour
{
  [SerializeField]
  private MoveEnemy moveEnemy;
  [SerializeField]
  private SphereCollider searchArea;
  [SerializeField]
  private float searchAngle = 80f;
  [SerializeField]
  private LayerMask obstacleLayer;

  void OnTriggerStay(Collider other)
  {
    if (other.tag == "Player")
    {
      var playerDirection = other.transform.position - transform.position;
      var angle = Vector3.Angle(transform.forward, playerDirection);
      if (angle <= searchAngle)
      {
        if (!Physics.Linecast(transform.position + Vector3.up, other.transform.position + Vector3.up, obstacleLayer))
        {
          Debug.Log("発見" + angle);
          moveEnemy.SetState(MoveEnemy.EnemyState.Chase, other.transform);
        }
      }
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      Debug.Log("見失う");
      moveEnemy.SetState(MoveEnemy.EnemyState.Wait);
    }
  }

#if UNITY_EDITOR
  void OnDrawGizmos()
  {
    Handles.color = Color.red;
    Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward, searchAngle * 2f, searchArea.radius);
  }
#endif

}
