using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liner : MonoBehaviour
{

  [SerializeField, Range(0, 10)]
  float time = 1.2f;

  [SerializeField]
  Vector3 endPosition1;

  [SerializeField]
  Vector3 endPosition2;

  private float startTime;
  private Vector3 startPosition;

  void OnEnable()
  {
    if (time <= 0)
    {
      if (this.transform.localScale == new Vector3(1.5f, 1.5f, 1.5f))
      {
        transform.localPosition = endPosition1;
      }
      else
      {
        transform.localPosition = endPosition2;
      }
      enabled = false;
      return;
    }

    startTime = Time.timeSinceLevelLoad;
    startPosition = transform.localPosition;
  }

  void Update()
  {
    if (this.transform.localScale == new Vector3(1.5f, 1.5f, 1.5f))
    {
      Move1();
    }
    else
    {
      Move2();
    }
  }

  void Move1()
  {
    var diff = Time.timeSinceLevelLoad - startTime;
    if (diff > time)
    {
      transform.localPosition = endPosition1;
      enabled = false;
    }

    var rate = diff / time;

    transform.localPosition = Vector3.Lerp(startPosition, endPosition1, rate);

    if (transform.localPosition == endPosition1)
    {
      Invoke("DestroyMethod", 0.1f);
    }
  }

  void Move2()
  {
    var diff = Time.timeSinceLevelLoad - startTime;
    if (diff > time)
    {
      transform.localPosition = endPosition2;
      enabled = false;
    }

    var rate = diff / time;

    transform.localPosition = Vector3.Lerp(startPosition, endPosition2, rate);

    if (transform.localPosition == endPosition2)
    {
      Invoke("DestroyMethod", 0.1f);
    }
  }

  void DestroyMethod()
  {
    Destroy(gameObject);
  }
}