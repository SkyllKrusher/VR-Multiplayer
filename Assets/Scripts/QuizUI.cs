using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text questionText;
    [SerializeField]
    private List<TMP_Text> optionTexts;
    [SerializeField]
    private List<QuestionScriptableObject> questionDatas;
    [SerializeField]
    private GameObject startGamePanel;
    [SerializeField]
    private GameObject quizPanel;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button startButton;

    private int numberOfOptions = 4;
    private int currentQuestionNumber;
    private int[] answers;

    private int questionsCount => questionDatas.Count;

    private void Start()
    {
        answers = new int[questionsCount];
        currentQuestionNumber = 0;
    }
    private void Awake()
    {
        nextButton.onClick.AddListener(() => OnNext());
        startButton.onClick.AddListener(() => StartQuiz());
    }


    private void StartQuiz()
    {
        startGamePanel.SetActive(false);
        quizPanel.SetActive(true);
    }
    private void SetQuestion(QuestionScriptableObject questionData)
    {
        questionText.text = questionData.questionStatement;
        for (int i = 0; i < numberOfOptions; i++)
        {
            optionTexts[i].text = questionData.options[i];
        }
    }
    private void OnNext()
    {
        // store current answer
        answers[currentQuestionNumber] =


        // check and submit if last
        currentQuestionNumber += 1;
        if (currentQuestionNumber > questionsCount)
        {
            SubmitQuiz();
            return;
        }

        //Update next question if not last
        SetQuestion(questionDatas[currentQuestionNumber]);
    }
    private void SubmitQuiz()
    {
        // evaluate
        int score = 0;
        for (int i = 0; i < questionsCount; i++)
        {
            if (answers[i] == questionDatas[i].correctAnswerIndex)
            {
                score++;
            }
        }

        // show results
        Debug.Log("Score");

        // review
    }
}
