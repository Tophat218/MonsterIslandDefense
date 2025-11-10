using System;
using System.Collections;
using UnityEngine;

public class TowerPulse : MonoBehaviour
{
    public GameObject ui;
    public int tier=0;
    public Material up1;

    public Material up2;

    public GameObject body;
    public GameObject range;
    public int num;
    void Awake()
    {
        ui = GameObject.FindWithTag("Interface");
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    num = 5;
                    range.GetComponent<SphereCollider>().radius = num;
                    matCopy[0] = up1;
                    gameObject.GetComponent<MeshRenderer>().materials = matCopy;
                    Debug.Log("Tier 1");
                }
                else
                {
                    tier = 2;
                    num = 8;
                    range.GetComponent<SphereCollider>().radius = num;
                    matCopy[0] = up2;
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
