using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private QuizTimer _quizTimer;

    [SerializeField] private GameObject _trueFalseButtons;
    [SerializeField] private GameObject _drowpDownPanel;
    [SerializeField] private GameObject _trueFalsePanel;
    [SerializeField] private GameObject _correctAnswerPanel;
    [SerializeField] private GameObject _incorrectAnswerPanel;
    [SerializeField] private GameObject _ResultsPanel;
    [SerializeField] private TextMeshProUGUI _comentText;

    [SerializeField] private Color _excelentResultsColor, _greatResultsColor, _badResultsColor;
    [SerializeField, TextArea] private string _excelentResultsText, _greatResultsText, _badResultsText;

    private int _rightAnswers;
    private bool _isInTrueFalseQuiz;

    //this methos is called for the button of the first panel of questionsPanels 
    public void StartMultipleChoiceQuiz()
    {
        _drowpDownPanel.SetActive(true);
        _dropDown.value = -1;
        _questionIndex = 0;
        ShowMultipleChoiceQuestion();

    }

//Set the data of the quiz scriptable object in the drop down UI
    private void ShowMultipleChoiceQuestion()
    {
        _quizTimer.gameObject.SetActive(true);
        _quizTimer.StartTimer();
        _multipleChoiceQuestionText.text = _quiz.MultipleChoiceQuestions[_questionIndex].Question;
        _dropDown.ClearOptions();
        _dropDown.value = -1;
        _dropDown.AddOptions(_quiz.MultipleChoiceQuestions[_questionIndex].options);
    }


    public void CheckDropDownAnswer()
    {
        if(_dropDown.value == -1) return;
        _quizTimer.StopTimer();
        _quizTimer.gameObject.SetActive(false);
        if(_dropDown.options[_dropDown.value].text == _quiz.MultipleChoiceQuestions[_questionIndex].Answer)
        {
            _rightAnswers++;
            _correctAnswerPanel.SetActive(true);
        }
        else
            _incorrectAnswerPanel.SetActive(true);
    }

    public void OnFinishTime()
    {
        if(!_isInTrueFalseQuiz && _dropDown.value != -1)
        {
            CheckDropDownAnswer();
            return;     
        }
        _incorrectAnswerPanel.SetActive(true);
    }

    public void Continue()
    {
        if(_isInTrueFalseQuiz)
        {
            ContinueTrueFalseQuiz();
            return;
        }
        else
        {
            ContinueDropDownQuiz();
        }
    }

    private void ContinueDropDownQuiz()
    {
        _questionIndex++;
        if(_questionIndex < _quiz.MultipleChoiceQuestions.Count)
            ShowMultipleChoiceQuestion();
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
        _quizTimer.gameObject.SetActive(true);
        _quizTimer.StartTimer();
        _trueFalseQuestionText.text = _quiz.TrueFalseQuestions[_questionIndex].Question;
    }

    public void CheckTrueFalseAnswer(bool answer)
    {
        _quizTimer.StopTimer();
        _quizTimer.gameObject.SetActive(false);
        if(_quiz.TrueFalseQuestions[_questionIndex].Answer == answer)
        {
            _rightAnswers++;
            _correctAnswerPanel.SetActive(true);
        }
        else
            _incorrectAnswerPanel.SetActive(true);
    }

    private void ContinueTrueFalseQuiz()
    {
        _questionIndex++;
        if(_questionIndex < _quiz.TrueFalseQuestions.Count)
        {
            ShowTrueFalseQuestion();
        }
        else
        {
            _trueFalsePanel.SetActive(false);
            _trueFalseButtons.SetActive(false);
            _quizTimer.gameObject.SetActive(false);
            ShowResults();
        }
    }

    public void ShowResults()
    {
        int questionsCount = _quiz.TrueFalseQuestions.Count + _quiz.MultipleChoiceQuestions.Count;

        if(_rightAnswers >= questionsCount)
        {
            _results.color = _excelentResultsColor;
            _comentText.text = _excelentResultsText;
        }

        if(_rightAnswers < questionsCount && _rightAnswers > (questionsCount/3))
        {
            _results.color = _greatResultsColor;
            _comentText.text =_greatResultsText;
        }

        if(_rightAnswers <= questionsCount/3)
        {
            _results.color = _badResultsColor;
            _comentText.text = _badResultsText;
        }

        _ResultsPanel.SetActive(true);
        _results.text = $"{_rightAnswers}/{questionsCount}";
        
    }

}
