// Perceptrons: Program.cs
// http://neuralnetworksanddeeplearning.com/chap1.html

using System;
using Designer.Circuit.Logic.Gates;

namespace Designer.Circuit.Logic
{
	class Program
	{
		static void Main()
		{
			TestCircuit();

			Console.ReadKey();
		}

		private static void TestCircuit()
		{
			bool?[] input = { true, true };

			Circuit circuit = new Circuit(input);
			circuit.Add(Gate.Types.And);
			circuit.Bind(circuit.GateList[0], circuit.GateList[2]);
			circuit.Bind(circuit.GateList[1], circuit.GateList[2]);

			circuit.Calculate();
#if DEBUG
			circuit.PrintFull(); 
#endif
		}
	}
}