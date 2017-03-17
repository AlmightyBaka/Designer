using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CDNF.Logic
{

	// canonical disjunctive normal form (CDNF) solver using Quine-McKluskey algorithm
	public class CDNFSolver
	{
		#region Properties

		public TruthTable Function { get; }
		public string CDNFString { get; }

		#endregion

		#region Private values

		private Random random = new Random();

		private readonly int _minVariables = 2;
		private readonly int _maxVariables = 4;
		private readonly bool _isUnicode;

		#endregion

		#region Constructors

		public CDNFSolver(bool isUnicode)
		{
			_isUnicode = isUnicode;
			Function = new TruthTable(random.Next(_minVariables, _maxVariables + 1));
			CDNFString = getCDNF(Function);
		}

		public CDNFSolver(bool isUnicode, int minVariables, int maxVariables)
		{
			_isUnicode = isUnicode;
			Function = new TruthTable(random.Next(minVariables, maxVariables + 1));
			CDNFString = getCDNF(Function);
		}

		public CDNFSolver(bool isUnicode, int variables)
		{
			_isUnicode = isUnicode;
			Function = new TruthTable(variables);
			CDNFString = getCDNF(Function);
		}

		#endregion

		#region Public methods

		public string Calculate()
		{
			string result = "";

			//Minimize();

			return result;
		}

		#endregion

		#region Private methods

		private string getCDNF(TruthTable function)
		{
			string result = "";

			int count = 0;

			int varsSqr = (int)Math.Pow(2, function.VariablesCount);
			for (int i = 0; i < varsSqr; i++)
			{
				if (function.Output[i])
				{
					result += $"({getCDNFPart(function, i)})";
					if (_isUnicode)
					{
						result += "∧";
					}
					else
					{
						result += "*";
					}
				}
			}
			result = result.Remove(result.Length - 1);

			return result;
		}

		private string getCDNFPart(TruthTable function, int row)
		{
			string result = "";

			Encoding encoding = Encoding.Unicode;

			for (int i = 0; i < function.VariablesCount; i++)
			{
				if (i == 0)
				{
					if (function.TruthTableBool[row][i])
					{
						result += "A";
					}
					else
					{
						if (_isUnicode)
						{
							//result += Encoding.Unicode.GetString(Encoding.Unicode.GetBytes("A\u203e"));
							result += "A\u0305";
						}
						else
						{
							result += "NOT A";
						}

					}
				}
				else if (i == 1)
				{
					if (function.TruthTableBool[row][i])
					{
						result += "B";
					}
					else
					{
						if (_isUnicode)
						{
							result += "B\u0305";
						}
						else
						{
							result += "NOT B";
						}
					}
				}
				else if (i == 2)
				{
					if (function.TruthTableBool[row][i])
					{
						result += "C";
					}
					else
					{
						if (_isUnicode)
						{
							result += "C\u0305";
						}
						else
						{
							result += "NOT C";
						}
					}
				}
				else if (i == 3)
				{
					if (function.TruthTableBool[row][i])
					{
						result += "D";
					}
					else
					{
						if (_isUnicode)
						{
							result += "D\u0305";

						}
						else
						{
							result += "NOT D";
						}
					}
				}

				if (i < function.VariablesCount - 1)
				{
					if (_isUnicode)
					{
						result += "∨";
					}
					else
					{
						result += "+"; 
					}
				}
			}

			return result;
		}

		private void Minimize()
		{
			List<Minterm> minterms = GetMinterms(Function.Output);
			List<MintermGroup> mintermsGrouped = GroupMinterms(minterms, Function.VariablesCount);
			List<Implicant> mintermsCombined = CombineMinterms(mintermsGrouped);
		}

		private List<Minterm> GetMinterms(bool[] terms)
		{
			List<Minterm> result = new List<Minterm>();

			for (int i = 0; i < terms.Length; i++)
			{
				if (terms[i])
				{
					result.Add(new Minterm((byte)i, i, Function.VariablesCount));
				}
			}

			return result;
		}

		private List<MintermGroup> GroupMinterms(List<Minterm> minterms, int variables)
		{
			List<MintermGroup> result = new List<MintermGroup>(variables + 1);

			for (int i = 0; i < variables + 1; i++)
			{
				result.Add(new MintermGroup(new List<Minterm>()));
			}

			foreach (Minterm minterm in minterms)
			{
				result[minterm.NumOfOnes].Minterms.Add(minterm);
			}

			return result;
		}

		private List<Implicant> CombineMinterms(List<MintermGroup> mintermsGrouped)
		{
			List<Implicant> result = new List<Implicant>();
			List<MintermGroup> groups = mintermsGrouped;
			bool hitOnce = true;

			while (hitOnce)
			{
				hitOnce = false;
				List<MintermGroup> groupsPass = new List<MintermGroup>();

				for (int i = 0; i < groups.Count; i++)
				{
					for (int j = 0; j < groups.Count; j++)
					{
						MintermGroup? group = CompareMinterms(groups[i], groups[j]);

						if (group != null)
						{
							hitOnce = true;
							groupsPass.Add((MintermGroup)group);
						}
					}
				}

				groups = groupsPass;
			}

			return result;
		}

		private MintermGroup? CompareMinterms(MintermGroup mintermsGroupLeft, MintermGroup mintermsGroupRight)
		{
			MintermGroup result = new MintermGroup(new List<Minterm>());
			bool hitOnce = false;

			foreach (Minterm mintermLeft in mintermsGroupLeft.Minterms)
			{
				foreach (Minterm mintermRight in mintermsGroupRight.Minterms)
				{
					if (mintermLeft != mintermRight)
					{
						Minterm diff = IsDiffByOneBit(mintermLeft, mintermRight);

						if (diff != null)
						{
							hitOnce = true;
							result.Minterms.Add(diff);
						}
					}
				}
			}

			if (!hitOnce)
			{
				return null;
			}
			return result;
		}

		private Minterm IsDiffByOneBit(Minterm mintermLeft, Minterm mintermRight)
		{
			Minterm result = null;
			bool isDiff = false;
			int index = -1;

			byte diff = (byte)(mintermLeft.Binary ^ mintermRight.Binary);
			string diffStr = Convert.ToString(diff, 2);

			for (int i = 0; i < diffStr.Length; i++)
			{
				if (diffStr[i] == '1')
				{
					if (isDiff)
					{
						return null;
					}
					isDiff = true;
					index = i;
				}
			}

			if (isDiff)
			{
				result = new Minterm(mintermLeft.Binary, mintermLeft.Position, mintermLeft.Variables);
				result.SByteArray[index] = -1;
			}

			return result;
		}

		#endregion

		#region wat
		//private string result = "";
		//private int NoOfVariables;
		//private int NoOfStage = 1;
		//private int MaxStage = 1;
		//private string[,,] cubes;
		//private string[,,] AllminTerms;
		//private string[,] minTermBinaries = new string[11, 30];
		//private int MaximumOnes = 0; //Number of maximum 1s for every Minterm
		//private string[,,] minTermIsImplicant = new string[11, 30, 30];


		//private string Pirated(string[] minTerms)
		//{
		//	string temp = "";
		//	string extraZeros = "";
		//	NoOfVariables = Variables;
		//	//Getting Minterms
		//	for (int i = 0; i < 11; i++)
		//	{
		//		for (int j = 0; j < 30; j++)
		//		{
		//			for (int k = 0; k < 30; k++)
		//			{
		//				minTermIsImplicant[i, j, k] = " *";
		//			}

		//		}
		//	}
		//	//Categorizing Minterms
		//	for (int i = 0; i < minTerms.Length; i++)
		//	{
		//		if (ConvertToBinary(Convert.ToInt32(minTerms[i])).Length < NoOfVariables)
		//		{
		//			extraZeros = "";
		//			for (int f = 1; f <= NoOfVariables - ConvertToBinary(Convert.ToInt32(minTerms[i])).Length; f++)
		//			{
		//				extraZeros += "0";
		//			}
		//			minTermBinaries[1, i] = extraZeros + ConvertToBinary(Convert.ToInt32(minTerms[i]));
		//		}
		//		else
		//			minTermBinaries[1, i] = ConvertToBinary(Convert.ToInt32(minTerms[i]));
		//		if (minTermBinaries[1, i].Split('1').Length - 1 > MaximumOnes)
		//		{
		//			MaximumOnes = minTermBinaries[1, i].Split('1').Length - 1;
		//		}

		//	}

		//	cubes = new string[10, MaximumOnes + 1, 20];
		//	AllminTerms = new string[10, MaximumOnes + 1, 20];
		//	int index;
		//	for (int NoOfOnes = 0; NoOfOnes <= MaximumOnes; NoOfOnes++)
		//	{
		//		index = 0;
		//		for (int minTermIndex = 0; minTermIndex < minTerms.Length; minTermIndex++)
		//		{
		//			if (minTermBinaries[1, minTermIndex].Split('1').Length - 1 == NoOfOnes)
		//			{
		//				cubes[1, NoOfOnes, index] = minTermBinaries[1, minTermIndex];
		//				AllminTerms[1, NoOfOnes, index] = ConvertToDecimal(minTermBinaries[1, minTermIndex]);
		//				index++;
		//			}

		//		}

		//	}

		//	int l = 0;
		//	for (int Stage = 1; minTermBinaries[Stage, 0] != null; Stage++)
		//	{
		//		int indexOfMinterm = 0;
		//		//
		//		for (int NoOfOnes = 1; NoOfOnes < MaximumOnes; NoOfOnes++)
		//		{
		//			l = 0;
		//			for (int j = 0; cubes[Stage, NoOfOnes, j] != null; j++)
		//			{
		//				for (int x = 0; cubes[Stage, NoOfOnes + 1, x] != null; x++)
		//				{
		//					if (diffOne(cubes[Stage, NoOfOnes, j], cubes[Stage, NoOfOnes + 1, x]) != null)
		//					{
		//						minTermIsImplicant[Stage, NoOfOnes, j] = "";
		//						minTermIsImplicant[Stage, NoOfOnes + 1, x] = "";
		//					}
		//					if (diffOne(cubes[Stage, NoOfOnes, j], cubes[Stage, NoOfOnes + 1, x]) != null &&
		//						temp != (string)diffOne(cubes[Stage, NoOfOnes, j], cubes[Stage, NoOfOnes + 1, x]))
		//					{
		//						minTermBinaries[Stage + 1, indexOfMinterm] =
		//							(string)diffOne(cubes[Stage, NoOfOnes, j], cubes[Stage, NoOfOnes + 1, x]);
		//						AllminTerms[Stage + 1, NoOfOnes, l] = AllminTerms[Stage, NoOfOnes, j] + "," +
		//															AllminTerms[Stage, NoOfOnes + 1, x];
		//						temp = (string)diffOne(cubes[Stage, NoOfOnes, j], cubes[Stage, NoOfOnes + 1, x]);
		//						indexOfMinterm++;
		//						l++;
		//					}
		//					//else
		//					//{
		//					//    minTermIsImplicant[Stage, NoOfOnes, j] = " *";
		//					//    minTermIsImplicant[Stage, NoOfOnes + 1, x] = " *";
		//					//}
		//				}
		//			}
		//		}

		//		//Removing extra repeated minterms


		//		//


		//		for (int NoOfOnes = 0; NoOfOnes <= MaximumOnes; NoOfOnes++)
		//		{
		//			index = 0;
		//			for (int minTermIndex = 0; minTermBinaries[Stage + 1, minTermIndex] != null; minTermIndex++)
		//			{
		//				if (minTermBinaries[Stage + 1, minTermIndex].Split('1').Length - 1 == NoOfOnes)
		//				{
		//					cubes[Stage + 1, NoOfOnes, index] = minTermBinaries[Stage + 1, minTermIndex];
		//					index++;
		//				}

		//			}

		//		}
		//		MaxStage = Stage;

		//	}
		//	//Showing the Results
		//	result += "Stage 1\n";
		//	NoOfStage = 1;
		//	for (int NoOfOnes = 1; NoOfOnes <= MaximumOnes; NoOfOnes++)
		//	{
		//		for (int p = 0; cubes[NoOfStage, NoOfOnes, p] != null; p++)
		//		{
		//			result += "m(" + AllminTerms[NoOfStage, NoOfOnes, p] + ")=" + cubes[NoOfStage, NoOfOnes, p] +
		//							minTermIsImplicant[NoOfStage, NoOfOnes, p] + "\r\n";
		//			// result +="m("+ConvertToDecimal(cubes[NoOfStage, NoOfOnes, p])+")=" + cubes[NoOfStage, NoOfOnes, p] + minTermIsImplicant[NoOfStage, NoOfOnes, p] + "\r\n";
		//		}
		//		result += "------------\r\n";
		//	}
		//	string resultTemp = "";
		//	string expression = "";
		//	int StageTemp = 1;
		//	for (int NoOfOnes = 1; NoOfOnes <= MaximumOnes; NoOfOnes++)
		//	{
		//		for (int p = 0; cubes[StageTemp, NoOfOnes, p] != null && minTermIsImplicant[StageTemp, NoOfOnes, p] == " *"; p++)
		//		{
		//			for (int NoOfVars = 0; NoOfVars < NoOfVariables; NoOfVars++)
		//			{
		//				if (cubes[StageTemp, NoOfOnes, p][NoOfVars] == '1')
		//				{
		//					expression = Convert.ToString(Convert.ToChar(65 + NoOfVars));
		//				}
		//				else if (cubes[StageTemp, NoOfOnes, p][NoOfVars] == '0')
		//				{
		//					expression = Convert.ToString(Convert.ToChar(65 + NoOfVars)) + "\'";
		//				}
		//				else
		//				{
		//					expression = "";
		//				}
		//				resultTemp += expression;
		//			}

		//			resultTemp += " + ";

		//		}

		//	}
		//	try
		//	{
		//		resultTemp = resultTemp.Remove(resultTemp.Length - 2);

		//	}
		//	catch (Exception)
		//	{

		//	}
		//	result += "\r\n" + resultTemp;

		//	//The Final Optimized Result

		//	expression = "";
		//	for (int i = 0; i < NoOfVariables; i++)
		//	{
		//		result += Convert.ToString(Convert.ToChar(65 + i)) + ",";
		//	}
		//	result = result.Remove(result.Length - 1);
		//	result += ") = ";
		//	for (int Stage = 1; minTermBinaries[Stage, 0] != null; Stage++)
		//	{
		//		for (int NoOfOnes = 1; NoOfOnes <= MaximumOnes; NoOfOnes++)
		//		{
		//			for (int p = 0; cubes[Stage, NoOfOnes, p] != null && minTermIsImplicant[Stage, NoOfOnes, p] == " *"; p++)
		//			{
		//				for (int NoOfVars = 0; NoOfVars < NoOfVariables; NoOfVars++)
		//				{
		//					if (cubes[Stage, NoOfOnes, p][NoOfVars] == '1')
		//					{
		//						expression = Convert.ToString(Convert.ToChar(65 + NoOfVars));
		//					}
		//					else if (cubes[Stage, NoOfOnes, p][NoOfVars] == '0')
		//					{
		//						expression = Convert.ToString(Convert.ToChar(65 + NoOfVars)) + "\'";
		//					}
		//					else
		//					{
		//						expression = "";
		//					}
		//					result += expression;
		//				}

		//				result += " + ";

		//			}

		//		}
		//	}
		//	result = result.Remove(result.Length - 2);
		//	return result;
		//}

		//private string ConvertToBinary(int x)
		//{
		//	string s = "";
		//	char[] res, cl;
		//	while (x > 0)
		//	{
		//		s = s + (x % 2).ToString();
		//		x = x / 2;
		//	}
		//	res = s.ToCharArray();
		//	cl = (char[])res.Clone();
		//	s = "";
		//	for (int i = 0; i < res.Length; i++)
		//	{
		//		res[i] = cl[res.Length - i - 1];
		//		s = s + res[i].ToString();
		//	}
		//	return s;
		//}

		//private string ConvertToDecimal(string x)
		//{
		//	int result = 0, m = 1, countDash = 0;

		//	for (int i = 0; i < x.Length; i++)
		//	{
		//		if (x[i] == '-')
		//		{
		//			countDash++;
		//		}
		//	}


		//	for (int i = 0; i < x.Length; i++)
		//	{
		//		result += m * Convert.ToInt32(Convert.ToString(x[x.Length - 1 - i]));
		//		m = m * 2;
		//	}
		//	return result.ToString();
		//}

		//private object diffOne(string a, string b)
		//{
		//	int t = 0;
		//	StringBuilder r = new StringBuilder();
		//	for (int i = 0; i < a.Length; i++)
		//	{
		//		if (a.Length < b.Length)
		//		{
		//			a = "0" + a;
		//		}
		//		if (a[i] != b[i])
		//		{
		//			t++;
		//			r.Append('-');
		//		}
		//		else
		//		{
		//			r.Append(a[i]);
		//		}
		//	}
		//	if (t == 1)
		//	{
		//		return r.ToString();
		//	}
		//	else
		//	{
		//		return null;
		//	}

		//} 
		#endregion
	}
}
