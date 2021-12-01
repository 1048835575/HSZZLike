using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIsWin : MonoBehaviour
{
    public static EnemyIsWin _isWin;
    public GameObject MainTowerS;
    public GameObject MainTowerZ;
    public GameObject MainTowerX;
    public bool MainTowerSB;
    public bool MainTowerZB;
    public bool MainTowerXB;
    // Start is called before the first frame update
    void Start()
    {
        _isWin = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainTowerZ == null)
        {
            MainTowerZB = true;
        }
        if (MainTowerX == null)
        {
            MainTowerXB = true;
        }
        if (MainTowerS == null)
        {
            MainTowerSB = true;
        }
        if (MainTowerSB == true && MainTowerXB == true && MainTowerZB == true)
        {
            Game._instance.FailImg.SetActive(true);
            Game._instance.WinPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
