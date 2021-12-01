using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoadScene : MonoBehaviour
{
    public Slider slider;
    public Text text;
    float loadingSpeed = 1;
    float targetValue;
    AsyncOperation async;
    public FadeInOut m_fade;
    public AudioSource audio1;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == ("Loading"))
        {
            StartCoroutine(AsyncLoading());
        }
    }
    IEnumerator AsyncLoading()
    {
        async = SceneManager.LoadSceneAsync(Globe.nextSceneName);
        async.allowSceneActivation = false;
        yield return async;
    }
    // Update is called once per frame
    void Update()
    {
        if(slider == null)
        {
            return;
        }
        if (text == null)
        {
            return;
        }
        targetValue = async.progress;
        if (async.progress >= 0.9f)
        {
            targetValue = 1;
        }
        if (targetValue != slider.value)
        {
            slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(slider.value - targetValue) < 0.01f)
            {
                slider.value = targetValue;
            }
        }
        text.text = ((int)(slider.value * 100)).ToString() + "%";
        if ((int)slider.value * 100 == 100)
        {
            m_fade.BackGroundContril(true);
            StartCoroutine(WaitTwo());       
        }
    }
    IEnumerator WaitTwo()
    {
        yield return new WaitForSeconds(2.8f);
        async.allowSceneActivation = true;
    }
    public void LoadNewScene()
    {
        audio1.Play();
        Globe.nextSceneName = "Game";
        m_fade.BackGroundContril(true);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(1);
    }
}
public class Globe
{
    public static string nextSceneName;
}
