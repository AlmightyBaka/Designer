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
	public partial class DebugWindow : Window
	{
		private Model.Model model;
		private int genVersion = 4;
		private int cellSize = 100;
		private List<NodeBox> nodeBoxes = new List<NodeBox>();

		public DebugWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			model = new Model.Model(genVersion);
			Draw(cellSize);
		}

		private void Draw(int cellSize)
		{
			Canvas.Children.Clear();
			nodeBoxes = new List<NodeBox>();

			int count = 0;
			foreach (Node node in model.Nodes)
			{
				int x = GetPoint(node.X, true);
				int y = GetPoint(node.Y, true);

				nodeBoxes.Add(new NodeBox(node, count, Canvas.Children.Count));
				count++;

				Ellipse ellipse;
				if (!node.IsToGuess)
				{
					if (node.Type != Gate.Types.Input)
					{
						ellipse = new Ellipse()
						{
							StrokeThickness = 2,
							Stroke = Brushes.Black,
							Fill = Brushes.White,
							Width = cellSize * model.NodeSize,
							Height = cellSize * model.NodeSize,
						};
					}
					else
					{
						ellipse = new Ellipse()
						{
							StrokeThickness = 2,
							Stroke = Brushes.LightSkyBlue,
							Fill = Brushes.White,
							Width = cellSize * model.NodeSize,
							Height = cellSize * model.NodeSize,
						};
					}
				}
				else
				{
					ellipse = new Ellipse()
					{
						StrokeThickness = 2,
						Stroke = Brushes.DarkRed,
						Fill = Brushes.White,
						Width = cellSize * model.NodeSize,
						Height = cellSize * model.NodeSize,
					};
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
					IsHitTestVisible = false
				};
				Canvas.SetLeft(label, x + (cellSize * model.NodeSize) / 4);
				Canvas.SetTop(label, y + (cellSize * model.NodeSize) / 4);
				Canvas.Children.Add(label);
			}

			foreach (ConnectionLine line in model.ConnectionLines)
			{
				if (line.Points.Count > 1)
				{
					Point firstPoint = line.Points[0];

					Ellipse ellipse;
					for (int i = 1; i < line.Points.Count; i++)
					{
						Canvas.Children.Add(GetLine(firstPoint.X, firstPoint.Y, line.Points[i].X, line.Points[i].Y));

						ellipse = new Ellipse()
						{
							StrokeThickness = 2,
							Stroke = Brushes.Black,
							Fill = Brushes.Black,
							Width = 6,
							Height = 6,
						};
						Canvas.SetLeft(ellipse, GetPoint(firstPoint.X, false) - 3);
						Canvas.SetTop(ellipse, GetPoint(firstPoint.Y, false) - 3);
						Canvas.Children.Add(ellipse);

						firstPoint = line.Points[i];
					}

					ellipse = new Ellipse()
					{
						StrokeThickness = 2,
						Stroke = Brushes.Black,
						Fill = Brushes.Black,
						Width = 6,
						Height = 6,
					};
					Canvas.SetLeft(ellipse, GetPoint(line.Points[line.Points.Count - 1].X, false) - 3);
					Canvas.SetTop(ellipse, GetPoint(line.Points[line.Points.Count - 1].Y, false) - 3);
					Canvas.Children.Add(ellipse);
				}
			}
		}

		private Line GetLine(float x1, float y1, float x2, float y2)
		{
			return new Line()
			{
				Stroke = Brushes.Black,
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

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			System.Windows.Point p = Mouse.GetPosition(Canvas);
			PositionLabel.Content = $"{(int)p.X,3};{(int)p.Y,3}";

			NodeBox nodeBox = GetNodeBoxMouseOver();
			if (nodeBox != null)
			{
				LabelValue.Content = $"{nodeBox.Node.Type,6} ({nodeBox.Number}): {nodeBox.Node.Value,6}";
				if (nodeBox.Node.IsToGuess)
				{
					LabelGuess.Content = $"Guess: {nodeBox.Node.ValueGuessed,6}";
				}
			}
			else
			{
				LabelValue.Content = "";
				LabelGuess.Content = "";
			}
		}

		private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Node node = GetNodeBoxMouseOver().Node;

			if (node != null)
			{
				if (node.IsToGuess)
				{
					if (node.ValueGuessed == null)
					{
						node.ValueGuessed = true;
					}
					else
					{
						node.ValueGuessed = !node.ValueGuessed;
					}
				}

				LabelGuess.Content = $"Guess: {node.ValueGuessed,6}";
			}
		}

		private void ButtonBreak_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Debugger.Break();
		}

		private void ButtonGenerateOld_Click(object sender, RoutedEventArgs e)
		{
			bool generated = false;
			while (!generated)
			{
				try
				{
					model = new Model.Model(genVersion - 1);
					generated = true;
				}
				catch (System.Exception)
				{
					generated = false;
				}
			}
			Draw(cellSize);
		}


		private void ButtonCheck_Click(object sender, RoutedEventArgs e)
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

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			//int windowHeightNew = (int) this.ActualHeight;
			//int windowWidthNew = (int) this.ActualWidth;

			//int resizeValueNew = (windowWidthNew + windowHeightNew)/100;
			//int resizeValueOld = (windowWidth + windowHeight)/100;
			//cellSize += resizeValueNew - resizeValueOld;
			//DrawGrid(cellSize);
			//Draw(cellSize);



			//if ( this.ActualWidth > windowWidth && this.windowHeight > windowHeight)
			//{
			//	cellSize += 1;
			//	DrawGrid(cellSize);
			//	Draw(cellSize);
			//}
			//else
			//{

			//	cellSize -= 1;
			//	DrawGrid(cellSize);
			//	Draw(cellSize);
			//}

			//windowWidth = (int) this.ActualWidth;
			//windowHeight = (int) this.ActualHeight;
		}
	}
}


