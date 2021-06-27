using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TFNAPI.Core.UseCases;
using TFNAPI.Infrastructure.Data.Interfaces;

namespace UnitTest.UseCases
{
    public class TFNValidatorUseCaseTest
    {
        readonly List<int> weightFactor9Digits = new List<int> { 10, 7, 8, 4, 6, 3, 5, 2, 1 };
        readonly List<int> weightFactor8Digits = new List<int> { 10, 7, 8, 4, 6, 3, 5, 1 };

        [TestCase("714925631", true)]
        [TestCase("1", false)]
        [TestCase("123456789", false)]
        [TestCase("648188499", true)]
        [TestCase("648188480", true)]
        [TestCase("648188519", true)]
        [TestCase("648188535", true)]
        [TestCase("-1", false)]
        [TestCase("0", false)]
        [TestCase("0.55", false)]
        public void TestTFNValidatorUseCase9Digits(string tfn, bool expectedResult)
        {
            // Arrange
            var _repo = A.Fake<IWeightingFactorRepository>();
            A.CallTo(() => _repo.GetWeightingFactorAsync(A<int>.Ignored)).Returns(weightFactor9Digits);
            var tfnValidatorUseCase = new TFNValidatorUseCase(_repo);

            // Act
            bool isValid = tfnValidatorUseCase.Validate(tfn).Result;

            // Assert
            Assert.AreEqual(expectedResult, isValid);
        }

        [TestCase("1", false)]
        [TestCase("123456789", false)]
        [TestCase("37118629", true)]
        [TestCase("37118676", true)]
        [TestCase("85655755", true)]
        [TestCase("85655797", true)]
        [TestCase("-1", false)]
        [TestCase("0", false)]
        [TestCase("0.55", false)]
        public void TestTFNValidatorUseCase8Digits(string tfn, bool expectedResult)
        {
            // Arrange
            var _repo = A.Fake<IWeightingFactorRepository>();
            A.CallTo(() => _repo.GetWeightingFactorAsync(A<int>.Ignored)).Returns(weightFactor8Digits);
            var tfnValidatorUseCase = new TFNValidatorUseCase(_repo);

            // Act
            bool isValid = tfnValidatorUseCase.Validate(tfn).Result;

            // Assert
            Assert.AreEqual(expectedResult, isValid);
        }


       
    }
}
