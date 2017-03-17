using System;
using System.Collections.Generic;
using Designer.Circuit.Logic.Gates;

namespace Circuit.Model
{
	public class Model
	{
		public List<Node> Nodes { get; }
		public List<ConnectionLine> ConnectionLines { get; }

		public float NodeSize { get; } = 0.45f;
		public float Offset { get; } = 0f;

		private Designer.Circuit.Logic.Circuit circuit; // TODO: add input after declaring
		private List<int> gateLayers;

		#region Constants
		private readonly int minX = 1;
		private readonly int maxX = 15;
		private readonly int minY = 1;
		private readonly int maxY = 15;
		private readonly int minInputGates = 2;
		private readonly int maxInputGates = 3;
		private readonly int minOutputGates = 3;
		private readonly int maxOutputGates = 4;
		private readonly int minGateLevels = 4;
		private readonly int maxGateLevels = 4;
		private readonly int maxGatesInLevel = 4;
		private readonly int maxGatesTotal = 15;
		#endregion

		public Model(int generatorVersion)
		{
			bool?[] input = GenerateInputs();
			circuit = new Designer.Circuit.Logic.Circuit(input);
			if (generatorVersion == 1)
			{
				circuit = GateGenerator.GenerateV1(circuit, input.Length, minGateLevels, maxGateLevels, ref gateLayers); 
			}
			else if (generatorVersion == 2)
			{
				circuit = GateGenerator.GenerateV2(circuit, minGateLevels, maxGateLevels, maxGatesInLevel, ref gateLayers);
			}
			else if (generatorVersion == 3)
			{
				circuit = GateGenerator.GenerateV3(circuit, minGateLevels, maxGateLevels, maxGatesInLevel, minOutputGates, maxOutputGates, ref gateLayers);
			}
			else if (generatorVersion == 4)
			{

				circuit = GateGenerator.GenerateV4(circuit, minGateLevels, maxGateLevels, maxGatesInLevel, maxGatesTotal, minOutputGates, maxOutputGates, ref gateLayers);
			}
			else
			{
				throw new ArgumentException();
			}

			Nodes = NodeGenerator.Generate(circuit, gateLayers);
			ConnectionLines = ConnectionLinesGenerator.Generate(Nodes, NodeSize, Offset);

			circuit.Calculate();
		}

		#region Public methods



		#endregion

		#region Private methods

		private void AddNode(int x, int y, Gate.Types type)
		{
			if (x > maxX || x < minX || y > maxY || y < minY)
			{
				throw new ArgumentOutOfRangeException();
			}

			Nodes.Add(new Node(x, y, type));
			circuit.Add(type);
		}

		private void BindNode(Node outputNode, Node inputNode)
		{
			outputNode.OutputNodes.Add(inputNode);
			inputNode.InputNodes.Add(outputNode);

			circuit.Bind(outputNode.Gate, inputNode.Gate);
		}

		private bool?[] GenerateInputs()
		{
			Random random = new Random();
			bool?[] inputs = new bool?[random.Next(minInputGates, maxInputGates + 1)];

			for (int i = 0; i < inputs.Length; i++)
			{
				inputs[i] = random.NextDouble() >= 0.5;
			}

			return inputs;
		}

		#endregion
	}
}
