using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class ExplodeEventArgsUnitTests
    {
        [TestMethod]
        public void TestExplodeEventArgs()
        {
            ExplodeEventArgs eventArgs = new ExplodeEventArgs(BlockTypeEnum.Explosion, 19, 19);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventArgs.BlockType == BlockTypeEnum.Explosion);
            Assert.IsTrue(eventArgs.PosX == 19);
            Assert.IsTrue(eventArgs.PosY == 19);
        }
    }
}
