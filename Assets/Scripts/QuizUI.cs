using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
    private GameObject reviewPanel;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Canvas vrCanvas;
    [SerializeField]
    private ToggleGroup optionsToggleGroup;

    private int numberOfOptions = 4;
    private int currentQuestionNumber;
    private int[] answers;
    private int questionsCount => questionDatas.Count;
    private PhotonView photonView;
    private string scoreString;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        Cursor.visible = false;
        answers = new int[questionsCount];
        currentQuestionNumber = 0;
    }
    private void Awake()
    {
        nextButton.onClick.AddListener(() => NextRPC());
        startButton.onClick.AddListener(() => QuizRPC());
    }

    public void ActivateCanvas()
    {
        vrCanvas.enabled = true;
    }

    private void QuizRPC()
    {
        photonView.RPC(nameof(StartQuiz), RpcTarget.All);
    }

    private void NextRPC()
    {
        photonView.RPC(nameof(OnNext), RpcTarget.All);
    }

    [PunRPC]
    private void StartQuiz()
    {
        startGamePanel.SetActive(false);
        quizPanel.SetActive(true);
        SetQuestion(questionDatas[0]);
    }
    private void SetQuestion(QuestionScriptableObject questionData)
    {
        questionText.text = questionData.questionStatement;
        for (int i = 0; i < numberOfOptions; i++)
        {
            optionTexts[i].text = questionData.options[i];
        }
    }

    [PunRPC]
    private void OnNext()
    {
        // store current answer
        IEnumerator toggleEnum = optionsToggleGroup.ActiveToggles().GetEnumerator();
        toggleEnum.MoveNext();
        Toggle activeToggle = (Toggle)toggleEnum.Current;
        int selectedAns = activeToggle.name[7] - 48;
        // Debug.Log(activeToggle.name + selectedAns, activeToggle.gameObject);

        answers[currentQuestionNumber] = selectedAns;

        // check and submit if last
        currentQuestionNumber += 1;
        if (currentQuestionNumber >= questionsCount)
        {
            photonView.RPC(nameof(SubmitQuiz), RpcTarget.All);
            return;
        }

        //Update next question if not last
        SetQuestion(questionDatas[currentQuestionNumber]);
    }

    [PunRPC]
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
        // Debug.Log("Score");
        scoreString = "Score = " + score + " / " + questionsCount;
        photonView.RPC(nameof(UpdateScoreText), RpcTarget.All);
        quizPanel.SetActive(false);
        reviewPanel.SetActive(true);
        // review
    }

    [PunRPC]
    public void UpdateScoreText()
    {
        scoreText.text = scoreString;
    }
}
