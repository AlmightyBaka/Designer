using System.Collections.Generic;
using Designer.Circuit.Logic;
using Designer.Circuit.Logic.Gates;

namespace Circuit.Model
{
	public class Node
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Gate Gate { get; }
		public bool Value => (bool) Gate.Value;
		public Gate.Types Type => Gate.Type;
		public List<Connection> Inputs => Gate.Input;
		public List<Connection> Outputs => Gate.Output;
		public List<Node> InputNodes { get; set; } = new List<Node>();
		public List<Node> OutputNodes { get; set; } = new List<Node>();
		public bool IsToGuess { get; set; } = false;
		public bool? ValueGuessed { get; set; }

		public Node(int x, int y, Gate gate)
		{
			X = x;
			Y = y;
			Gate = gate;
		}

		public Node(int x, int y, Gate.Types type)
		{
			X = x;
			Y = y;
			Gate = new Gate {Type = type};
		}

		public override string ToString()
		{
			return base.ToString() + Type;
		}
	}
}
