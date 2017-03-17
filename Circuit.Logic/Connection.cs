// Perceptrons: Connection.cs

using Designer.Circuit.Logic.Gates;

namespace Designer.Circuit.Logic
{
	public class Connection
	{
		public Gate Input;
		public Gate Output;

		public Connection(Gate input, Gate output)
		{
			Input = input;
			Output = output;
		}

		public override string ToString()
		{
			return base.ToString() + $": in:{Input.Type}; out:{Output.Type}";
		}
	}
}