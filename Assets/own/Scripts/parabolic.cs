using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabolic : MonoBehaviour
{
    private float horizontalSpeed = 30;
    private float verticalSpeed;
    private float g = 9.81f;
    private Vector3 need_direction;
    private float dis;
    private float speed = 6;
    public GameObject BombObj;
    RaycastHit hit;
    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            dis = Vector3.Distance(transform.position, hit.point);
            need_direction = (hit.point - transform.position).normalized;

        }
        float need_time = dis / speed / 2;
        verticalSpeed = g * need_time;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verticalSpeed = verticalSpeed - g * Time.fixedDeltaTime;
        transform.Translate(need_direction * speed * Time.fixedDeltaTime, Space.World);
        transform.Translate(Vector3.up * verticalSpeed * Time.fixedDeltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane x")
        {
            audioSource.PlayOneShot(audioClip);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            GameObject go = Instantiate(BombObj, transform.position, BombObj.transform.rotation);
            Destroy(go, 2f);
            Destroy(gameObject,2);
        }
        if (other.tag == "Plane s")
        {
            audioSource.PlayOneShot(audioClip);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            GameObject go = Instantiate(BombObj, transform.position, BombObj.transform.rotation);
            Destroy(go, 2f);
            Destroy(gameObject, 2);
        }
        if (other.tag == "Plane z")
        {
            audioSource.PlayOneShot(audioClip);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            GameObject go = Instantiate(BombObj, transform.position, BombObj.transform.rotation);
            Destroy(go, 2f);
            Destroy(gameObject,2);
        }
        if (other.tag == "EnemyMainTower")
        {
            audioSource.PlayOneShot(audioClip);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            other.transform.parent.GetComponent<EnemyTower>().LessBlood(800);
            GameObject go = Instantiate(BombObj, transform.position, BombObj.transform.rotation);
            Destroy(go, 2f);
            if (other.transform.parent.GetComponent<EnemyTower>().slider.value <= 0)
            {
                Destroy(other.transform.parent.gameObject,1);
            }
            Destroy(gameObject,2);
        }
        if (other.tag == "EnemyTower")
        {
            audioSource.PlayOneShot(audioClip);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            other.transform.parent.GetComponent<EnemyTower>().LessBlood(800);
            GameObject go = Instantiate(BombObj, transform.position, BombObj.transform.rotation);
            Destroy(go, 2f);
            if (other.transform.parent.GetComponent<EnemyTower>().slider.value <= 0)
            {
                Destroy(other.transform.parent.gameObject,1);
            }
            Destroy(gameObject, 2);
        }
        if (other.tag == "enemy")
        {
            audioSource.PlayOneShot(audioClip);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            other.GetComponent<Enemy>().LessBlood(800);
            GameObject go = Instantiate(BombObj, transform.position, BombObj.transform.rotation);
            Destroy(go, 2f);
            if (other.GetComponent<Enemy>().slider.value <= 0)
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject, 2);
        }
    }
}