#region OLD XAML

//<Window x:Class="Circuit.GUI.MainWindow"
//        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
//        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
//        xmlns:local="clr-namespace:Circuit.GUI"
//        mc:Ignorable="d"
//        Title="MainWindow" Height="550" Width="550" MouseMove="Canvas_MouseMove" SizeChanged="Window_SizeChanged">
//    <Grid>
//        <Grid.RowDefinitions>
//            <RowDefinition Height = "50" />

//			< RowDefinition Height="Auto"/>
//        </Grid.RowDefinitions>
//        <Grid Row = "1" >

//			< Canvas Name="Canvas" Height="Auto" Width="Auto" MouseMove="Canvas_MouseMove" Grid.RowSpan="2" MouseDown="Canvas_MouseDown"/>
//        </Grid>
//        <Grid Row = "0" >

//			< StackPanel Orientation="Horizontal" Width="Auto">
//                <StackPanel.Resources>
//                    <Style TargetType = "{x:Type Button}" >

//						< Setter Property="Margin" Value="10,0,0,0"/>
//                    </Style>
//                </StackPanel.Resources>
//                <Label x:Name="PositionLabel" Content="Cursor Pos" Width="75" Height="30" />
//                <Label x:Name="LabelValue" Content="Node Value" Width="100" Height="30"/>
//                <Label x:Name="LabelGuess" Content="ToGuess" Width="100" Height="30"/>
//                <Button x:Name="ButtonBreak" Content="Break" Width="75" Height="30" Click="ButtonBreak_Click" />
//                <Button x:Name="ButtonGenerate" Content="Gen" Width="75"  Height="30" Click="Button_Click"/>
//                <Button x:Name="ButtonGenerateOld" Content="Gen Old" Width="75" Height="30" Click="ButtonGenerateOld_Click"/>
//            </StackPanel>
//        </Grid>
//        <Button x:Name="ButtonCheck" Content="Check" HorizontalAlignment="Left" Margin="10,0,0,-458" Grid.Row="1" VerticalAlignment="Bottom" Width="75" Height="30" Click="ButtonCheck_Click"/>
//    </Grid>
//</Window>

#endregion

#region NEW XAML

//<Window x:Class="Circuit.GUI.MainWindow"
//        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
//        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
//        xmlns:local="clr-namespace:Circuit.GUI"
//        mc:Ignorable="d"
//        Title="MainWindow" Height="550" Width="550" MouseMove="Canvas_MouseMove" SizeChanged="Window_SizeChanged">
//	<Grid>
//		<Grid.RowDefinitions>
//			<RowDefinition Height = "83" />
//			< RowDefinition Height="53*"/>
//			<RowDefinition Height = "13*" />
//		</ Grid.RowDefinitions >
//		< Grid.ColumnDefinitions >
//			< ColumnDefinition Width="119"/>
//			<ColumnDefinition Width = "0*" />
//			< ColumnDefinition />
//		</ Grid.ColumnDefinitions >
//		< Canvas Grid.Row="1" Grid.Column="2" Grid.RowSpan="2" Background="White"></Canvas>
//		<Label Grid.Row="2" Content= "Проверить" />
//	</ Grid >
//</ Window >

#endregion