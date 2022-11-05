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
    public class Tests
    {
        [Test]
        public void PlayerTakesDamage_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.TakeDamage(1);

            Assert.That(player.Health, Is.EqualTo(9));
        }

        [Test]
        public void PlayerGetsHealth_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeHealth(5);

            Assert.That(player.Health, Is.EqualTo(15));
        }

        [Test]
        public void PlayerSpeedIncrease_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeSpeed(-5);

            Assert.That(player.MovementSpeed, Is.EqualTo(5));
        }

        [Test]
        public void SetCostumePlayerSpeed_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.SetMoveSpeed(5);

            Assert.That(player.MovementSpeed, Is.EqualTo(5));
        }

        [Test]
        public void ChangeExplosivesCapacityForPlayer_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeExplosivesCapacity(5);

            Assert.That(player.ExplosivesCapacity, Is.EqualTo(15));
        }

        [Test]
        public void ChangeExplosiveRangeForPlayer_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeExplosiveRange(5);

            Assert.That(player.ExplosiveRange, Is.EqualTo(7));
        }

        [Test]
        public void ChangeExplosiveDamageForPlayer_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeExplosiveDamage(5);

            Assert.That(player.ExplosiveFireDamage, Is.EqualTo(6));
        }

        [Test]
        public void ChangeDestructableGameObjectDurability_ShouldPass()
        {
            PaperWall wall = new PaperWall();

            wall.DecreaseDurability();

            Assert.That(wall.Durability, Is.EqualTo(2));
        }

        [Test]
        public void GetPlayerSpeed_ShouldPass()
        {
            Player player = new Player(10, 0, 10);

            Assert.That(player.GetSpeed(), Is.EqualTo(0));
        }

    }
}