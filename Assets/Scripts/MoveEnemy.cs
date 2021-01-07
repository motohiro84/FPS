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
    destination = new Vector3(25f, 0f, 25f);
    velocity = Vector3.zero
  }

  // Update is called once per frame
  void Update()
  {

  }
}
