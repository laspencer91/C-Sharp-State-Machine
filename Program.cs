using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloomyStateMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
        }

        StateMachine machine;

        public Program()
        {
            machine = StateMachine.Create()
                .AddState(StateType.debug_1, debug1, debug1OnEnter)
                .AddState(StateType.debug_2, debug2);

            machine.ExecuteState();

            machine.ChangeState(StateType.debug_2);
            machine.ExecuteState();
            machine.ExecuteState();

            machine.ChangeState(StateType.debug_1);
            machine.ExecuteState();
            machine.ExecuteState();
            machine.ExecuteState();

            machine.ChangeState(StateType.debug_1);

            List<StateHistoryEntry> stateHistory = machine.GetStateHistory();

            foreach (StateHistoryEntry entry in stateHistory)
                Console.WriteLine("State: " + entry.GetState() + " played " + entry.GetIterations() + " times.");
        }

        private void debug1OnEnter()
        {
            Console.WriteLine("debug 1 just entered");
        }

        private void debug1()
        {
            Console.WriteLine("debug 1 execute");
        }

        private void debug2()
        {
            Console.WriteLine("debug 2");
        }
    }

    enum StateType
    {
        debug_1,
        debug_2
    }
}
