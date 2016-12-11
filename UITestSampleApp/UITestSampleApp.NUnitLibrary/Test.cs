using System.Collections.ObjectModel;

using NUnit.Framework;

namespace UITestSampleApp.NUnitLibrary
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void Pass ()
		{
			// Arrange
			int zero = 0;
			int one = 1;
			int two = 2;

			// Act
			var firstDataModel = new SampleDataModel (0);
			var secondDataModel = new SampleDataModel (1);
			var thirdDataModel = new SampleDataModel (2);

			var dataCollection = new ObservableCollection<SampleDataModel> ();
			dataCollection.Add (firstDataModel);
			dataCollection.Add (secondDataModel);
			dataCollection.Add (thirdDataModel);

			// Assert
			Assert.AreEqual (zero, dataCollection.IndexOf (firstDataModel));
			Assert.AreEqual (one, dataCollection.IndexOf (secondDataModel));
			Assert.AreEqual (two, dataCollection.IndexOf (thirdDataModel));
		}

		[Test]
		public void Fail ()
		{
			// Arrange
			int zero = 0;
			int one = 1;
			int two = 2;

			// Act
			var firstDataModel = new SampleDataModel (0);
			var secondDataModel = new SampleDataModel (1);
			var thirdDataModel = new SampleDataModel (2);

			var dataCollection = new ObservableCollection<SampleDataModel> ();
			dataCollection.Add (firstDataModel);
			dataCollection.Add (secondDataModel);
			dataCollection.Add (thirdDataModel);

			// Assert
			Assert.AreEqual (two, dataCollection.IndexOf (firstDataModel));
			Assert.AreEqual (zero, dataCollection.IndexOf (secondDataModel));
			Assert.AreEqual (one, dataCollection.IndexOf (thirdDataModel));
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}
	}
}

