using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BomberWars_MP.Model;

namespace UnitTests
{
    [TestClass]
    public class PlayerUnitTests
    {
        PowerUpEnum[] powerUps = {
            PowerUpEnum.BombNumberIncreasePowerUp,
            PowerUpEnum.BombRangeIncreasePowerUp,
            PowerUpEnum.DetonatorPowerUp,
            PowerUpEnum.RollerSkatePowerUp,
            PowerUpEnum.InvincibilityPowerUp,
            PowerUpEnum.GhostPowerUp,
            PowerUpEnum.ObstaclePowerUp
            };

        PowerDownEnum[] powerDowns =
        {
            PowerDownEnum.SlowDownPowerDown,
            PowerDownEnum.BombRangeDecreasePowerDown,
            PowerDownEnum.DisableBombPlacementPowerDown,
            PowerDownEnum.InstantBombPlacementPowerDown
            };

        [TestMethod]
        public void TestsCtorPplayer()
        {
            Player player = new Player(0, powerUps, powerDowns);
            Assert.IsNotNull(player);

            Type type = typeof(Player);

            FieldInfo? privateFieldInfoId = type.GetField("_id", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoId);
            int id = (int)privateFieldInfoId.GetValue(player)!;
            Assert.IsTrue(id == 0);

            FieldInfo? privateFieldInfoAvailablePowerUps = type.GetField("_availablePowerUps", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoAvailablePowerUps);
            PowerUpEnum[] playerPowerUps = (PowerUpEnum[])privateFieldInfoAvailablePowerUps.GetValue(player)!;
            Assert.IsTrue(powerUps == playerPowerUps);

            FieldInfo? privateFieldInfoAvailablePowerDowns = type.GetField("_availablePowerDowns", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoAvailablePowerDowns);
            PowerDownEnum[] playerPowerDowns = (PowerDownEnum[])privateFieldInfoAvailablePowerDowns.GetValue(player)!;
            Assert.IsTrue(powerDowns == playerPowerDowns);

            FieldInfo? privateFieldInfoPoints = type.GetField("_points", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPoints);
            int points = (int)privateFieldInfoPoints.GetValue(player)!;
            Assert.IsTrue(points == 0);

            FieldInfo? privateInfoSpeed = type.GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoSpeed);
            int speed = (int)privateInfoSpeed.GetValue(player)!;
            Assert.IsTrue(speed == 10);

