using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EnemyClass : MonoBehaviour
{
    public virtual float health { get; set; }
    public virtual float moveSpeed { get; set; }
    public virtual int reward { get; set; }
    
    [SerializeField] private GameObject gameMap;
    private List<GameObject> genPath; 
    [SerializeField]private GameObject next;
    private Vector3 startPoint;
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 point3;
    private Vector3 point4;
    public bool happend = false;
    public GameObject body;
    public Material good;
    public Material worry;
    public Material bad;
    public GameObject ui;

    //When unit is spawned
    void Awake()
    {
        ui = GameObject.FindWithTag("Interface");
        gameMap = GameObject.FindWithTag("Ground");
        if (gameMap == null)
        {
            Debug.Log("Not working");
        }
        point1 = gameMap.GetComponent<GenTerrain>().portal1Coords;
        point2 = gameMap.GetComponent<GenTerrain>().portal2Coords;
        point3 = gameMap.GetComponent<GenTerrain>().portal3Coords;
        point4 = gameMap.GetComponent<GenTerrain>().portal4Coords;
        genPath = gameMap.GetComponent<GenTerrain>().GeneratedPaths;
        startPoint = gameObject.transform.position;
        next = FindNextDestination();
        Move(next);
        
        
    }
    public void Update()
    {
        
        if (health <= 0)
        {
            ui.GetComponent<UIController>().monies += reward;
            Destroy(gameObject);
        }
        Move(next);
    }

    public virtual void OnTriggerStay(Collider other)
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            //teleport to random portal and start again
            //tower takes damage
            int rand = Random.Range(0, 4);
            if (rand == 0)
            {
                transform.position = gameMap.GetComponent<GenTerrain>().portal1Coords;
                next = FindNextDestination();
            }else if (rand == 1)
            {
                transform.position = gameMap.GetComponent<GenTerrain>().portal2Coords;
                next = FindNextDestination();
            }else if (rand == 2)
            {
                transform.position = gameMap.GetComponent<GenTerrain>().portal3Coords;
                next = FindNextDestination();
            }else if (rand == 3)
            {
                transform.position = gameMap.GetComponent<GenTerrain>().portal4Coords;
                next = FindNextDestination();
            }
            else
            {
                transform.position = gameMap.GetComponent<GenTerrain>().portal1Coords;
                next = FindNextDestination();
            }

            happend = false;


        }
        
    }

    public virtual void Move(GameObject destination)
    {
        float speed = 1.0f;
        //Enemy chooses to move to nearest pathway 
        //that also gets them closer destination(Tower)
        Vector3 newDirection = Vector3.RotateTowards(transform.forward,
            destination.transform.position - transform.position, speed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, moveSpeed);
        if (transform.position == next.transform.position)
        {
            next=FindNextDestination();
        }
    }

    public GameObject FindNextDestination()
    {
        float pathCount = genPath.Count;
        GameObject nextTile = null;
        for (int i = 0; i <= genPath.Count-1; i++)
        {
            if (genPath[i].transform.position == gameObject.transform.position)
            {
                if (i >= 17)
                {
                    
                    if (!happend)
                    {
                        for (int j = 21; j <= genPath.Count - 1; j++)
                        {
                            if ((genPath[j].transform.position.x == gameObject.transform.position.x+1.5 
                                 || genPath[j].transform.position.x-1.5 == gameObject.transform.position.x-1.5))
                            {
                                nextTile = genPath[j];
                            }else if ((genPath[j].transform.position.z+1.5 == gameObject.transform.position.z+1.5 
                                       || genPath[j].transform.position.z-1.5 == gameObject.transform.position.z-1.5))
                            {
                                nextTile = genPath[j];
                            }
                        }
                        happend = true;
                        return nextTile;
                    }
                    else
                    {
                        nextTile = genPath[i+1];
                        return nextTile;
                    }
                }
                else
                {
                    nextTile = genPath[i+4];
                    return nextTile;
                }
                
            }
        }
        
        return null;
    }
}
