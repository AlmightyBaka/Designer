// Designer.CDNF.Logic: Minterm.cs
// 

using System;

namespace CDNF.Logic
{
	public class Minterm
	{
		public int NumOfOnes { get; }
		public int Position { get; }
		public int Variables { get; }
		public byte Binary { get; }
		public string BinaryStr { get; }
		public sbyte[] SByteArray { get; }

		public Minterm(byte binary, int position, int variables)
		{
			Position = position;
			Variables = variables;

			Binary = binary;
			BinaryStr = Convert.ToString(binary, 2).PadLeft(variables, '0');

			SByteArray = new sbyte[variables];
			NumOfOnes = 0;

			for (int i = 0; i < BinaryStr.Length; i++)
			{
				SByteArray[i] = SByte.Parse(BinaryStr[i].ToString());
				if (SByteArray[i] == 1)
				{
					NumOfOnes++;
				}
			}
		}
	}
}