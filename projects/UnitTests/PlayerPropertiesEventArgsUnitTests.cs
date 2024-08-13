using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class PlayerPropertiesEventArgsUnitTests
    {
        [TestMethod]
        public void TestPlayerPropertiesEventArgs()
        {
            PlayerPropertiesEventArgs eventArgs = new PlayerPropertiesEventArgs(
                1, 3, 3, 3, 12, true, 100, true, true, 99, 4, 13, 45, 67, true, 50
                );

            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventArgs._playerId == 1);
            Assert.IsTrue(eventArgs._maxNumberOfBombs == 3);
            Assert.IsTrue(eventArgs._currentNumberBombs == 3);
            Assert.IsTrue(eventArgs._range == 3);
            Assert.IsTrue(eventArgs._speed == 12);
            Assert.IsTrue(eventArgs._invincibility);
            Assert.IsTrue(eventArgs._invincibilityCoolDown == 100);
            Assert.IsTrue(eventArgs._detonator);
            Assert.IsTrue(eventArgs._ghostMode);
            Assert.IsTrue(eventArgs._ghostModeCoolDown == 99);
            Assert.IsTrue(eventArgs._numberOfObstacles == 4);
            Assert.IsTrue(eventArgs._slowDownCoolDown == 13);
            Assert.IsTrue(eventArgs._rangeDecreasedCoolDown == 45);
            Assert.IsTrue(eventArgs._disableBombPlacementCoolDown == 67);
            Assert.IsTrue(eventArgs._instantPlacingBombs);
            Assert.IsTrue(eventArgs._instantPlacingBombsCoolDown == 50);
        }
    }
}
