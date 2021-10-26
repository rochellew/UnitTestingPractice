using NUnit.Framework;
using BankAccountNS;

namespace BankTests
{
    public class BankTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Walter White", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        [Test]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;

            BankAccount account = new BankAccount("Mr. Walter White", beginningBalance);

            // Act and assert
            Assert.Throws<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));

            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                Assert.IsTrue(e.Message.Contains(BankAccount.DebitAmountLessThanZeroMessage));
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [Test]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 100.00;

            BankAccount account = new BankAccount("Mr. Walter White", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                Assert.IsTrue(e.Message.Contains(BankAccount.DebitAmountExceedsBalanceMessage));
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }
    }
}
