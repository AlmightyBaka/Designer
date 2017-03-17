using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Designer.Circuit.Logic.Gates;

namespace Circuit.Model
{
	static class GateGenerator
	{
		private static readonly Random random = new Random();
		private static Designer.Circuit.Logic.Circuit circuit;
		private static int inputsAmount;
		private static int minLevels;
		private static int maxLevels;
		private static int maxGatesInLevel;
		private static int minOutputGates;
		private static int maxOutputGates;
		private static int maxGatesTotal;
		private static List<int> gateLayers;
		private static int levelsGenerated;

		#region Public methods
		public static Designer.Circuit.Logic.Circuit GenerateV4(
	Designer.Circuit.Logic.Circuit circuit, int minLevels, int maxLevels, int maxGatesInLevel, int maxGatesTotal,
	int minOutputGates, int maxOutputGates,
	ref List<int> gateLayers)
		{
			GateGenerator.circuit = circuit;
			GateGenerator.minLevels = minLevels;
			GateGenerator.maxLevels = maxLevels;
			GateGenerator.maxGatesInLevel = maxGatesInLevel;
			GateGenerator.maxGatesTotal = maxGatesTotal;
			GateGenerator.minOutputGates = minOutputGates;
			GateGenerator.maxOutputGates = maxOutputGates;
			GateGenerator.gateLayers = new List<int>();

			GenerateGatesV4();

			gateLayers = GateGenerator.gateLayers;
			return GateGenerator.circuit;
		}

		public static Designer.Circuit.Logic.Circuit GenerateV3(
			Designer.Circuit.Logic.Circuit circuit, int minLevels, int maxLevels, int maxGatesInLevel,
			int minOutputGates, int maxOutputGates,
			ref List<int> gateLayers)
		{
			GateGenerator.circuit = circuit;
			GateGenerator.minLevels = minLevels;
			GateGenerator.maxLevels = maxLevels;
			GateGenerator.minOutputGates = minOutputGates;
			GateGenerator.maxOutputGates = maxOutputGates;
			GateGenerator.maxGatesInLevel = maxGatesInLevel;
			GateGenerator.gateLayers = new List<int>();

			GenerateGatesV3();

			gateLayers = GateGenerator.gateLayers;
			return GateGenerator.circuit;
		}

		public static Designer.Circuit.Logic.Circuit GenerateV2(
			Designer.Circuit.Logic.Circuit circuit, int minLevels, int maxLevels, int maxGatesInLevel,
			ref List<int> gateLayers)
		{
			GateGenerator.circuit = circuit;
			GateGenerator.minLevels = minLevels;
			GateGenerator.maxLevels = maxLevels;
			GateGenerator.maxGatesInLevel = maxGatesInLevel;
			GateGenerator.gateLayers = new List<int>();

			GenerateGatesV2();

			gateLayers = GateGenerator.gateLayers;
			return GateGenerator.circuit;
		}

		public static Designer.Circuit.Logic.Circuit GenerateV1(
			Designer.Circuit.Logic.Circuit circuit,
			int inputsAmount, int minLevels, int maxLevels,
			ref List<int> gateLayers)
		{
			GateGenerator.circuit = circuit;
			GateGenerator.inputsAmount = inputsAmount;
			GateGenerator.minLevels = minLevels;
			GateGenerator.maxLevels = maxLevels;
			GateGenerator.gateLayers = new List<int>();

			GateGenerator.circuit = GenerateGatesV1();

			gateLayers = GateGenerator.gateLayers;
			return GateGenerator.circuit;
		}
		#endregion

		#region Version 4
		private static void GenerateGatesV4()
		{
			int levels = random.Next(minLevels, maxLevels + 1);
			levelsGenerated = levels;
			int prevLayerStart = 0;
			int prevLayerEnd = circuit.GateList.Count;
			gateLayers.Add(prevLayerEnd);
			int gatesGeneratedCount = GetGatesAmountV4(prevLayerEnd, true);

			for (int i = 0; i < levels; i++)
			{
				GenerateGateLevelV4(gatesGeneratedCount, prevLayerStart, prevLayerEnd);

				gateLayers.Add(gatesGeneratedCount);
				prevLayerStart = prevLayerEnd;
				prevLayerEnd += gatesGeneratedCount;
				if (i == levels - 2)
				{
					gatesGeneratedCount = GetGatesAmountV4(gatesGeneratedCount, false);
				}
				else
				{
					gatesGeneratedCount = GetGatesAmountV4(gatesGeneratedCount, false);
				}
			}
		}

