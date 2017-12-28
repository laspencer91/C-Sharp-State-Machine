using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloomyStateMachine
{
    class StateMachine
    {
        Dictionary<Enum, MachineState> stateActions;
        List<StateHistoryEntry> machineHistory;
        StateHistoryEntry currentStateEntry;

        Enum currentState;

        int maxHistorySize = 8;

        private StateMachine()
        {
            stateActions = new Dictionary<Enum, MachineState>();
            machineHistory = new List<StateHistoryEntry>();
        }

        public static StateMachine Create()
        {
            return new StateMachine();
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
        public StateMachine AddState(Enum stateId, Action onExecute, Action onEnter, Action onExit)
        {
            // Set default state if its the first state added
            if (stateActions.Count == 0)
            {
                currentState = stateId;
                currentStateEntry = new StateHistoryEntry(currentState);
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
        public StateMachine AddState(Enum stateId, Action onExecute)
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
        public StateMachine AddState(Enum stateId, Action onExecute, Action onEnter)
        {
            return AddState(stateId, onExecute, onEnter, null);
        }

        /// <summary>
        /// Remove State, returns true if the item existed and false if not
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>True if the key to remove exists</returns>
        public bool RemoveState(Enum stateId)
        {
            return stateActions.Remove(stateId);
        }

        /// <summary>
        /// Change the current state
        /// </summary>
        /// <param name="stateId"></param>
        public void ChangeState(Enum stateId)
        {
            AddHistoryEntry(currentStateEntry);
            currentState = stateId;
            currentStateEntry = new StateHistoryEntry(currentState);
        }

        /// <summary>
        /// Set state without adding previous state to history
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>This state machine</returns>
        public StateMachine SetState(Enum stateId)
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

        public StateMachine setMaxHistorySize(int maxHistorySize)
        {
            this.maxHistorySize = maxHistorySize;
            return this;
        }

        public List<StateHistoryEntry> GetStateHistory()
        {
            return machineHistory;
        }

        // Add a completed entry to the machine history list
        void AddHistoryEntry(StateHistoryEntry historyEntry)
        {
            machineHistory.Add(historyEntry);

            if (machineHistory.Count > maxHistorySize)
                machineHistory.RemoveAt(0);

            currentStateEntry = null;
        }
    }
}
