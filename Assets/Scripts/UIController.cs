using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI wealth;
    public TextMeshProUGUI taunt;
    public Button restart;
    private Button saved;

    public Button begin;

    public Button placePulse;

    public Button placeStrike;

    public Button placeZap;

    public int Towertype;
    public int monies;
    [SerializeField] private int lvl = 4;

    public GameObject enemyScout;
    public GameObject enemyTank;
    public GameObject enemyCyote;
    [SerializeField] private GameObject gameMap;
    public int pricePulse = 5;
    public int priceStrike = 10;
    public int priceZap = 15;
    public bool placing = false;
    public int price;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        taunt.text = "";
        wealth.text = "Budget: " + wealth + "\u25a1\u00b0";
        restart.onClick.AddListener(onRestartClick);
        restart.enabled = false;
        restart.gameObject.SetActive(false);
        begin.onClick.AddListener(onBeginClick);
        
        placePulse.onClick.AddListener(onPulseClick);
        placePulse.GetComponentInChildren<TextMeshProUGUI>().text = pricePulse + " \u25a1\u00b0";
        
        placeStrike.onClick.AddListener(onStrikeClick);
        placeStrike.GetComponentInChildren<TextMeshProUGUI>().text = priceStrike + " \u25a1\u00b0";
        
        placeZap.onClick.AddListener(onZapClick);
        placeZap.GetComponentInChildren<TextMeshProUGUI>().text = priceZap + " \u25a1\u00b0";
        
        
    }

    // Update is called once per frame
    void Update()
    {
        wealth.text = "Budget: " + monies + "\u25a1\u00b0";
    }

    void onBeginClick()
    {
        //onGameOver();
        begin.enabled = false;
        List<GameObject> enemies = new List<GameObject>();
        GameObject pulse = GameObject.FindWithTag("PowAttack");
        GameObject boom = GameObject.FindWithTag("TowerBoom");
        GameObject zap = GameObject.FindWithTag("TowerZap");
        int normal = 1;
        int tankNum = 0;
        int cyoteNum = 0;
        if (pulse != null)
        {
            cyoteNum += 1;
        }
        if (boom != null)
        {
            normal += 1;
        }
        if (zap != null)
        {
            tankNum += 1;
        }

        normal = normal * lvl;
        tankNum = tankNum + lvl;
        cyoteNum = cyoteNum + lvl;

        for (int i = 0; i <= normal; i++)
        {
            enemies.Add(enemyScout);
        }
        for (int i = 0; i <= cyoteNum; i++)
        {
            enemies.Add(enemyCyote);
        }
        for (int i = 0; i <= tankNum; i++)
        {
            enemies.Add(enemyTank);
        }

        int counter = 1;
        foreach (var monster in enemies)
        {
            if (counter == 1)
            {
                counter++;
                GameObject enemy = Instantiate(monster, gameMap.GetComponent<GenTerrain>().portal1Coords,
                    Quaternion.identity);
            }else if (counter == 2)
            {
                counter++;
                GameObject enemy = Instantiate(monster, gameMap.GetComponent<GenTerrain>().portal2Coords,
                    Quaternion.identity);
            }else if (counter == 3)
            {
                counter++;
                GameObject enemy = Instantiate(monster, gameMap.GetComponent<GenTerrain>().portal3Coords,
                    Quaternion.identity);
            }else if (counter == 4)
            {
                counter = 1;
                GameObject enemy = Instantiate(monster, gameMap.GetComponent<GenTerrain>().portal4Coords,
                    Quaternion.identity);
            }
            else
            {
                counter = 1;
                GameObject enemy = Instantiate(monster, gameMap.GetComponent<GenTerrain>().portal4Coords,
                    Quaternion.identity);
            }

            new WaitForSecondsRealtime(5f);

        }
        StartCoroutine(WaitingTillReady(10f));
        lvl++;
    }

    IEnumerator WaitingTillReady(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        begin.enabled = true;
    }

    void onPulseClick()
    {
        price = pricePulse;
        Towertype = 0;
        if (monies >= price)
        {
            placing = true;
        }
        else
        {
            placing = false;
        }
        
    }

    void onStrikeClick()
    {
        price = priceStrike;
        Towertype = 1;
        if (monies >= price)
        {
            placing = true;
        }
        else
        {
            placing = false;
        }
    }

    void onZapClick()
    {
        price = priceZap;
        Towertype = 3;
        if (monies >= price)
        {
            placing = true;
        }
        else
        {
            placing = false;
        }
    }

    void onRestartClick()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void onGameOver()
    {
        taunt.text = "Game Over";
        restart.enabled = true;
        restart.gameObject.SetActive(true);
    }
}
