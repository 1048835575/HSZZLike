using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject HeroGo;
    public GameObject BuildEffect;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void buildHero(HeroData heroData)
    {
        Instantiate(heroData.heroPerfab, transform.position, Quaternion.identity);
        GameObject go = Instantiate(BuildEffect, transform.position, Quaternion.identity);
        Destroy(go, 1.5f);
    }


}
