using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyTower : MonoBehaviour
{
    public static EnemyTower _instance;
    public List<GameObject> enemys = new List<GameObject>();
    public Animator anim;
    float startTime = 0;
    public float AttackTime = 0;
    public Slider slider;
    public GameObject buttle;
    public Transform firePosition;
    public GameObject BombObj;
    public GameObject TowerObj;
    public AudioSource audioSource;
 
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")

        {
            Debug.Log("发现玩家！");
            enemys.Add(other.gameObject);

            anim.SetBool("attack", true);
            gameObject.transform.LookAt(other.transform.position);

        }
    }
    void Start()
    {
        _instance = this;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                enemys.RemoveAt(i);
            }
        }
        if (enemys.Count > 0)
        {
            if (enemys[0].transform.parent.GetComponent<Navigation>().slider.value <= 0)
            {
                anim.SetBool("attack", false);
                Destroy(enemys[0].transform.parent.GetComponent<Navigation>());
                enemys.Remove(enemys[0].transform.parent.gameObject);
                Destroy(enemys[0].transform.parent.gameObject);
               
            }
            else
            {
                startTime += Time.deltaTime;
                if (startTime >= AttackTime)
                {
                    Attack();
                    startTime = 0;
                }

            }
          
        }

        //if (enemys.Count > 0 && enemys[0].GetComponent<Enemy>().slider.value <= 0)
        //{
        //    enemys[0] = null;
        //    anim.SetBool("attack", false);
        //    myNavMeshAgent.speed = 1.5f;
        //}
        //if (myNavMeshAgent.hasPath == false)
        //{
        //    anim.SetBool("Run", false);
        //}
        //if (TargetEnemy == null)
        //{
        //    anim.SetBool("attack",false);
        //    myNavMeshAgent.speed = 1.5f;
        //}
        slider.transform.parent.transform.rotation = Camera.main.transform.rotation;

    }
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (this.enemys.Count > 0 )
        {
            if (this.enemys[0].tag == "Player")
            {
                GameObject go = Instantiate(buttle, firePosition.position, firePosition.rotation);
                go.GetComponent<Bullet>().SetTarget(this.enemys[0].transform);

            }

        }
        else
        {
            GameObject go = Instantiate(buttle, firePosition.position, firePosition.rotation);
            go.GetComponent<Bullet>().SetTarget(enemys[0].transform);

        }
    }
    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for (int i = 0; i < enemys.Count; i++)//0 1
        {
            if (enemys[i] == null)
            {
                emptyIndex.Add(i);//0 1
            }
        }
        for (int index = 0; index < emptyIndex.Count; index++)//index=0 1
        {
            enemys.RemoveAt(emptyIndex[index] - index);//索引0-0 1-1,1
        }
    }
    public void LessBlood(float att)
    {
        if (slider.value > 0)
        {
            if (slider.value < att)
            {
                slider.value = 0;

            }
            slider.value -= att;
        }
        if (slider.value <= 0)
        {
            audioSource.Play();
            GameObject go = Instantiate(BombObj, transform.position, BombObj.transform.rotation);
            Destroy(go, 2f);
            Destroy(TowerObj, 1);
            //Destroy(gameObject);
        }

    }
}
