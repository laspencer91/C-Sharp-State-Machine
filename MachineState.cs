using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloomyStateMachine
{
    class MachineState
    {
        Action onEnter;
        Action onExecute;
        Action onExit;

        public MachineState(Action onExecute, Action onEnter, Action onExit)
        {
            this.onExecute = onExecute;
            this.onEnter = onEnter;
            this.onExit = onExit;
        }

        public void OnEnter()
        {
            if (onEnter != null)
                onEnter.Invoke();
        }

        public void OnExit()
        {
            if (onExit != null)
                onExit.Invoke();
        }

        public void OnExecute()
        {
            onExecute.Invoke();
        }
    }
}
