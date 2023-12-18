using UnityEngine;
using UnityEngine.Events;

namespace Inmersys.StateMachine
{
    public enum ExecuteMode
    {
        BeforeStart,
        ToFinish,
        ToContinue
    }
    public enum SkipMode
    {
        None,
        IgnoreSkipEvents,
        InvokeSkipEvents
    }
    
    [System.Serializable]
    public class State
    {
        [Tooltip("According to the order of the instructional script (UX)")]
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _notes;
        [SerializeField] private SkipMode _excludeMode;
        
        [Tooltip("Timing must to be bigger than 0")]
        [SerializeField] private ExecuteMode _timerMode;
        [Range(0f, 300f)]
        [SerializeField] private float _timing = 0f;
        [SerializeField] private StateObject _stateObject;
        [SerializeField] private UnityEvent _onStart;
        [SerializeField] private UnityEvent _onComplete;
        [SerializeField] private UnityEvent _onSkip;
        
        #region Properties
        public StateObject StateObject => _stateObject;
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public float Timing
        {
            get => _timing;
            set => _timing = value;
        }
        public ExecuteMode TimeExecuteMode
        {
            get => _timerMode;
            set => _timerMode = value;
        }
        public UnityEvent OnStart
        {
            get => _onStart;
            set => _onStart = value;
        }
        public UnityEvent OnComplete
        {
            get => _onComplete;
            set => _onComplete = value;
        }
        public UnityEvent OnSkip
        {
            get => _onSkip;
            set => _onSkip = value;
        }
        public SkipMode ExcludeMode
        {
            get => _excludeMode;
            set => _excludeMode = value;
        } 
    
        #endregion
    }
}