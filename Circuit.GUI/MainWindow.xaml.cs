using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Circuit.Model;
using Designer.Circuit.Logic.Gates;
using Point = Circuit.Model.Point;

namespace Circuit.GUI
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Model.Model model;
		private List<NodeBox> nodeBoxes = new List<NodeBox>();

		#region Constants
		private int genVersion = 4;
		private int cellSize = 150;

		private readonly Brush connectionLineColor = Brushes.White;
		private readonly Brush defaultEllipseColor = new SolidColorBrush(Color.FromArgb(0xff, 0x31, 0x8b, 0xff));
		private readonly Brush ellipseFillColor = new SolidColorBrush(Color.FromArgb(0xff, 0x2e, 0x2e, 0x2e));

        private readonly Brush toGuessEllipseColorDefault = Brushes.Gold;
		private readonly Brush toGuessEllipseColorTrue = new SolidColorBrush(Color.FromArgb(0xff, 0x34, 0xff, 0x31));
		private readonly Brush toGuessEllipseColorFalse = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x34, 0x31));

		private readonly Brush inputEllipseColorTrue = new SolidColorBrush(Color.FromArgb(0xff, 0x34, 0xff, 0x31));
		private readonly Brush inputEllipseColorFalse = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x34, 0x31));
		#endregion

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Generate()
		{
			model = new Model.Model(genVersion);
			Draw(cellSize);
		}

		private void Draw(int cellSize)
		{
			Canvas.Children.Clear();
			nodeBoxes = new List<NodeBox>();

			foreach (Node node in model.Nodes)
			{
				DrawNode(cellSize, node);
			}

			foreach (ConnectionLine line in model.ConnectionLines)
			{
				DrawLine(line);
			}
		}

		private void DrawNode(int cellSize, Node node)
		{
			int count = 0;
			int x = GetPoint(node.X, true);
			int y = GetPoint(node.Y, true);

			nodeBoxes.Add(new NodeBox(node, count, Canvas.Children.Count));
			count++;

			Ellipse ellipse;
			if (!node.IsToGuess)
			{
				if (node.Type != Gate.Types.Input)
				{
					ellipse = GetEllipse(cellSize, defaultEllipseColor, true);
				}
				else
				{
					if (node.Value)
					{
						ellipse = GetEllipse(cellSize, inputEllipseColorTrue, true);
					}
					else
					{
						ellipse = GetEllipse(cellSize, inputEllipseColorFalse, true);
					}
				}
			}
			else
			{
				ellipse = GetEllipse(cellSize, toGuessEllipseColorDefault, true);
			}
			Canvas.SetLeft(ellipse, x);
			Canvas.SetTop(ellipse, y);
			Canvas.Children.Add(ellipse);

			string content = "";
			if (node.Type == Gate.Types.And)
			{
				content = "AND";
			}
			if (node.Type == Gate.Types.Not)
			{
				content = "NOT";
			}
			if (node.Type == Gate.Types.Or)
			{
				content = "OR";
			}
			if (node.Type == Gate.Types.Xor)
			{
				content = "XOR";
			}
			if (node.Type == Gate.Types.Input)
			{
				content = "IN";
			}

			Label label = new Label()
			{
				Content = content,
				Foreground = Brushes.White,
				IsHitTestVisible = false
			};
			Canvas.SetLeft(label, x + (cellSize * model.NodeSize) / 4);
			Canvas.SetTop(label, y + (cellSize * model.NodeSize) / 4);
			Canvas.Children.Add(label);
		}

		private void DrawLine(ConnectionLine line)
		{
			int ellipseSize = 6;

			if (line.Points.Count > 1)
			{
				Point firstPoint = line.Points[0];

				Ellipse ellipse;
				for (int i = 1; i < line.Points.Count; i++)
				{
					Canvas.Children.Add(GetLine(firstPoint.X, firstPoint.Y, line.Points[i].X, line.Points[i].Y));

					ellipse = GetEllipse(ellipseSize, connectionLineColor, false);

					Canvas.SetLeft(ellipse, GetPoint(firstPoint.X, false) - ellipseSize / 2);
					Canvas.SetTop(ellipse, GetPoint(firstPoint.Y, false) - ellipseSize / 2);
					Canvas.Children.Add(ellipse);

					firstPoint = line.Points[i];
				}

				ellipse = GetEllipse(ellipseSize, connectionLineColor, false);

				Canvas.SetLeft(ellipse, GetPoint(line.Points[line.Points.Count - 1].X, false) - ellipseSize / 2);
				Canvas.SetTop(ellipse, GetPoint(line.Points[line.Points.Count - 1].Y, false) - ellipseSize / 2);
				Canvas.Children.Add(ellipse);
			}
		}

		private Ellipse GetEllipse(int cellSize, Brush color, bool isNode)
		{
			Ellipse ellipse;
			if (isNode)
			{
				ellipse = new Ellipse()
				{
					StrokeThickness = 2,
					Stroke = color,
					Fill = ellipseFillColor,
					Width = cellSize * model.NodeSize,
					Height = cellSize * model.NodeSize,
				};
			}
			else
			{
				ellipse = new Ellipse()
				{
					StrokeThickness = 2,
					Stroke = color,
					Fill = color,
					Width = cellSize,
					Height = cellSize,
				};
			}
			return ellipse;
		}

		private Line GetLine(float x1, float y1, float x2, float y2)
		{
			return new Line()
			{
				Stroke = connectionLineColor,
				StrokeThickness = 2,
				X2 = GetPoint(x2, false),
				X1 = GetPoint(x1, false),
				Y1 = GetPoint(y1, false),
				Y2 = GetPoint(y2, false)
			};
		}

		private int GetPoint(float x, bool isNode)
		{
			if (isNode)
			{
				float positionNode = x * cellSize;
				float positionNodeOffset = positionNode - (cellSize * model.NodeSize) / 2;
				float positionFinal = positionNodeOffset + model.Offset * cellSize;

				return (int)positionFinal;
			}
			float position = (x - x % 1) * cellSize;
			float positionOffset = position + (x % 1) * cellSize;

			return (int)positionOffset;
		}

		private NodeBox GetNodeBoxMouseOver()
		{
			for (int i = 0; i < Canvas.Children.Count; i++)
			{
				if (Canvas.Children[i].IsMouseOver)
				{
					foreach (NodeBox nodeBox in nodeBoxes)
					{
						if (nodeBox.EllipseNumber == i)
						{
							return nodeBox;
						}
					}
					break;
				}
			}
			return null;
		}

		private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			NodeBox nodeBox = GetNodeBoxMouseOver();

			if (nodeBox != null)
			{
				if (nodeBox.Node.IsToGuess)
				{
					if (nodeBox.Node.ValueGuessed == null)
					{
						nodeBox.Node.ValueGuessed = true;
					}
					else
					{
						nodeBox.Node.ValueGuessed = !nodeBox.Node.ValueGuessed;
					}

					Ellipse ellipse = Canvas.Children[nodeBox.EllipseNumber] as Ellipse;
					if ((bool)nodeBox.Node.ValueGuessed)
					{
						ellipse.Stroke = toGuessEllipseColorTrue;
					}
					else
					{
						ellipse.Stroke = toGuessEllipseColorFalse;
					}
				}
			}
		}

		private void LabelCheck_MouseDown(object sender, MouseButtonEventArgs e)
		{
			int errorsCount = 0;

			foreach (Node node in model.Nodes)
			{
				if (node.IsToGuess)
				{
					if (node.Value != node.ValueGuessed)
					{
						errorsCount++;
					}
				}
			}

			MessageBox.Show($"Errors: {errorsCount}");
		}

		private void Canvas_Loaded(object sender, RoutedEventArgs e)
		{
			Generate();
		}

		private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.R && Keyboard.Modifiers == ModifierKeys.Control)
			{
				Generate();
			}
		}
	}
}