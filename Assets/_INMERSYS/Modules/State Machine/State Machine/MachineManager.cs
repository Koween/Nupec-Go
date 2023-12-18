using System.Collections.Generic;
using UnityEngine;

namespace Inmersys.StateMachine
{
    public class MachineManager : MonoBehaviour
    {
        [SerializeField] private List<StateMachine> _stateMachines = new List<StateMachine>();
        public StateMachine CurrentStateMachine { get; set; }
        private int _stateManagerIndex = 0;

        #region MonoBehaviour Callbacks
        
        private void Start()
        {
            StartIn(0);
        }
        
        #endregion

        #region Public methods

        /// <summary>
        /// Starts a given flow through the inspector on state machines.
        /// </summary>
        public void StartMachines()
        {
            if (_stateMachines.Count > 0)
            {
                if (!_stateMachines[_stateManagerIndex].ExcludeFromTheGroup)
                    SetStateMachine(_stateMachines[_stateManagerIndex], _stateManagerIndex);
                else
                    NextStateMachine();
            }
        }

        /// <summary>
        /// Goes to the next state machine, if it exists.
        /// </summary>
        public void NextStateMachine()
        {
            if (_stateMachines.Count > _stateManagerIndex + 1)
            {
                if (!_stateMachines[_stateManagerIndex + 1].ExcludeFromTheGroup)
                    SetStateMachine(_stateMachines[_stateManagerIndex + 1], _stateManagerIndex + 1);
                else
                {
                    _stateManagerIndex++;
                    NextStateMachine();
                }
            }
            else
            {
                Debug.Log("No more State machines");
            }
        }
        
        public void FinishCurrentState()
        {
            if (!CurrentStateMachine) return;
            CurrentStateMachine.FinishCurrentState();
        }

        public void JumpStateMachine()
        {
            CurrentStateMachine.JumpStateMachine();
        }

        #endregion

        /// <summary>
        /// Sets a new state machine and then starts it.
        /// </summary>
        /// <param name="currentStateMachine"></param>
        /// <param name="stateManagerIndex"></param>
        private void SetStateMachine(StateMachine currentStateMachine, int stateManagerIndex)
        {
            _stateManagerIndex = stateManagerIndex;
            CurrentStateMachine = currentStateMachine;
            CurrentStateMachine.StartStateMachine();
        }
        
        #region Experimental

        /// <summary>
        /// Sets all state machines to be omitted except for one given by index.
        /// </summary>
        /// <param name="index"></param>
        public void StartIn(int index)
        {
            if (index >= _stateMachines.Count)
            {
                return;
            }
            
            for (var i = 0; i < _stateMachines.Count; i++)
            {
                if (index == i)
                {
                    CurrentStateMachine = _stateMachines[index];
                    
                    _stateMachines[index].StartStateMachine();
                    break;
                }

                _stateMachines[i].ExcludeFromTheGroup = true;
            }
        }

        public void SkipAllExcept(int index)
        {
            if (index >= _stateMachines.Count)
            {
                return;
            }

            for (var i = 0; i < _stateMachines.Count - 1; i++)
            {
                if (index == i)
                    break;

                _stateMachines[i].ExcludeFromTheGroup = true;
            }
        }

        #endregion
    }
}