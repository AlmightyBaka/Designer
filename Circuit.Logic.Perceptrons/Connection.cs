// Designer.Circuit.Logic.Perceptrons: Connection.cs
namespace Designer.Circuit.Logic.Perceptrons
{
	internal class Connection
	{
		public bool? Value;
		public float Weight;

		public Connection(float weight)
		{
			Weight = weight;
		}
	}
}