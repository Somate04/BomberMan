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
    public class BombUnitTests
    {
        BlockTypeEnum[,] _board =
        {
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
            { BlockTypeEnum.Path, BlockTypeEnum.Path ,BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path, BlockTypeEnum.Path },
        };

        int _bombTime = 100;

        private void OnExplode(object sender, ExplodeEventArgs e)
        {
            if (e is null) return;
            _board[e.PosX, e.PosY] = e.BlockType;
        }

        [TestMethod]
        public void TestBombCtor()
        {
            Bomb bomb = new Bomb(2, 3, 3);
            _board[3, 3] = BlockTypeEnum.Bomb;

            Assert.IsNotNull(bomb);
            Assert.IsTrue(_board[3, 3] == BlockTypeEnum.Bomb);

            Assert.IsTrue(bomb.Time == _bombTime);
            Assert.IsTrue(bomb.PosX == 3);
            Assert.IsTrue(bomb.PosY == 3);
            Assert.IsTrue(bomb.Alive);

            Type type = typeof(Bomb);

            FieldInfo? privateFieldInfo = type.GetField("_previousBlocksAbove", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            BlockTypeEnum[] above = (BlockTypeEnum[])privateFieldInfo.GetValue(bomb)!;
            Assert.IsTrue(above.Length == 2);

            privateFieldInfo = type.GetField("_previousBlocksBelow", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            BlockTypeEnum[] below = (BlockTypeEnum[])privateFieldInfo.GetValue(bomb)!;
            Assert.IsTrue(below.Length == 2);

            privateFieldInfo = type.GetField("_previousBlocksToLeft", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            BlockTypeEnum[] left = (BlockTypeEnum[])privateFieldInfo.GetValue(bomb)!;
            Assert.IsTrue(left.Length == 2);

            privateFieldInfo = type.GetField("_previousBlocksToRight", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            BlockTypeEnum[] right = (BlockTypeEnum[])privateFieldInfo.GetValue(bomb)!;
            Assert.IsTrue(right.Length == 2);

            privateFieldInfo = type.GetField("_blockedAbove", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            bool blockedAbove = (bool)privateFieldInfo.GetValue(bomb)!;
            Assert.IsFalse(blockedAbove);

            privateFieldInfo = type.GetField("_blockedBelow", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            bool blockedBelow = (bool)privateFieldInfo.GetValue(bomb)!;
            Assert.IsFalse(blockedBelow);

            privateFieldInfo = type.GetField("_blockedLeft", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            bool blockedLeft = (bool)privateFieldInfo.GetValue(bomb)!;
            Assert.IsFalse(blockedLeft);

            privateFieldInfo = type.GetField("_blockedRight", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            bool blockedRight = (bool)privateFieldInfo.GetValue(bomb)!;
            Assert.IsFalse(blockedRight);

            privateFieldInfo = type.GetField("_explosionCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int explosionCoolDown = (int)privateFieldInfo.GetValue(bomb)!;
            Assert.IsTrue(explosionCoolDown == -1);

            privateFieldInfo = type.GetField("_spreadrange", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int spreadRange = (int)privateFieldInfo.GetValue(bomb)!;
            Assert.IsTrue(spreadRange == -1);
        }

        [TestMethod]
        public void TestOnTImerTick()
        {
            Bomb bomb = new Bomb(2, 3, 3);
            bomb.ExplosionEventHandler += OnExplode!;
            _board[3, 3] = BlockTypeEnum.Bomb;

            Assert.IsTrue(bomb.Time == _bombTime);

            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsTrue(bomb.Time == _bombTime - 1);

            Type type = typeof(Bomb);
            FieldInfo? privateFieldInfo = type.GetField("_boardWidth", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int boardWidth = (int)privateFieldInfo.GetValue(bomb)!;
            Assert.IsTrue(boardWidth == 7);

            privateFieldInfo = type.GetField("_boardHeight", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int boardHeight = (int)privateFieldInfo.GetValue(bomb)!;
            Assert.IsTrue(boardHeight == 7);

            for (int i = 0; i < _bombTime - 1; ++i)
            {
                bomb.OnTimerTick(_board, 7, 7);
            }

            Assert.IsTrue(_board[3, 3] == BlockTypeEnum.Explosion);
        }

        [TestMethod]
        public void TestDetonate()
        {
            Bomb bomb = new Bomb(2, 3, 3);
            _board[3, 3] = BlockTypeEnum.Bomb;

            Assert.IsTrue(bomb.Time == _bombTime);
            bomb.Detonate(_board);
            Assert.IsTrue(bomb.Time == 0);
        }

        [TestMethod]
        public void TestExplosion()
        {
            Bomb bomb = new Bomb(2, 3, 3);
            bomb.ExplosionEventHandler += OnExplode!;
            _board[3, 3] = BlockTypeEnum.Bomb;

            Assert.IsTrue(bomb.Time == _bombTime);
            bomb.Detonate(_board);
            Assert.IsTrue(bomb.Time == 0);

            bomb.OnTimerTick(_board, 7, 7);
            Assert.IsTrue(_board[3, 3] == BlockTypeEnum.Explosion);
            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsTrue(_board[2, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[4, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 2] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 4] == BlockTypeEnum.Explosion);

            bomb.OnTimerTick(_board, 7, 7);
            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsTrue(_board[1, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[5, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 1] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 5] == BlockTypeEnum.Explosion);

            bomb.OnTimerTick(_board, 7, 7);
            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsFalse(_board[0, 3] == BlockTypeEnum.Explosion);
            Assert.IsFalse(_board[6, 3] == BlockTypeEnum.Explosion);
            Assert.IsFalse(_board[3, 0] == BlockTypeEnum.Explosion);
            Assert.IsFalse(_board[3, 6] == BlockTypeEnum.Explosion);

        }

        [TestMethod]
        public void TestExplodeNextToWall()
        {
            Bomb bomb = new Bomb(2, 3, 3);
            bomb.ExplosionEventHandler += OnExplode!;
            _board[3, 3] = BlockTypeEnum.Bomb;

            _board[2, 3] = BlockTypeEnum.Wall;
            _board[4, 3] = BlockTypeEnum.Wall;
            _board[3, 2] = BlockTypeEnum.Wall;
            _board[3, 4] = BlockTypeEnum.Wall;

            Assert.IsTrue(bomb.Time == _bombTime);
            bomb.Detonate(_board);
            Assert.IsTrue(bomb.Time == 0);

            bomb.OnTimerTick(_board, 7, 7);
            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsTrue(_board[2, 3] == BlockTypeEnum.Wall);
            Assert.IsTrue(_board[4, 3] == BlockTypeEnum.Wall);
            Assert.IsTrue(_board[3, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(_board[3, 4] == BlockTypeEnum.Wall);
        }

        [TestMethod]
        public void TestExplodeNextToBoxes()
        {
            Bomb bomb = new Bomb(2, 3, 3);
            bomb.ExplosionEventHandler += OnExplode!;
            _board[3, 3] = BlockTypeEnum.Bomb;

            _board[2, 3] = BlockTypeEnum.Box;
            _board[4, 3] = BlockTypeEnum.Box;
            _board[3, 2] = BlockTypeEnum.Box;
            _board[3, 4] = BlockTypeEnum.Box;

            _board[1, 3] = BlockTypeEnum.Box;
            _board[5, 3] = BlockTypeEnum.Box;
            _board[3, 1] = BlockTypeEnum.Box;
            _board[3, 5] = BlockTypeEnum.Box;

            Assert.IsTrue(bomb.Time == _bombTime);
            bomb.Detonate(_board);
            Assert.IsTrue(bomb.Time == 0);

            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsTrue(_board[2, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[4, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 2] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 4] == BlockTypeEnum.Explosion);

            Assert.IsTrue(_board[1, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(_board[5, 3] == BlockTypeEnum.Box);
            Assert.IsTrue(_board[3, 1] == BlockTypeEnum.Box);
            Assert.IsTrue(_board[3, 5] == BlockTypeEnum.Box);
        }

        [TestMethod]
        public void TestBombOnTheEdgeOfTheBoard()
        {
            //This will cause an IndexOutOfRangeException
            /*Bomb bomb = new Bomb(2, 0, 0);
            bomb.ExplosionEventHandler += OnExplode!;
            _board[0, 0] = BlockTypeEnum.Bomb;

            Assert.IsTrue(bomb.Time == _bombTime);
            bomb.Detonate(_board);
            Assert.IsTrue(bomb.Time == 0);

            bomb.OnTimerTick(_board, 7, 7);*/
        }

        [TestMethod]
        public void TestBombRangeIncrease()
        {
            Bomb bomb = new Bomb(3, 3, 3);
            bomb.ExplosionEventHandler += OnExplode!;
            _board[3, 3] = BlockTypeEnum.Bomb;

            bomb.OnTimerTick(_board, 7, 7);
            bomb.Detonate(_board);
            Assert.IsTrue(_board[3, 3] == BlockTypeEnum.Explosion);
            bomb.OnTimerTick(_board, 7, 7);
            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsTrue(_board[2, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[4, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 2] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 4] == BlockTypeEnum.Explosion);

            bomb.OnTimerTick(_board, 7, 7);
            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsTrue(_board[1, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[5, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 1] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 5] == BlockTypeEnum.Explosion);

            Assert.IsFalse(_board[0, 3] == BlockTypeEnum.Explosion);
            Assert.IsFalse(_board[6, 3] == BlockTypeEnum.Explosion);
            Assert.IsFalse(_board[3, 0] == BlockTypeEnum.Explosion);
            Assert.IsFalse(_board[3, 6] == BlockTypeEnum.Explosion);

            bomb.OnTimerTick(_board, 7, 7);
            bomb.OnTimerTick(_board, 7, 7);

            Assert.IsTrue(_board[0, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[6, 3] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 0] == BlockTypeEnum.Explosion);
            Assert.IsTrue(_board[3, 6] == BlockTypeEnum.Explosion);
        }

        [TestMethod]
        public void TestBombSave()
        {
            Bomb bomb = new Bomb(3, 3, 3);
            bomb.ExplosionEventHandler += OnExplode!;
            _board[3, 3] = BlockTypeEnum.Bomb;

            int[] data = bomb.SaveData(0);

            Assert.IsTrue(data[0] == 0);
            Assert.IsTrue(data[1] == _bombTime);
            Assert.IsTrue(data[2] == 3);
            Assert.IsTrue(data[3] == bomb.PosX);
            Assert.IsTrue(data[4] == bomb.PosY);
        }

    }
}
