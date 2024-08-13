using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class DijkstraMonsterUnitTests
    {
        BlockTypeEnum[,] _board =
        {
            { BlockTypeEnum.Wall, BlockTypeEnum.Wall ,BlockTypeEnum.Wall, BlockTypeEnum.Wall, BlockTypeEnum.Wall, BlockTypeEnum.Wall, BlockTypeEnum.Wall},
            { BlockTypeEnum.Wall, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Wall},
            { BlockTypeEnum.Wall, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Wall},
            { BlockTypeEnum.Wall, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Wall},
            { BlockTypeEnum.Wall, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Wall},
            { BlockTypeEnum.Wall, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Wall},
            { BlockTypeEnum.Wall, BlockTypeEnum.Wall ,BlockTypeEnum.Wall, BlockTypeEnum.Wall, BlockTypeEnum.Wall, BlockTypeEnum.Wall, BlockTypeEnum.Wall},
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
            DijkstraMonster monster = new DijkstraMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.DijkstraMonster, 2, 2, BlockTypeEnum.DijkstraMonster, 2, 2));

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.DijkstraMonster);

            Assert.IsTrue(monster.PosX == 2);
            Assert.IsTrue(monster.PosY == 2);
            Assert.IsTrue(monster.Alive);

            Type type = typeof(DijkstraMonster);

            FieldInfo? privateFieldInfo = type.GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int speed = (int)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(speed == 20);

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
            DijkstraMonster monster = new DijkstraMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.DijkstraMonster, 2, 2, BlockTypeEnum.DijkstraMonster, 2, 2));

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.DijkstraMonster);

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
            DijkstraMonster monster = new DijkstraMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[2, 1] = BlockTypeEnum.Wall;
            _board[2, 3] = BlockTypeEnum.Wall;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.DijkstraMonster, 2, 2, BlockTypeEnum.DijkstraMonster, 2, 2));

            for (int i = 0; i < 20; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.DijkstraMonster);

            _board[2, 2] = BlockTypeEnum.Explosion;
            Assert.IsTrue(monster.Alive);

            monster.OnTimerTick(_board);

            
            Assert.IsFalse(monster.Alive);
        }

        [TestMethod]
        public void TestMoveWithNoPlayer()
        {
            DijkstraMonster monster = new DijkstraMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.DijkstraMonster, 2, 2, BlockTypeEnum.DijkstraMonster, 2, 2));

            for (int i = 0; i < 20; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsFalse(_board[2, 2] == BlockTypeEnum.DijkstraMonster);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.DijkstraMonster || _board[3, 2] == BlockTypeEnum.DijkstraMonster ||
                _board[2, 1] == BlockTypeEnum.DijkstraMonster || _board[2, 3] == BlockTypeEnum.DijkstraMonster);
        }

        [TestMethod]
        public void TestMoveWithOnePlayerFirstCase()
        {
            DijkstraMonster monster = new DijkstraMonster(3, 3, 7, 7);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            _board[2, 3] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[3, 4] = BlockTypeEnum.Wall;
            _board[5, 3] = BlockTypeEnum.Wall;

            _board[5, 5] = BlockTypeEnum.PlayerA;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.DijkstraMonster, 2, 2, BlockTypeEnum.DijkstraMonster, 2, 2));

            for (int i = 0; i < 20; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsFalse(_board[3, 3] == BlockTypeEnum.DijkstraMonster);
            Assert.IsTrue(_board[4, 3] == BlockTypeEnum.DijkstraMonster);

            for (int i = 0; i < 20; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsFalse(_board[4, 3] == BlockTypeEnum.DijkstraMonster);
            Assert.IsTrue(_board[4, 4] == BlockTypeEnum.DijkstraMonster);

            for (int i = 0; i < 40; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[5, 5] == BlockTypeEnum.DijkstraMonster);
        }

        [TestMethod]
        public void TestMoveWithOnePlayerSecondCase()
        {
            DijkstraMonster monster = new DijkstraMonster(3, 3, 7, 7);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            _board[2, 3] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[3, 4] = BlockTypeEnum.Wall;

            _board[5, 3] = BlockTypeEnum.Wall;
            _board[3, 1] = BlockTypeEnum.Wall;

            _board[2, 4] = BlockTypeEnum.PlayerA;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.DijkstraMonster, 3, 3, BlockTypeEnum.DijkstraMonster, 3, 3));

            
            for (int i = 0; i < 20; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsFalse(_board[3, 3] == BlockTypeEnum.DijkstraMonster);
            Assert.IsTrue(_board[4, 3] == BlockTypeEnum.DijkstraMonster);

            for (int i = 0; i < 20; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsFalse(_board[4, 3] == BlockTypeEnum.DijkstraMonster);
            Assert.IsTrue(_board[4, 4] == BlockTypeEnum.DijkstraMonster);


            for (int i = 0; i < 80; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[2, 4] == BlockTypeEnum.DijkstraMonster);
        }

        [TestMethod]
        public void TestMoveWithTwoPlayers()
        {
            DijkstraMonster monster = new DijkstraMonster(3, 3, 7, 7);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            _board[2, 3] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[3, 4] = BlockTypeEnum.Wall;

            _board[5, 3] = BlockTypeEnum.Wall;
            _board[3, 1] = BlockTypeEnum.Wall;

            _board[2, 4] = BlockTypeEnum.PlayerA;
            _board[5, 1] = BlockTypeEnum.PlayerB;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.DijkstraMonster, 3, 3, BlockTypeEnum.DijkstraMonster, 3, 3));


            for (int i = 0; i < 20; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsFalse(_board[3, 3] == BlockTypeEnum.DijkstraMonster);
            Assert.IsTrue(_board[4, 3] == BlockTypeEnum.DijkstraMonster);

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[5, 1] == BlockTypeEnum.DijkstraMonster);
        }
    }
}
