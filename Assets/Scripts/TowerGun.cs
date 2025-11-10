using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerGun: MonoBehaviour
 {
     [SerializeField] private GameObject bomb;
     [SerializeField] private GameObject newBomb;
     [SerializeField] private float sec = 10f;
     private Vector3 target;
     private float _time;
     public GameObject ui;
     public int tier=0;
     public Material up1;

     public Material up2;

     public GameObject body;

     private void Awake()
     {
         ui = GameObject.FindWithTag("Interface");
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

     private void OnMouseDown()
     {
         var matCopy = gameObject.GetComponent<MeshRenderer>().materials;
         if (ui.GetComponent<UIController>().tierUp)
         {
             if (tier !< 2)
             {
                 if (tier !< 1)
                 {
                     tier = 1;
                     bomb = newBomb;
                     matCopy[1] = up1;
                     gameObject.GetComponent<MeshRenderer>().materials = matCopy;
                     Debug.Log("Tier 1");
                 }
                 else
                 {
                     tier = 2;
                     sec = 5f;
                     matCopy[1] = up2;
                     gameObject.GetComponent<MeshRenderer>().materials = matCopy;
                     Debug.Log("Tier 2");
                 }
             }
             else
             {
                 ui.GetComponent<UIController>().taunt.text = "Fully Upgraded";
                 StartCoroutine(WaitingTillReady(5f));
             }

             ui.GetComponent<UIController>().tierUp = false;
             ui.GetComponent<UIController>().monies = ui.GetComponent<UIController>().monies-
                                                      ui.GetComponent<UIController>().price;
         }
         else
         {
             ui.GetComponent<UIController>().taunt.text = "Click the Green Button to Upgrade";
             StartCoroutine(WaitingTillReady(5f));
         }
     }
     IEnumerator WaitingTillReady(float sec)
     {
         yield return new WaitForSecondsRealtime(sec);
         ui.GetComponent<UIController>().taunt.text = "";
     }
 }
