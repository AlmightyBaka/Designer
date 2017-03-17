using Designer.Circuit.Logic.Perceptrons;

namespace Designer.Circuit.Logic.Gates
{
	public class GateXor : Gate
	{
		public GateXor()
		{
			Type = Types.Xor;

			float[][] weight =
			{
				new float[] {1, 1, 1, 1},
				new float[] {0.6f, 1.1f, 0.6f, 1.1f},
				new float[] {-2, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {2, 2, 4, 4},
				new float[] {1.8f, 1},
				new float[] {1}
			};

			_network = new Network(weight, threshold);
		}

		protected override bool?[] GetInput()
		{
			bool?[] input = new bool?[Input.Count * 2];

			for (int i = 0; i < Input.Count; i++)
			{
				input[i * 2] = Input[i].Input.Value;
			}
			for (int i = 0; i < Input.Count; i++)
			{
				input[i * 2 + 1] = Input[i].Input.Value;
			}

			return input;
		}
	}
}