		private static int GetGatesAmountV4(int baseValue, bool isFirst)
		{
			float rand = (float)random.NextDouble();
			int gatesAvg = maxGatesTotal / levelsGenerated;

			if (isFirst)
			{
				return gatesAvg;
			}

			if (rand < 0.3 && baseValue > 1 && (gatesAvg + 1 >= baseValue && gatesAvg - 1 <= baseValue))
			{
				return baseValue - 1;
			}
			else if (rand < 0.6 && baseValue + 1 <= maxGatesInLevel && (gatesAvg + 1 >= baseValue && gatesAvg - 1 <= baseValue))
			{
				return baseValue + 1;
			}
			return baseValue;
		}

		private static void GenerateGateLevelV4(int amount, int prevLayerStart, int prevLayerEnd)
		{
			AddRandomGatesV2(amount);
			BindGatesV4(prevLayerStart, prevLayerEnd);
		}

		private static void BindGatesV4(int prevLayerStart, int prevLayerEnd)
		{
			prevLayerEnd -= 1;
			int prevLayerCount = prevLayerEnd - prevLayerStart + 1; // TODO
			int thisLayerStart = prevLayerEnd + 1;
			int thisLayerEnd = circuit.GateList.Count;
			bool[] isBinded = new bool[prevLayerCount];

			bool allBinded = false;
			int index = prevLayerEnd + 1;
			while (!allBinded)
			{
				int indexBind = random.Next(prevLayerStart, prevLayerEnd + 1);

				if (!isBinded[indexBind - prevLayerStart])
				{
					if (circuit.GateList[index].Type == Gate.Types.Not && circuit.GateList[index].Input.Count == 0)
					{
						circuit.Bind(circuit.GateList[indexBind], circuit.GateList[index]);
						isBinded[indexBind - prevLayerStart] = true;

						if (index != circuit.GateList.Count - 1)
						{
							index++;
						}
						else
						{
							index = random.Next(prevLayerEnd + 1, circuit.GateList.Count);
						}
					}
					else if (circuit.GateList[index].Type != Gate.Types.Not && circuit.GateList[index].Input.Count <= 1)
					{
						circuit.Bind(circuit.GateList[indexBind], circuit.GateList[index]);
						isBinded[indexBind - prevLayerStart] = true;

						if (index != circuit.GateList.Count - 1)
						{
							index++;
						}
						else
						{
							index = random.Next(prevLayerEnd + 1, circuit.GateList.Count);
						}
					}
					else
					{
						isBinded[indexBind - prevLayerStart] = true;
					}
				}

				foreach (bool bind in isBinded)
				{
					if (!bind)
					{
						allBinded = false;
						break;
					}
					else
					{
						allBinded = true;
					}
				}
			}

			bool allInputsBinded = false;
			while (!allInputsBinded)
			{
				for (int i = thisLayerStart; i < thisLayerEnd; i++)
				{
					int rand = random.Next(prevLayerStart, prevLayerEnd + 1);

					if (circuit.GateList[i].Type == Gate.Types.Not && circuit.GateList[i].Input.Count == 0)
					{
						circuit.Bind(circuit.GateList[rand], circuit.GateList[i]);
					}
					else if (circuit.GateList[i].Type != Gate.Types.Not && circuit.GateList[i].Input.Count <= 1)
					{
						circuit.Bind(circuit.GateList[rand], circuit.GateList[i]);
					}
				}

				for (int j = thisLayerStart; j < thisLayerEnd; j++)
				{
					if (circuit.GateList[j].Type == Gate.Types.Not && circuit.GateList[j].Input.Count == 0)
					{
						allInputsBinded = false;
						break;
					}
					if (circuit.GateList[j].Type != Gate.Types.Not && circuit.GateList[j].Input.Count <= 1)
					{
						allInputsBinded = false;
						break;
					}
					else
					{
						allInputsBinded = true;
					}
				}
			}
		}

