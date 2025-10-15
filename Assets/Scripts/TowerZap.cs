using System;
using System.Collections;
using UnityEngine;

public class TowerZap : MonoBehaviour
{
    [SerializeField] private GameObject thunderBolt;
    [SerializeField] private GameObject target;
    [SerializeField] private float sec = 10f;
    private float _time;

    private void Awake()
    {
        _time = 0f;
    }

    private void Update()
    {
        target = GameObject.FindWithTag("Enemy");
        _time += Time.deltaTime;
        while (_time >= sec)
        {
            FindTarget();
            _time -= sec;
        }
    }

    public void FindTarget()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            GameObject zap = Instantiate(thunderBolt, target.transform.position,Quaternion.identity);
        }
    }
    

    
}