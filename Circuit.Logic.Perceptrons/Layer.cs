// Designer.Circuit.Logic.Perceptrons: Layer.cs

using System.Collections.Generic;

namespace Designer.Circuit.Logic.Perceptrons
{
	internal class Layer
	{
		public List<Connection> Connections;

#if DEBUG
		public List<Perceptron> Perceptrons;
#else
		private List<Perceptron> Perceptrons;
#endif


		public Layer(float[] weight, float[] threshold)
		{
			Connections = new List<Connection>();
			Perceptrons = new List<Perceptron>();

			for (int i = 0; i < weight.Length; i++)
			{
				Connections.Add(new Connection(weight[i]));
			}
			for (int i = 0; i < threshold.Length; i++)
			{
				Perceptrons.Add(new Perceptron(threshold[i]));
			}
		}

		public List<bool> Update(List<Connection> input)
		{
			List<bool> result = new List<bool>();

			for (int i = 0; i < Perceptrons.Count; i++)
			{
				//Connections[i].Value = Perceptrons[i].Update(input);
				if (Perceptrons[i] != null)
				{
					result.Add(Perceptrons[i].Update(input)); 
				}
			}

			return result;
		}
	}
}