# C# State Machine
##### Small basic state machine for use in C#
---

Basic use.

    StateMachine machine = StateMachine.Create().AddState(State.stop, StopFunction, StopOnEnterFunction);

As you can see, you can add states in line or after initialization. A state takes in an Enum value that is required. An execute method is also required. OnEnter and OnExit methods are optional. These are simply set to null if not specified.

You then simply use `StateMachine.ChangeState(State.run)` to change the state. If specified, the OnEnter method will be run for that state on the first iteration.

You can also use the function `machine.getStateHistory()` to retrieve a list of previous states where the oldest state is at index 0. The size by default is 8, but you may change the max size by calling `StateMachine.setMaxStateHistorySize(amount)`.


