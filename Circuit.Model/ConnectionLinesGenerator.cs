using System;
using System.Collections.Generic;

namespace Circuit.Model
{
	class ConnectionLinesGenerator
	{
		private static float _nodeSize;
		private static float _offset;

		public static List<ConnectionLine> Generate(List<Node> nodes, float nodeSize, float offset)
		{
			_nodeSize = nodeSize;
			_offset = offset;
			List<ConnectionLine> result = new List<ConnectionLine>();
			int[] verticalLines = new int[nodes[nodes.Count - 1].X]; // TODO: beforehand pass
			bool isOffsetLeft = true;
			bool isOffsetUp = true;

			foreach (Node node in nodes)
			{
				foreach (Node output in node.OutputNodes)
				{
					isOffsetLeft = !isOffsetLeft;

					verticalLines[output.X - 1] += 1;

					result.Add(new ConnectionLine(GeneratePoints(node, output, verticalLines[output.X - 1], isOffsetLeft, output.InputNodes.Count, ref isOffsetUp)));
				}
			}

			return result;
		}

		private static List<Point> GeneratePoints(Node node, Node output, int xOffset, bool isXOffsetLeft, int yOffset, ref bool isYOffsetUp)
		{
			if (node.X == output.X - 1 && node.Y != output.Y)
			{
				return GeneratePointsIncXDiffY(node, output, xOffset, isXOffsetLeft, yOffset, ref isYOffsetUp);
			}
			if (node.X == output.X - 1 && node.Y == output.Y)
			{
				return GeneratePointsIncXSameY(node, output);
			}

			throw new Exception(); // UNSAFE
			//return GeneratePointsIncXSameY(node, output);
		}

		//public static List<ConnectionLine> Generate(List<Node> nodes, float nodeSize, float offset)
		//{
		//	_nodeSize = nodeSize;
		//	_offset = offset;
		//	List<ConnectionLine> result = new List<ConnectionLine>();
		//	int[] verticalLines = new int[nodes[nodes.Count - 1].X];
		//	int[,] horisontalLines = new int[nodes[nodes.Count - 1].X, 4]; // TODO
		//	bool isOffsetLeft = true;
		//	bool isOffsetUp = true;

		//	foreach (Node node in nodes)
		//	{
		//		foreach (Node output in node.OutputNodes)
		//		{
		//			isOffsetLeft = !isOffsetLeft;

		//			verticalLines[output.X - 1] += 1;
		//			horisontalLines[output.X - 1, output.Y - 1] += 1;

		//			result.Add(new ConnectionLine(GeneratePoints(node, output, verticalLines[output.X - 1], horisontalLines[output.X - 1, output.Y], isOffsetLeft, ref isOffsetUp)));
		//		}
		//	}

		//	return result;
		//}

		//private static List<Point> GeneratePoints(Node node, Node output, int xOffset, int yOffset, bool isXOffsetLeft, ref bool isYOffsetUp)
		//{
		//	if (node.X == output.X - 1 && node.Y != output.Y)
		//	{
		//		return GeneratePointsIncXDiffY(node, output, xOffset, isXOffsetLeft, yOffset, ref isYOffsetUp);
		//	}
		//	if (node.X == output.X - 1 && node.Y == output.Y)
		//	{
		//		return GeneratePointsIncXSameY(node, output);
		//	}

		//	throw new Exception(); // UNSAFE
		//	//return GeneratePointsIncXSameY(node, output);
		//}


		private static List<Point> GeneratePointsIncXDiffY(Node node, Node output, int xOffset, bool isOffsetLeft, int yOffset, ref bool isYOffsetUp)
		{
			List<Point> result = new List<Point>();
			float offsetX;
			float offsetY;

			if (xOffset > 1)
			{
				offsetX = 0.05f * xOffset;
			}
			else
			{
				offsetX = 0;
			}
			if (isOffsetLeft)
			{
				offsetX *= -1;
			}

			if (yOffset > 1)
			{
				offsetY = 0.025f * yOffset;
			}
			else
			{
				offsetY = 0;
			}
			if (isYOffsetUp)
			{
				offsetY *= -1;
			}

			result.Add(new Point(GetX(node.X, Position.Beginning, 0), GetY(node.Y, 0)));
			result.Add(new Point(GetX(node.X, Position.Middle, offsetX), GetY(node.Y, 0)));
			result.Add(new Point(GetX(node.X, Position.Middle, offsetX), GetY(output.Y, offsetY)));
			result.Add(new Point(GetX(output.X, Position.End, 0), GetY(output.Y, offsetY)));

			isYOffsetUp = !isYOffsetUp;

			return result;
		}

