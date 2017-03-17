using Designer.Circuit.Logic.Perceptrons;

namespace Designer.Circuit.Logic.Gates
{
	public class GateNot : Gate
	{
		public GateNot()
		{
			Type = Types.Not;

			float[][] weight =
			{
				new float[] {-1}
			};
			float[][] threshold =
			{
				new float[] {-0.5f}
			};

			_network = new Network(weight, threshold);
		}
	}
}