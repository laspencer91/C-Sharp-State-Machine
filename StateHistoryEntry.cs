using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloomyStateMachine
{
    class StateHistoryEntry
    {
        Enum state;
        int iterations = 0;

        public StateHistoryEntry(Enum stateId)
        {
            state = stateId;
        }

        public void Iterate()
        {
            ++iterations;
        }

        public int GetIterations()
        {
            return iterations;
        }

        public Enum GetState()
        {
            return state;
        }
    }
}