            FieldInfo? privateInfoMoveCoolDown = type.GetField("_moveCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoMoveCoolDown);
            int moveCoolDown = (int)privateInfoMoveCoolDown.GetValue(player)!;
            Assert.IsTrue(moveCoolDown == speed);

            FieldInfo? privateInfoCanMove = type.GetField("_canMove", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoCanMove);
            bool canMove = (bool)privateInfoCanMove.GetValue(player)!;
            Assert.IsTrue(canMove);

            FieldInfo? privateInfoAlive = type.GetField("_alive", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoAlive);
            bool alive = (bool)privateInfoAlive.GetValue(player)!;
            Assert.IsTrue(alive);

            FieldInfo? privateFieldInfoNumberOfBombs = type.GetField("_numberOfBombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoNumberOfBombs);
            int numberOfBombs = (int)privateFieldInfoNumberOfBombs.GetValue(player)!;
            Assert.IsTrue(numberOfBombs == 2);

            FieldInfo? privateInfoBombs = type.GetField("_bombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoBombs);
            List<Bomb> bombs = (List<Bomb>)privateInfoBombs.GetValue(player)!;
            Assert.IsNotNull(bombs);
            Assert.IsTrue(bombs.Count == 0);

            FieldInfo? privateFieldInfoRange = type.GetField("_range", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoRange);
            int range = (int)privateFieldInfoRange.GetValue(player)!;
            Assert.IsTrue(range == 2);

            FieldInfo? privateInfoBaseCoolDownValue = type.GetField("_baseCoolDownValue", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoBaseCoolDownValue);
            int baseCoolDOwn = (int)privateInfoBaseCoolDownValue.GetValue(player)!;
            Assert.IsTrue(baseCoolDOwn == 500);

            FieldInfo? privateInvinvcibility = type.GetField("_invincibility", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInvinvcibility);
            bool invincibility = (bool)privateInvinvcibility.GetValue(player)!;
            Assert.IsFalse(invincibility);

            FieldInfo? privateInfoDetonator = type.GetField("_detonator", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoDetonator);
            bool detonator = (bool)privateInfoDetonator.GetValue(player)!;
            Assert.IsFalse(detonator);

            FieldInfo? privateInfoGhostMode = type.GetField("_ghostMode", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoGhostMode);
            bool ghostMode = (bool)privateInfoGhostMode.GetValue(player)!;
            Assert.IsFalse(ghostMode);

            FieldInfo? privateInfoNumberOfObstackles = type.GetField("_numberOfObstacles", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoNumberOfObstackles);
            int numberOfObstackles = (int)privateInfoNumberOfObstackles.GetValue(player)!;
            Assert.IsTrue(numberOfObstackles == 0);

            FieldInfo? priavteInfoCanPlaceBomb = type.GetField("_canPlaceBomb", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(priavteInfoCanPlaceBomb);
            bool canPlaceBomb = (bool)priavteInfoCanPlaceBomb.GetValue(player)!;
            Assert.IsTrue(canPlaceBomb);

            FieldInfo? privateInstantPlacingBombs = type.GetField("_instantPlacingBombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInstantPlacingBombs);
            bool instantPlacingBomb = (bool)privateInstantPlacingBombs.GetValue(player)!;
            Assert.IsFalse(instantPlacingBomb);
        }

        [TestMethod]
        public void TestFindPositionWith3Player()
        {
            Model model = new Model(0, 3, powerUps, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Type type = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = type.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 1);

            Assert.IsTrue(players[1].PosX == 16);
            Assert.IsTrue(players[1].PosY == 24);

            Assert.IsTrue(players[2].PosX == 16);
            Assert.IsTrue(players[2].PosY == 1);
        }

        [TestMethod]
        public void TestFindPositionWith2Player()
        {
            Model model = new Model(0, 2, powerUps, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Type type = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = type.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 1);

            Assert.IsTrue(players[1].PosX == 16);
            Assert.IsTrue(players[1].PosY == 24);
        }

        [TestMethod]
        public void TestMove()
        {
        }

        [TestMethod]
        public void TestPlaceBomb()
        {
            Model model = new Model(0, 3, powerUps, powerDowns, 3, true);
            model.NewGame();

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfoNumberOfBombs = typePlayer.GetField("_numberOfBombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoNumberOfBombs);
            int numberOfBombs = (int)privateFieldInfoNumberOfBombs.GetValue(players[0])!;
            Assert.IsTrue(numberOfBombs == 2);

            FieldInfo? privateInfoBombs = typePlayer.GetField("_bombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateInfoBombs);
            List<Bomb> bombs = (List<Bomb>)privateInfoBombs.GetValue(players[0])!;
            Assert.IsNotNull(bombs);
            Assert.IsTrue(bombs.Count == 0);

            if (model.Board == null) return;
            players[0].PlaceBomb();
            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 1);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].PlaceBomb();
            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 2);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].PlaceBomb();
            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 2);
        }

        [TestMethod]
        public void TestPlaceObstackle()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.ObstaclePowerUp;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerUp;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfoNumberOfObstackles = typePlayer.GetField("_numberOfObstacles", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoNumberOfObstackles);
            int numberOfObstackles = (int)privateFieldInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstackles == 0);

            players[0].Move(DirectionEnum.Right, model.Board);
            numberOfObstackles = (int)privateFieldInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstackles == 3);

