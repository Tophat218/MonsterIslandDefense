using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyProcedural : EnemyClass
{
    public override float health { set; get; }
    [SerializeField]public override float moveSpeed { set; get; } 
    [SerializeField]public override int reward { set; get; } 

    private void Start()
    {
        health = Random.Range(1000, 3000);
        if (health > 2000)
        {
            moveSpeed = Random.Range(0.001f, 0.005f);
            reward = Random.Range(1, 21);
        }
        else
        {
            moveSpeed = Random.Range(0.005f, 0.01f);
            reward = Random.Range(7, 10);
        }
        //moveSpeed = Random.Range(0.004f,0.01f);
        //reward = Random.Range(5,10);
    }

    public void UpdateHealthBar()
    {
        var matCopy = body.GetComponent<MeshRenderer>().materials;
        if (health >= 700)
        {
            matCopy[0] = good;
            body.GetComponent<MeshRenderer>().materials = matCopy;
        }else if (health < 700 && health >= 300)
        {
            matCopy[0] = worry;
            body.GetComponent<MeshRenderer>().materials = matCopy;
        }else if (health < 300 && health >= 0)
        {
            matCopy[0] = bad;
            body.GetComponent<MeshRenderer>().materials = matCopy;
        }
        else
        {
            matCopy[0] = bad;
            body.GetComponent<MeshRenderer>().materials = matCopy;
        }
        
    }
    public override void OnTriggerStay(Collider other)
    {
        //Enters range of defenders
        if (other.CompareTag("MainAttack"))
        {
            int delt = Random.Range(1, 5);
            health=health-delt;
            UpdateHealthBar();
        }

        if (other.CompareTag("PowAttack"))
        {
            int delt = Random.Range(1, 5);
            health=health-delt;
            UpdateHealthBar();
        }
        
        
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //For new tower attacks
        if (other.CompareTag("BoomAttack"))
        {
            int delt = Random.Range(100, 900);
            health=health-delt;
            UpdateHealthBar();
        }
        if (other.CompareTag("ZapAttack"))
        {
            int delt = Random.Range(100, 900);
            health=health-delt;
            UpdateHealthBar();
        }
    }
}
