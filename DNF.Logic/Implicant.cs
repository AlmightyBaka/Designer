// Designer.CDNF.Logic: Implicant.cs
// 

using System.Collections.Generic;

namespace CDNF.Logic
{
	public struct Implicant
	{
		public List<MintermGroup> MintermGroups { get; }
		public bool isFinished { get; set; }

		public Implicant(List<MintermGroup> mintermGroups)
		{
			MintermGroups = mintermGroups;
			isFinished = false;
		}
	}
}