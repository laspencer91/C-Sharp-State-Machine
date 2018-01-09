# C# State Machine
##### Small basic state machine for use in C-Sharp
---

Basic use.

    StateMachine<Enum> machine 
			= StateMachine.Create().AddState(State.stop, StopFunction, StopOnEnterFunction);

As you can see, you can add states in line or after initialization. A StateMachine has a generic type which is a type of identifier for the state. The example shows the use of an Enum but you could also use an int, string, or any other type you would like. An execute Action is required while OnEnter and OnExit methods are optional. These are simply set to null if not specified.

You then simply use `StateMachine.ChangeState(State.run)` to change the state of the machine, though you will need a reference to that machine. If specified, the OnEnter Action will be run for that state on the first iteration and the onExit Action will be run if the state was changed during that iteration.

You can also use the function `machine.getStateHistory()` to retrieve a list of previous states where the oldest state is at index 0. The size by default is 8, but you may change the max size by calling `StateMachine.setMaxStateHistorySize(amount)`.

Another example of a use is as follows:

	StateMachine<Enum> machine = StateMachine<Enum>.Create().AddState(StateType.debug_1, debug1, debug1OnEnter).AddState(StateType.debug_2, debug2).setMaxHistorySize(100).SetState(StateType.debug_2);


