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

  void Start()
  {
    enemyController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
    destination = new Vector3(0f, 0f, 0f);
    velocity = Vector3.zero;
  }


  // Update is called once per frame
  void Update()
  {
    if (enemyController.isGrounded)
    {
      velocity = Vector3.zero;
      animator.SetFloat("Speed", 2.0f);
      direction = (destination - transform.position).normalized;
      transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
      velocity = direction * walkSpeed;
      Debug.Log(destination);
    }
    velocity.y += Physics.gravity.y * Time.deltaTime;
    enemyController.Move(velocity * Time.deltaTime);
  }
}