            players[0].PlaceObstacle();
            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            numberOfObstackles = (int)privateFieldInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstackles == 2);

            players[0].PlaceObstacle();
            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            numberOfObstackles = (int)privateFieldInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstackles == 1);

            Assert.IsTrue(model.Board[players[0].PosX, players[0].PosY - 1] == BlockTypeEnum.BoxPlacedByPlayer);
            Assert.IsTrue(model.Board[players[0].PosX, players[0].PosY - 2] == BlockTypeEnum.BoxPlacedByPlayer);

            players[0].PlaceObstacle();
            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            numberOfObstackles = (int)privateFieldInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstackles == 0);

            players[0].PlaceObstacle();
            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            numberOfObstackles = (int)privateFieldInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstackles == 0);
            Assert.IsTrue(model.Board[players[0].PosX, players[0].PosY - 1] == BlockTypeEnum.Path);

        }

        [TestMethod]
        public void TestAddPowerUp()
        {
        }

        [TestMethod]
        public void TestBombNumberIncreasePowerUp()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.BombNumberIncreasePowerUp;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerUp;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfoNumberOfObstackles = typePlayer.GetField("_numberOfBombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoNumberOfObstackles);
            int numberOfBombs = (int)privateFieldInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfBombs == 2);

            players[0].Move(DirectionEnum.Right, model.Board);
            numberOfBombs = (int)privateFieldInfoNumberOfObstackles.GetValue(players[0])!;
            Assert.IsTrue(numberOfBombs == 3);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 3] == BlockTypeEnum.PlayerA);
        }

        [TestMethod]
        public void TestBombRangeIncreasePowerUp()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.BombRangeIncreasePowerUp;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerUp;
            model.Board[1, 3] = BlockTypeEnum.PowerUp;
            model.Board[1, 4] = BlockTypeEnum.PowerUp;
            model.Board[1, 5] = BlockTypeEnum.PowerUp;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_range", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int range = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(range == 2);

            players[0].Move(DirectionEnum.Right, model.Board);
            range = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(range == 3);
            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            range = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(range == 4);
            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            range = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(range == 5);
            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            range = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(range == 5);
            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }



        }

        [TestMethod]
        public void TestDetonatorPowerUp()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.DetonatorPowerUp;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);
            model.Board[1, 2] = BlockTypeEnum.PowerUp;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_detonator", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            bool detonator = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(!detonator);

            players[0].Move(DirectionEnum.Right, model.Board);
            detonator = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(detonator);

            model.PlayerPlacesBomb(0);
            for (int i = 0; i < 10; ++i)
            {
                model.OnTimerTick();
            }
            model.PlayerMoves(0, DirectionEnum.Right);

            for (int i = 0; i < 10; ++i)
            {
                model.OnTimerTick();
            }
            model.PlayerMoves(0, DirectionEnum.Right);

            for (int i = 0; i < 10; ++i)
            {
                model.OnTimerTick();
            }
            model.PlayerMoves(0, DirectionEnum.Right);

            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.Bomb);
            model.PlayerDetonates(0);
            model.OnTimerTick();
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.Explosion);
        }

        [TestMethod]
        public void TestRollerSkatePowerUp()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.RollerSkatePowerUp;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerUp;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int speed = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(speed == 10);

            players[0].Move(DirectionEnum.Right, model.Board);
            speed = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(speed == 5);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);

            for (int i = 0; i < 5; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            speed = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(speed == 5);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 3);
        }

        [TestMethod]
        public void TestInvincibilityPowerUp()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.InvincibilityPowerUp;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerUp;
            model.Board[1, 3] = BlockTypeEnum.Monster;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_invincibility", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            bool invincibility = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(!invincibility);

            FieldInfo? privateFieldInfoBaseCoolDown = typePlayer.GetField("_baseCoolDownValue", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoBaseCoolDown);
            int baseCoolDown = (int)privateFieldInfoBaseCoolDown.GetValue(players[0])!;
            Assert.IsTrue(baseCoolDown == 500);

            players[0].Move(DirectionEnum.Right, model.Board);
            invincibility = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(invincibility);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(players[0].Alive);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            invincibility = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(invincibility);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(players[0].Alive);

            model.Board[1, 3] = BlockTypeEnum.Explosion;

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            invincibility = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(invincibility);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(players[0].Alive);

            for (int i = 0; i < (baseCoolDown); ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            invincibility = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(!invincibility);
            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(!players[0].Alive);

            for (int i = 0; i < (baseCoolDown); ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }
;
            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);

        }

        [TestMethod]
        public void TestGhostPowerUp()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.GhostPowerUp;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerUp;
            model.Board[1, 3] = BlockTypeEnum.Monster;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_ghostMode", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            bool ghostMode = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(!ghostMode);

            FieldInfo? privateFieldInfoBaseCoolDown = typePlayer.GetField("_baseCoolDownValue", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoBaseCoolDown);
            int baseCoolDown = (int)privateFieldInfoBaseCoolDown.GetValue(players[0])!;
            Assert.IsTrue(baseCoolDown == 500);

            players[0].Move(DirectionEnum.Right, model.Board);
            ghostMode = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(ghostMode);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(players[0].Alive);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }
