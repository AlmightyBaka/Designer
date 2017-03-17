using Designer.Circuit.Logic.Gates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable InconsistentNaming

namespace Designer.Circuit.Logic.Tests
{
	[TestClass]
	public class AndGateTest
	{
		[TestMethod]
		public void AndGateFF()
		{
			bool?[] input = {false, false};
			Gate.Types typeType = Gate.Types.And;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = false;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AndGateTF()
		{
			bool?[] input = { true, false };
			Gate.Types typeType = Gate.Types.And;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = false;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AndGateFT()
		{
			bool?[] input = { false, true };
			Gate.Types typeType = Gate.Types.And;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = false;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AndGateTT()
		{
			bool?[] input = { true, true };
			Gate.Types typeType = Gate.Types.And;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = true;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}
	}

	[TestClass]
	public class OrGateTest
	{
		[TestMethod]
		public void OrGateFF()
		{
			bool?[] input = { false, false };
			Gate.Types typeType = Gate.Types.Or;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = false;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OrGateTF()
		{
			bool?[] input = { true, false };
			Gate.Types typeType = Gate.Types.Or;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = true;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OrGateFT()
		{
			bool?[] input = { false, true };
			Gate.Types typeType = Gate.Types.Or;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = true;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OrGateTT()
		{
			bool?[] input = { true, true };
			Gate.Types typeType = Gate.Types.Or;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = true;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}
	}

	[TestClass]
	public class NotGateTest
	{
		[TestMethod]
		public void NotGateF()
		{
			bool?[] input = { false };

			Circuit circuit = TestTools.NotGateCalculate(input);

			bool? expected = true;
			bool? actual = circuit.GateList[1].Value;
		
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NotGateT()
		{
			bool?[] input = { true };

			Circuit circuit = TestTools.NotGateCalculate(input);

			bool? expected = false;
			bool? actual = circuit.GateList[1].Value;

			Assert.AreEqual(expected, actual);
		}
	}

	[TestClass]
	public class XorGateTest
	{
		[TestMethod]
		public void XorGateFF()
		{
			bool?[] input = { false, false };
			Gate.Types typeType = Gate.Types.Xor;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = false;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void XorGateTF()
		{
			bool?[] input = { true, false };
			Gate.Types typeType = Gate.Types.Xor;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = true;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void XorGateFT()
		{
			bool?[] input = { false, true };
			Gate.Types typeType = Gate.Types.Xor;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = true;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void XorGateTT()
		{
			bool?[] input = { true, true };
			Gate.Types typeType = Gate.Types.Xor;

			Circuit circuit = TestTools.GateCalculate(input, typeType);

			bool? expected = false;
			bool? actual = circuit.GateList[2].Value;

			Assert.AreEqual(expected, actual);
		}
	}
}