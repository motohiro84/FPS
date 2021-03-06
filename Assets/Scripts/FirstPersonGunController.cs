﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonGunController : MonoBehaviour
{
  public enum ShootMode { AUTO, SEMIAUTO }

  public bool shootEnabled = true;

  [SerializeField]
  ShootMode shootMode = ShootMode.AUTO;
  [SerializeField]
  int maxAmmo = 100;
  [SerializeField]
  int maxSupplyValue = 100;
  [SerializeField]
  int damage = 1;
  [SerializeField]
  float shootInterval = 0.1f;
  [SerializeField]
  float shootRange = 50;
  [SerializeField]
  float supplyInterval = 0.1f;
  [SerializeField]
  Vector3 muzzleFlashScale;
  [SerializeField]
  GameObject muzzleFlashPrefab;
  [SerializeField]
  GameObject hitEffectPrefab;
  [SerializeField]
  Image ammoGauge;
  [SerializeField]
  Text ammoText;
  [SerializeField]
  Image supplyGauge;

  bool shooting = false;
  bool supplying = false;
  int ammo = 0;
  int supplyValue = 0;
  GameObject muzzleFlash;
  GameObject hitEffect;
  int layerMask = ~(1 << 9 | 1 << 12);
  public AudioClip sound1;
  public AudioClip sound2;
  AudioSource audioSource;
  bool rhythm;
  bool missKey = false;

  public int Ammo
  {
    set
    {
      ammo = Mathf.Clamp(value, 0, maxAmmo);
      //UIの表示を操作
      //テキスト
      ammoText.text = ammo.ToString("D3");
      //ゲージ
      float scaleX = (float)ammo / maxAmmo;
      ammoGauge.rectTransform.localScale = new Vector3(scaleX, 1, 1);
    }
    get
    {
      return ammo;
    }
  }

  public int SupplyValue
  {
    set
    {
      supplyValue = Mathf.Clamp(value, 0, maxSupplyValue);
      if (SupplyValue >= maxSupplyValue)
      {
        Ammo = maxAmmo;
        supplyValue = 0;
      }
      float scaleX = (float)supplyValue / maxSupplyValue;
      supplyGauge.rectTransform.localScale = new Vector3(scaleX, 1, 1);
    }
    get
    {
      return supplyValue;
    }
  }

  void Start()
  {
    InitGun();
    Music.Play("Player");
    audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
  }

  void Update()
  {
    Rhythm();

    if (shootEnabled & ammo > 0 & GetInput())
    {
      StartCoroutine(ShootTimer());
    }
    if (shootEnabled)
    {
      StartCoroutine(SupplyTimer());
    }
  }

  void Rhythm()
  {
    if (Music.IsJustChangedBar())
    {
      rhythm = false;
    }
    Invoke("RhythmMethod", 0.1f);
    // else if (Music.IsJustChangedBeat())
    // {
    //   rhythm = true;
    // }
  }

  void RhythmMethod()
  {
    rhythm = true;
  }

  void InitGun()
  {
    Ammo = maxAmmo;
    SupplyValue = 0;
  }

  bool GetInput()
  {
    switch (shootMode)
    {
      case ShootMode.AUTO:
        return Input.GetMouseButton(0);

      case ShootMode.SEMIAUTO:
        return Input.GetMouseButtonDown(0);
    }

    return false;
  }

  IEnumerator ShootTimer()
  {
    if (!shooting && rhythm && !missKey)
    {
      shooting = true;

      //マズルフラッシュON
      if (muzzleFlashPrefab != null)
      {
        if (muzzleFlash != null)
        {
          muzzleFlash.SetActive(true);
        }
        else
        {
          muzzleFlash = Instantiate(muzzleFlashPrefab, transform.position, transform.rotation);
          muzzleFlash.transform.SetParent(gameObject.transform);
          muzzleFlash.transform.localScale = muzzleFlashScale;
        }
      }

      Shoot();

      yield return new WaitForSeconds(shootInterval);

      //マズルフラッシュOFF
      if (muzzleFlash != null)
      {
        muzzleFlash.SetActive(false);
      }

      //ヒットエフェクトOFF
      if (hitEffect != null)
      {
        if (hitEffect.activeSelf)
        {
          hitEffect.SetActive(false);
        }
      }

      shooting = false;
      audioSource.PlayOneShot(sound2);
    }
    else
    {
      missKey = true;
      audioSource.PlayOneShot(sound1);
      Invoke("Miss", 0.2f);
      yield return null;
    }
  }

  void Miss()
  {
    missKey = false;
  }

  void Shoot()
  {
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    //レイを飛ばして、ヒットしたオブジェクトの情報を得る
    if (Physics.Raycast(ray, out hit, shootRange, layerMask))
    {
      Debug.Log(hit.collider.gameObject.layer);
      Debug.Log(hit.collider.gameObject.tag);
      //ヒットエフェクトON
      if (hitEffectPrefab != null)
      {
        if (hitEffect != null)
        {
          hitEffect.transform.position = hit.point;
          hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
          hitEffect.SetActive(true);
        }
        else
        {
          hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
        }
      }

      string tagName = hit.collider.gameObject.tag;
      if (tagName == "Enemy")
      {
        // EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController>();
        // enemy.Hp -= damage;
      }
    }

    Ammo--;
  }

  IEnumerator SupplyTimer()
  {
    if (!supplying)
    {
      supplying = true;
      SupplyValue++;
      yield return new WaitForSeconds(supplyInterval);
      supplying = false;
    }
  }

}