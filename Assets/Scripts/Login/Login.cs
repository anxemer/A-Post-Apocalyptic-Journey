using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField UserName;
    [SerializeField] private TMP_InputField Password;
    [SerializeField] private Button loginBtn;

    void Start()
    {
        loginBtn.onClick.AddListener(OnLoginButtonClick);
    }

    public void OnLoginButtonClick()
    {
        string username = UserName.text;
        string password = Password.text;
        StartCoroutine(LoginRequest(username, password));
    }

    public IEnumerator LoginRequest(string username, string password)
    {
        string url = $"https://localhost:7027/Login?userName={username}&password={password}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Gửi yêu cầu và đợi phản hồi
            yield return webRequest.SendWebRequest();

            // Kiểm tra lỗi khi gửi yêu cầu
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Lỗi khi gửi yêu cầu: " + webRequest.error);
            }
            else
            {
                string apiResponse = webRequest.downloadHandler.text;
                ApiResponse responseData = JsonUtility.FromJson<ApiResponse>(apiResponse);

                if (responseData != null && responseData.response_Code == 0)
                {
                    Debug.Log("Đăng nhập thành công");
                    SceneManager.LoadScene("Tivo");
                }
                else
                {
                    Debug.Log("Đăng nhập thất bại hoặc dữ liệu trả về không hợp lệ");
                }
            }
        }
    }
}

[System.Serializable]
public class ApiResponse
{
    public int response_Code;
    // Các trường khác nếu có
}
