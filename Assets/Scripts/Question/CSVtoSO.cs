using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
using System.IO; // Thêm thư viện này

public class CVStoSO : MonoBehaviour
{
    private static string apiUrl = "https://localhost:7027/api/Trivia?amount=10&difficulty=medium";

    [MenuItem("Utilities/Generate Questions")]
    public static void GenerateQuestions()
    {
        // Bắt đầu coroutine yêu cầu API
        EditorCoroutineUtility.StartCoroutineOwnerless(FetchQuestionsFromAPI());
    }

    private static IEnumerator FetchQuestionsFromAPI()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            request.SetRequestHeader("Accept", "application/json");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("API Response: " + jsonResponse); // Kiểm tra phản hồi từ API

                if (string.IsNullOrEmpty(jsonResponse))
                {
                    Debug.LogError("API response is empty");
                    yield break;
                }

                // Kiểm tra phản hồi JSON
                APIResponse response = JsonUtility.FromJson<APIResponse>(jsonResponse);

                if (response == null || response.results == null)
                {
                    Debug.LogError("API response parsing error");
                    yield break;
                }

                if (response.response_Code == 0)
                {
                    foreach (var result in response.results)
                    {
                        if (result != null)
                        {
                            CreateQuestionAsset(result);
                        }
                    }

                    AssetDatabase.SaveAssets();
                    Debug.Log("Generated Questions");
                }
                else
                {
                    Debug.LogError("API response error: " + response.response_Code);
                }
            }
        }
    }

    private static void CreateQuestionAsset(Result result)
    {
        QuestionData questionData = ScriptableObject.CreateInstance<QuestionData>();
        questionData.question = result.question;
        questionData.category = result.category;
        questionData.answers = new string[result.incorrect_Answers.Length + 1];
        questionData.answers[0] = result.correct_Answer;

        for (int i = 0; i < result.incorrect_Answers.Length; i++)
        {
            questionData.answers[i + 1] = result.incorrect_Answers[i];
        }

        string assetName = questionData.question.Length > 50 ? questionData.question.Substring(0, 50) : questionData.question;
        assetName = string.Join("_", assetName.Split(Path.GetInvalidFileNameChars()));

        AssetDatabase.CreateAsset(questionData, $"Assets/Resources/Questions/{assetName}.asset");
        AssetDatabase.SaveAssets();
    }
}

[System.Serializable]
public class APIResponse
{
    public int response_Code;
    public Result[] results;
}

[System.Serializable]
public class Result
{
    public string category;
    public string type;
    public string difficulty;
    public string question;
    public string correct_Answer;
    public string[] incorrect_Answers;
}
