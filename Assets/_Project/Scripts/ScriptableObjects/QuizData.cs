using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newQuiz", menuName = "CreateQuiz")]
public class QuizData : ScriptableObject
{

    [Serializable]
    public struct TrueFalseQuestion
    {
        [TextArea] public string Question;
        public bool Answer;
    }

    [Serializable]
    public struct MultipleChoiceQuestion
    {
        [TextArea] public string Question;
        public string Answer;
        public List<string> options;
    }

    [SerializeField] private List<MultipleChoiceQuestion> _multipleChoiceQuestions;
    [SerializeField] private List<TrueFalseQuestion> _trueFalseQuestions;

    public List<MultipleChoiceQuestion> MultipleChoiceQuestions => _multipleChoiceQuestions;
    public List<TrueFalseQuestion> TrueFalseQuestions => _trueFalseQuestions;
}
