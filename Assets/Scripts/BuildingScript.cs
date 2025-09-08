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
            
            GameObject tower = Instantiate(pulse, gameObject.transform.position, Quaternion.identity);
            ui.GetComponent<UIController>().placing = false;
            ui.GetComponent<UIController>().monies=ui.GetComponent<UIController>().monies-
                                                   ui.GetComponent<UIController>().price;
        }
        else
        {
            ui.GetComponent<UIController>().taunt.text = "Not enough Money / Click the buttons";
            StartCoroutine(WaitingTillReady(5f));
        }
        
        
    }
    IEnumerator WaitingTillReady(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        ui.GetComponent<UIController>().taunt.text = "";
    }
}
