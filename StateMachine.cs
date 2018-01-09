using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloomyStateMachine
{
    class StateMachine <T>
    {
        Dictionary<T, MachineState> stateActions;
        List<StateHistoryEntry<T>> machineHistory;
        StateHistoryEntry<T> currentStateEntry;

        T currentState;

        int maxHistorySize = 8;

        private StateMachine()
        {
            stateActions = new Dictionary<T, MachineState>();
            machineHistory = new List<StateHistoryEntry<T>>();
        }

        public static StateMachine<T> Create()
        {
            return new StateMachine<T>();
        }

        /// <summary>
        /// Add a state function with the given id to this machine, specifying the onExecute, onEnter, and onExit functions
        /// </summary>
        /// <param name="stateId">The state identifier (an Enum)</param>
        /// <param name="onExecute">Function to call every iteration if this state</param>
        /// <param name="onEnter">Function to execute upon the first iteration of the state</param>
        /// <param name="onExit">Function to execute upon ex</param>
        /// <returns></returns>
        // 
        public StateMachine<T> AddState(T stateId, Action onExecute, Action onEnter, Action onExit)
        {
            // Set default state if its the first state added
            if (stateActions.Count == 0)
            {
                currentState = stateId;
                currentStateEntry = new StateHistoryEntry<T>(currentState);
            }

            MachineState newState = new MachineState(onExecute, onEnter, onExit);

            stateActions.Add(stateId, newState);

            return this;
        }

        /// <summary>
        /// Add a state to this machine specifying the execute functions
        /// </summary>
        /// <param name="stateId">The state identifier (an Enum)</param>
        /// <param name="onExecute">Function to call every iteration if this state</param>
        /// <returns>This state machine</returns>
        public StateMachine<T> AddState(T stateId, Action onExecute)
        {
            return AddState(stateId, onExecute, null, null);
        }

        /// <summary>
        /// Add a state to this machine specifying the execute and on enter functions
        /// </summary>
        /// <param name="stateId">The state identifier (an Enum)</param>
        /// <param name="onExecute">Function to call every iteration if this state</param>
        /// <param name="onEnter">Function to execute upon the first iteration of the state</param>
        /// <returns>This state machine</returns>
        public StateMachine<T> AddState(T stateId, Action onExecute, Action onEnter)
        {
            return AddState(stateId, onExecute, onEnter, null);
        }

        /// <summary>
        /// Remove State, returns true if the item existed and false if not
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>True if the key to remove exists</returns>
        public bool RemoveState(T stateId)
        {
            return stateActions.Remove(stateId);
        }

        /// <summary>
        /// Change the current state
        /// </summary>
        /// <param name="stateId"></param>
        public void ChangeState(T stateId)
        {
            AddHistoryEntry(currentStateEntry);
            currentState = stateId;
            currentStateEntry = new StateHistoryEntry<T>(currentState);
        }

        /// <summary>
        /// Set state without adding previous state to history
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>This state machine</returns>
        public StateMachine<T> SetState(T stateId)
        {
            currentState = stateId;
            return this;
        }

        // Execute the current state
        public void ExecuteState()
        {
            if (currentStateEntry.GetIterations() == 0)
                stateActions[currentState].OnEnter();

            stateActions[currentState].OnExecute();
            currentStateEntry.Iterate();
        }

        public StateMachine<T> setMaxHistorySize(int maxHistorySize)
        {
            this.maxHistorySize = maxHistorySize;
            return this;
        }

        public List<StateHistoryEntry<T>> GetStateHistory()
        {
            return machineHistory;
        }

        // Add a completed entry to the machine history list
        void AddHistoryEntry(StateHistoryEntry<T> historyEntry)
        {
            machineHistory.Add(historyEntry);

            if (machineHistory.Count > maxHistorySize)
                machineHistory.RemoveAt(0);

            currentStateEntry = null;
        }
    }
}
