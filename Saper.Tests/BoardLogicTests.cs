using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Saper.Data.Logics;
using Saper.Data.Models;

namespace Saper.Tests
{
    [TestClass]
    public class BoardLogicTests
    {
        [TestMethod]
        public void FillTest()
        {
            Board board = new Board(10, 10, 10);
            BoardLogic logic = new BoardLogic();
            logic.Fill(board, 5, 5);
            Assert.AreEqual(board.Fields.Count(x => x.Value == -1), 10);
            Assert.AreNotEqual(board[5, 5], -1);
        }
        [TestMethod]
        public void ShowTest()
        {
            Board board = new Board(10, 10, 10);
            BoardLogic logic = new BoardLogic();
            logic.Fill(board, 5, 5);
            try
            {
                logic.Show(board, 5, 5);
            }
            catch (MineException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void WinTest()
        {
            Board board = new Board(10, 10, 10);
            BoardLogic logic = new BoardLogic();
            logic.Fill(board, 5, 5);
            Assert.IsFalse(logic.CheckWin(board));
            foreach (var item in board.Fields)
            {
                if (item.Value != -1 && item.State != Data.Enums.FieldState.Showed)
                    logic.Show(board, item.X, item.Y);
            }
            Assert.IsTrue(logic.CheckWin(board));
        }
        [TestMethod]
        public void ChangeStateTest()
        {
            Board board = new Board(10, 10, 10);
            BoardLogic logic = new BoardLogic();
            logic.Fill(board, 5, 5);
            logic.ChangeState(board[5, 5]);
            Assert.AreEqual(board[5, 5].State, Data.Enums.FieldState.Coverd);
            logic.ChangeState(board[5, 5]);
            Assert.AreEqual(board[5, 5].State, Data.Enums.FieldState.Ask);
            logic.ChangeState(board[5, 5]);
            Assert.AreEqual(board[5, 5].State, Data.Enums.FieldState.Defalut);
        }
    }
}
