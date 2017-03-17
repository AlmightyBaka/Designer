using System;

namespace CDNF.Logic
{
	static class BoolFunctionGen
	{
		private readonly static int minTrueOutputs = 3;
		private readonly static float TruthChance = 0.7f;

		static public bool[] GetRandomFunction(int variables)
		{
			int varsSqr = (int) Math.Pow(2, variables);
			bool[] result = new bool[varsSqr];
			int truthCount = 0;
			Random random = new Random();

			for (int i = 0; i < varsSqr; i++)
			{
				if (random.NextDouble() > TruthChance)
				{
					result[i] = true;
					truthCount++;
				}
			}

			if (varsSqr > minTrueOutputs && truthCount < minTrueOutputs)
			{
				truthCount = 0;
				while (truthCount < 2)
				{
					int index = random.Next(0, varsSqr);
					if (!result[index])
					{
						result[index] = true; 
					}

					truthCount++;
				}
			}

			return result;
		}

		#region GetTruthTable methods
		static public bool[][] GetTruthTableBool(int variables)
		{
			int varsSqr = (int)Math.Pow(2, variables);
			byte[] resultByte = new byte[varsSqr];
			string[] resultStr = new string[varsSqr];
			bool[][] resultBool = new bool[varsSqr][];


			for (byte x = 0; x < varsSqr; x++)
			{
				resultByte[x] = x;
				resultStr[x] = Convert.ToString(resultByte[x], 2).PadLeft(variables, '0');

				resultBool[x] = new bool[variables];
				for (int y = 0; y < variables; y++)
				{
					resultBool[x][y] = resultStr[x][y].Equals('1');
				}
			}

			return resultBool;
		}

		static public string[] GetTruthTableStr(int variables)
		{
			int varsSqr = (int)Math.Pow(2, variables);
			byte[] resultByte = new byte[varsSqr];
			string[] resultStr = new string[varsSqr];

			for (byte y = 0; y < varsSqr; y++)
			{
				resultByte[y] = y;
				resultStr[y] = Convert.ToString(resultByte[y], 2).PadLeft(variables, '0');
			}

			return resultStr;
		}

		static public byte[] GetTruthTableByte(int variables)
		{
			int varsSqr = (int)Math.Pow(2, variables);
			byte[] resultByte = new byte[varsSqr];


			for (byte y = 0; y < varsSqr; y++)
			{
				resultByte[y] = y;
			}

			return resultByte;
		} 
		#endregion
	}
}
