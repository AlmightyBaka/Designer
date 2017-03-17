// Designer: TestTools.cs

using Designer.Circuit.Logic.Gates;

namespace Designer.Circuit.Logic.Tests
{
	internal static class TestTools
	{
		public static Circuit GateCalculate(bool?[] input, Gate.Types typeType)
		{
			Circuit circuit = new Circuit(input);
			circuit.Add(typeType);
			circuit.Bind(circuit.GateList[0], circuit.GateList[2]);
			circuit.Bind(circuit.GateList[1], circuit.GateList[2]);
			circuit.Calculate();

			return circuit;
		}

		public static Circuit NotGateCalculate(bool?[] input)
		{
			Circuit circuit = new Circuit(input);
			circuit.Add(Gate.Types.Not);
			circuit.Bind(circuit.GateList[0], circuit.GateList[1]);
			circuit.Calculate();

			return circuit;
		}

		public static Circuit CircuitInput(bool?[] input)
		{
			Circuit circuit = new Circuit(input);
			circuit.Add(Gate.Types.Input);
			circuit.Add(Gate.Types.Input);
			circuit.Add(Gate.Types.And);

			circuit.GateList[3].Value = true;
			circuit.GateList[2].Value = true;

			circuit.Bind(circuit.GateList[2], circuit.GateList[4]);
			circuit.Bind(circuit.GateList[3], circuit.GateList[4]);
			circuit.Calculate();
			return circuit;
		}

		public static Circuit CircuitAndOrOr(bool?[] input)
		{
			Circuit circuit = new Circuit(input);
			circuit.Add(Gate.Types.And);
			circuit.Add(Gate.Types.Or);
			circuit.Add(Gate.Types.Or);

			circuit.Bind(circuit.GateList[0], circuit.GateList[2]);
			circuit.Bind(circuit.GateList[1], circuit.GateList[2]);
			circuit.Bind(circuit.GateList[0], circuit.GateList[3]);
			circuit.Bind(circuit.GateList[1], circuit.GateList[3]);
			circuit.Bind(circuit.GateList[2], circuit.GateList[4]);
			circuit.Bind(circuit.GateList[3], circuit.GateList[4]);
			circuit.Calculate();
			return circuit;
		}

		public static Circuit CircuitAndOrXor(bool?[] input)
		{
			Circuit circuit = new Circuit(input);
			circuit.Add(Gate.Types.And);
			circuit.Add(Gate.Types.Or);
			circuit.Add(Gate.Types.Xor);

			circuit.Bind(circuit.GateList[0], circuit.GateList[2]);
			circuit.Bind(circuit.GateList[1], circuit.GateList[2]);
			circuit.Bind(circuit.GateList[0], circuit.GateList[3]);
			circuit.Bind(circuit.GateList[1], circuit.GateList[3]);
			circuit.Bind(circuit.GateList[2], circuit.GateList[4]);
			circuit.Bind(circuit.GateList[3], circuit.GateList[4]);
			circuit.Calculate();
			return circuit;
		}

		public static Circuit CircuitNotOrNotAndAnd(bool?[] input)
		{
			Circuit circuit = new Circuit(input);
			circuit.Add(Gate.Types.Or); // 2
			circuit.Add(Gate.Types.Not); // 3
			circuit.Add(Gate.Types.And); // 4
			circuit.Add(Gate.Types.Not); // 5
			circuit.Add(Gate.Types.And); // 6
		
			circuit.Bind(circuit.GateList[0], circuit.GateList[2]);
			circuit.Bind(circuit.GateList[1], circuit.GateList[2]);
			circuit.Bind(circuit.GateList[2], circuit.GateList[3]);

			circuit.Bind(circuit.GateList[0], circuit.GateList[4]);
			circuit.Bind(circuit.GateList[1], circuit.GateList[4]);
			circuit.Bind(circuit.GateList[4], circuit.GateList[5]);
		
			circuit.Bind(circuit.GateList[3], circuit.GateList[6]);
			circuit.Bind(circuit.GateList[5], circuit.GateList[6]);
		
			circuit.Calculate();
			return circuit;
		}
	}
}