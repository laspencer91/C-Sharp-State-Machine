using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloomyStateMachine
{
    class StateHistoryEntry<T>
    {
        T state;
        int iterations = 0;

        public StateHistoryEntry(T stateId)
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

        public T GetState()
        {
            return state;
        }
    }
}
