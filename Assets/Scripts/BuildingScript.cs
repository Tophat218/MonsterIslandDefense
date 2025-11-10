using System;
using System.Collections;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [SerializeField]private GameObject pulse;
    [SerializeField]private GameObject strike;
    [SerializeField]private GameObject zap;
    public GameObject ui;

    public void Awake()
    {
        ui = GameObject.FindWithTag("Interface");
        
    }

    public void OnMouseDown()
    {
        if (ui.GetComponent<UIController>().placing)
        {
            GameObject temp;
            if (ui.GetComponent<UIController>().Towertype == 0)
            {
                temp = pulse;
            }else if (ui.GetComponent<UIController>().Towertype == 1)
            {
                temp = strike;
            }else if (ui.GetComponent<UIController>().Towertype == 3)
            {
                temp = zap;
            }
            else
            {
                ui.GetComponent<UIController>().taunt.text = "No tower selected";
                temp = null;
                StartCoroutine(WaitingTillReady(5f));
                return;
            }
            GameObject tower = Instantiate(temp, gameObject.transform.position, Quaternion.identity);
            ui.GetComponent<UIController>().placing = false;
            ui.GetComponent<UIController>().monies=ui.GetComponent<UIController>().monies-
                                                   ui.GetComponent<UIController>().price;
        }
        else
        {
            ui.GetComponent<UIController>().taunt.text = "Click the buttons";
            StartCoroutine(WaitingTillReady(5f));
        }
        
        
    }
    IEnumerator WaitingTillReady(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        ui.GetComponent<UIController>().taunt.text = "";
    }
}
