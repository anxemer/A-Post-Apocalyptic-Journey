using Assets.Scripts.Login;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField UserName;
    [SerializeField] private TMP_InputField Password;
    [SerializeField] private Button loginBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnLoginButtonClick()
    {
        string username = UserName.text;
        string password = Password.text;
        int pass = int.Parse(password);
        StartCoroutine(LoginRequest("https://66779215145714a1bd750fcd.mockapi.io/api/account/user/", pass));
    }
   public IEnumerator LoginRequest(string url, int id)
    {
        string urlString = url + id;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlString))
        {
            // Gửi yêu cầu và đợi phản hồi
            yield return webRequest.SendWebRequest();
            //string json = webRequest.downloadHandler.text;
            //Account account = JsonUtility.FromJson<Account>(jsonData);
            // Kiểm tra lỗi khi gửi yêu cầu
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Lỗi khi gửi yêu cầu: " + webRequest.error);
            }
            else
            {
                string apiResponse = webRequest.downloadHandler.text;
                Account apiData = JsonUtility.FromJson<Account>(apiResponse);

                if (apiData != null)
                {
                    LoginResult(apiData);
                }
                else
                {
                    Debug.Log("API returned null data or data could not be parsed");
                }


            }
        }
    }
    void LoginResult(Account account)
    {
        if(account.id != int.Parse(Password.text))
        {
            Debug.Log("thành công");
            SceneManager.LoadScene("Tivo");
        }
    }
}
