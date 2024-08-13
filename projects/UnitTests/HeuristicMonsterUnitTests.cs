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
    public class HeuristicMonsterUnitTests
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
            HeuristicMonster monster = new HeuristicMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.HeuristicMonster, 2, 2, BlockTypeEnum.HeuristicMonster, 2, 2));

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.HeuristicMonster);

            Assert.IsTrue(monster.PosX == 2);
            Assert.IsTrue(monster.PosY == 2);
            Assert.IsTrue(monster.Alive);

            Type type = typeof(HeuristicMonster);

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
            HeuristicMonster monster = new HeuristicMonster(2, 2, 5, 5);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.HeuristicMonster, 2, 2, BlockTypeEnum.HeuristicMonster, 2, 2));

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.HeuristicMonster);

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
            HeuristicMonster monster = new HeuristicMonster(2, 2, 7, 7);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            _board[1, 2] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[2, 1] = BlockTypeEnum.Wall;
            _board[2, 3] = BlockTypeEnum.Wall;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.HeuristicMonster, 2, 2, BlockTypeEnum.HeuristicMonster, 2, 2));

            for (int i = 0; i < 40; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsTrue(_board[2, 2] == BlockTypeEnum.HeuristicMonster);

            _board[2, 2] = BlockTypeEnum.Explosion;
            Assert.IsTrue(monster.Alive);

            monster.OnTimerTick(_board);


            Assert.IsFalse(monster.Alive);
        }

        [TestMethod]
        public void TestMoveWithNoPlayer()
        {
            HeuristicMonster monster = new HeuristicMonster(2, 2, 7, 7);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.HeuristicMonster, 2, 2, BlockTypeEnum.HeuristicMonster, 2, 2));

            for (int i = 0; i < 40; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsFalse(_board[2, 2] == BlockTypeEnum.HeuristicMonster);
            Assert.IsTrue(_board[1, 2] == BlockTypeEnum.HeuristicMonster || _board[3, 2] == BlockTypeEnum.HeuristicMonster ||
                _board[2, 1] == BlockTypeEnum.HeuristicMonster || _board[2, 3] == BlockTypeEnum.HeuristicMonster);
        }

        [TestMethod]
        public void TestFindPlayer()
        {
            HeuristicMonster monster = new HeuristicMonster(3, 3, 7, 7);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;
            _board[2, 3] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[3, 4] = BlockTypeEnum.Wall;
            _board[5, 3] = BlockTypeEnum.Wall;

            _board[5, 5] = BlockTypeEnum.PlayerA;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.HeuristicMonster, 3, 3, BlockTypeEnum.HeuristicMonster, 3, 3));

            Type type = typeof(HeuristicMonster);

            /*MethodInfo? methodInfo = type.GetMethod("FindShortestPath", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(methodInfo);
            object[] param = { _board };
            methodInfo?.Invoke(monster, param);*/

            monster.FindShortestPath(_board);

            FieldInfo? privateFieldInfo = type.GetField("_shortestPath", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            List<(int, int)> shortestPaths = (List<(int, int)>)privateFieldInfo.GetValue(monster)!;
            Assert.IsTrue(shortestPaths[0].Item1 == 5);
            Assert.IsTrue(shortestPaths[0].Item2 == 5);
            Assert.IsTrue(shortestPaths[1].Item1 == 5);
            Assert.IsTrue(shortestPaths[1].Item2 == 4);
            Assert.IsTrue(shortestPaths[2].Item1 == 4);
            Assert.IsTrue(shortestPaths[2].Item2 == 4);
            Assert.IsTrue(shortestPaths[3].Item1 == 4);
            Assert.IsTrue(shortestPaths[3].Item2 == 3);
        }

        [TestMethod]
        public void TestMove()
        {
            HeuristicMonster monster = new HeuristicMonster(3, 3, 7, 7);
            Assert.IsNotNull(monster);
            monster.OnBoardChangeEventHandler += OnMove!;

            OnMove(this, new BoardChangeEventArgs(BlockTypeEnum.HeuristicMonster, 3, 3, BlockTypeEnum.HeuristicMonster, 3, 3));

            Type type = typeof(HeuristicMonster);

            Assert.IsTrue(_board[3, 3] == BlockTypeEnum.HeuristicMonster);

            for (int i = 0; i < 40; i++)
            {
                monster.OnTimerTick(_board);
            }

            Assert.IsFalse(_board[3, 3] == BlockTypeEnum.HeuristicMonster);
            Assert.IsTrue(_board[3, 2] == BlockTypeEnum.HeuristicMonster || _board[3, 4] == BlockTypeEnum.HeuristicMonster 
                || _board[2, 3] == BlockTypeEnum.HeuristicMonster || _board[4, 3] == BlockTypeEnum.HeuristicMonster);

        }

    }
}
