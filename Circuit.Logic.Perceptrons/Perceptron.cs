// Designer.Circuit.Logic.Perceptrons: Perceptron.cs

using System;
using System.Collections.Generic;

namespace Designer.Circuit.Logic.Perceptrons
{
	internal class Perceptron
	{
		public float Threshold;
	
		public Perceptron(float threshold)
		{
			Threshold = threshold;
		}
	
		public bool Update(List<Connection> input)
		{
			#region input check
			for (int i = 0; i < input.Count; i++)
			{
				if (input[i].Value == null)
				{
					throw new Exception("Input value is null");
				}
			} 
			#endregion

			float value = 0;
		
			for (int i = 0; i < input.Count; i++)
			{
				value += Convert.ToInt16(input[i].Value) * input[i].Weight;
			}
		
			if (value >= Threshold)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}