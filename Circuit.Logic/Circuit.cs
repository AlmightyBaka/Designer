// Perceptrons: Circuit.cs

using System;
using System.Collections.Generic;
using Designer.Circuit.Logic.Gates;

namespace Designer.Circuit.Logic
{
	public class Circuit
	{


		public List<Gate> GateList;
#if DEBUG
		private bool?[] _input;
#endif

		public Circuit(bool?[] input)
		{
			GateList = new List<Gate>(input.Length);

			for (int i = 0; i < input.Length; i++)
			{
				GateList.Add(new GateInput(input[i]));
				//GateList[GateList.Count].Value = input[i];
			}

#if DEBUG
			_input = input; 
#endif
		}

		public void Add(Gate.Types type)
		{
			if (type == Gate.Types.Input)
			{
				throw new Exception("Cannot add input gate"); // TODO
			}

			if (type == Gate.Types.And)
			{
				GateList.Add(new GateAnd());
			}
			else if (type == Gate.Types.Not)
			{
				GateList.Add(new GateNot());
			}
			else if (type == Gate.Types.Or)
			{
				GateList.Add(new GateOr());
			}
			else if (type == Gate.Types.Xor)
			{
				GateList.Add(new GateXor());
			}
		}

		public void Bind(Gate outputGate, Gate inputGate)
		{
			outputGate.AddOutput(inputGate);
			inputGate.AddInput(outputGate);
		}

		public void Calculate()
		{
			for (int i = 0; i < GateList.Count; i++)
			{
				GateList[i].Update();
			}
		}

#if DEBUG
		#region Output
		public void PrintFull()
		{
			Console.Write("Input:\n");
			for (int i = 0; i < _input.Length; i++)
			{
				Console.Write($"{_input[i]} ");
			}
			Console.WriteLine();

			for (int i = 0; i < GateList.Count; i++)
			{
				PrintGateType(i);
				PrintGateOutput(i);
			}
		}

		public void PrintGateOutput(int gate)
		{
			Console.WriteLine($"Gate[{gate}] output: {GateList[gate].Value}");
		}

		public void PrintGateType(int gate)
		{
			Console.WriteLine($"Gate[{gate}] type: {GateList[gate].Type}");
		}
		#endregion
#endif
	}
}