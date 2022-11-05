using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameLogic;
using Utils.GameObjects.Animates;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Walls;
using Utils.Helpers;
using Utils.Map;
using Utils.Math;
using Utils.Observer;

namespace Testing
{
    [TestFixture]
    internal static class ParameterizedTests
    {
        public static IEnumerable<object[]> GenerateTestData_False()
        {
            Vector2 size = new Vector2(5, 5);
            Vector2 viewSize = new Vector2(15, 15);
            GameMap gameMap = new GameMap(size, viewSize);
            for (int y = 0; y < gameMap.Size.Y; y++)
                for (int x = 0; x < gameMap.Size.Y; x++)
                    gameMap.SetTile(x, y, new Bitmap(1, 1));

            var prm = gameMap.CreateScaledGameObjectParameters(2, 2, new Bitmap(1, 1));
            Fire fire = new Fire(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            ExplosiveHV explosive = new ExplosiveHV(prm.Item1, prm.Item2, prm.Item3, prm.Item4, fire);
            Subject subject = new Subject();
            Player player = new Player(prm.Item1, prm.Item2, prm.Item3, prm.Item4, explosive, subject);

            prm = gameMap.CreateScaledGameObjectParameters(2, 0, new Bitmap(1, 1));
            PaperWall paperWall0 = new PaperWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

            prm = gameMap.CreateScaledGameObjectParameters(2, 3, new Bitmap(1, 1));
            PaperWall paperWall1 = new PaperWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

            gameMap[2, 0].GameObjects.Add(paperWall0);
            gameMap[2, 3].GameObjects.Add(paperWall1);
            gameMap[2, 2].GameObjects.Add(explosive);

            yield return new object[] { Direction.Up, player, gameMap };

            player.Move(Direction.Left);
            yield return new object[] { Direction.Up, player, gameMap };

            player.Move(Direction.Right);
            player.Move(Direction.Right);
            yield return new object[] { Direction.Up, player, gameMap };

            player.Move(Direction.Up);
            yield return new object[] { Direction.Right, player, gameMap };
        }

        [Test, TestCaseSource(nameof(GenerateTestData_False))]
        public static void PlayerCanMove_True(Vector2 direction, Player player, GameMap gameMap)
        {
            bool actualResult = GamePhysics.IsDummyColliding(direction, player, gameMap);
            Assert.IsFalse(actualResult);
        }

        public static IEnumerable<object[]> GenerateTestData_True()
        {
            Vector2 size = new Vector2(5, 5);
            Vector2 viewSize = new Vector2(15, 15);
            GameMap gameMap = new GameMap(size, viewSize);
            for (int y = 0; y < gameMap.Size.Y; y++)
                for (int x = 0; x < gameMap.Size.Y; x++)
                    gameMap.SetTile(x, y, new Bitmap(1, 1));

            var prm = gameMap.CreateScaledGameObjectParameters(2, 2, new Bitmap(1, 1));
            Fire fire = new Fire(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            ExplosiveHV explosive = new ExplosiveHV(prm.Item1, prm.Item2, prm.Item3, prm.Item4, fire);
            Subject subject = new Subject();
            Player player = new Player(prm.Item1, prm.Item2, prm.Item3, prm.Item4, explosive, subject);

            prm = gameMap.CreateScaledGameObjectParameters(2, 0, new Bitmap(1, 1));
            PaperWall paperWall0 = new PaperWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

            prm = gameMap.CreateScaledGameObjectParameters(2, 3, new Bitmap(1, 1));
            PaperWall paperWall1 = new PaperWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

            gameMap[2, 0].GameObjects.Add(paperWall0);
            gameMap[2, 3].GameObjects.Add(paperWall1);
            gameMap[2, 2].GameObjects.Add(explosive);

            yield return new object[] { Direction.Down, player, gameMap };

            player.Move(Direction.Left);
            yield return new object[] { Direction.Down, player, gameMap };

            player.Move(Direction.Right);
            player.Move(Direction.Right);
            yield return new object[] { Direction.Down, player, gameMap };

            player.Move(Direction.Down);
            yield return new object[] { Direction.Down, player, gameMap };
        }

        [Test, TestCaseSource(nameof(GenerateTestData_True))]
        public static void PlayerCanMove_False(Vector2 direction, Player player, GameMap gameMap)
        {
            bool actualResult = GamePhysics.IsDummyColliding(direction, player, gameMap);
            Assert.IsTrue(actualResult);
        }
    }
}
