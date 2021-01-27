using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
  public enum EnemyState
  {
    Walk,
    Wait,
    Chase
  };
  private CharacterController enemyController;
  private Animator animator;
  private Vector3 destination;
  [SerializeField]
  private float walkSpeed = 1.0f;
  private Vector3 velocity;
  private Vector3 direction;
  private bool arrived;
  private SetPosition setPosition;
  [SerializeField]
  private float waitTime = 3f;
  private float elapsedTime;
  private EnemyState state;
  private Transform playerTransform;

  void Start()
  {
    enemyController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
    setPosition = GetComponent<SetPosition>();
    setPosition.CreateRandomPosition();
    velocity = Vector3.zero;
    arrived = false;
    elapsedTime = 0f;
    SetState(EnemyState.Walk);
  }


  // Update is called once per frame
  void Update()
  {
    if (state == EnemyState.Walk || state == EnemyState.Chase)
    {
      if (state == EnemyState.Chase)
      {
        setPosition.SetDestination(playerTransform.position);
      }
      if (enemyController.isGrounded)
      {
        velocity = Vector3.zero;
        animator.SetFloat("Speed", 2.0f);
        direction = (setPosition.GetDestination() - transform.position).normalized;
        transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
        velocity = direction * walkSpeed;
      }

      if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 0.5f)
      {
        SetState(EnemyState.Wait);
        animator.SetFloat("Speed", 0.0f);
      }
    }
    else if (state == EnemyState.Wait)
    {
      elapsedTime += Time.deltaTime;
      if (elapsedTime > waitTime)
      {
        SetState(EnemyState.Walk);
      }
    }
    velocity.y += Physics.gravity.y * Time.deltaTime;
    enemyController.Move(velocity * Time.deltaTime);
  }

  public void SetState(EnemyState tempState, Transform targetObj = null)
  {
    if (tempState == EnemyState.Walk)
    {
      arrived = false;
      elapsedTime = 0f;
      state = tempState;
      setPosition.CreateRandomPosition();
    }
    else if (tempState == EnemyState.Chase)
    {
      state = tempState;
      arrived = false;
      playerTransform = targetObj;
    }
    else if (tempState == EnemyState.Wait)
    {
      elapsedTime = 0f;
      state = tempState;
      arrived = true;
      velocity = Vector3.zero;
      animator.SetFloat("Speed", 0f);
    }
  }
  public EnemyState GetState()
  {
    return state;
  }

}
