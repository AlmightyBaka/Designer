using System;
using System.Text;
using CDNF.Logic;

namespace CDNF.ConsoleOutput
{
	class Program
	{
		static void Main()
		{
			Console.OutputEncoding = Encoding.Unicode;

			for (int j = 0; j < 2; j++)
			{
				CDNFSolver solver = new CDNFSolver(true, 3);

				for (int i = 0; i < solver.Function.TruthTableStr.Length; i++)
				{
					Console.WriteLine($"{solver.Function.TruthTableStr[i]} | {solver.Function.Output[i]}");
				}

				Console.WriteLine("\nMinimized:");
				Console.WriteLine(solver.Calculate());

				Console.WriteLine("CDNF:");
				Console.WriteLine(solver.CDNFString + "\n");
			}

			Console.ReadKey();
		}
	}
}
