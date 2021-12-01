using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 667;
    public float speed = 2;
    Transform target;
    float distanceArriveTarget = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed*Time.deltaTime);
        Vector3 dir = target.position - transform.position;
        if(dir.magnitude < distanceArriveTarget)
        {
            if (target.tag == "enemy")
            {
            target.GetComponent<Enemy>().LessBlood(damage);
            Destroy(gameObject);

            }
            else if(target.tag == "Player")
            {
                target.transform.parent.GetComponent<Navigation>().LessBlood(damage);
                Destroy(gameObject);
            }
            else if (target.tag == "EnemyTower")
            {
                target.transform.parent.GetComponent<EnemyTower>().LessBlood(damage);
                Destroy(gameObject);
            }
            else if (target.tag == "EnemyMainTowerS"||target.tag == "EnemyMainTowerZ"|| target.tag == "EnemyMainTowerX")
            {
                target.transform.parent.GetComponent<EnemyTower>().LessBlood(damage);
                Destroy(gameObject);
            }
            else if (target.tag == "MainTowerS" || target.tag == "MainTowerZ" || target.tag == "MainTowerX")
            {
                target.transform.parent.GetComponent<EnemyTower>().LessBlood(damage);
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
