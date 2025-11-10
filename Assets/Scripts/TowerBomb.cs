using System;
using System.Collections;
using UnityEngine;
public class TowerBomb: MonoBehaviour
{
    [SerializeField] private Material tick;
    [SerializeField] private Material tock;
    [SerializeField] private int scale = 2;
    [SerializeField] private float sec = 5f;
    private float _time;
    
    
    private void Awake()
    {
        StartCoroutine(Countdown(sec));
    }

    private void Update()
    {
        
    }
    IEnumerator Countdown(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        gameObject.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale);
        yield return new WaitForSecondsRealtime(sec);
        Destroy(gameObject);
    }

    public void Ticking()
    {
        gameObject.GetComponent<MeshRenderer>().materials[0] = tock;
        new WaitForSecondsRealtime(3);
        gameObject.GetComponent<MeshRenderer>().materials[0] = tick;
        new WaitForSecondsRealtime(3);
    }
}
