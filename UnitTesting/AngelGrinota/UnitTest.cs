using Microsoft.VisualStudio.TestPlatform.TestHost;
using TestingLib.Math;

namespace UnitTesting.AngelGinota
{
    public class UnitTest
    {
        private readonly BasicCalc _calculator;

        public UnitTest()
        {
            _calculator = new BasicCalc();
        }

        // Задание 1
        [Fact]
        public void LCM_PositiveNumbers_ReturnsCorrectLCM()
        {
            int a = 12;
            int b = 15;
            int expectedResult = 60;

            int result = _calculator.LCM(a, b);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(4, 6)]
        [InlineData(7, 11)]
        public void LCM_MultipleInputs_ReturnsCorrectLCM(int a, int b)
        {
            int result = _calculator.LCM(a, b);

            Assert.True(result % a == 0 && result % b == 0);
        }

        [Fact]
        public void LCM_NegativeNumbers_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.LCM(-13, 16));
        }

        // Задание 2
        [Fact]
        public void IsPrime_ValidInput_ReturnsCorrectResult()
        {
            int n = 7;

            bool result = _calculator.IsPrime(n);

            Assert.True(result);
        }

        [Theory]
        [InlineData(1,true)]
        [InlineData(3, true)]
        [InlineData(5, true)]
        [InlineData(11, true)]
        public void IsPrime_MultipleInputs_ReturnsExpectedResults(int n,bool expectedResult)
        {
            bool result = _calculator.IsPrime(n);

            Assert.Equal(expectedResult, result);
        }
        
        [Fact]
        public void IsPrime_InvalidInput_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.IsPrime(-1));
        }

        //Лабораторная работа №6

        [Fact]
        public void LCM_ShouldReturnCorrectLCM()
        {
            int result = _calculator.LCM(6, 2);
            Assert.Equal(6, result);
        }

        [Fact]
        public void LCM_ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.LCM(-2, 2));
        }

        [Theory]
        [InlineData(5, 2, 10)]
        [InlineData(8, 4, 8)]
        [InlineData(2, 3, 6)]
        public void LCM_Theory(int a, int b, int expectedResult)
        {
            int result = _calculator.LCM(a, b);
            Assert.Equal(expectedResult, result);
        }

        [Fact]

        public void SolveQuadraticEquation_ShouldReturnCorrectSolveQuadraticEquation()
        {
            (double?, double?) result = _calculator.SolveQuadraticEquation(1, 7, 10);
            Assert.Equal(-2, result.Item1);
            Assert.Equal(-5, result.Item2);
        }

        public void SolveQuadraticEquation_ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.SolveQuadraticEquation(0, 2, 3));
        }


        [Theory]
        [InlineData(1d, 2d, -8d, 2d, -4d)]
        [InlineData(1d, 10d, -39d, 3d, -13d)]
        [InlineData(1d, -2d, -3d, 3d, -1d)]
        public void SolveQuadraticEquation_Theory(double a, double b, double c, double expectedResult1, double expectedResult2)
        {
            (double?, double?) result = _calculator.SolveQuadraticEquation(a, b, c);
            Assert.Equal(expectedResult1, result.Item1);
            Assert.Equal(expectedResult2, result.Item2);
        }

    }
}
