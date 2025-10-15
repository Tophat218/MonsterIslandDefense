using System;
using UnityEngine;
public class EnemyTank : EnemyClass
{
    public override float health { set; get; } = 10000;
    public override float moveSpeed => 0.001f;
    public override int reward => 5;
    
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

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //For new tower attacks
        if (other.CompareTag("BoomAttack"))
        {
            health=health-900;
            UpdateHealthBar();
        }
        if (other.CompareTag("ZapAttack"))
        {
            health=health/2;
            UpdateHealthBar();
        }
    }
    
    
    
    public void UpdateHealthBar()
    {
        var matCopy = body.GetComponent<SkinnedMeshRenderer>().materials;
        if (health >= 5000)
        {
            matCopy[1] = good;
            body.GetComponent<SkinnedMeshRenderer>().materials = matCopy;
        }else if (health < 5000 && health >= 500)
        {
            matCopy[1] = worry;
            body.GetComponent<SkinnedMeshRenderer>().materials = matCopy;
        }else if (health < 500 && health >= 0)
        {
            matCopy[1] = bad;
            body.GetComponent<SkinnedMeshRenderer>().materials = matCopy;
        }
        else
        {
            matCopy[1] = bad;
            body.GetComponent<SkinnedMeshRenderer>().materials = matCopy;
        }
        
    }
}