using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraRoam : MonoBehaviour
{
    public static CameraRoam _instance;
    float time;
    bool isBool;
    public GameObject BackPackerBtn;
    public List<GameObject> Combat = new List<GameObject>();
    public List<Sprite> sprites = new List<Sprite>();
    public AudioSource audioSource;
    bool Asbool =true;
    public AudioSource audio1;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (isBool)
        {
            transform.Rotate(new Vector3(0, 0 + Time.deltaTime, 0));

        }
        else
        {
            transform.Rotate(new Vector3(0, 0 - Time.deltaTime, 0));
        }
        if (time > 5)
        {
            isBool = !isBool;
            time = 0;
        }
    }
    public void OpenBackPacker()
    {
        audio1.Play();
        BackPackerBtn.SetActive(true);
        for (int i = 0; i < 8; i++)
        {
            sprites.Add(Combat[i].transform.GetChild(0).GetComponent<Image>().sprite);
        }
    }
    public void CloseBackPacker()
    {
        BackPackerBtn.SetActive(false);
        for (int i = 0; i < sprites.Count; i++)
        {
            sprites[i] = Combat[i].transform.GetChild(0).GetComponent<Image>().sprite;
        }
    }
    public void Music()
    {

        Asbool = !Asbool;
        audioSource.enabled = Asbool;
    }

    public void PlayerGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void ExitGame()
    {
       
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
