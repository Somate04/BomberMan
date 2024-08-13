using BomberWars_MP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UnitUnitTests
    {
        [TestMethod]
        public void TestUnitFields()
        {
            TestUnit unit = new TestUnit();

            Type type = typeof(TestUnit);

            FieldInfo? privateFieldInfo = type.GetField("_posX", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            privateFieldInfo.SetValue(unit, 5);
            int x = (int)privateFieldInfo.GetValue(unit)!;
            Assert.IsTrue(x == 5);
            Assert.IsTrue(unit.PosX == 5);

            privateFieldInfo = type.GetField("_posY", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            privateFieldInfo.SetValue(unit, 5);
            int y = (int)privateFieldInfo.GetValue(unit)!;
            Assert.IsTrue(y == 5);
            Assert.IsTrue(unit.PosY == 5);

            privateFieldInfo = type.GetField("_alive", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            privateFieldInfo.SetValue(unit, true);
            bool alive = (bool)privateFieldInfo.GetValue(unit)!;
            Assert.IsTrue(alive);
            Assert.IsTrue(unit.Alive);

            privateFieldInfo = type.GetField("random", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            Random random = (Random)privateFieldInfo.GetValue(unit)!;
            Assert.IsNotNull(random);
        }

        [TestMethod]
        public void TestOnBoardChangeEvent()
        {
            TestUnit unit = new TestUnit();


            bool eventRaised = false;
            BoardChangeEventArgs? eventArgs = null;
            unit.OnBoardChangeEventHandler += (sender, args) =>
            {
                eventRaised = true;
                eventArgs = args;
            };

            Type type = typeof(TestUnit);
            MethodInfo? methodInfo = type.GetMethod("OnBoardChange", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            object[] param = {BlockTypeEnum.Path, 1, 1, BlockTypeEnum.PlayerA, 1, 2};
            methodInfo?.Invoke(unit, param);

            Assert.IsTrue(eventRaised);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventArgs._prevPosX == 1);
            Assert.IsTrue(eventArgs._prevPosY == 1);
            Assert.IsTrue(eventArgs._prevtype == BlockTypeEnum.Path);
            Assert.IsTrue(eventArgs._currentPosX == 1);
            Assert.IsTrue(eventArgs._currentPosY == 2);
            Assert.IsTrue(eventArgs._currenttype == BlockTypeEnum.PlayerA);
        }
    }

    public class TestUnit : Unit
    {
    }
}
