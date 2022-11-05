using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameLogic;
using Utils.GameObjects.Animates;
using Moq;
using Utils.Map;
using Utils.GameObjects;
using Utils.Math;
using Utils.GameObjects.Explosives;
using Utils.Observer;
using System.Drawing;
using Utils.GameObjects.Interactables;

namespace Testing
{
    public class MockTests
    {
        private Mock<MapTile> mockTile;
        private Mock<Powerup> mockPowerup;
        private GameMap map;
        private Player player;
        private GameObject[] gameObjects;

        [OneTimeSetUp]
        public void Setup()
        {
            mockTile = new Mock<MapTile>(MockBehavior.Strict, new Vector2(0, 0), new Vector2(1, 1), new Bitmap(1, 1));
            mockPowerup = new Mock<Powerup>(MockBehavior.Strict, new Vector2(0, 0), new Vector2(1, 1), new Vector4(1, 1, 1, 1), new Bitmap(1, 1));
            map = new GameMap(new Vector2(1, 1), new Vector2(1, 1));
            map._tiles[0, 0] = mockTile.Object;
            gameObjects = new GameObject[1];
            player = new Player(new ExplosiveDi(), new Subject());
        }

        [Test]
        public void ApplyEffects_ApplyTileEffect_SpeedUp_Pass()
        {
            //Arrange
            mockTile.Setup(t => t.AffectPlayer(player)).Callback<Player>((player) => player.SpeedModifier = 3);
            map[0, 0].GameObjects.Add(player);
            var initial = GameSettings.InitialPlayerSpeed;

            //Act
            GameLogic.ApplyEffects(player, map, gameObjects);

            //Assert
            var expected = initial + 3;
            Assert.AreEqual(expected, player.GetSpeed());
        }

        [Test]
        public void ApplyEffects_ApplyPowerUpEffect_SpeedUp_Pass()
        {
            //Arrange
            mockTile.Setup(t => t.AffectPlayer(player)).Callback<Player>((player) => player.ChangeSpeed(0));    //does nothing
            mockPowerup.Setup(t => t.Affect(player, map)).Callback<Player, GameMap>((player, map) => player.SpeedModifier = 3);
            gameObjects[0] = mockPowerup.Object;
            map[0, 0].GameObjects.Add(player);
            var initial = GameSettings.InitialPlayerSpeed;

            //Act
            GameLogic.ApplyEffects(player, map, gameObjects);

            //Assert
            var expected = initial + 3;
            Assert.AreEqual(expected, player.GetSpeed());
        }
    }
}
