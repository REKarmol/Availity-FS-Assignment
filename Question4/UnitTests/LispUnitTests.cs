using Microsoft.VisualStudio.TestTools.UnitTesting;
using LispChecker;

namespace UnitTests
{
    [TestClass]
    public class LispUnitTests
    {
        [TestMethod]
        public void valid_list_with_parentheses_bookends()
        {
            // Arrange
            LispReader lispReader = new LispReader("(+ 1 2 3 4)");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void invalid_list_with_inverted_parentheses_bookends()
        {
            // Arrange
            LispReader lispReader = new LispReader(")+ 1 2 3 4(");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void invalid_list_with_unmatched_parentheses_bookends()
        {
            // Arrange
            LispReader lispReader = new LispReader("(+ 1 2 3 4))");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void invalid_list_with_unmatched_parentheses_bookends_the_other_way()
        {
            // Arrange
            LispReader lispReader = new LispReader("((+ 1 2 3 4)");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void valid_list_with_parentheses_bookends_and_string()
        {
            // Arrange
            LispReader lispReader = new LispReader("(+ 1 2 \")\" 4)");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void invalid_list_with_parentheses_bookends_and_string()
        {
            // Arrange
            LispReader lispReader = new LispReader("(+ 1 2 \"(\" 4))");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void valid_list_with_parentheses_bookends_and_quote()
        {
            // Arrange
            LispReader lispReader = new LispReader("(+ 1 2 '(skip) 4)");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void invalid_list_with_parentheses_bookends_and_quote()
        {
            // Arrange
            LispReader lispReader = new LispReader("(+ 1 2 '(skip)) 4)");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void valid_list_with_bizarre_quote()
        {
            // Arrange
            LispReader lispReader = new LispReader("(setf(third stuff) 'bizarre)");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void invalid_list_with_bizarre_quote()
        {
            // Arrange
            LispReader lispReader = new LispReader("(setf(third stuff) 'bizarre))");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void valid_list_with_comment()
        {
            // Arrange
            LispReader lispReader = new LispReader("(setf(third stuff)) ; 'bizarre)");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void invalid_list_with_comment()
        {
            // Arrange
            LispReader lispReader = new LispReader("((setf(third stuff) 'bizarre) ;)");

            //Act
            bool result = lispReader.CheckParentheses();

            //Assert
            Assert.IsFalse(result);
        }
    }
}