		#endregion

		#region Version 3
		private static void GenerateGatesV3()
		{
			int levels = random.Next(minLevels, maxLevels + 1);
			int prevLayerStart = 0;
			int prevLayerEnd = circuit.GateList.Count;
			gateLayers.Add(prevLayerEnd);
			int gatesGeneratedCount = GetGatesAmountV3(prevLayerEnd, false);

			for (int i = 0; i < levels; i++)
			{
				GenerateGateLevelV3(gatesGeneratedCount, prevLayerStart, prevLayerEnd);

				gateLayers.Add(gatesGeneratedCount);
				prevLayerStart = prevLayerEnd;
				prevLayerEnd += gatesGeneratedCount;
				if (i == levels - 2)
				{
					gatesGeneratedCount = GetGatesAmountV3(gatesGeneratedCount, true);
				}
				else
				{
					gatesGeneratedCount = GetGatesAmountV3(gatesGeneratedCount, false);
				}
			}
		}

		private static int GetGatesAmountV3(int baseValue, bool isLast)
		{
			float rand = (float)random.NextDouble();

			if (isLast)
			{
				if (baseValue + 1 < minOutputGates) // TODO
				{
					return random.Next(minOutputGates, maxOutputGates + 1);
				}
				else
				{
					return baseValue;
				}
			}

			if (baseValue > 2 && rand < 0.3)
			{
				return baseValue - 2;
			}
			else if (baseValue > 1 && rand < 0.5)
			{
				return baseValue - 1;
			}
			else if (baseValue > 1 && rand < 0.7)
			{
				return baseValue;
			}
			else if (rand < 0.85)
			{
				return baseValue + 1;
			}
			else if (baseValue < 3)
			{
				return baseValue + 2;
			}
			return baseValue;
		}

		private static void GenerateGateLevelV3(int amount, int prevLayerStart, int prevLayerEnd)
		{
			AddRandomGatesV2(amount);
			BindGatesV3(prevLayerStart, prevLayerEnd);
		}

		private static void BindGatesV3(int prevLayerStart, int prevLayerEnd)
		{
			int prevLayerCount = prevLayerEnd - prevLayerStart;
			int thisLayerStart = prevLayerEnd;
			int thisLayerEnd = circuit.GateList.Count;
			int thisLayerCount = thisLayerEnd - thisLayerStart;
			bool[] isBinded = new bool[prevLayerCount];

			for (int i = thisLayerStart; i < thisLayerEnd; i++) // TODO
			{
				circuit.Bind(circuit.GateList[random.Next(prevLayerStart, prevLayerEnd)], circuit.GateList[i]);
			}
			for (int i = thisLayerStart; i < thisLayerEnd; i++)
			{
				if (circuit.GateList[i].Type != Gate.Types.Not)
				{
					circuit.Bind(circuit.GateList[random.Next(prevLayerStart, prevLayerEnd)], circuit.GateList[i]);
				}
			}
		}

		#endregion

		#region Version 2
		private static void GenerateGatesV2()
		{
			int levels = random.Next(minLevels, maxLevels + 1);
			int prevLayerStart = 0;
			int prevLayerEnd = circuit.GateList.Count;
			gateLayers.Add(prevLayerEnd);
			int gatesGeneratedCount = GetGatesAmountV2(prevLayerEnd, false);

			for (int i = 0; i < levels; i++)
			{
				GenerateGateLevelV2(gatesGeneratedCount, prevLayerStart, prevLayerEnd);

				gateLayers.Add(gatesGeneratedCount);
				prevLayerStart = prevLayerEnd;
				prevLayerEnd += gatesGeneratedCount;
				if (i == levels - 2)
				{
					gatesGeneratedCount = GetGatesAmountV2(gatesGeneratedCount, true);
				}
				else
				{
					gatesGeneratedCount = GetGatesAmountV2(gatesGeneratedCount, false);
				}
			}
		}

