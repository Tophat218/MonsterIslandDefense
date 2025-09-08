using System;
using UnityEngine;

public class EnemyScout : EnemyClass
{
    public override float health { get; set; } = 1000;
    [SerializeField]public override float moveSpeed => 0.005f;
    [SerializeField]public override int reward => 2;

    
    public override void OnTriggerStay(Collider other)
    {
        //Enters range of defenders
        if (other.CompareTag("MainAttack"))
        {
            health=health-1;
            UpdateHealthBar();
        }

        if (other.CompareTag("PowAttack"))
        {
            health=health-1;
            UpdateHealthBar();
        }
        
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

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
