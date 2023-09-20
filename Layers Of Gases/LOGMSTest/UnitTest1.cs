using Task4;

namespace LOGMSTest
{
    [TestClass]
    public class UnitTest1
    {
        private LOG? LogTest { get; set; } = null;

        private List<char>? atmosVarsArrTest = null;
        private List<char>? layerTypesArrTest = null;

        [TestInitialize]
        public void Init()
        {
            LogTest = new LOG();

            atmosVarsArrTest = new List<char>() { 'T', 'O', 'S', 'O', 'T', 'S'};
            layerTypesArrTest = new List<char>() { 'X', 'Z', 'C', 'Z', 'C' };
        }

        #region Atmosphere Variable Type Valid Test Cases
        [TestMethod]
        [DataRow(0)]
        [DataRow(4)]
        public void IsTypeThunderstormValidTest(int i)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsNotNull(atmosVarsArrTest);
            Assert.IsTrue(atmosVarsArrTest.Count > 0);

            Assert.IsTrue(LogTest.IsTypeThunderstorm(atmosVarsArrTest[i]));
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(5)]
        public void IsTypeSunshineValidTest(int i)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsNotNull(atmosVarsArrTest);
            Assert.IsTrue(atmosVarsArrTest.Count > 0);

            Assert.IsTrue(LogTest.IsTypeSunshine(atmosVarsArrTest[i]));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        public void IsTypeOthersValidTest(int i)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsNotNull(atmosVarsArrTest);
            Assert.IsTrue(atmosVarsArrTest.Count > 0);

            Assert.IsTrue(LogTest.IsTypeOthers(atmosVarsArrTest[i]));
        }
        #endregion

        #region Atmosphere Variable Type Invalid Test Cases
        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        public void IsTypeThunderstormInvalidTest(int i)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsNotNull(atmosVarsArrTest);
            Assert.IsTrue(atmosVarsArrTest.Count > 0);

            Assert.IsFalse(LogTest.IsTypeThunderstorm(atmosVarsArrTest[i]));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        public void IsTypeSunshineInalidTest(int i)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsNotNull(atmosVarsArrTest);
            Assert.IsTrue(atmosVarsArrTest.Count > 0);

            Assert.IsFalse(LogTest.IsTypeSunshine(atmosVarsArrTest[i]));
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        public void IsTypeOthersInvalidTest(int i)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsNotNull(atmosVarsArrTest);
            Assert.IsTrue(atmosVarsArrTest.Count > 0);

            Assert.IsFalse(LogTest.IsTypeOthers(atmosVarsArrTest[i]));
        }
        #endregion

        #region Layers Of Gases Valid Test Cases
        [TestMethod]
        [DataRow(1,3)]
        [DataRow(2,4)]
        public void FoundFirstIdenticalLayerValidTest(int i, int j)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsNotNull(layerTypesArrTest);
            Assert.IsTrue(layerTypesArrTest.Count > 0);

            Assert.IsTrue(LogTest.FoundFirstIdenticalLayer(layerTypesArrTest[i], layerTypesArrTest[j]));
        }

        [TestMethod]
        [DataRow(0.2)]
        [DataRow(0.4)]
        public void ThicknessCheckValidTest(double thickness)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsTrue(LogTest.ThicknessCheck(thickness));
        }
        #endregion

        #region Layers Of Gases Invalid Test Cases
        [TestMethod]
        [DataRow(2, 3)]
        [DataRow(0, 4)]
        public void FoundFirstIdenticalLayerInvalidTest(int i, int j)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsNotNull(layerTypesArrTest);
            Assert.IsTrue(layerTypesArrTest.Count > 0);

            Assert.IsFalse(LogTest.FoundFirstIdenticalLayer(layerTypesArrTest[i], layerTypesArrTest[j]));
        }

        [TestMethod]
        [DataRow(0.5)]
        [DataRow(12)]
        public void ThicknessCheckInvalidTest(double thickness)
        {
            Assert.IsNotNull(LogTest);

            Assert.IsFalse(LogTest.ThicknessCheck(thickness));
        }
        #endregion
    }
}