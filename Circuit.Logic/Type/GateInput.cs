using Designer.Circuit.Logic.Perceptrons;

namespace Designer.Circuit.Logic.Gates
{
	public class GateInput : Gate
	{
		public GateInput(bool? value)
		{
			Type = Types.Input;
			_inputValue = value;

			float[][] weight =
			{
				new float[] {1}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			//Output.Add(new Connection(this, null));

			_network = new Network(weight, threshold);
		} 
	}
}