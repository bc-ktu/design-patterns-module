using Utils.GameObjects.Animates;
using NUnit.Framework;
using Utils.Math;
using Utils.GameLogic;
using System.Timers;
using Utils.GameObjects.Explosives;
using Utils.AbstractFactory;
using Utils.Observer;
using Utils.Map;
using Utils.Decorator;
using Utils.GameObjects;
using Utils.GameObjects.Walls;

namespace Testing
{
    public class ParameterizedTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(4)]
        [TestCase(3)]
        [Parallelizable(ParallelScope.All)]
        public void IsSpeedLimitReached_ShouldBeFalse(int speed)
        {
            int defaultSpeed = 5;
            Player player = new Player(10, defaultSpeed, 10);

            Assert.IsFalse(player.IsSpeedLimit(speed));
        }

        [Test]
        [TestCase(6)]
        [TestCase(10)]
        [TestCase(1000)]
        [TestCase(8)]
        [Parallelizable(ParallelScope.All)]
        public void IsSpeedLimitReached_ShouldBeTrue(int speed)
        {
            int defaultSpeed = 5;
            Player player = new Player(10, defaultSpeed, 10);

            Assert.IsTrue(player.IsSpeedLimit(speed));
        }
    }
}
