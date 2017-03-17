using Designer.Circuit.Logic.Perceptrons;

namespace Designer.Circuit.Logic.Gates
{
	public class GateAnd : Gate
	{
		public GateAnd()
		{
			Type = Types.And;

			float[][] weight =
			{
				new float[] {0.6f, 0.6f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			_network = new Network(weight, threshold);
		} 
	}
}