using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "ScriptableObjects/QuestionDataSO", order = 1)]
public class QuestionScriptableObject : ScriptableObject
{

    public string questionStatement;

    public string[] options;
    public int correctAnswerIndex; //0 to 3
}
