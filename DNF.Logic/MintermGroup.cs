// Designer.CDNF.Logic: MintermGroup.cs
// 

using System.Collections.Generic;

namespace CDNF.Logic
{
	public struct MintermGroup
	{
		public List<Minterm> Minterms { get; }

		public MintermGroup(List<Minterm> minterms)
		{
			Minterms = minterms;
		}
	}
}