		private static List<Point> GeneratePointsIncXSameY(Node node, Node output)
		{
			List<Point> result = new List<Point>
			{
				new Point(GetX(node.X, Position.Beginning, 0), GetY(node.Y, 0)),
				new Point(GetX(output.X, Position.End, 0), GetY(output.Y, 0))
			};


			return result;
		}

		private static float GetX(float x, Position position, float offset)
		{
			if (position == Position.Beginning)
			{
				return (x + _nodeSize / 2 + _offset) + offset;
			}
			if (position == Position.End)
			{
				return (x - _nodeSize / 2 + _offset) + offset;
			}
			if (position == Position.Middle)
			{
				return (x + _offset + 0.5f) + offset;
			}

			throw new ArgumentException();
		}

		private static float GetY(float y, float offset)
		{
			return (y + _offset) + offset;
		}
	}

	public class ConnectionLine
	{
		public List<Point> Points { get; }

		public ConnectionLine(List<Point> points)
		{
			Points = points;
		}
	}

	public struct Point
	{
		public float X { get; }
		public float Y { get; }

		public Point(float x, float y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return $"{X}; {Y}";
		}
	}

	public enum Position
	{
		Beginning,
		Middle,
		End
	}
}



//public static List<ConnectionLine> Generate(List<Node> nodes, float nodeSize, float offset)
//{
//	_nodeSize = nodeSize;
//	_offset = offset;
//	List<ConnectionLine> result = new List<ConnectionLine>();
//	int[] VerticalLines = new int[nodes[nodes.Count - 1].X]; // TODO: beforehand pass
//	bool isOffsetLeft = true;
//	bool isOffsetUp = true;

//	foreach (Node node in nodes)
//	{
//		foreach (Node output in node.OutputNodes)
//		{
//			isOffsetLeft = !isOffsetLeft;
//			isOffsetUp = !isOffsetUp;

//			VerticalLines[output.X - 1] += 1;

//			result.Add(new ConnectionLine(GeneratePoints(node, output, VerticalLines[output.X - 1], isOffsetLeft, output.InputNodes.Count, isOffsetUp)));
//		}
//	}

//	return result;
//}

//private static List<Point> GeneratePoints(Node node, Node output, int xOffset, bool isXOffsetLeft, int yOffset, bool isYOffsetUp)
//{
//	if (node.X == output.X - 1 && node.Y != output.Y || (node.X == output.X - 1 && node.Y == output.Y && yOffset != 1))
//	{
//		return GeneratePointsIncXDiffY(node, output, xOffset, isXOffsetLeft, yOffset, isYOffsetUp);
//	}
//	if (node.X == output.X - 1 && node.Y == output.Y && yOffset == 1)
//	{
//		return GeneratePointsIncXSameY(node, output);
//	}

//	//throw new Exception();
//	return GeneratePointsIncXSameY(node, output); // TODO
//}

//private static List<Point> GeneratePointsIncXDiffY(Node node, Node output, int xOffset, bool isOffsetLeft, int yOffset, bool isYOffsetUp)
//{
//	List<Point> result = new List<Point>();
//	float offsetX;
//	float offsetY;

//	if (xOffset > 1)
//	{
//		offsetX = 0.05f * xOffset;
//	}
//	else
//	{
//		offsetX = 0;
//	}
//	if (isOffsetLeft)
//	{
//		offsetX *= -1;
//	}

//	if (yOffset > 1)
//	{
//		offsetY = 0.05f * yOffset;
//	}
//	else
//	{
//		offsetY = 0;
//	}
//	if (isYOffsetUp)
//	{
//		offsetY *= -1;
//	}

//	result.Add(new Point(GetX(node.X, Position.Beginning, 0), GetY(node.Y, 0)));
//	result.Add(new Point(GetX(node.X, Position.Middle, offsetX), GetY(node.Y, 0)));
//	result.Add(new Point(GetX(node.X, Position.Middle, offsetX), GetY(output.Y, offsetY)));
//	result.Add(new Point(GetX(output.X, Position.End, 0), GetY(output.Y, offsetY)));

//	return result;
//}

//private static List<Point> GeneratePointsIncXSameY(Node node, Node output)
//{
//	List<Point> result = new List<Point>();

//	result.Add(new Point(GetX(node.X, Position.Beginning, 0), GetY(node.Y, 0)));
//	result.Add(new Point(GetX(output.X, Position.End, 0), GetY(output.Y, 0)));

//	return result;
//}
