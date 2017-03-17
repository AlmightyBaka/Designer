using Designer.Circuit.Logic.Perceptrons;

namespace Designer.Circuit.Logic.Gates
{
	public class GateOr : Gate
	{
		public GateOr()
		{
			Type = Types.Or;

			float[][] weight =
			{
				new float[] {1.1f, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			_network = new Network(weight, threshold);
		}
	}
}