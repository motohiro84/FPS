using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
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

  void Start()
  {
    setPosition = GetComponent<SetPosition>();
    enemyController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
    setPosition.CreateRandomPosition();
    velocity = Vector3.zero;
    arrived = false;
    elapsedTime = 0f;
  }


  // Update is called once per frame
  void Update()
  {
    if (!arrived)
    {
      if (enemyController.isGrounded)
      {
        velocity = Vector3.zero;
        animator.SetFloat("Speed", 2.0f);
        direction = (destination - transform.position).normalized;
        transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
        velocity = direction * walkSpeed;
        // Debug.Log(destination);
      }
      velocity.y += Physics.gravity.y * Time.deltaTime;
      enemyController.Move(velocity * Time.deltaTime);

      if (Vector3.Distance(transform.position, destination) < 0.5f)
      {
        arrived = true;
        animator.SetFloat("Speed", 0.0f);
      }
    }
    else
    {
      elapsedTime += Time.deltaTime;
      if (elapsedTime > waitTime)
      {
        setPosition.CreateRandomPosition();
        destination = setPosition.GetDestination();
        arrived = false;
        elapsedTime = 0f;
      }
    }
  }
}
