// Perceptrons: Network.cs

using System;
using System.Collections.Generic;

namespace Designer.Circuit.Logic.Perceptrons
{
	public class Network
	{
		private readonly Layer[] _layers;
		private readonly int _size;

		public Network(float[][] layerWeight, float[][] layerThreshold)
		{
			#region check amount
			if (layerWeight.Length != layerThreshold.Length)
			{
				throw new Exception("Inequal amount of weigths and thresholds");
			}
			#endregion

			// Layers from parameters plus 2 hidden layers: input and output
			_size = layerThreshold.Length + 1;
			_layers = new Layer[_size + 1];
			// output layer
			_layers[_size] = new Layer(new[] { 1f }, new[] { 0f });
			// input layer is assigned with UpdateInput method

			for (int i = 1; i < _size; i++)
			{
				_layers[i] = new Layer(layerWeight[i - 1], layerThreshold[i - 1]);
			}
		}

		public List<bool?> Update()
		{
			List<bool?> output = new List<bool?>();

			for (int i = 1; i < _size; i++)
			{
				// Update current layer's values...
				List<bool> prevLayerOutput = _layers[i].Update(_layers[i].Connections);
				for (int j = 0; j < _layers[i + 1].Connections.Count; j++)
				{
					// ...and write them to next layer's inputs
					_layers[i + 1].Connections[j].Value = prevLayerOutput[j];
				}
			}

			for (int i = 0; i < _layers[_size].Connections.Count; i++)
			{
				output.Add(_layers[_size].Connections[i].Value);
			}

			return output;
		}

		public void UpdateInput(bool?[] input)
		{
			float[] inputSize = new float[input.Length];
			for (int i = 0; i < inputSize.Length; i++)
			{
				inputSize[i] = 0;
			}

			_layers[0] = new Layer(inputSize, inputSize);

			for (int i = 0; i < input.Length; i++)
			{
				_layers[0].Connections[i].Value = input[i];
				_layers[1].Connections[i].Value = input[i];
			}
		}

#if DEBUG
		#region DebugOutput
		public void PrintFull()
		{
			Console.Write("Input:\n{0,4:0.00}", _layers[0].Connections[0].Value);
			for (int i = 1; i < _layers[0].Connections.Count; i++)
			{
				Console.Write("{0,6:0.00}", _layers[0].Connections[i].Value);

			}
			Console.WriteLine("\n");
			for (int i = 1; i < _layers.Length; i++)
			{
				PrintLayerWeights(i);
				PrintLayerthreshold(i);
				PrintLayerOutput(i);
				Console.WriteLine();
			}
		}

		public void Print()
		{
			PrintLayerOutput(_layers.Length - 1);
		}

		public void PrintLayerOutput(int layer)
		{
			Console.Write("L{0} output:\n{1,4:0.00}", layer, _layers[layer].Connections[0].Value);
			for (int i = 1; i < _layers[layer].Perceptrons.Count; i++)
			{
				Console.Write("{0,6:0.00}", _layers[layer].Connections[i].Value);
			}
			Console.WriteLine();
		}

		public void PrintLayerWeights(int layer)
		{
			Console.Write("L{0} Weight:\n{1,4:0.00}", layer, _layers[layer].Connections[0].Weight);
			for (int i = 1; i < _layers[layer].Perceptrons.Count; i++)
			{
				Console.Write("{0,6:0.00}", _layers[layer].Connections[i].Weight);
			}
			Console.WriteLine();
		}

		public void PrintLayerthreshold(int layer)
		{
			Console.Write("L{0} threshold:\n{1,4:0.00}", layer, _layers[layer].Perceptrons[0].Threshold);
			for (int i = 1; i < _layers[layer].Perceptrons.Count; i++)
			{
				Console.Write("{0,6:0.00}", _layers[layer].Connections[i].Weight);
			}
			Console.WriteLine();
		}
		#endregion
#endif
	}
}