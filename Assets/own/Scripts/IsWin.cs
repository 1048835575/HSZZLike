using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWin : MonoBehaviour
{
    public static IsWin _isWin;
    public GameObject EnemyMainTowerS;
    public GameObject EnemyMainTowerZ;
    public GameObject EnemyMainTowerX;
    public bool EnemyMainTowerSB;
    public bool EnemyMainTowerZB;
    public bool EnemyMainTowerXB;


    // Start is called before the first frame update
    void Start()
    {
        _isWin = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyMainTowerZ == null)
        {
            EnemyMainTowerZB = true;
        }
        if (EnemyMainTowerX == null)
        {
            EnemyMainTowerXB = true;
        }
        if (EnemyMainTowerS == null)
        {
            EnemyMainTowerSB = true;
        }
        if (EnemyMainTowerSB == true && EnemyMainTowerXB == true && EnemyMainTowerZB == true)
        {
            Game._instance.WinImg.SetActive(true);
            Game._instance.WinPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
