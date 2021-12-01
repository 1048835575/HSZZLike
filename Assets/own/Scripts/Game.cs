using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game _instance;
    int i = 0;
    float timer;
    public GameObject[] enemyArr;
    public HeroData gongjianshouData;
    public HeroData jiqirenData;
    public HeroData shourenData;
    public HeroData jianshiData;
    public HeroData FireData;
    public HeroData YaSuoData;
    public HeroData AhriData;
    public HeroData KarthusData;
    public HeroData MasterYiData;
    public HeroData SonaData;
    public HeroData selectedHeroData;
    [SerializeField]
    public Transform Pos;
    public Transform Pos1;
    public Transform Pos2;
    GameObject obj;
    float HolyWater = 0;
    public Text HolyWaterText;
    public Animator HolyWaterAnimator;
    public Slider slider;
    public GameObject Suspend;
    public GameObject setBtn;
    int SpeedNum = 1;
    public Text speedText;
    public GameObject FailImg;
    public GameObject WinImg;
    public GameObject WinPanel;
    float GameTime = 0;
    public Text GameTimeText;
    public List<GameObject> list = new List<GameObject>(8);
    public GameObject[] chooseObj;
    int num;
    void Start()
    {
        _instance = this;

        selectedHeroData = null;
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Image>().sprite = CameraRoam._instance.sprites[i];
            list[i].name = CameraRoam._instance.sprites[i].name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float a = GameTime;
        GameTime += Time.deltaTime;
        TimeSpan gt = TimeSpan.FromSeconds(a);
        GameTimeText.text = string.Format("{0:D1}m:{1:D2}s", gt.Minutes, gt.Seconds);
        if (GameTime >= 300)
        {
            Time.timeScale = 0;
            FailImg.SetActive(true);
            WinPanel.SetActive(true);
        }
        if (HolyWater <= 10)
        {
            HolyWater += Time.deltaTime;
            TimeSpan t = TimeSpan.FromSeconds(HolyWater);
            HolyWaterText.text = string.Format("{0:D1}", t.Seconds);
            slider.value = HolyWater;
        }
        timer += Time.deltaTime;
        if (timer > 12)
        {
            Instantiate(enemyArr[i], Pos.position, Quaternion.identity);
            Instantiate(enemyArr[i], Pos1.position, Quaternion.identity);
            Instantiate(enemyArr[i], Pos2.position, Quaternion.identity);
            i++;
            if (i >= enemyArr.Length)
            {
                i = 0;
            }
            timer = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedHeroData == null)
                    {
                        Debug.Log("请选择英雄！");
                    }
                    else
                    {
                        if (HolyWater >= selectedHeroData.cost)
                        {
                            changeHolyWater(selectedHeroData.cost);
                            if (selectedHeroData.type != 1)
                            {
                                mapCube.buildHero(selectedHeroData);
                            }
                            else
                            {
                                Instantiate(selectedHeroData.heroPerfab, GameObject.Find("startPoint").transform.position, Quaternion.identity);
                            }
                            chooseObj[num].transform.parent = null;
                            chooseObj[num].transform.parent = GameObject.Find("ChoosePanel").transform;
                            selectedHeroData = null;
                        }
                        else
                        {
                            HolyWaterAnimator.SetTrigger("Failed");
                            Debug.Log("圣水不足！");
                        }

                    }
                }
                if (selectedHeroData != null && selectedHeroData.type == 1)
                {
                    Debug.Log("错误");
                    if (selectedHeroData == null)
                    {
                        Debug.Log("请选择英雄！");
                    }
                    else
                    {
                        if (HolyWater >= selectedHeroData.cost)
                        {
                            changeHolyWater(selectedHeroData.cost);
                            Instantiate(selectedHeroData.heroPerfab, GameObject.Find("startPoint").transform.position, Quaternion.identity);
                            chooseObj[num].transform.parent = null;
                            chooseObj[num].transform.parent = GameObject.Find("ChoosePanel").transform;
                            selectedHeroData = null;
                        }
                        else
                        {
                            HolyWaterAnimator.SetTrigger("Failed");
                            Debug.Log("圣水不足！");
                        }
                    }
                }
            }
        }

    }
    public void GoOn()
    {
        Time.timeScale = SpeedNum;
        Suspend.SetActive(false);
        setBtn.SetActive(true);
    }
    public void SuspendBtn()
    {
        Time.timeScale = 0;
        Suspend.SetActive(true);
        setBtn.SetActive(false);
    }
    public void Speed()
    {
        if (SpeedNum < 4)
        {
            SpeedNum++;
            Time.timeScale = SpeedNum;
            Suspend.SetActive(false);
            setBtn.SetActive(true);
            speedText.text = "X" + SpeedNum;
        }
        else
        {
            SpeedNum = 1;
            Time.timeScale = SpeedNum;
            Suspend.SetActive(false);
            setBtn.SetActive(true);
            speedText.text = "X" + SpeedNum;
        }
    }
    void changeHolyWater(int change)
    {
        HolyWater -= change;
        slider.value = HolyWater;
    }

    public void bianfu(bool ison)
    {
        if (ison)
        {
            switch (list[0].name)
            {
                case "sanpao":
                    selectedHeroData = gongjianshouData;
                    break;
                case "huoqiu":
                    selectedHeroData = FireData;
                    break;
                case "dahuzi":
                    selectedHeroData = jiqirenData;
                    break;
                case "dapika":
                    selectedHeroData = shourenData;
                    break;
                case "dianfa":
                    selectedHeroData = jianshiData;
                    break;
                case "YaSuo":
                    selectedHeroData = YaSuoData;
                    break;
                case "Ahri":
                    selectedHeroData = AhriData;
                    break;
                case "Karthus":
                    selectedHeroData = KarthusData;
                    break;
                case "MasterYi":
                    selectedHeroData = MasterYiData;
                    break;
                case "Sona":
                    selectedHeroData = SonaData;
                    break;

            }
            num = 0;
        }
    }
    public void dahuzi(bool ison)
    {
        if (ison)
        {
            switch (list[1].name)
            {
                case "sanpao":
                    selectedHeroData = gongjianshouData;
                    break;
                case "huoqiu":
                    selectedHeroData = FireData;
                    break;
                case "dahuzi":
                    selectedHeroData = jiqirenData;
                    break;
                case "dapika":
                    selectedHeroData = shourenData;
                    break;
                case "dianfa":
                    selectedHeroData = jianshiData;
                    break;
                case "YaSuo":
                    selectedHeroData = YaSuoData;
                    break;
                case "Ahri":
                    selectedHeroData = AhriData;
                    break;
                case "Karthus":
                    selectedHeroData = KarthusData;
                    break;
                case "MasterYi":
                    selectedHeroData = MasterYiData;
                    break;
                case "Sona":
                    selectedHeroData = SonaData;
                    break;
            }
            num = 1;
        }
    }
    public void shibing(bool ison)
    {
        if (ison)
        {
            switch (list[2].name)
            {
                case "sanpao":
                    selectedHeroData = gongjianshouData;
                    break;
                case "huoqiu":
                    selectedHeroData = FireData;
                    break;
                case "dahuzi":
                    selectedHeroData = jiqirenData;
                    break;
                case "dapika":
                    selectedHeroData = shourenData;
                    break;
                case "dianfa":
                    selectedHeroData = jianshiData;
                    break;
                case "YaSuo":
                    selectedHeroData = YaSuoData;
                    break;
                case "Ahri":
                    selectedHeroData = AhriData;
                    break;
                case "Karthus":
                    selectedHeroData = KarthusData;
                    break;
                case "MasterYi":
                    selectedHeroData = MasterYiData;
                    break;
                case "Sona":
                    selectedHeroData = SonaData;
                    break;
            }
            num = 2;
        }
    }
    public void siling(bool ison)
    {
        if (ison)
        {
            switch (list[3].name)
            {
                case "sanpao":
                    selectedHeroData = gongjianshouData;
                    break;
                case "huoqiu":
                    selectedHeroData = FireData;
                    break;
                case "dahuzi":
                    selectedHeroData = jiqirenData;
                    break;
                case "dapika":
                    selectedHeroData = shourenData;
                    break;
                case "dianfa":
                    selectedHeroData = jianshiData;
                    break;
                case "YaSuo":
                    selectedHeroData = YaSuoData;
                    break;
                case "Ahri":
                    selectedHeroData = AhriData;
                    break;
                case "Karthus":
                    selectedHeroData = KarthusData;
                    break;
                case "MasterYi":
                    selectedHeroData = MasterYiData;
                    break;
                case "Sona":
                    selectedHeroData = SonaData;
                    break;
            }
            num = 3;
        }
    }
    public void bianfu2(bool ison)
    {
        if (ison)
        {
            switch (list[4].name)
            {
                case "sanpao":
                    selectedHeroData = gongjianshouData;
                    break;
                case "huoqiu":
                    selectedHeroData = FireData;
                    break;
                case "dahuzi":
                    selectedHeroData = jiqirenData;
                    break;
                case "dapika":
                    selectedHeroData = shourenData;
                    break;
                case "dianfa":
                    selectedHeroData = jianshiData;
                    break;
                case "YaSuo":
                    selectedHeroData = YaSuoData;
                    break;
                case "Ahri":
                    selectedHeroData = AhriData;
                    break;
                case "Karthus":
                    selectedHeroData = KarthusData;
                    break;
                case "MasterYi":
                    selectedHeroData = MasterYiData;
                    break;
                case "Sona":
                    selectedHeroData = SonaData;
                    break;
            }
            num = 4;
        }
    }
    public void bianfu3(bool ison)
    {
        if (ison)
        {
            switch (list[5].name)
            {
                case "sanpao":
                    selectedHeroData = gongjianshouData;
                    break;
                case "huoqiu":
                    selectedHeroData = FireData;
                    break;
                case "dahuzi":
                    selectedHeroData = jiqirenData;
                    break;
                case "dapika":
                    selectedHeroData = shourenData;
                    break;
                case "dianfa":
                    selectedHeroData = jianshiData;
                    break;
                case "YaSuo":
                    selectedHeroData = YaSuoData;
                    break;
                case "Ahri":
                    selectedHeroData = AhriData;
                    break;
                case "Karthus":
                    selectedHeroData = KarthusData;
                    break;
                case "MasterYi":
                    selectedHeroData = MasterYiData;
                    break;
                case "Sona":
                    selectedHeroData = SonaData;
                    break;
            }
            num = 5;
        }
    }
    public void bianfu4(bool ison)
    {
        if (ison)
        {
            switch (list[6].name)
            {
                case "sanpao":
                    selectedHeroData = gongjianshouData;
                    break;
                case "huoqiu":
                    selectedHeroData = FireData;
                    break;
                case "dahuzi":
                    selectedHeroData = jiqirenData;
                    break;
                case "dapika":
                    selectedHeroData = shourenData;
                    break;
                case "dianfa":
                    selectedHeroData = jianshiData;
                    break;
                case "YaSuo":
                    selectedHeroData = YaSuoData;
                    break;
                case "Ahri":
                    selectedHeroData = AhriData;
                    break;
                case "Karthus":
                    selectedHeroData = KarthusData;
                    break;
                case "MasterYi":
                    selectedHeroData = MasterYiData;
                    break;
                case "Sona":
                    selectedHeroData = SonaData;
                    break;
            }
            num = 6;
        }
    }
    public void bianfu5(bool ison)
    {
        if (ison)
        {
            switch (list[7].name)
            {
                case "sanpao":
                    selectedHeroData = gongjianshouData;
                    break;
                case "huoqiu":
                    selectedHeroData = FireData;
                    break;
                case "dahuzi":
                    selectedHeroData = jiqirenData;
                    break;
                case "dapika":
                    selectedHeroData = shourenData;
                    break;
                case "dianfa":
                    selectedHeroData = jianshiData;
                    break;
                case "YaSuo":
                    selectedHeroData = YaSuoData;
                    break;
                case "Ahri":
                    selectedHeroData = AhriData;
                    break;
                case "Karthus":
                    selectedHeroData = KarthusData;
                    break;
                case "MasterYi":
                    selectedHeroData = MasterYiData;
                    break;
                case "Sona":
                    selectedHeroData = SonaData;
                    break;
            }
            num = 7;
        }
    }

}
