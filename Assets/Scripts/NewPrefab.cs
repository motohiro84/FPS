using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPrefab : MonoBehaviour
{
  bool key = true;
  public GameObject prefab1;
  public GameObject obj;

  void Start()
  {
  }

  void Update()
  {
    if (Music.IsJustChangedBar() && Time.time > 1)
    {
      PrefabNew();
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