		private static void GenerateGateLevelV2(int amount, int prevLayerStart, int prevLayerEnd)
		{
			AddRandomGatesV2(amount);
			BindGatesV2(prevLayerStart, prevLayerEnd);
		}

		private static int GetGatesAmountV2(int baseValue, bool isLast)
		{
			float rand = (float)random.NextDouble();

			if (isLast || baseValue > maxGatesInLevel)
			{
				if (baseValue > 2 && rand < 0.75)
				{
					return baseValue - 2;
				}
				else if (baseValue > 1)
				{
					return baseValue - 1;
				}
				return baseValue;
			}

			if (baseValue > 2 && rand < 0.20)
			{
				return baseValue - 2;
			}
			else if (baseValue > 1 && rand < 0.4)
			{
				return baseValue - 1;
			}
			else if (baseValue > 1 && rand < 0.6)
			{
				return baseValue;
			}
			else if (rand < 0.9)
			{
				return baseValue + 1;
			}
			return baseValue + 2;
		}

		private static void AddRandomGatesV2(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				circuit.Add(GetRandomType());
			}
		}

		private static void BindGatesV2(int prevLayerStart, int prevLayerEnd)
		{
			int prevLayerCount = prevLayerEnd - prevLayerStart;
			int thisLayerStart = prevLayerEnd;
			int thisLayerEnd = circuit.GateList.Count;
			int thisLayerCount = thisLayerEnd - thisLayerStart;
			bool[] isBinded = new bool[prevLayerCount];

			for (int i = thisLayerStart; i < thisLayerEnd; i++)
			{
				circuit.Bind(circuit.GateList[random.Next(prevLayerStart, prevLayerEnd)], circuit.GateList[i]);
			}
			for (int i = thisLayerStart; i < thisLayerEnd; i++)
			{
				if (circuit.GateList[i].Type != Gate.Types.Not)
				{
					circuit.Bind(circuit.GateList[random.Next(prevLayerStart, prevLayerEnd)], circuit.GateList[i]);
				}
			}
		}
		#endregion

		#region Version 1
		private static Designer.Circuit.Logic.Circuit GenerateGatesV1()
		{
			gateLayers = new List<int>();
			int used;

			int oldCount = circuit.GateList.Count;
			gateLayers.Add(oldCount);
			GenerateGateLevelV1(0, out used);

			int startIndex = circuit.GateList.Count - oldCount;
			gateLayers.Add(startIndex);

			int levels = random.Next(minLevels, maxLevels + 1);
			for (int i = 0; i < levels; i++)
			{
				oldCount = circuit.GateList.Count;

				if (!GenerateGateLevelV1(inputsAmount + inputsAmount - used, out used))
				{
					break;
				}

				startIndex = circuit.GateList.Count - oldCount;
				gateLayers.Add(startIndex);
			}

			return circuit;
		}

		private static bool GenerateGateLevelV1(int startIndex, out int used)
		{
			used = 0;
			int range = circuit.GateList.Count;
			if (startIndex < range - 1)
			{
				for (int i = startIndex; i < range; i++)
				{
					Gate.Types type = GetRandomType();

					circuit.Add(type);
					circuit.Bind(circuit.GateList[i], circuit.GateList[circuit.GateList.Count - 1]);
					used++;
					i++;
					if (i <= range && circuit.GateList[circuit.GateList.Count - 1].Type != Gate.Types.Not)
					{
						circuit.Bind(circuit.GateList[i], circuit.GateList[circuit.GateList.Count - 1]);
						used++;
					}
					else
					{
						break;
					}
				}
				return true;
			}
			return false;
		}

		#endregion

		#region Private helper methods
		private static Gate.Types GetRandomType()
		{
			int type = random.Next(0, 4);

			switch (type)
			{
				case 0:
					return Gate.Types.And;
				case 1:
					return Gate.Types.Or;
				case 2:
					return Gate.Types.Not;
				case 3:
					return Gate.Types.Xor;
			}

			throw new Exception();
		}
		#endregion
	}
}