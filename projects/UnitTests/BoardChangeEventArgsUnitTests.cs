using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class BoardChangeEventArgsUnitTests
    {
        [TestMethod]
        public void TestBoardChangeEventArgs()
        {
            BoardChangeEventArgs eventArgs = new BoardChangeEventArgs(BlockTypeEnum.GhostPlayerAOnBox, 19, 19, BlockTypeEnum.Box, 20, 19);
            Assert.IsTrue(eventArgs._prevtype == BlockTypeEnum.GhostPlayerAOnBox);
            Assert.IsTrue(eventArgs._prevPosX == 19);
            Assert.IsTrue(eventArgs._prevPosY == 19);
            Assert.IsTrue(eventArgs._currenttype == BlockTypeEnum.Box);
            Assert.IsTrue(eventArgs._currentPosX == 20);
            Assert.IsTrue(eventArgs._currentPosY == 19);
        }
    }
}
