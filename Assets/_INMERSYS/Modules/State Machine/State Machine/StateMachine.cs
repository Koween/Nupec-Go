using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Inmersys.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        #region Public fields

        [Tooltip("Exclude this State machine from the group in Machine Manager?")]
        public bool ExcludeFromTheGroup;

        public State CurrentState { get; set; }
        public List<State> States = new List<State>();

        #endregion

        #region Private fields

        private MachineManager _machineManager { get; set; }
        private int _stateIndex = 0;
        #endregion

        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _machineManager = GetComponentInParent<MachineManager>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts the current State Machine
        /// </summary>
        public void StartStateMachine()
        {
            if (States.Count == 0)
                return;

            SetState(States[_stateIndex], _stateIndex);
        }
        
        /// <summary>
        /// Finishes the current State Machine
        /// </summary>
        public void FinishStateMachine()
        {
            _machineManager.NextStateMachine();
        }

        /// <summary>
        /// Jumps the current State Machine and continue with the next one.
        /// </summary>
        public void JumpStateMachine()
        {
            for (var i = _stateIndex; i < States.Count; i++)
            {
                States[i].OnSkip?.Invoke();
                //State object detected
                if (States[i].StateObject)
                    States[i].StateObject.OnSkip();
                //
            }
            
            _machineManager.NextStateMachine();
        }

        /// <summary>
        /// Finishes the current state
        /// </summary>
        public void FinishCurrentState()
        {
            StopAllCoroutines();

            //State object detected
            if (CurrentState.StateObject)
                CurrentState.StateObject.OnComplete();
            //
            CurrentState.OnComplete?.Invoke();

            if (CurrentState.Timing > 0f && CurrentState.TimeExecuteMode == ExecuteMode.ToContinue)
            {
                StartCoroutine(TimingToContinue(CurrentState.Timing));
                return;
            }

            NextState();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets a new state in the current State Machine
        /// </summary>
        /// <param name="state"></param>
        /// <param name="stateIndex"></param>
        private void SetState(State state, int stateIndex)
        {
            _stateIndex = stateIndex;
            CurrentState = state;
            StartCurrentState();
        }
        
        /// <summary>
        /// Starts the current state.
        /// </summary>
        /// <returns></returns>
        private void StartCurrentState()
        {
            if (CurrentState.ExcludeMode != SkipMode.None)
            {
                Debug.Log("Current State: " + '[' + CurrentState.Name.ToUpper() + ']' + " was excluded");

                if (CurrentState.ExcludeMode == SkipMode.InvokeSkipEvents)
                {
                    //State object detected
                    if (CurrentState.StateObject)
                        CurrentState.StateObject.OnSkip();
                    //
                    CurrentState.OnSkip?.Invoke();
                }
                
                NextState();
                return;
            }

            Debug.Log("Current State: " + '[' + CurrentState.Name.ToUpper() + ']');

            if (CurrentState.Timing > 0f && CurrentState.TimeExecuteMode == ExecuteMode.BeforeStart)
            {
                StartCoroutine(Timing(CurrentState.Timing));
                return;
            }

            //State object detected
            if (CurrentState.StateObject)
                 CurrentState.StateObject.OnStart();
            //
            CurrentState.OnStart?.Invoke();

            if (CurrentState.Timing > 0f && CurrentState.TimeExecuteMode == ExecuteMode.ToFinish)
            {
                StartCoroutine(TimingToFinish(CurrentState.Timing));
            }
        }

        /// <summary>
        /// Continues with the next state
        /// </summary>
        private void NextState()
        {
            if (States.Count > _stateIndex + 1)
                SetState(States[_stateIndex + 1], _stateIndex + 1);
            else
                FinishStateMachine();
        }

        #endregion

        #region Private Coroutines

        /// <summary>
        /// Countdown to start a state.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator Timing(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            //State object detected
            if (CurrentState.StateObject)
                CurrentState.StateObject.OnStart();
            //
            CurrentState.OnStart?.Invoke();
            
        }

        /// <summary>
        /// Countdown after start a state and before finish it.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator TimingToFinish(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            FinishCurrentState();
        }

        /// <summary>
        /// Countdown after finish a state and before start the next one.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator TimingToContinue(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            NextState();
        }

        #endregion
    }
}