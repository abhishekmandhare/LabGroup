using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TFNAPI.Core.UseCases;
using TFNAPI.Infrastructure.Data.Entities;
using TFNAPI.Infrastructure.Data.Interfaces.Repositories;

namespace UnitTest.UseCases
{
    public class LinkedAttemptUseCaseTest
    {
        [Test]
        public void TestIsLinkedAsync()
        {
            // Arrange
            List<Attempt> _attempt = new List<Attempt>()
            {
                new Attempt{AttemptTime = DateTime.Now.AddSeconds(-20), Tfn = "443459871"},
                new Attempt{AttemptTime = DateTime.Now.AddSeconds(-10), Tfn = "123459876"}
            };
            ILinkedAttemptRepository _mockRepo = A.Fake<ILinkedAttemptRepository>();
            A.CallTo(() => _mockRepo.GetAttemptsAsync()).Returns(_attempt);

            var linkedAttemptsUC = new LinkedAttemptsUseCase(_mockRepo);
            string tfnNumber = "12345678";
            // ACT
            // Call function to check if the current TF number is linked
            bool isLinked = linkedAttemptsUC.IsLinkedAsync(tfnNumber).Result;

            // Assert
            Assert.IsTrue(isLinked);

        }

        [Test]
        public void TestIsStaleDate()
        {
            // Arrange
            ILinkedAttemptRepository _mockRepo = A.Fake<ILinkedAttemptRepository>();
            var linkedAttemptUC = new LinkedAttemptsUseCase(_mockRepo);

            // Act
            bool isStale1 = linkedAttemptUC.IsStaleDate(DateTime.Now.AddDays(-1));
            bool isStale2 = linkedAttemptUC.IsStaleDate(DateTime.Now.AddSeconds(-1));
            bool isStale3 = linkedAttemptUC.IsStaleDate(DateTime.Now.AddSeconds(-29));

            // Assert
            Assert.IsTrue(isStale1);
            Assert.IsFalse(isStale2);
            Assert.IsFalse(isStale3);
        }

        [Test]
        public void TestAreNumbersLinked()
        {
            // Arrange
            ILinkedAttemptRepository _mockRepo = A.Fake<ILinkedAttemptRepository>();
            var linkedAttemptUC = new LinkedAttemptsUseCase(_mockRepo);

            // ACT
            bool isLinked1 = linkedAttemptUC.AreNumbersLinked("12345678", "123459876");
            bool isLinked2 = linkedAttemptUC.AreNumbersLinked("123459876", "443459871");
            bool isLinked3 = linkedAttemptUC.AreNumbersLinked("12345678", "443459871");

            // Assert
            Assert.IsTrue(isLinked1);
            Assert.IsTrue(isLinked2);
            Assert.IsFalse(isLinked3);
        }
    }
}
