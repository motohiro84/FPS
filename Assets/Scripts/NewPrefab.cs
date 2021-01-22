using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPrefab : MonoBehaviour
{
  bool key = true;
  public GameObject prefab1;
  public GameObject obj;

  float n = 0;
  float a;
  float b;
  float ave;
  float plus;

  void Start()
  {
  }

  void Update()
  {
    if (Music.IsJustChangedBar() && Time.time > 4)
    {
      PrefabNew();
      // a = Time.time;
      // if (n == 0)
      // {
      //   b = Time.time;
      //   n++;
      // }
      // else
      // {
      //   plus += (a - b);
      //   ave = plus / n;
      //   n++;
      //   b = Time.time;
      // }
      // Debug.Log(ave);
    }
  }


  void PrefabNew()
  {
    if (key)
    {
      prefab1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
      key = false;
    }
    else
    {
      prefab1.transform.localScale = new Vector3(1, 1, 1);
      key = true;
    }
    GameObject clone = Instantiate(prefab1, obj.transform);
    clone.transform.SetParent(obj.transform, false);
  }

}
