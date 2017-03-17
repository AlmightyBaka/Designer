// Perceptrons: Gate.cs

using System.Collections.Generic;
using Designer.Circuit.Logic.Perceptrons;

namespace Designer.Circuit.Logic.Gates
{
	public class Gate
	{
		public enum Types
		{
			Or,
			And,
			Xor,
			Not,
			Input
		};

		public Types Type;
		public bool? Value;
		public List<Connection> Input;
		public List<Connection> Output;

		protected Network _network;
		protected bool? _inputValue;

		public Gate()
		{
			InitIO();
		}

		private void InitIO()
		{
			Output = new List<Connection>();
			Input = new List<Connection>();
		}

		public void Update()
		{
			bool?[] input;
			if (Type != Types.Input)
			{
				input = GetInput(); 
			}
			else
			{
				input = new [] {_inputValue};
			}
			_network.UpdateInput(input);
		
			Value = _network.Update()[0]; //TODO: more than one output
		}

		public void AddInput(Gate outputGate)
		{
			Input.Add(new Connection(outputGate, this));
		}

		public void AddOutput(Gate inputGate)
		{
			Output.Add(new Connection(this, inputGate));
		}

		protected virtual bool?[] GetInput()
		{
			//CONSIDER: disallow any amount of inputs not equal to 2?
			bool?[] input = new bool?[Input.Count];
		
			for (int i = 0; i < input.Length; i++)
			{
				input[i] = Input[i].Input.Value;
			}
		
			return input;
		}
	}
}