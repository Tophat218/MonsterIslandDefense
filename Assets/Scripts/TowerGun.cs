using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerGun: MonoBehaviour
 {
     [SerializeField] private GameObject bomb;
     [SerializeField] private float sec = 1f;
     private Vector3 target;
     private float _time;

     private void Awake()
     {
         target = transform.position;
         
         target.y += 1;
     }

     private void Update()
     {
         _time += Time.deltaTime;
         while (_time >= sec)
         {
             BombsAway();
             _time -= sec;
         }
     }

     public void BombsAway()
     {
         int rand = Random.Range(0, 4);
         if (rand == 0)
         {
             target.x += 2;
         }else if (rand==1)
         {
             target.z += 2;
         }else if (rand == 2)
         {
             target.x += -2;
         }else if (rand == 3)
         {
             target.z += -2;
         }
         GameObject boom = Instantiate(bomb, target, Quaternion.identity);
         target = transform.position;
         target.y += 1;
     }
 }
