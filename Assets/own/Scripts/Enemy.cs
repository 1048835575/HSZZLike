using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent myNavMeshAgent;
    public Slider slider;
    Animation ani;
    public float Attack;
    float startTime = 0;
    public float AttackTime = 0;
    public GameObject TargetPlayer;
    public string name1;
    
    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        slider.transform.parent.transform.rotation = Camera.main.transform.rotation;
        startTime += Time.deltaTime;
        if (startTime >= AttackTime && TargetPlayer != null)
        {
            switch (TargetPlayer.tag)
            {
                case "Tower":
                    TargetPlayer.transform.parent.GetComponent<Tower>().LessBlood(Attack);
                    break;
                case "MainTowerS":
                    TargetPlayer.transform.parent.GetComponent<Tower>().LessBlood(Attack);
                    break;
                case "MainTowerZ":
                    TargetPlayer.transform.parent.GetComponent<Tower>().LessBlood(Attack);
                    break;
                case "MainTowerX":
                    TargetPlayer.transform.parent.GetComponent<Tower>().LessBlood(Attack);
                    break;
                case "Player":
                    TargetPlayer.transform.parent.GetComponent<Navigation>().LessBlood(Attack);
                    break;
            }
            startTime = 0;
        }
        switch (this.name1)
        {
            case "archer mesh下":
                if (TargetPlayer == null && EnemyIsWin._isWin.MainTowerXB == false)
                {
                    ani.Play("walk");
                    myNavMeshAgent.SetDestination(GameObject.Find(name1).transform.position);
                    myNavMeshAgent.speed = 1.5f;
                }
                break;
            case "archer mesh中":
                if (TargetPlayer == null && EnemyIsWin._isWin.MainTowerZB == false)
                {
                    ani.Play("walk");
                    myNavMeshAgent.SetDestination(GameObject.Find(name1).transform.position);
                    myNavMeshAgent.speed = 1.5f;
                }
                break;
            case "archer mesh上":
                if (TargetPlayer == null && EnemyIsWin._isWin.MainTowerSB == false)
                {

                    ani.Play("walk");
                    myNavMeshAgent.SetDestination(GameObject.Find(name1).transform.position);
                    myNavMeshAgent.speed = 1.5f;
                }
                break;
        }
        if (TargetPlayer != null && TargetPlayer.tag == "Tower" && TargetPlayer.transform.parent.GetComponent<Tower>().slider.value <= 0)
        {
            ani.Play("walk");
            myNavMeshAgent.speed = 1.5f;
        }
        if (TargetPlayer != null && TargetPlayer.tag == "Player" && TargetPlayer.transform.parent.GetComponent<Navigation>().slider.value <= 0)
        {
            Destroy(TargetPlayer.transform.parent.gameObject);
            ani.Play("walk");
            myNavMeshAgent.speed = 1.5f;
        }//11
        if (TargetPlayer != null && (TargetPlayer.tag == "MainTowerS" || TargetPlayer.tag == "MainTowerZ" || TargetPlayer.tag == "MainTowerX") && TargetPlayer.transform.parent.GetComponent<Tower>().slider.value <= 0)
        {
            Destroy(TargetPlayer.transform.parent.gameObject);
            ani.Play("idle");
            myNavMeshAgent.speed = 0;
        }
        //if ((TargetPlayer != null && TargetPlayer.tag == "MainTowerS" && TargetPlayer.transform.parent.GetComponent<Tower>().slider.value <= 0)&& (TargetPlayer != null && TargetPlayer.tag == "MainTowerZ" && TargetPlayer.transform.parent.GetComponent<Tower>().slider.value <= 0)&& (TargetPlayer != null && TargetPlayer.tag == "MainTowerX" && TargetPlayer.transform.parent.GetComponent<Tower>().slider.value <= 0))
        //{

        //    Debug.Log("被敌人成功突破");
        //    Game._instance.FailImg.SetActive(true);
        //    Game._instance.WinPanel.SetActive(true);
        //    Time.timeScale = 0;
        //}
    }
    void Attacking()
    {
        ani.Play("attack01");
        myNavMeshAgent.speed = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Tower":
                TargetPlayer = other.gameObject;
                Attacking();
                gameObject.transform.LookAt(other.transform.position);
                break;
            case "MainTowerS":
                TargetPlayer = other.gameObject;
                Attacking();
                gameObject.transform.LookAt(other.transform.position);
                break;
            case "MainTowerZ":
                TargetPlayer = other.gameObject;
                Attacking();
                gameObject.transform.LookAt(other.transform.position);
                break;
            case "MainTowerX":
                TargetPlayer = other.gameObject;
                Attacking();
                gameObject.transform.LookAt(other.transform.position);
                break;
            case "Player":
                TargetPlayer = other.gameObject;
                Attacking();
                gameObject.transform.LookAt(other.transform.position);
                break;
            case "KNight":
                TargetPlayer = other.gameObject;
                myNavMeshAgent.SetDestination(TargetPlayer.transform.position);
                myNavMeshAgent.speed = 1.5f;
                gameObject.transform.LookAt(other.transform.position);
                break;

        }
        if (other.gameObject.tag == "Plane x")
        {

            name1 = "archer mesh下";
        }
        if (other.gameObject.tag == "Plane z")
        {

            name1 = "archer mesh中";
        }
        if (other.gameObject.tag == "Plane s")
        {
            name1 = "archer mesh上";
                
        }
    }
    public void AddBlood()
    {
        if (slider.value <= 100)
        {
            if (slider.value > 95)
            {
                slider.value = 100;
            }
            slider.value += 5;
        }
    }
    public void LessBlood(float att)
    {
        if (slider.value >= 0)
        {
            if (slider.value < att)
            {
                slider.value = 0;
            }
            slider.value -= att;
            if (slider.value <= 0)
            {
                ani.Play("die");
                myNavMeshAgent.speed = 0;
                //Destroy(gameObject, 1.5f);

            }

        }
    }

}
