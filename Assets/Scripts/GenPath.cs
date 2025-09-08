using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private List<GameObject> path = new List<GameObject>();
    private List<GameObject> topTiles = new List<GameObject>();
    private List<GameObject> midTiles = new List<GameObject>();
    private List<GameObject> bottomTiles = new List<GameObject>();
    private List<GameObject> sideTiles = new List<GameObject>();
    private List<GameObject> sideTilesExtra = new List<GameObject>();
    private List<GameObject> buildingSpots = new List<GameObject>();
    
    private int radius;
    private int currentTileIndex;
    

    private GameObject startTile1;
    private GameObject startTile2;
    private GameObject startTile3;
    private GameObject startTile4;
    
    private GameObject endTile;
    
    private List<GameObject> path1 = new List<GameObject>();
    private List<GameObject> path2 = new List<GameObject>();
    private List<GameObject> path3 = new List<GameObject>();
    private List<GameObject> path4 = new List<GameObject>();

    public List<GameObject> GetPath()
    {
       
        return path;
    }

    public Vector3 GetStart1()
    {
        return startTile1.transform.position;
    }
    public Vector3 GetStart2()
    {
        return startTile2.transform.position;
    }
    public Vector3 GetStart3()
    {
        return startTile3.transform.position;
    }
    public Vector3 GetStart4()
    {
        return startTile4.transform.position;
    }

    public Vector3 GetEnd()
    {
        return endTile.transform.position;
    }
    
//Parse in radius during World Gen
    public Path(int radius)
    {
        this.radius = radius;
    }

    public void TopandBottomTiles(int z, int x, GameObject tile)
    {
        int num = Random.Range(0, 100);
        if (z == 0)
        {
            topTiles.Add(tile);
        }

        if (z == radius/2 - 1)
        {
            midTiles.Add(tile);
        }

        if (z == radius - 1)
        {
            bottomTiles.Add(tile);
        }

        if (x == 0)
        {
            sideTiles.Add(tile);
        }
        if (x == radius - 1)
        {
            sideTilesExtra.Add(tile);
        }
    }

    private bool AssignCheckStartEndTiles()
    {
        int xIndex = Random.Range(2, topTiles.Count - 2);
        int zIndex1 = Random.Range(2, bottomTiles.Count - 2);
        int yIndex1 = Random.Range(2, sideTiles.Count - 2);
        int yIndex2 = Random.Range(2, sideTilesExtra.Count - 2);

        startTile1 = topTiles[xIndex];
        startTile2 = bottomTiles[zIndex1];
        startTile3 = sideTiles[yIndex1];
        startTile4 = sideTilesExtra[yIndex2];
        endTile = midTiles[radius/2];
        return startTile1 != null && endTile != null && startTile2 != null && 
               startTile3 != null && startTile4 !=null;
    }

    public void MakePaths()
    {
        if (AssignCheckStartEndTiles())
        {
            GameObject currentTile1 = startTile1;
            GameObject currentTile2 = startTile2;
            GameObject currentTile3 = startTile3;
            GameObject currentTile4 = startTile4;

            for (int i = 0; i < 5; i++)
            {
                MoveLeft(ref currentTile1);
                MoveRight(ref currentTile2);
                MoveUp(ref currentTile3);
                MoveDown(ref currentTile4);
            }

            PathLoops(currentTile1);
            PathLoops(currentTile2);
            PathLoops(currentTile3);
            PathLoops(currentTile4);

            path.Add(endTile);
        }
    }

    private void MoveDown(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = GenTerrain.GeneratedTiles.IndexOf(currentTile);
        buildingSpots.Add(GenTerrain.GeneratedTiles[currentTileIndex++]);
        buildingSpots.Add(GenTerrain.GeneratedTiles[currentTileIndex--]);
        int n = currentTileIndex - radius;
        currentTile = GenTerrain.GeneratedTiles[n];
    }
    private void MoveUp(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = GenTerrain.GeneratedTiles.IndexOf(currentTile);
        buildingSpots.Add(GenTerrain.GeneratedTiles[currentTileIndex++]);
        buildingSpots.Add(GenTerrain.GeneratedTiles[currentTileIndex--]);
        int n = currentTileIndex + radius;
        currentTile = GenTerrain.GeneratedTiles[n];
    }

    private void MoveLeft(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = GenTerrain.GeneratedTiles.IndexOf(currentTile);
        buildingSpots.Add(GenTerrain.GeneratedTiles[currentTileIndex-radius]);
        buildingSpots.Add(GenTerrain.GeneratedTiles[currentTileIndex+radius]);
        currentTileIndex++;
        currentTile = GenTerrain.GeneratedTiles[currentTileIndex];
    }
    private void MoveRight(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = GenTerrain.GeneratedTiles.IndexOf(currentTile);
        buildingSpots.Add(GenTerrain.GeneratedTiles[currentTileIndex-radius]);
        buildingSpots.Add(GenTerrain.GeneratedTiles[currentTileIndex+radius]);
        currentTileIndex--;
        currentTile = GenTerrain.GeneratedTiles[currentTileIndex];
    }

    private void PathLoops(GameObject currentTile1)
    {
        bool reachedX = false;
        bool reachedZ = false;
        //ensure no crash
        var safetyX = 0;
        while (!reachedX)
        {
            safetyX++;
            if (safetyX > 100)
            {
                break;
            }

            if (currentTile1.transform.position.x > endTile.transform.position.x)
            {
                MoveDown(ref currentTile1);

            }
            else if (currentTile1.transform.position.x < endTile.transform.position.x)
            {
                MoveUp(ref currentTile1);

            }
            else
            {
                reachedX = true;
            }
            
        }

        var safetyZ = 0;
            while (!reachedZ)
            {
                safetyZ++;
                if (safetyZ > 100)
                {
                    break;
                }

                if (currentTile1.transform.position.z > endTile.transform.position.z)
                {
                    MoveRight(ref currentTile1);
                }
                else if (currentTile1.transform.position.z < endTile.transform.position.z)
                {
                    MoveLeft(ref currentTile1);
                }
                else
                {
                    reachedZ = true;
                }
            }
        
    }

    public List<GameObject> GetBuildingSpots()
    {
        for (int i = 0; i < buildingSpots.Count; i++)
        {
            for (int j = 0; j < path.Count; j++)
            {
                if (buildingSpots[i] == path[j])
                {
                    buildingSpots.Remove(buildingSpots[i]);
                }
            }
        }

        for (int i = 0; i < buildingSpots.Count; i++)
        {
            if (buildingSpots[i].transform.position.x == 0 || buildingSpots[i].transform.position.z == 0|| 
                buildingSpots[i].transform.position.x == radius || buildingSpots[i].transform.position.z == radius)
            {
                buildingSpots.Remove(buildingSpots[i]);
            }
        }

        while (buildingSpots.Count != 15)
        {
            int rand = Random.Range(0, buildingSpots.Count-1);
            buildingSpots.Remove(buildingSpots[rand]);
        }

        
        return buildingSpots;
    }

    

}
