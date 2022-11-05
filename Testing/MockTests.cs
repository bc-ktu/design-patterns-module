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

namespace Testing
{
    public class MockTests
    {
        private Mock<MapTile> mockTile;
        private GameMap map;
        private Player player;
        private GameObject[] gameObjects;

        [OneTimeSetUp]
        public void Setup()
        {
            mockTile = new Mock<MapTile>();
        }

        [Test]
        public void Test1()
        {

            GameLogic.ApplyEffects(player, map, gameObjects);
            Assert.Pass();
        }
    }
}
