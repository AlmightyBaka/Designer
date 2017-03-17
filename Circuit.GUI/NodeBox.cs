using Circuit.Model;

namespace Circuit.GUI
{
	public class NodeBox
	{
		public Node Node { get; }
		public int Number { get; }
		public int EllipseNumber { get; }

		public NodeBox(Node node, int number, int ellipseNumber)
		{
			Node = node;
			Number = number;
			EllipseNumber = ellipseNumber;
		}
	}
}