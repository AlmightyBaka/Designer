using System.Collections.Generic;
using Designer.Circuit.Logic;

namespace Circuit.Model
{
	static class NodeGenerator
	{
		private static Designer.Circuit.Logic.Circuit circuit;
		private static List<Node> nodes;

		static public List<Node> Generate(Designer.Circuit.Logic.Circuit circuit, List<int> gateLayers)
		{
			NodeGenerator.circuit = circuit;
			nodes = new List<Node>();

			int index = 0;
			for (int x = 1; x < gateLayers.Count + 1; x++)
			{
				int layer = gateLayers[x - 1];
				int y = 1;

				for (int i = index; i < index + layer; i++)
				{
					nodes.Add(GetNode(x, y, i));
					if (x == gateLayers.Count)
					{
						nodes[nodes.Count - 1].IsToGuess = true;
					}
					y++;
				}

				index += layer;
			}

			return nodes;
		}

		private static Node GetNode(int x, int y, int i)
		{
			Node node = new Node(x, y, circuit.GateList[i]); // TODO: connections

			foreach (Connection input in node.Inputs)
			{
				foreach (Node searchNode in nodes)
				{
					if (searchNode == node)
					{
						continue;
					}
					if (input.Input == searchNode.Gate)
					{
						node.InputNodes.Add(searchNode);
						searchNode.OutputNodes.Add(node);
					}
				}
			}

			return node;
		}
	}
}
