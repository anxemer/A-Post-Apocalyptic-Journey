using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private QuestionSetup questionSetup;
    [SerializeField] private GameObject questionPanel; // Thêm biến này để tham chiếu đến panel câu hỏi
    [SerializeField] private Interactor interactor; // Thêm biến này để tham chiếu đến Interactor
    //[SerializeField] private QuestionSetup questionSetup;

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            Debug.Log("CORRECT ANSWER");
            interactor.HandleCorrectAnswer(); // Gọi hàm xử lý câu trả lời đúng trong Interactor
            questionPanel.SetActive(false);

        }
        else
        {
            Debug.Log("WRONG ANSWER");
            interactor.HandleWrongAnswer(); // Gọi hàm xử lý câu trả lời sai trong Interactor
            questionPanel.SetActive(false);

        }
        if (questionSetup.questions.Count > 0)
        {
            // Generate a new question
            questionSetup.Start();
            questionPanel.SetActive(false);

        }
        // Hide the question panel

    }
}
