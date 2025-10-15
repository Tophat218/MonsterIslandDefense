using System;
using System.Collections;
using UnityEngine;
 public class TowerBolt: MonoBehaviour
 {
     [SerializeField] private float sec = 0.5f;
     private void Start()
     {
         StartCoroutine(Countdown(sec));
     }

     IEnumerator Countdown(float sec)
     {
         yield return new WaitForSecondsRealtime(sec);
         Destroy(gameObject);
     }
 }