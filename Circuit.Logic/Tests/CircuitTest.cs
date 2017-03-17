using Designer.Circuit.Logic.Gates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Designer.Circuit.Logic.Tests
{
	[TestClass]
	public class CircuitAndOrOr
	{
		//[TestMethod]
		//public void TestInput()
		//{
		//	bool?[] input = { false, false };

		//	Circuit circuit = TestTools.CircuitInput(input);

		//	bool? expected = true;
		//	bool? actual = circuit.GateList[4].Value;

		//	Assert.AreEqual(expected, actual);
		//}

		[TestMethod]
		public void TestAndOrOrFF()
		{
			bool?[] input = { false, false };

			Circuit circuit = TestTools.CircuitAndOrOr(input);

			bool? expected = false;
			bool? actual = circuit.GateList[4].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestAndOrOrFT()
		{
			bool?[] input = { false, true };

			Circuit circuit = TestTools.CircuitAndOrOr(input);

			bool? expected = true;
			bool? actual = circuit.GateList[4].Value;
		
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestAndOrOrTF()
		{
			bool?[] input = { true, false};

			Circuit circuit = TestTools.CircuitAndOrOr(input);

			bool? expected = true;
			bool? actual = circuit.GateList[4].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestAndOrOrTT()
		{
			bool?[] input = { true, true };

			Circuit circuit = TestTools.CircuitAndOrOr(input);

			bool? expected = true;
			bool? actual = circuit.GateList[4].Value;

			Assert.AreEqual(expected, actual);
		}
	}

	[TestClass]
	public class CircuitAndOrXor
	{
		[TestMethod]
		public void TestAndOrXorFF()
		{
			bool?[] input = { false, false };

			Circuit circuit = TestTools.CircuitAndOrXor(input);

			bool? expected = false;
			bool? actual = circuit.GateList[4].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestAndOrXorFT()
		{
			bool?[] input = { false, true };

			Circuit circuit = TestTools.CircuitAndOrXor(input);

			bool? expected = true;
			bool? actual = circuit.GateList[4].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestAndOrXorTF()
		{
			bool?[] input = { true, false };

			Circuit circuit = TestTools.CircuitAndOrXor(input);

			bool? expected = true;
			bool? actual = circuit.GateList[4].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestAndOrXorTT()
		{
			bool?[] input = { true, true };

			Circuit circuit = TestTools.CircuitAndOrXor(input);

			bool? expected = false;
			bool? actual = circuit.GateList[4].Value;

			Assert.AreEqual(expected, actual);
		}
	}

	[TestClass]
	public class CircuitNotOrNotAndAnd
	{
		[TestMethod]
		public void TestNotOrNotAndAndFF()
		{
			bool?[] input = { false, false };

			Circuit circuit = TestTools.CircuitNotOrNotAndAnd(input);

			bool? expected = true;
			bool? actual = circuit.GateList[6].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestNotOrNotAndAndFT()
		{
			bool?[] input = { false, true };

			Circuit circuit = TestTools.CircuitNotOrNotAndAnd(input);

			bool? expected = false;
			bool? actual = circuit.GateList[6].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestNotOrNotAndAndTF()
		{
			bool?[] input = { true, false };

			Circuit circuit = TestTools.CircuitNotOrNotAndAnd(input);

			bool? expected = false;
			bool? actual = circuit.GateList[6].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestNotOrNotAndAndTT()
		{
			bool?[] input = { true, true };

			Circuit circuit = TestTools.CircuitNotOrNotAndAnd(input);

			bool? expected = false;
			bool? actual = circuit.GateList[6].Value;

			Assert.AreEqual(expected, actual);
		}
	}

}