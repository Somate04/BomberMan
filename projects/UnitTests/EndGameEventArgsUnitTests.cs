using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class EndGameEventArgsUnitTests
    {
        [TestMethod]
        public void TestEndGameEventArgs()
        {
            EndGameEventArgs eventArgs = new EndGameEventArgs(true, 1, 2, 3);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventArgs._end);
            Assert.IsTrue(eventArgs._firstPlayerPoints == 1);
            Assert.IsTrue(eventArgs._secondPlayerPoints == 2);
            Assert.IsTrue(eventArgs._thirdPlayerPoints == 3);
        }
    }
}
