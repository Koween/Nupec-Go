using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Inmersys.StateMachine
{
    public enum FirstInteractionMode
    {
        None,
        OverrideStateEvents,
        RunStateEvents
    }

    public class StateObject : MonoBehaviour
    {
        #region Private fields

        [SerializeField] private UnityEvent _onStart;
        [SerializeField] private UnityEvent _onComplete;
        [SerializeField] private UnityEvent _onSkip;
        [SerializeField] private FirstInteractionMode _firstInteractionMode;
        [SerializeField] private UnityEvent _onFirstInteraction;
        [SerializeField] private UnityEvent _onCompleteFirstInteraction;
        private bool _isFirstInteractionCalled;
        private bool _isFirstInteractionCompleted;

        #endregion

        #region Public methods

        public virtual void OnStart()
        {
            switch (_isFirstInteractionCalled)
            {
                case false when _firstInteractionMode == FirstInteractionMode.OverrideStateEvents:
                    OnFirstInteraction();
                    return;
                case false when _firstInteractionMode == FirstInteractionMode.RunStateEvents:
                    OnFirstInteraction();
                    break;
            }

            _onStart?.Invoke();
        }

        public virtual void OnComplete()
        {
            switch (_isFirstInteractionCompleted)
            {
                case false when _firstInteractionMode == FirstInteractionMode.OverrideStateEvents:
                    OnCompleteFirstInteraction();
                    return;
                case false when _firstInteractionMode == FirstInteractionMode.RunStateEvents:
                    OnCompleteFirstInteraction();
                    break;
            }

            _onComplete?.Invoke();
        }

        public virtual void OnSkip()
        {
            _onSkip?.Invoke();
        }

        public virtual void OnFirstInteraction()
        {
            if (_isFirstInteractionCalled)
                return;

            _onFirstInteraction?.Invoke();
            _isFirstInteractionCalled = true;
        }

        public virtual void OnCompleteFirstInteraction()
        {
            if (_isFirstInteractionCompleted)
                return;

            _onCompleteFirstInteraction?.Invoke();
            _isFirstInteractionCompleted = true;
        }

        public void FirstInteractionDone()
        {
            _isFirstInteractionCalled = true;
            _isFirstInteractionCompleted = true;
        }

        #endregion
    }
}