;
            players[0].Move(DirectionEnum.Down, model.Board);
            ghostMode = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(ghostMode);
            Assert.IsTrue(players[0].PosX == 2);
            Assert.IsTrue(players[0].PosY == 2);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }
;
            players[0].Move(DirectionEnum.Down, model.Board);
            ghostMode = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(ghostMode);
            Assert.IsTrue(players[0].PosX == 3);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(model.Board[2, 2] == BlockTypeEnum.Wall);
            Assert.IsTrue(model.Board[3, 2] == BlockTypeEnum.GhostPlayerAOnBox);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }
;
            players[0].Move(DirectionEnum.Left, model.Board);
            ghostMode = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(ghostMode);
            Assert.IsTrue(players[0].PosX == 3);
            Assert.IsTrue(players[0].PosY == 1);
            Assert.IsTrue(model.Board[3, 2] == BlockTypeEnum.Box);
            Assert.IsTrue(model.Board[3, 1] == BlockTypeEnum.PlayerA);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }
;
            players[0].Move(DirectionEnum.Right, model.Board);
            ghostMode = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(ghostMode);
            Assert.IsTrue(players[0].PosX == 3);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(model.Board[3, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[3, 2] == BlockTypeEnum.GhostPlayerAOnBox);

            for (int i = 0; i < baseCoolDown; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            ghostMode = (bool)privateFieldInfo.GetValue(players[0])!;
            Assert.IsFalse(ghostMode);
            Assert.IsFalse(players[0].Alive);
            Assert.IsTrue(model.Board[3, 2] == BlockTypeEnum.Box);

            model.Board[15, 1] = BlockTypeEnum.PowerUp;
            players[2].Move(DirectionEnum.Up, model.Board);
            for (int i = 0; i < 10; ++i)
            {
                players[2].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }
            players[2].Move(DirectionEnum.Right, model.Board);

            Assert.IsTrue(model.Board[15, 2] == BlockTypeEnum.GhostPlayerCOnBox);
        }

        [TestMethod]
        public void TestObstaclePowerUp()
        {
            PowerUpEnum[] currentPowerups = new PowerUpEnum[1];
            currentPowerups[0] = PowerUpEnum.ObstaclePowerUp;
            Model model = new Model(0, 3, currentPowerups, powerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerUp;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_numberOfObstacles", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int numberOfObstacles = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstacles == 0);

            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(players[0].Alive);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            numberOfObstacles = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstacles == 3);
            model.PlayerPlacesObstackles(0);
            model.PlayerMoves(0, DirectionEnum.Right);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.BoxPlacedByPlayer);
            Assert.IsTrue(model.Board[1, 3] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 3);
            Assert.IsTrue(players[0].Alive);
            numberOfObstacles = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(numberOfObstacles == 2);
        }

        [TestMethod]
        public void TestSlowDownPowerUp()
        {
            PowerDownEnum[] currentPowerDowns = new PowerDownEnum[1];
            currentPowerDowns[0] = PowerDownEnum.SlowDownPowerDown;
            Model model = new Model(0, 3, powerUps, currentPowerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerDown;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int speed = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(speed == 10);

            players[0].Move(DirectionEnum.Right, model.Board);
            speed = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(speed == 15);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);

            for (int i = 0; i < 5; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            speed = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(speed == 15);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            speed = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(speed == 15);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 3);

            for (int i = 0; i < 485; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }
            speed = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(speed == 10);

        }

        [TestMethod]
        public void TestBombRangeDecreasePowerUp()
        {
            PowerDownEnum[] currentPowerDowns = new PowerDownEnum[1];
            currentPowerDowns[0] = PowerDownEnum.BombRangeDecreasePowerDown;
            Model model = new Model(0, 3, powerUps, currentPowerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerDown;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_range", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            int range = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(range == 2);

            players[0].Move(DirectionEnum.Right, model.Board);
            range = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(range == 0);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.PlayerA);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.Path);
            Assert.IsTrue(model.Board[1, 3] == BlockTypeEnum.PlayerA);

            for (int i = 0; i < 490; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }
            range = (int)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(range == 2);

        }

        [TestMethod]
        public void TestDisableBombPlacementPowerUp()
        {
            PowerDownEnum[] currentPowerDowns = new PowerDownEnum[1];
            currentPowerDowns[0] = PowerDownEnum.DisableBombPlacementPowerDown;
            Model model = new Model(0, 3, powerUps, currentPowerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerDown;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_bombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            List<Bomb> bombs = (List<Bomb>)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(bombs.Count == 0);

            FieldInfo? privateFieldInfoDisableBombPlacement = typePlayer.GetField("_canPlaceBomb", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoDisableBombPlacement);
            bool canPlaceBomb = (bool)privateFieldInfoDisableBombPlacement.GetValue(players[0])!;
            Assert.IsTrue(canPlaceBomb);

            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 0);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);
            canPlaceBomb = (bool)privateFieldInfoDisableBombPlacement.GetValue(players[0])!;
            Assert.IsFalse(canPlaceBomb);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].PlaceBomb();
            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 0);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 3);
            canPlaceBomb = (bool)privateFieldInfoDisableBombPlacement.GetValue(players[0])!;
            Assert.IsTrue(!canPlaceBomb);
            Assert.IsTrue(bombs.Count == 0);

            for (int i = 0; i < 500; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].PlaceBomb();
            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 1);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 4);
            canPlaceBomb = (bool)privateFieldInfoDisableBombPlacement.GetValue(players[0])!;
            Assert.IsTrue(canPlaceBomb);
            Assert.IsTrue(bombs.Count == 1);

        }

        [TestMethod]
        public void TestInstantBombPlacementPowerUp()
        {
            PowerDownEnum[] currentPowerDowns = new PowerDownEnum[1];
            currentPowerDowns[0] = PowerDownEnum.InstantBombPlacementPowerDown;
            Model model = new Model(0, 3, powerUps, currentPowerDowns, 3, true);
            model.NewGame();
            model.Board![1, 2] = BlockTypeEnum.PowerDown;

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_bombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            List<Bomb> bombs = (List<Bomb>)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(bombs.Count == 0);

            FieldInfo? privateFieldInfoInstantBombPlacement = typePlayer.GetField("_instantPlacingBombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoInstantBombPlacement);
            bool instantPlacingBomb = (bool)privateFieldInfoInstantBombPlacement.GetValue(players[0])!;
            Assert.IsFalse(instantPlacingBomb);

            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 0);
            instantPlacingBomb = (bool)privateFieldInfoInstantBombPlacement.GetValue(players[0])!;
            Assert.IsTrue(instantPlacingBomb);
            Assert.IsTrue(players[0].PosX == 1);
            Assert.IsTrue(players[0].PosY == 2);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 1);
            instantPlacingBomb = (bool)privateFieldInfoInstantBombPlacement.GetValue(players[0])!;
            Assert.IsTrue(instantPlacingBomb);

            for (int i = 0; i < 10; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
            }

            players[0].Move(DirectionEnum.Right, model.Board);
            Assert.IsTrue(bombs.Count == 2);
            instantPlacingBomb = (bool)privateFieldInfoInstantBombPlacement.GetValue(players[0])!;
            Assert.IsTrue(instantPlacingBomb);

            for (int i = 0; i < 500; ++i)
            {
                players[0].OnTimerTick(model.Board, model.BoardHeight, model.BoardWidth);
                players[0].Move(DirectionEnum.Right, model.Board);
            }
            instantPlacingBomb = (bool)privateFieldInfoInstantBombPlacement.GetValue(players[0])!;
            Assert.IsFalse(instantPlacingBomb);
        }

        [TestMethod]
        public void TestOnTimerTick()
        {
            Model model = new Model(2, 3, new PowerUpEnum[0], new PowerDownEnum[0], 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfo = typePlayer.GetField("_bombs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfo);
            List<Bomb> bombs = (List<Bomb>)privateFieldInfo.GetValue(players[0])!;
            Assert.IsTrue(bombs.Count == 0);

            FieldInfo? privateFieldInfoCanMove = typePlayer.GetField("_canMove", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoCanMove);
            bool canMove = (bool)privateFieldInfoCanMove.GetValue(players[0])!;
            Assert.IsTrue(canMove);

            FieldInfo? privateFieldInfoMoveCoolDown = typePlayer.GetField("_moveCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoMoveCoolDown);
            int moveCoolDown = (int)privateFieldInfoMoveCoolDown.GetValue(players[0])!;
            Assert.IsTrue(moveCoolDown == 10);

            FieldInfo? privateFieldInfoInvincibility = typePlayer.GetField("_invincibility", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoInvincibility);
            privateFieldInfoInvincibility.SetValue(players[0], true);

            FieldInfo? privateFieldInfoInvincibilityDown = typePlayer.GetField("_invincibilityCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoInvincibilityDown);
            privateFieldInfoInvincibilityDown.SetValue(players[0], 10);
            int invincCoolDown = (int)privateFieldInfoInvincibilityDown.GetValue(players[0])!;
            Assert.IsTrue(invincCoolDown == 10);

            FieldInfo? privateFieldInfoGhostMode = typePlayer.GetField("_ghostMode", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoGhostMode);
            privateFieldInfoGhostMode.SetValue(players[0], true);

            FieldInfo? privateFieldInfoGhostModeDown = typePlayer.GetField("_ghostModeCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoGhostModeDown);
            privateFieldInfoGhostModeDown.SetValue(players[0], 10);
            int ghostCoolDown = (int)privateFieldInfoGhostModeDown.GetValue(players[0])!;
            Assert.IsTrue(ghostCoolDown == 10);

            FieldInfo? privateFieldInfoSlowDown = typePlayer.GetField("_slowDownCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoSlowDown);
            privateFieldInfoSlowDown.SetValue(players[0], 10);
            int slowlDown = (int)privateFieldInfoSlowDown.GetValue(players[0])!;
            Assert.IsTrue(slowlDown == 10);

            FieldInfo? privateFieldInfoRangeCoolDown = typePlayer.GetField("_rangeDecreasedCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoRangeCoolDown);
            privateFieldInfoRangeCoolDown.SetValue(players[0], 10);
            int rangeCoolDown = (int)privateFieldInfoRangeCoolDown.GetValue(players[0])!;
            Assert.IsTrue(rangeCoolDown == 10);

            FieldInfo? privateFieldInfoDisable = typePlayer.GetField("_disableBombPlacementCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoDisable);
            privateFieldInfoDisable.SetValue(players[0], 10);
            int disable = (int)privateFieldInfoDisable.GetValue(players[0])!;
            Assert.IsTrue(disable == 10);

            FieldInfo? privateFieldInfoInstant = typePlayer.GetField("_instantPlacingBombsCoolDown", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoInstant);
            privateFieldInfoInstant.SetValue(players[0], 10);
            int instant = (int)privateFieldInfoInstant.GetValue(players[0])!;
            Assert.IsTrue(instant == 10);

            model.PlayerPlacesBomb(0);
            model.PlayerMoves(0, DirectionEnum.Right);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.Bomb);

            Assert.IsTrue(bombs.Count == 1);
            Assert.IsTrue(bombs[0].Time == 100);

            model.OnTimerTick();

            canMove = (bool)privateFieldInfoCanMove.GetValue(players[0])!;
            Assert.IsFalse(canMove);
            moveCoolDown = (int)privateFieldInfoMoveCoolDown.GetValue(players[0])!;
            Assert.IsTrue(moveCoolDown == 9);
            Assert.IsTrue(bombs[0].Time == 99);
            invincCoolDown = (int)privateFieldInfoInvincibilityDown.GetValue(players[0])!;
            Assert.IsTrue(invincCoolDown == 9);
            ghostCoolDown = (int)privateFieldInfoGhostModeDown.GetValue(players[0])!;
            Assert.IsTrue(ghostCoolDown == 9);
            slowlDown = (int)privateFieldInfoSlowDown.GetValue(players[0])!;
            Assert.IsTrue(slowlDown == 9);
            rangeCoolDown = (int)privateFieldInfoRangeCoolDown.GetValue(players[0])!;
            Assert.IsTrue(rangeCoolDown == 9);
            disable = (int)privateFieldInfoDisable.GetValue(players[0])!;
            Assert.IsTrue(disable == 9);
            instant = (int)privateFieldInfoInstant.GetValue(players[0])!;
            Assert.IsTrue(instant == 9);
        }

        [TestMethod]
        public void TestKill()
        {
            Model model = new Model(2, 3, new PowerUpEnum[0], new PowerDownEnum[0], 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            Assert.IsTrue(players[0].Alive);
            players[0].Kill();
            Assert.IsFalse(players[0].Alive);

            Type typePlayer = typeof(Player);
            FieldInfo? privateFieldInfoGhostMode = typePlayer.GetField("_invincibility", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoGhostMode);
            privateFieldInfoGhostMode.SetValue(players[1], true);

            Assert.IsTrue(players[1].Alive);
            players[1].Kill();
            Assert.IsTrue(players[1].Alive);
        }

        [TestMethod]
        public void TestOnPlayerProperties()
        {
            PowerDownEnum[] currentPowerDowns = new PowerDownEnum[1];
            currentPowerDowns[0] = PowerDownEnum.InstantBombPlacementPowerDown;
            Model model = new Model(0, 3, powerUps, currentPowerDowns, 3, true);

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            bool eventRaised = false;
            PlayerPropertiesEventArgs? eventArgs = null;
            players[0].OnPlayerPropertiesEventHandler += (sender, args) =>
            {
                eventRaised = true;
                eventArgs = args;
            };
            model.NewGame();
            Assert.IsTrue(eventRaised);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventArgs._playerId == 0);
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
        public void TestPlayerMoves()
        {
            Model model = new Model(2, 3, new PowerUpEnum[0], new PowerDownEnum[0], 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            model.PlayerMoves(3, DirectionEnum.Right);

            model.PlayerMoves(0, DirectionEnum.Right);
            model.PlayerMoves(1, DirectionEnum.Left);
            model.PlayerMoves(2, DirectionEnum.Up);
            Assert.IsTrue(model.Board[1, 2] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 23] == BlockTypeEnum.PlayerB);
            Assert.IsTrue(model.Board[15, 1] == BlockTypeEnum.PlayerC);

            for (int i = 0; i < 10; i++)
            {
                model.OnTimerTick();
            }

            model.PlayerMoves(0, DirectionEnum.Left);
            model.PlayerMoves(1, DirectionEnum.Right);
            model.PlayerMoves(2, DirectionEnum.Down);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 24] == BlockTypeEnum.PlayerB);
            Assert.IsTrue(model.Board[16, 1] == BlockTypeEnum.PlayerC);

            for (int i = 0; i < 10; i++)
            {
                model.OnTimerTick();
            }

            model.PlayerMoves(0, DirectionEnum.Down);
            model.PlayerMoves(1, DirectionEnum.Down);
            model.PlayerMoves(2, DirectionEnum.Right);
            Assert.IsTrue(model.Board[2, 1] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[2, 24] == BlockTypeEnum.PlayerB);
            Assert.IsTrue(model.Board[16, 2] == BlockTypeEnum.PlayerC);

            for (int i = 0; i < 10; i++)
            {
                model.OnTimerTick();
            }

            model.PlayerMoves(0, DirectionEnum.Up);
            model.PlayerMoves(1, DirectionEnum.Up);
            model.PlayerMoves(2, DirectionEnum.Left);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 24] == BlockTypeEnum.PlayerB);
            Assert.IsTrue(model.Board[16, 1] == BlockTypeEnum.PlayerC);

            for (int i = 0; i < 10; i++)
            {
                model.OnTimerTick();
            }

            model.PlayerMoves(0, DirectionEnum.Up);
            model.PlayerMoves(1, DirectionEnum.Up);
            model.PlayerMoves(2, DirectionEnum.Left);
            Assert.IsTrue(model.Board[1, 1] == BlockTypeEnum.PlayerA);
            Assert.IsTrue(model.Board[1, 24] == BlockTypeEnum.PlayerB);
            Assert.IsTrue(model.Board[16, 1] == BlockTypeEnum.PlayerC);

            for (int i = 0; i < 10; i++)
            {
                model.OnTimerTick();
            }
        }

        [TestMethod]
        public void TestSaveData()
        {
            Model model = new Model(2, 3, new PowerUpEnum[0], new PowerDownEnum[0], 3, true);
            model.NewGame();
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Board);

            Type typeModel = typeof(Model);
            FieldInfo? privateFieldInfoPlayers = typeModel.GetField("_players", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(privateFieldInfoPlayers);
            Player[] players = (Player[])privateFieldInfoPlayers.GetValue(model)!;
            Assert.IsNotNull(players);

            players[0].Points = 2;
            players[0].Move(DirectionEnum.Right, model.Board);

            int[] data = players[0].SaveData();
            Assert.IsNotNull(data);

            Assert.IsTrue(data[0] == 0);
            Assert.IsTrue(data[1] == 1);
            Assert.IsTrue(data[2] == 2);
            Assert.IsTrue(data[3] == 2);
            Assert.IsTrue(data[4] == 10);
            Assert.IsTrue(data[5] == 10);
            Assert.IsTrue(data[6] == 0);
            Assert.IsTrue(data[7] == 2);
            Assert.IsTrue(data[8] == 0);
            Assert.IsTrue(data[9] == 2);
            Assert.IsTrue(data[10] == 500);
            Assert.IsTrue((BlockTypeEnum)data[11] == BlockTypeEnum.Path);
            Assert.IsTrue(data[12] == 0);
            Assert.IsTrue(data[13] == 0);
            Assert.IsTrue(data[14] == 0);
            Assert.IsTrue(data[15] == 0);
            Assert.IsTrue(data[16] == 0);
            Assert.IsTrue(data[17] == 0);
            Assert.IsTrue(data[18] == 0);
            Assert.IsTrue(data[19] == 0);
            Assert.IsTrue(data[20] == 0);
            Assert.IsTrue(data[21] == 1);
            Assert.IsTrue(data[22] == 0);
            Assert.IsTrue(data[23] == 0);
            Assert.IsTrue(data[24] == 0);
            Assert.IsTrue(data[25] == 1);
        }
    }
}
