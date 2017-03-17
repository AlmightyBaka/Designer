// Designer: TruthTable.cs
namespace CDNF.Logic
{
	public struct TruthTable
	{
		public int VariablesCount { get; }
		public bool[] Output { get; }
		public string[] TruthTableStr { get; }
		public bool[][] TruthTableBool { get; }
		public byte[] TruthTableByte { get; }

		public TruthTable(int variables)
		{
			VariablesCount = variables;
			Output = BoolFunctionGen.GetRandomFunction(VariablesCount);
			TruthTableStr = BoolFunctionGen.GetTruthTableStr(VariablesCount);
			TruthTableBool = BoolFunctionGen.GetTruthTableBool(VariablesCount);
			TruthTableByte = BoolFunctionGen.GetTruthTableByte(VariablesCount);
		}

	}
}