// Designer.Circuit.Logic.Perceptrons: TestNetwork.cs

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;
// ReSharper disable InconsistentNaming

namespace Designer.Circuit.Logic.Perceptrons.Tests
{
	public static class TestTools
	{
		public static List<bool?> Calculate(bool?[] input, float[][] weight, float[][] threshold)
		{
			List<bool?> result;

			Network network = new Network(weight, threshold);
			network.UpdateInput(input);
			result = network.Update();

			return result;
		}
	}

	[TestClass]
	public class AndGateTest
	{
		#region AndGate
		[TestMethod]
		public void AndGateFF()
		{
			bool?[] input = { false, false };

			float[][] weight =
			{
				new float[] {0.6f, 0.6f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { false };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AndGateTF()
		{
			bool?[] input = { true, false };

			float[][] weight =
			{
				new float[] {0.6f, 0.6f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { false };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AndGateFT()
		{
			bool?[] input = { false, true };

			float[][] weight =
			{
				new float[] {0.6f, 0.6f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { false };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AndGateTT()
		{
			bool?[] input = { true, true };

			float[][] weight =
			{
				new float[] {0.6f, 0.6f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { true };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}
		#endregion
	}

	[TestClass]
	public class OrGateTest
	{
		#region OrGate

		[TestMethod]
		public void OrGateFF()
		{
			bool?[] input = { false, false };

			float[][] weight =
			{
				new float[] {1.1f, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { false };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OrGateTF()
		{
			bool?[] input = { true, false };

			float[][] weight =
			{
				new float[] {1.1f, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { true };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OrGateFT()
		{
			bool?[] input = { false, true };

			float[][] weight =
			{
				new float[] {1.1f, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { true };
			var actual = TestTools.Calculate(input: input, weight: weight, threshold: threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OrGateTT()
		{
			bool?[] input = { true, true };

			float[][] weight =
			{
				new float[] {1.1f, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { true };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		#endregion
	}

	[TestClass]
	public class InputGateTest
	{
		#region InputGate

		[TestMethod]
		public void InputGateF()
		{
			bool?[] input = { false };

			float[][] weight =
			{
				new float[] {1}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { false };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void InputGateT()
		{
			bool?[] input = { true };

			float[][] weight =
			{
				new float[] {1}
			};
			float[][] threshold =
			{
				new float[] {1}
			};

			var expected = new List<bool?> { true };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}
		#endregion
	}

	[TestClass]
	public class NotGateTest
	{
		#region NotGate

		[TestMethod]
		public void NotGateF()
		{
			bool?[] input = { false };

			float[][] weight =
			{
				new float[] {1}
			};
			float[][] threshold =
			{
				new float[] {0.5f}
			};

			var expected = new List<bool?> { false };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NotGateT()
		{
			bool?[] input = { true };

			float[][] weight =
			{
				new float[] {1}
			};
			float[][] threshold =
			{
				new float[] {0.5f}
			};

			var expected = new List<bool?> { true };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}
		#endregion
	}

	[TestClass]
	public class XorGateTest
	{
		#region XorGate

		[TestMethod]
		public void XorGateFF()
		{
			bool?[] input = { false, false, false, false };

			float[][] weight =
			{
				new float[] {1, 1, 1, 1},
				new float[] {0.6f, 1.1f, 0.6f, 1.1f},
				new float[] {-2, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {2, 2, 4, 4},
				new float[] {1.8f, 1},
				new float[] {1}
			};

			var expected = new List<bool?> { false };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void XorGateTF()
		{
			bool?[] input = { true, true, false, false };

			float[][] weight =
			{
				new float[] {1, 1, 1, 1},
				new float[] {0.6f, 1.1f, 0.6f, 1.1f},
				new float[] {-2, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {2, 2, 4, 4},
				new float[] {1.8f, 1},
				new float[] {1}
			};

			var expected = new List<bool?> { true };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void XorGateFT()
		{
			bool?[] input = { false, false, true, true };

			float[][] weight =
			{
				new float[] {1, 1, 1, 1},
				new float[] {0.6f, 1.1f, 0.6f, 1.1f},
				new float[] {-2, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {2, 2, 4, 4},
				new float[] {1.8f, 1},
				new float[] {1}
			};

			var expected = new List<bool?> { true };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void XorGateTT()
		{
			bool?[] input = { true, true, true, true };

			float[][] weight =
			{
				new float[] {1, 1, 1, 1},
				new float[] {0.6f, 1.1f, 0.6f, 1.1f},
				new float[] {-2, 1.1f}
			};
			float[][] threshold =
			{
				new float[] {2, 2, 4, 4},
				new float[] {1.8f, 1},
				new float[] {1}
			};

			var expected = new List<bool?> { false };
			var actual = TestTools.Calculate(input, weight, threshold);

			CollectionAssert.AreEqual(expected, actual);
		}

		#endregion
	}
}