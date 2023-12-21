using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    [Serializable]
    public struct Grade
    {
        public Color Color;
        public int requiredGrade;
        public string commentText;
    }

    private int _questionIndex;
    [SerializeField] private TextMeshProUGUI _multipleChoiceQuestionText;
    [SerializeField] private TextMeshProUGUI _trueFalseQuestionText;
    [SerializeField] private TextMeshProUGUI _results;
    
    [SerializeField] private TMP_Dropdown _dropDown;
    [SerializeField] private QuizData _quiz;

    [SerializeField] private GameObject _trueFalseButtons;
    [SerializeField] private GameObject _drowpDownPanel;
    [SerializeField] private GameObject _trueFalsePanel;
    [SerializeField] private GameObject _correctAnswerPanel;
    [SerializeField] private GameObject _incorrectAnswerPanel;
    [SerializeField] private GameObject _ResultsPanel;
    [SerializeField] private TextMeshProUGUI _comentText;
    [SerializeField] private List<Grade> _gradeColors;

    private int _rightAnswers;
    private bool _isInTrueFalseQuiz;

    //this methos is called for the button of the first panle of questionsPanels 
    public void StartMultipleChoiceQuiz()
    {
        _drowpDownPanel.SetActive(true);
        _dropDown.value = -1;
        _questionIndex = 0;
        ShowMultipleChoiceQuestion();
        
    }

    private void ShowMultipleChoiceQuestion()
    {
        _multipleChoiceQuestionText.text = _quiz.MultipleChoiceQuestions[_questionIndex].Question;
        _dropDown.ClearOptions();
        _dropDown.AddOptions(_quiz.MultipleChoiceQuestions[_questionIndex].options);
    }

    public void CheckDropDownAnswer()
    {
        if(_dropDown.options[_dropDown.value - 1].text == _quiz.MultipleChoiceQuestions[_questionIndex].Answer)
        {
            _rightAnswers++;
            _correctAnswerPanel.SetActive(true);
        }
        else
            _incorrectAnswerPanel.SetActive(true);
    }

    public void Continue()
    {
        if(_isInTrueFalseQuiz)
        {
            ContinueTrueFalseQuiz();
            return;
        }
        if(!_isInTrueFalseQuiz)
        {
            ContinueDropDownQuiz();
        }
    }

    public void ContinueDropDownQuiz()
    {
        _questionIndex++;
        if(_questionIndex < _quiz.MultipleChoiceQuestions.Count)
        {
            ShowMultipleChoiceQuestion();
        }
        else
        {
            _questionIndex = 0;
            _isInTrueFalseQuiz = true;
            _drowpDownPanel.SetActive(false);
            StartTrueFalseQuestions();
        }
    }

    private void StartTrueFalseQuestions()
    {
        _trueFalsePanel.SetActive(true);
        _trueFalseButtons.SetActive(true);
        _questionIndex = 0;
        ShowTrueFalseQuestion();
    }

    public void ShowTrueFalseQuestion()
    {
        _trueFalseQuestionText.text = _quiz.TrueFalseQuestions[_questionIndex].Question;
    }

    public void CheckTrueFalseAnswer(bool answer)
    {
        if(_quiz.TrueFalseQuestions[_questionIndex].Answer == answer)
        {
            _rightAnswers++;
            _correctAnswerPanel.SetActive(true);
        }
        else
            _incorrectAnswerPanel.SetActive(true);
    }

    public void ContinueTrueFalseQuiz()
    {
        _questionIndex++;
        if(_questionIndex < _quiz.TrueFalseQuestions.Count)
        {
            ShowTrueFalseQuestion();
        }
        else
        {
            _trueFalsePanel.SetActive(false);
            ShowResults();
        }
    }

    public void ShowResults()
    {
        _results.color = _gradeColors[_gradeColors.Count -1].Color;
        _comentText.text = _gradeColors[_gradeColors.Count -1].commentText;
        for (int i = 0; i < _gradeColors.Count; i++)
        {
            if(_rightAnswers <= _gradeColors[i].requiredGrade)
            {
                _results.color = _gradeColors[i].Color;
                _comentText.text = _gradeColors[i].commentText;
                break;
            }
        }

        _ResultsPanel.SetActive(true);
        _results.text = $"{_rightAnswers}/{_quiz.TrueFalseQuestions.Count + _quiz.MultipleChoiceQuestions.Count}";
    }


}
