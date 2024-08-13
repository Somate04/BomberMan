using BomberWars_MP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class MonsterUnitTests
    {
        BlockTypeEnum[,] _board =
        {
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path},
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path},
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path},
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path},
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path}
        };

        private void OnMove(object sender, BoardChangeEventArgs e)
        {
            if (e is null) return;
            if (_board is null) return;

            _board[e._prevPosX, e._prevPosY] = e._prevtype;
            _board[e._currentPosX, e._currentPosY] = e._currenttype;
        }

        [TestMethod]
        public void TestMonsterCTor()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Monster);

            Assert.IsTrue(monster.PosX == 2);
            Assert.IsTrue(monster.PosY == 2);
            Assert.IsTrue(monster.Alive);

            Type type = typeof(Monster);

            FieldInfo? privateFieldInfo = type.GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int speed = (int)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(speed == 40);

            privateFieldInfo = type.GetField("_moveCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int moveCoolDown = (int)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(moveCoolDown == 0);

            privateFieldInfo = type.GetField("_currentBlock", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            BlockTypeEnum previous = (BlockTypeEnum)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(previous == BlockTypeEnum.Path);

            privateFieldInfo = type.GetField("_direction", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            DirectionEnum direction = (DirectionEnum)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(direction == DirectionEnum.Up || direction == DirectionEnum.Down || direction == DirectionEnum.Right || direction == DirectionEnum.Left);
        }

        [TestMethod]
        public void TestOnTimerTick()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Monster);

            Type type = typeof(Monster);

            FieldInfo? privateFieldInfo = type.GetField("_moveCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            privateFieldInfo.SetValue(monster, 5);
            int moveCoolDown = (int)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(moveCoolDown == 5);

            monster.OnTimerTick(_board);

            moveCoolDown = (int)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(moveCoolDown == 6);
        }

        [TestMethod]
        public void TestKill()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Explosion;
            _board[3, 2] = BlockTypeEnum.Explosion;
            _board[2, 1] = BlockTypeEnum.Explosion;
            _board[2, 3] = BlockTypeEnum.Explosion;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));
            Assert.IsTrue(monster.Alive);

            for (int i = 0; i < 40; i++)
            {
                monster.OnTimerTick(_board);
            }

            monster.OnTimerTick(_board);
            monster.OnTimerTick(_board);
            Assert.IsFalse(monster.Alive);
        }

        [TestMethod]
        public void TestMoveRight()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[2, 1] = BlockTypeEnum.Wall;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Monster);

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(_board[2, 3] == BlockTypeEnum.Monster);
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[2, 3] = BlockTypeEnum.Wall;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Monster);

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(_board[2, 1] == BlockTypeEnum.Monster);
        }

        [TestMethod]
        public void TestMoveDown()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Wall;
            _board[2, 1] = BlockTypeEnum.Wall;
            _board[2, 3] = BlockTypeEnum.Wall;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Monster);

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(_board[3, 2] == BlockTypeEnum.Monster);
        }

        [TestMethod]
        public void TestMoveUp()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[3, 2] = BlockTypeEnum.Wall;
            _board[2, 1] = BlockTypeEnum.Wall;
            _board[2, 3] = BlockTypeEnum.Wall;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Monster);

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.Monster);
        }

        [TestMethod]
        public void TestMoveGenerally()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Monster);

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.Monster || _board[3, 2] == BlockTypeEnum.Monster || _board[2, 1] == BlockTypeEnum.Monster || _board[2, 3] == BlockTypeEnum.Monster);
        }

        [TestMethod]
        public void TestMoveOnPlayer()
        {
            Monster monster = new Monster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[3, 2] = BlockTypeEnum.PlayerA;
            _board[1, 2] = BlockTypeEnum.PlayerA;
            _board[2, 1] = BlockTypeEnum.PlayerB;
            _board[2, 3] = BlockTypeEnum.PlayerC;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 2, 2, BlockTypeEnum.Monster, 2, 2));

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Monster);

            for (int i = 0; i < 39; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.Monster || _board[3, 2] == BlockTypeEnum.Monster || _board[2, 1] == BlockTypeEnum.Monster || _board[2, 3] == BlockTypeEnum.Monster);
        }

        [TestMethod]
        public void MoveNextToEdgeBoard()
        {
            Monster monster = new Monster(0, 0, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 0] = BlockTypeEnum.Wall;
            _board[1, 1] = BlockTypeEnum.Wall;
            _board[0, 1] = BlockTypeEnum.Wall;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 0, 0, BlockTypeEnum.Monster, 0, 0));

            for (int i = 0; i < 20; ++i)
            {

                for (int j = 0; j < 39; j++)
                {
                    monster.OnTimerTick(_board);
                }

                Assert.IsTrue(_board[0, 0] == BlockTypeEnum.Monster);
            }
        }

        [TestMethod]
        public void TestSaveData()
        {
            Monster monster = new Monster(0, 0, 5, 5);
            Assert.IsNotNull(monster);

            _board[1, 0] = BlockTypeEnum.Wall;
            _board[1, 1] = BlockTypeEnum.Wall;
            _board[0, 1] = BlockTypeEnum.Wall;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.Monster, 0, 0, BlockTypeEnum.Monster, 0, 0));

            for (int i = 0; i < 20; ++i)
            {

                for (int j = 0; j < 39; j++)
                {
                    monster.OnTimerTick(_board);
                }

                Assert.IsTrue(_board[0, 0] == BlockTypeEnum.Monster);
            }

            int[] data = monster.SaveData();

            Assert.IsTrue(data[0] == 0);
            Assert.IsTrue(data[1] == 40);
            Assert.IsTrue(data[2] == 780);
            Assert.IsTrue((DirectionEnum)data[3] == DirectionEnum.Right || (DirectionEnum)data[3] == DirectionEnum.Left || (DirectionEnum)data[3] == DirectionEnum.Up || (DirectionEnum)data[3] == DirectionEnum.Down);
            Assert.IsTrue(data[4] == 0);
            Assert.IsTrue(data[5] == 0);
            Assert.IsTrue(data[6] == 1);

        }
    }
}
