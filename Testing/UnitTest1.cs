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

            Assert.AreEqual(9, player.Health);
        }

        [Test]
        public void PlayerGetsHealth_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeHealth(5);

            Assert.AreEqual(15, player.Health);
        }

        [Test]
        public void PlayerSpeedIncrease_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeSpeed(-5);

            Assert.AreEqual(5, player.MovementSpeed);
        }

        [Test]
        public void SetCostumePlayerSpeed_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.SetMoveSpeed(5);

            Assert.AreEqual(5, player.MovementSpeed);
        }

        [Test]
        public void ChangeExplosivesCapacityForPlayer_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeExplosivesCapacity(5);

            Assert.AreEqual(15, player.ExplosivesCapacity);
        }

        [Test]
        public void ChangeExplosiveRangeForPlayer_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeExplosiveRange(5);

            Assert.AreEqual(7, player.ExplosiveRange);
        }

        [Test]
        public void ChangeExplosiveDamageForPlayer_ShouldPass()
        {
            Player player = new Player(10, 10, 10);

            player.ChangeExplosiveDamage(5);

            Assert.AreEqual(6, player.ExplosiveFireDamage);
        }

        [Test]
        public void ChangeDestructableGameObjectDurability_ShouldPass()
        {
            PaperWall wall = new PaperWall();

            wall.DecreaseDurability();

            Assert.AreEqual(2, wall.Durability);
        }

        [Test]
        public void GetPlayerSpeed_ShouldPass()
        {
            Player player = new Player(10, 0, 10);

            Assert.AreEqual(0, player.GetSpeed());
        }

    }
    //public void ChangeExplosivesCapacity(int amount)
    //{
    //    ExplosivesCapacity += amount;
    //}

    //public void ChangeExplosiveRange(int amount)
    //{
    //    Explosive.Range += amount;
    //}

    //public void ChangeExplosiveDamage(int amount)
    //{
    //    Explosive.Fire.Damage += amount;
    //}
}