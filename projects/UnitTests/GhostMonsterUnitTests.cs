using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class GhostMonsterUnitTests
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
        public void TestGhostMonsterCTor()
        {
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.GhostMonsterOnPath, 2, 2, BlockTypeEnum.GhostMonsterOnPath, 2, 2));

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.GhostMonsterOnPath);

            Assert.IsTrue(monster.PosX == 2);
            Assert.IsTrue(monster.PosY == 2);
            Assert.IsTrue(monster.Alive);

            Type type = typeof(GhostMonster);

            FieldInfo? privateFieldInfo = type.GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int speed = (int)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(speed == 60);

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
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.GhostMonsterOnPath, 2, 2, BlockTypeEnum.GhostMonsterOnPath, 2, 2));

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.GhostMonsterOnPath);

            Type type = typeof(GhostMonster);

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
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.GhostMonsterOnPath, 2, 2, BlockTypeEnum.GhostMonsterOnPath, 2, 2));

            for (int i = 0; i < 59; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.GhostMonsterOnPath);

            _board[2, 2] = BlockTypeEnum.Explosion;
            _board[1, 2] = BlockTypeEnum.Explosion;
            _board[3, 2] = BlockTypeEnum.Explosion;
            _board[2, 1] = BlockTypeEnum.Explosion;
            _board[2, 3] = BlockTypeEnum.Explosion;
            Assert.IsTrue(monster.Alive);
            monster.OnTimerTick(_board);
            Assert.IsFalse(monster.Alive);
        }

        [TestMethod]
        public void TestMoveOnPath()
        {
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(monster.Alive);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.GhostMonsterOnPath || _board[3, 2] == BlockTypeEnum.GhostMonsterOnPath ||
                _board[2, 3] == BlockTypeEnum.GhostMonsterOnPath || _board[2, 1] == BlockTypeEnum.GhostMonsterOnPath);
        }

        [TestMethod]
        public void TestMoveOnWall()
        {
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[2, 1] = BlockTypeEnum.Wall;
            _board[2, 3] = BlockTypeEnum.Wall;

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(monster.Alive);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.GhostMonsterOnWall || _board[3, 2] == BlockTypeEnum.GhostMonsterOnWall ||
                _board[2, 3] == BlockTypeEnum.GhostMonsterOnWall || _board[2, 1] == BlockTypeEnum.GhostMonsterOnWall);
        }

        [TestMethod]
        public void MoveTroughWalls()
        {
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[2, 1] = BlockTypeEnum.Wall;
            _board[2, 3] = BlockTypeEnum.Wall;

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(monster.Alive);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.GhostMonsterOnWall || _board[3, 2] == BlockTypeEnum.GhostMonsterOnWall ||
                _board[2, 3] == BlockTypeEnum.GhostMonsterOnWall || _board[2, 1] == BlockTypeEnum.GhostMonsterOnWall);

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(monster.Alive);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.Wall || _board[3, 2] == BlockTypeEnum.Wall || _board[2, 3] == BlockTypeEnum.Wall || _board[2, 1] == BlockTypeEnum.Wall);
        }

        [TestMethod]
        public void TestMoveOnBox()
        {
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Box;
            _board[3, 2] = BlockTypeEnum.Box;
            _board[2, 1] = BlockTypeEnum.Box;
            _board[2, 3] = BlockTypeEnum.Box;

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(monster.Alive);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.GhostMonsterOnBox || _board[3, 2] == BlockTypeEnum.GhostMonsterOnBox ||
                _board[2, 3] == BlockTypeEnum.GhostMonsterOnBox || _board[2, 1] == BlockTypeEnum.GhostMonsterOnBox);
        }

        [TestMethod]
        public void TestMoveOnBoxPlacedByPlayer()
        {
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.BoxPlacedByPlayer;
            _board[3, 2] = BlockTypeEnum.BoxPlacedByPlayer;
            _board[2, 1] = BlockTypeEnum.BoxPlacedByPlayer;
            _board[2, 3] = BlockTypeEnum.BoxPlacedByPlayer;

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(monster.Alive);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.GhostMonsterOnBox || _board[3, 2] == BlockTypeEnum.GhostMonsterOnBox ||
                _board[2, 3] == BlockTypeEnum.GhostMonsterOnBox || _board[2, 1] == BlockTypeEnum.GhostMonsterOnBox);
        }


        [TestMethod]
        public void KillPlayer()
        {
            GhostMonster monster = new GhostMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.PlayerA;
            _board[3, 2] = BlockTypeEnum.PlayerA;
            _board[2, 1] = BlockTypeEnum.PlayerA;
            _board[2, 3] = BlockTypeEnum.PlayerA;

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(monster.Alive);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.GhostMonsterOnPath || _board[3, 2] == BlockTypeEnum.GhostMonsterOnPath ||
                _board[2, 3] == BlockTypeEnum.GhostMonsterOnPath || _board[2, 1] == BlockTypeEnum.GhostMonsterOnPath);

            for (int i = 0; i < 60; i++)
            {
                monster.OnTimerTick(_board);
            }
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.Path || _board[3, 2] == BlockTypeEnum.Path || _board[2, 3] == BlockTypeEnum.Path || _board[2, 1] == BlockTypeEnum.Path);
        }
    }
}
