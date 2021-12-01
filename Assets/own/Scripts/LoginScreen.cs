using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoginScreen : MonoBehaviour
{
    [SerializeField]
    private InputField accountInput;
    [SerializeField]
    private InputField passwordInput;
    [SerializeField]
    private Button loginBtn;
    private bool password = false;
    private bool account = false;
    public GameObject startPanel;
    public void loginClick()
    {
        if(accountInput.text.Length == 0|| accountInput.text.Length > 10)
        {
            Debug.Log("账号不合法");
            return;
        }
        else
        {
            account = true;
        }
        if (passwordInput.text.Length == 0 || passwordInput.text.Length > 10)
        {
            Debug.Log("密码不合法");
            return;
        }
        else
        {
            password = true;
        }
        if (account==true&& password == true)
        {
            startPanel.SetActive(false);

        }
    }
}
