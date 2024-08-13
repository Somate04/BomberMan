using BomberWars_MP.DataAccess;
using BomberWars_MP.Model;
using System;
using System.Reflection;

namespace UnitTests
{
    [TestClass]
    public class ModelUnitTests
    {
        [TestMethod]
        public void TestGenBoardFirstMap3Player()
        {
            PowerUpEnum[] powerups = new PowerUpEnum[0];
            PowerDownEnum[] powerDowns = new PowerDownEnum[0];
            Model model = new Model(0, 3, powerups, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Assert.IsTrue(model.Board[0, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 1] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 3] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 5] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 7] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 9] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 11] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 13] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[1, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 3] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 4] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 5] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 6] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 7] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 8] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 9] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 11] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 12] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 13] == BlockTypeEnum.Path);

            Assert.IsTrue(model.Board[2, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[2, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[3, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[3, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[3, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[4, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[4, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[5, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[5, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[5, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[6, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[6, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[7, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[7, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[7, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[8, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[8, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[9, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[9, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[9, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[10, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[10, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[11, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[11, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 12] == BlockTypeEnum.Box);
        }

        [TestMethod]
        public void TestGenBoardFirstMap2Player()
        {
            PowerUpEnum[] powerups = new PowerUpEnum[0];
            PowerDownEnum[] powerDowns = new PowerDownEnum[0];
            Model model = new Model(0, 2, powerups, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Assert.IsTrue(model.Board[0, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 1] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 3] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 5] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 7] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 9] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 11] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 13] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[1, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 3] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 4] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 5] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 6] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 7] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 8] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 9] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 11] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 12] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 13] == BlockTypeEnum.Path);

            Assert.IsTrue(model.Board[2, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[2, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[3, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[3, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[3, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[4, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[4, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[5, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[5, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[5, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[6, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[6, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[7, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[7, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[7, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[8, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[8, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[9, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[9, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[9, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[10, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[10, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[11, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[11, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 12] == BlockTypeEnum.Box);

        }

        [TestMethod]
        public void TestGenBoardSecondMap()
        {
        }

        [TestMethod]
        public void TestGenBoardThirdMap()
        {
        }

        [TestMethod]
        public void TestMoveEventHandler()
        {
            PowerUpEnum[] powerups = new PowerUpEnum[0];
            PowerDownEnum[] powerDowns = new PowerDownEnum[0];
            Model model = new Model(0, 3, powerups, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            bool eventRaised = false;
            BoardChangeEventArgs? eventArgs = null;
            model.ModelOnMoveEventHandler += (sender, args) =>
            {
                eventRaised = true;
                eventArgs = args;
            };

            model.PlayerMoves(0, DirectionEnum.Right);
            Assert.IsTrue(eventRaised);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventArgs._prevPosX == 1);
            Assert.IsTrue(eventArgs._prevPosY == 1);
            Assert.IsTrue(eventArgs._prevtype == BlockTypeEnum.Path);
            Assert.IsTrue(eventArgs._currentPosX == 1);
            Assert.IsTrue(eventArgs._currentPosY == 2);
            Assert.IsTrue(eventArgs._currenttype == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.PlayerA);

            for (int i = 0; i < 10; ++i)
            {
                model.OnTimerTick();
            }

            model.PlayerMoves(0, DirectionEnum.Right);
            Assert.IsTrue(eventRaised);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventArgs._prevPosX == 1);
            Assert.IsTrue(eventArgs._prevPosY == 2);
            Assert.IsTrue(eventArgs._prevtype == BlockTypeEnum.Path);
            Assert.IsTrue(eventArgs._currentPosX == 1);
            Assert.IsTrue(eventArgs._currentPosY == 3);
            Assert.IsTrue(eventArgs._currenttype == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 3] == BlockTypeEnum.PlayerA);
        }

        [TestMethod]
        public void TesOnEndRoundEventEventHandler()
        {
            PowerUpEnum[] powerups = new PowerUpEnum[0];
            PowerDownEnum[] powerDowns = new PowerDownEnum[0];
            Model model = new Model(0, 2, powerups, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);
            model.Board[1, 2] = BlockTypeEnum.Explosion;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            bool eventRaised = false;
            EndRoundEventArgs? eventArgs = null;
            model.ModelOnEndRoundEventHandler += (sender, args) =>
            {
                eventRaised = true;
                eventArgs = args;
            };

            model.PlayerMoves(0, DirectionEnum.Right);
            Assert.IsFalse(players[0].Alive);
            Assert.IsTrue(players[1].Alive);

            try
            {
                for (int i = 0; i < 499; i++)
                {
                    model.OnTimerTick();
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }

            if (players[1].Alive)
            {
                Assert.IsFalse(eventRaised);

                model.OnTimerTick();

                Assert.IsTrue(eventRaised);
                Assert.IsNotNull(eventArgs);
                Assert.IsTrue(eventArgs._end);
                Assert.IsTrue(eventArgs._firstPlayerPoints == 0);
                Assert.IsTrue(eventArgs._secondPlayerPoints == 1);
                Assert.IsTrue(eventArgs._thirdPlayerPoints == 0);
            }
            else
            {
                model.OnTimerTick();

                Assert.IsTrue(eventRaised);
                Assert.IsNotNull(eventArgs);
                Assert.IsTrue(eventArgs._end);
                Assert.IsTrue(eventArgs._firstPlayerPoints == 0);
                Assert.IsTrue(eventArgs._secondPlayerPoints == 0);
                Assert.IsTrue(eventArgs._thirdPlayerPoints == 0);
            }

            model.NewGame();

            Assert.IsTrue(players[0].Alive);
            Assert.IsTrue(players[1].Alive);
        }

        [TestMethod]
        public void TesOnEndGameEventEventHandler()
        {
            PowerUpEnum[] powerups = new PowerUpEnum[0];
            PowerDownEnum[] powerDowns = new PowerDownEnum[0];
            Model model = new Model(0, 2, powerups, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);
            model.Board[1, 2] = BlockTypeEnum.Explosion;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            players[0].Points = 2;
            players[1].Points = 2;

            bool eventRaised = false;
            EndGameEventArgs? eventArgs = null;
            model.ModelOnEndGameEventHandler += (sender, args) =>
            {
                eventRaised = true;
                eventArgs = args;
            };

            Assert.IsFalse(eventRaised);

            model.PlayerMoves(0, DirectionEnum.Right);
            Assert.IsFalse(players[0].Alive);
            Assert.IsTrue(players[1].Alive);

            try
            {
                for (int i = 0; i < 499; i++)
                {
                    model.OnTimerTick();
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }


            if (players[1].Alive)
            {
                Assert.IsFalse(eventRaised);

                model.OnTimerTick();

                Assert.IsTrue(eventRaised);
                Assert.IsNotNull(eventArgs);
                Assert.IsTrue(eventArgs._end);
                Assert.IsTrue(eventArgs._firstPlayerPoints == 2);
                Assert.IsTrue(eventArgs._secondPlayerPoints == 3);
                Assert.IsTrue(eventArgs._thirdPlayerPoints == 0);

                model.NewGame();

                Assert.IsFalse(players[0].Alive);
                Assert.IsTrue(players[1].Alive);
            }
            else
            {
                model.OnTimerTick();

                Assert.IsTrue(eventRaised);
                Assert.IsNotNull(eventArgs);
                Assert.IsTrue(eventArgs._end);
                Assert.IsTrue(eventArgs._firstPlayerPoints == 2);
                Assert.IsTrue(eventArgs._secondPlayerPoints == 2);
                Assert.IsTrue(eventArgs._thirdPlayerPoints == 0);
            }
        }

        [TestMethod]
        public void TestOnPlayerProperties()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[0];
            PowerDownEnum[] powerDowns = new PowerDownEnum[1];
            powerDowns[0] = PowerDownEnum.InstantBombPlacementPowerDown;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            bool eventRaised = false;
            PlayerPropertiesEventArgs? eventArgs = null;
            model.ModelOnPlayerPropertiesEventHandler += (sender, args) =>
            {
                eventRaised = true;
                eventArgs = args;
            };
            model.NewGame();
            model.OnTimerTick();
            Assert.IsTrue(eventRaised);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventArgs._maxNumberOfBombs == 2);
            Assert.IsTrue(eventArgs._currentNumberBombs == 0);
            Assert.IsTrue(eventArgs._range == 2);
            Assert.IsTrue(eventArgs._speed == 10);
            Assert.IsFalse(eventArgs._invincibility);
            Assert.IsFalse(eventArgs._detonator);
            Assert.IsFalse(eventArgs._ghostMode);
            Assert.IsTrue(eventArgs._ghostModeCoolDown == 0);
            Assert.IsTrue(eventArgs._numberOfObstacles == 0);
            Assert.IsTrue(eventArgs._slowDownCoolDown == 0);
            Assert.IsTrue(eventArgs._rangeDecreasedCoolDown == 0);
            Assert.IsTrue(eventArgs._disableBombPlacementCoolDown == 0);
            Assert.IsTrue(eventArgs._rangeDecreasedCoolDown == 0);
            Assert.IsFalse(eventArgs._instantPlacingBombs);
            Assert.IsTrue(eventArgs._instantPlacingBombsCoolDown == 0);

        }

        [TestMethod]
        public void TestSaveLoadGame()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.BombNumberIncreasePowerUp;
            PowerDownEnum[] powerDowns = new PowerDownEnum[1];
            powerDowns[0] = PowerDownEnum.InstantBombPlacementPowerDown;
            Model modelForSaving = new Model(0, 3, currentPowerups, powerDowns, 1, true);
            modelForSaving.NewGame();
            Assert.IsNotNull(modelForSaving);
            Assert.IsNotNull(modelForSaving.Board);

            Type type = typeof(Model);
            FieldInfo? privateFieldInfoMonsters = type.GetField("_monstersOnBoard", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoMonsters);
            List<MonsterBase> monsters = (List<MonsterBase>)privateFieldInfoMonsters.GetValue(modelForSaving)!;
            Assert.IsNotNull(monsters);
            monsters.Add(new Monster(1, 10, 13, 15));
            modelForSaving.Board[1, 10] = BlockTypeEnum.Monster;
            modelForSaving.PlayerPlacesBomb(0);
            modelForSaving.PlayerMoves(0, DirectionEnum.Right);

            modelForSaving.SaveModel();
            string testResourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maps");
            string testpath = Path.Combine(testResourcesPath, "test_map.json");

            Model model = new Model(testpath, true);
            Assert.IsNotNull(model);

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            #region Checking Board
            Assert.IsTrue(model.Board[0, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 1] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 3] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 5] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 7] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 9] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 11] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 13] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[0, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[1, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.Bomb);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 3] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 4] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 5] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 6] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 7] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 8] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 9] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 10] == BlockTypeEnum.Monster);
            Assert.IsTrue(model.Board[1, 11] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 12] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 13] == BlockTypeEnum.Path);

            Assert.IsTrue(model.Board[2, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[2, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[2, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[2, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[3, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[3, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[3, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[4, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[4, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[4, 12] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[4, 14] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[5, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[5, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[5, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[5, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[6, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[6, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[6, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[6, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[7, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[7, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[7, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[7, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[8, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[8, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[8, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[8, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[9, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[9, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[9, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[9, 12] == BlockTypeEnum.Box);

            Assert.IsTrue(model.Board[10, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[10, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 4] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 6] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 8] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 10] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[10, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[10, 12] == BlockTypeEnum.Wall);

            Assert.IsTrue(model.Board[11, 0] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[11, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 4] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 5] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 6] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 7] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 8] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 9] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 10] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 11] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[11, 12] == BlockTypeEnum.Box);


            #endregion

            #region Checking Model fields

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoCurrentMap = typeModel.GetField("_currentMap", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoCurrentMap);
            int map = (int)privateFieldInfoCurrentMap.GetValue(model)!;
            Assert.IsTrue(map == 0);

            FieldInfo? privateFieldInfoCWidth = typeModel.GetField("_boardWidth", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoCWidth);
            int width = (int)privateFieldInfoCWidth.GetValue(model)!;
            Assert.IsTrue(width == 26);

            FieldInfo? privateFieldInfoCHeight = typeModel.GetField("_boardHeight", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoCHeight);
            int height = (int)privateFieldInfoCHeight.GetValue(model)!;
            Assert.IsTrue(height == 18);

            FieldInfo? privateFieldInfoCPlayerNum = typeModel.GetField("_numberOfPlayers", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoCPlayerNum);
            int numberOfPlayer = (int)privateFieldInfoCPlayerNum.GetValue(model)!;
            Assert.IsTrue(numberOfPlayer == 3);

            FieldInfo? privateFieldInfoOnePlayerAlive = typeModel.GetField("_onePlayerAlive", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoOnePlayerAlive);
            bool oneplayer = (bool)privateFieldInfoOnePlayerAlive.GetValue(model)!;
            Assert.IsFalse(oneplayer);

            FieldInfo? privateFieldInfoTimeAfterDeath = typeModel.GetField("_timeAfterOnePlayerAlive", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoTimeAfterDeath);
            int timeAfterDeath = (int)privateFieldInfoTimeAfterDeath.GetValue(model)!;
            Assert.IsTrue(timeAfterDeath == 0);

            FieldInfo? privateFieldInfoAvailablePowerUps = typeModel.GetField("_availablePowerUps", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoAvailablePowerUps);
            PowerUpEnum[] powerUps = (PowerUpEnum[])privateFieldInfoAvailablePowerUps.GetValue(model)!;
            Assert.IsTrue(powerUps.Length == 1);
            Assert.IsTrue(powerUps[0] == PowerUpEnum.BombNumberIncreasePowerUp);

            FieldInfo? privateFieldInfoAvailablePowerDowns = typeModel.GetField("_availablePowerDowns", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoAvailablePowerDowns);
            PowerDownEnum[] currpowerDowns = (PowerDownEnum[])privateFieldInfoAvailablePowerDowns.GetValue(model)!;
            Assert.IsTrue(currpowerDowns.Length == 1);
            Assert.IsTrue(currpowerDowns[0] == PowerDownEnum.InstantBombPlacementPowerDown);

            #endregion

            #region Checking First player fields

            FieldInfo? privateFieldInfoPlayers = type.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type playerType = typeof(Player);

            FieldInfo? privateFieldInfoId = playerType.GetField("_id", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoId);
            int id = (int)privateFieldInfoId.GetValue(players[0])!;
            Assert.IsTrue(id == 0);

            FieldInfo? currprivateFieldInfoAvailablePowerUps = playerType.GetField("_availablePowerUps", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(currprivateFieldInfoAvailablePowerUps);
            PowerUpEnum[] playerPowerUps = (PowerUpEnum[])currprivateFieldInfoAvailablePowerUps.GetValue(players[0])!;
            Assert.IsTrue(powerUps == playerPowerUps);

            FieldInfo? currprivateFieldInfoAvailableDownUps = playerType.GetField("_availablePowerDowns", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(currprivateFieldInfoAvailableDownUps);
            PowerDownEnum[] playerPowerDowns = (PowerDownEnum[])currprivateFieldInfoAvailableDownUps.GetValue(players[0])!;
            Assert.IsTrue(PowerDownEnum.InstantBombPlacementPowerDown == playerPowerDowns[0]);

            FieldInfo? privateFieldInfoPoints = playerType.GetField("_points", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPoints);
            int points = (int)privateFieldInfoPoints.GetValue(players[0])!;
            Assert.IsTrue(points == 0);

            FieldInfo? privateInfoSpeed = playerType.GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoSpeed);
            int speed = (int)privateInfoSpeed.GetValue(players[0])!;
            Assert.IsTrue(speed == 10);

            FieldInfo? privateInfoMoveCoolDown = playerType.GetField("_moveCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoMoveCoolDown);
            int moveCoolDown = (int)privateInfoMoveCoolDown.GetValue(players[0])!;
            Assert.IsTrue(moveCoolDown == speed);

            FieldInfo? privateInfoCanMove = playerType.GetField("_canMove", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoCanMove);
            bool canMove = (bool)privateInfoCanMove.GetValue(players[0])!;
            Assert.IsFalse(canMove);

            FieldInfo? privateInfoAlive = playerType.GetField("_alive", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoAlive);
            bool alive = (bool)privateInfoAlive.GetValue(players[0])!;
            Assert.IsTrue(alive);

            FieldInfo? privateFieldInfoNumberOfBombs = playerType.GetField("_numberOfBombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoNumberOfBombs);
            int numberOfBombs = (int)privateFieldInfoNumberOfBombs.GetValue(players[0])!;
            Assert.IsTrue(numberOfBombs == 2);

            FieldInfo? privateInfoBombs = playerType.GetField("_bombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoBombs);
            List<Bomb> bombs = (List<Bomb>)privateInfoBombs.GetValue(players[0])!;
            Assert.IsNotNull(bombs);
            Assert.IsTrue(bombs.Count == 1);
            Assert.IsTrue(bombs[0].Alive);
            Assert.IsTrue(bombs[0].PosX == 1);
            Assert.IsTrue(bombs[0].PosY == 1);
            Assert.IsTrue(bombs[0].Time == 100);

            FieldInfo? privateFieldInfoRange = playerType.GetField("_range", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoRange);
            int range = (int)privateFieldInfoRange.GetValue(players[0])!;
            Assert.IsTrue(range == 2);

            FieldInfo? privateInfoBaseCoolDownValue = playerType.GetField("_baseCoolDownValue", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoBaseCoolDownValue);
            int baseCoolDOwn = (int)privateInfoBaseCoolDownValue.GetValue(players[0])!;
            Assert.IsTrue(baseCoolDOwn == 500);

            FieldInfo? privateInvinvcibility = playerType.GetField("_invincibility", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInvinvcibility);
            bool invincibility = (bool)privateInvinvcibility.GetValue(players[0])!;
            Assert.IsFalse(invincibility);

            FieldInfo? privateInfoDetonator = playerType.GetField("_detonator", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoDetonator);
            bool detonator = (bool)privateInfoDetonator.GetValue(players[0])!;
            Assert.IsFalse(detonator);

            FieldInfo? privateInfoGhostMode = playerType.GetField("_ghostMode", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoGhostMode);
            bool ghostMode = (bool)privateInfoGhostMode.GetValue(players[0])!;
            Assert.IsFalse(ghostMode);

            FieldInfo? privateInfoNumberOfObstackles = playerType.GetField("_numberOfObstacles", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoNumberOfObstackles);
            int numberOfObstackles = (int)privateInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstackles == 0);

            FieldInfo? priavteInfoCanPlaceBomb = playerType.GetField("_canPlaceBomb", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(priavteInfoCanPlaceBomb);
            bool canPlaceBomb = (bool)priavteInfoCanPlaceBomb.GetValue(players[0])!;
            Assert.IsTrue(canPlaceBomb);

            FieldInfo? privateInstantPlacingBombs = playerType.GetField("_instantPlacingBombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInstantPlacingBombs);
            bool instantPlacingBomb = (bool)privateInstantPlacingBombs.GetValue(players[0])!;
            Assert.IsFalse(instantPlacingBomb);

            #endregion

            #region Checking Monsters

            FieldInfo? privateFieldInfoNewMonsters = typeModel.GetField("_monstersOnBoard", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoNewMonsters);
            List<MonsterBase> monstersOnBoard = (List<MonsterBase>)privateFieldInfoNewMonsters.GetValue(model)!;
            Assert.IsNotNull(monstersOnBoard);
            Assert.IsTrue(monstersOnBoard.Count == 1);
            Assert.IsTrue(monstersOnBoard[0].PosX == 1);
            Assert.IsTrue(monstersOnBoard[0].PosY == 10);
            Assert.IsTrue(monstersOnBoard[0].Alive);

            #endregion

            FileInfo file = new FileInfo(testpath);
            if (File.Exists(testpath))
            {
                file.Delete();
            }
        }

        [TestMethod]
        public void TestListSaves()
        {
            (List<string> Paths, List<string> Titles) tuple = DataAccess.ListSaves();
            Assert.IsNotNull(tuple);
            string[] paths = tuple.Paths.ToArray();

            /*if (paths.Length > 0)
            {
                string firstpath = paths[0];
                Model model = new Model(firstpath);
                Assert.IsNotNull(model);

                Type typeModel = typeof(Model);
                FieldInfo? privateFieldInfoCPlayerNum = typeModel.GetField("_numberOfPlayers", BindingFlags.NonPublic | BindingFlags.Instance);
                Assert.IsNotNull(privateFieldInfoCPlayerNum);
                int numberOfPlayer = (int)privateFieldInfoCPlayerNum.GetValue(model)!;
                Assert.IsTrue(numberOfPlayer == 3);

                model.SaveModel();
            }*/
        }

        [TestMethod]
        public void TestGenerateMonster()
        {
            BlockTypeEnum[,] board =
            {
                { BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
                { BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
                { BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path }
            };
            PowerUpEnum[] powerups = new PowerUpEnum[0];
            PowerDownEnum[] powerDowns = new PowerDownEnum[0];

            Model model = new Model(0, 2, powerups, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Type type = typeof(Model);
            Assert.IsNotNull(model);
            FieldInfo fieldInfo = type.GetField("_monsterSpawnsProbability", BindingFlags.NonPublic | BindingFlags.Instance)!;
            fieldInfo.SetValue(model, 4);

            fieldInfo = type.GetField("_boardWidth", BindingFlags.NonPublic | BindingFlags.Instance)!;
            fieldInfo.SetValue(model, 3);

            fieldInfo = type.GetField("_boardHeight", BindingFlags.NonPublic | BindingFlags.Instance)!;
            fieldInfo.SetValue(model, 3);

            fieldInfo = type.GetField("_testMonsters", BindingFlags.NonPublic | BindingFlags.Instance)!;
            fieldInfo.SetValue(model, true);

            model.Board = board;
            Assert.IsNotNull(model.Board);
            Assert.IsTrue(model.Board[0, 0] == BlockTypeEnum.Path);

            MethodInfo? methodInfo = type.GetMethod("GenerateMonster", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            methodInfo.Invoke(model, null);

            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.Monster 
                || model.Board[1, 1] == BlockTypeEnum.GhostMonsterOnPath
                || model.Board[1, 1] == BlockTypeEnum.DijkstraMonster
                || model.Board[1, 1] == BlockTypeEnum.HeuristicMonster);

            //setting it back to false
            fieldInfo = type.GetField("_testMonsters", BindingFlags.NonPublic | BindingFlags.Instance)!;
            fieldInfo.SetValue(model, false);
        }

        [TestMethod]
        public void TestIsPLayerNearby()
        {
            BlockTypeEnum[,] board =
            {
                { BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
                { BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
                { BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path }
            };
            PowerUpEnum[] powerups = new PowerUpEnum[0];
            PowerDownEnum[] powerDowns = new PowerDownEnum[0];

            Model model = new Model(0, 2, powerups, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Type type = typeof(Model);
            Assert.IsNotNull(model);

            FieldInfo? fieldInfo = type.GetField("_boardWidth", BindingFlags.NonPublic | BindingFlags.Instance)!;
            fieldInfo.SetValue(model, 3);

            fieldInfo = type.GetField("_boardHeight", BindingFlags.NonPublic | BindingFlags.Instance)!;
            fieldInfo.SetValue(model, 3);

            model.Board = board;
            Assert.IsNotNull(model.Board);
            Assert.IsTrue(model.Board[0, 0] == BlockTypeEnum.Path);

            MethodInfo? methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            object[] param = { 1, 1 };
            Assert.IsFalse((bool)methodInfo?.Invoke(model, param)!);

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 0;
            param[1] = 1;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 2;
            param[1] = 1;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 1;
            param[1] = 0;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 1;
            param[1] = 2;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 0;
            param[1] = 0;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 2;
            param[1] = 2;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 0;
            param[1] = 2;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 2;
            param[1] = 0;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            board[0, 0] = BlockTypeEnum.PlayerA;

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 1;
            param[1] = 1;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            board[0, 0] = BlockTypeEnum.PlayerB;

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 1;
            param[1] = 1;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

            board[0, 0] = BlockTypeEnum.PlayerC;

            methodInfo = type.GetMethod("IsPlayerNearBy", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            param[0] = 1;
            param[1] = 1;
            Assert.IsTrue((bool)methodInfo?.Invoke(model, param)!);

        }
    }
}