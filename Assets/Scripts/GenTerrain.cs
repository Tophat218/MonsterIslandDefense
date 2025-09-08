
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTerrain : MonoBehaviour
{
    [SerializeField]private GameObject tilePrefab1;
    [SerializeField]private GameObject tilePrefab2;
    [SerializeField] private GameObject mainTower;
    [SerializeField] private GameObject buildingSpot;
    [SerializeField] private GameObject portal;
    [SerializeField] private int radius = 10;

    public static List<GameObject> GeneratedTiles = new List<GameObject>();
    public List<GameObject> GeneratedPaths = new List<GameObject>();

    public Vector3 portal1Coords;
    public Vector3 portal2Coords;
    public Vector3 portal3Coords;
    public Vector3 portal4Coords;

    [SerializeField]private Path pathGen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathGen = new Path(radius);
        
        //Generate Grid
        for (int x = 0; x < radius; x++)
        {
            for (int z = 0; z < radius; z++)
            {
                GameObject tile = Instantiate(tilePrefab1, 
                    new Vector3(x * 1.5f, 0, z * 1.5f), Quaternion.identity);
                GeneratedTiles.Add(tile);
                pathGen.TopandBottomTiles(z, x, tile);
            }
        }
        pathGen.MakePaths();
        foreach (GameObject pObject in pathGen.GetPath())
        {
            pObject.SetActive(false);
            GameObject pathway = Instantiate(tilePrefab2, pObject.transform.position,Quaternion.identity);
            GeneratedPaths.Add(pathway);
            
        }
        GameObject myTower = Instantiate(mainTower, pathGen.GetEnd(), Quaternion.identity);
        GameObject portal1 = Instantiate(portal, pathGen.GetStart1(), Quaternion.identity);
        portal1Coords = portal1.transform.position;
        GameObject portal2 = Instantiate(portal, pathGen.GetStart2(), Quaternion.identity);
        portal2Coords = portal2.transform.position;
        GameObject portal3 = Instantiate(portal, pathGen.GetStart3(), Quaternion.identity);
        portal3Coords = portal3.transform.position;
        GameObject portal4 = Instantiate(portal, pathGen.GetStart4(), Quaternion.identity);
        portal4Coords = portal4.transform.position;
        
        foreach (GameObject pObject in pathGen.GetBuildingSpots())
        {
            pObject.SetActive(false);
            GameObject spot = Instantiate(buildingSpot, pObject.transform.position,Quaternion.identity);
            
            
        }

    }

    
}
