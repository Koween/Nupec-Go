using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    private int _questionIndex;
    [SerializeField] private TextMeshProUGUI _multipleChoiceQuestionText;
    [SerializeField] private TextMeshProUGUI _trueFalseQuestionText;
    [SerializeField] private TextMeshProUGUI _results;
    
    [SerializeField] private TMP_Dropdown _dropDown;
    [SerializeField] private QuizData _quiz;

    [SerializeField] private GameObject _trueFalseButtons;
    [SerializeField] private GameObject _drowpDownPanel;
    [SerializeField] private GameObject _trueFalsePanel;
    [SerializeField] private GameObject _rightAnswerPanel;
    [SerializeField] private GameObject _wrongAnswerPanel;
    [SerializeField] private GameObject _ResultsPanel;
    private int _rightAnswers;
    
    private void StartMultipleChoiceQuiz()
    {
        _drowpDownPanel.SetActive(true);
        _dropDown.value = 0;
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
        if(_dropDown.options[_dropDown.value].text == _quiz.MultipleChoiceQuestions[_questionIndex].Answer)
        _rightAnswers++;
    }

    public void ContinueDropDownQuiz()
    {
        if(_questionIndex < _quiz.MultipleChoiceQuestions.Count)
        {
            _questionIndex++;
            ShowMultipleChoiceQuestion();
        }
        else
        {
            _drowpDownPanel.SetActive(false);
            StartTrueFalseQuestions();
        }
    }

    public void StartTrueFalseQuestions()
    {
        _trueFalsePanel.SetActive(true);
        _questionIndex = 0;

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
        }
    }

    public void ContinueTrueFalseQuiz()
    {
        if(_questionIndex < _quiz.MultipleChoiceQuestions.Count)
        {
            _questionIndex++;
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
        _ResultsPanel.SetActive(true);
        _results.text = $"{_rightAnswers}/{_quiz.TrueFalseQuestions.Count + _quiz.MultipleChoiceQuestions.Count}";
    }
}
