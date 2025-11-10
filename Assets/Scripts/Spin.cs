using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Spin : MonoBehaviour
{
    public GameObject me;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        me.transform.Rotate(0,1,0,Space.Self);
        
    }
}
