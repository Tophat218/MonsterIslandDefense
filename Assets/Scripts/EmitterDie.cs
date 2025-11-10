using System.Collections;
using UnityEngine;

public class EmitterDie : MonoBehaviour
{
    public int sec=15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Countdown(sec));
    }

    IEnumerator Countdown(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        Destroy(gameObject);
    }
}
