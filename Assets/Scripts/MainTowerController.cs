using System;
using UnityEngine;

public class MainTowerController : MonoBehaviour
{
    [SerializeField] private Material good;

    [SerializeField] private Material worry;

    [SerializeField] private Material bad;
    [SerializeField] private Material deathDoor;

    [SerializeField] private int health = 10;
    public GameObject ui;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.GetComponent<MeshRenderer>().materials[4] = good;
        ui = GameObject.FindWithTag("Interface");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ui.GetComponent<UIController>().onGameOver();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //subtract health
            health--;
            
            //Update healthbar
            var matCopy = gameObject.GetComponent<MeshRenderer>().materials;
            if (health >= 7)
            {
                matCopy[4] = good;
                gameObject.GetComponent<MeshRenderer>().materials = matCopy;
                //gameObject.GetComponent<MeshRenderer>().materials[4] = good;
                Debug.Log(health + "above");
            }else if (health <= 6 && health >= 3)
            {
                matCopy[4] = worry;
                gameObject.GetComponent<MeshRenderer>().materials = matCopy;
                //gameObject.GetComponent<MeshRenderer>().materials[4] = worry;
                Debug.Log(health + "worry");
            }else if (health < 3 && health > 1)
            {
                matCopy[4] = bad;
                gameObject.GetComponent<MeshRenderer>().materials = matCopy;
                //gameObject.GetComponent<MeshRenderer>().materials[4] = bad;
                Debug.Log(health + "bad");
            }
            else
            {
                matCopy[4] = deathDoor;
                gameObject.GetComponent<MeshRenderer>().materials = matCopy;
                //gameObject.GetComponent<MeshRenderer>().materials[4] = deathDoor;
                Debug.Log(health + "gone wrong/dead");
            }
        }
    }
}
