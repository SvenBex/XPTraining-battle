using System;
using FluentAssertions;
using Xunit;

namespace Battle.Tests
{
    public class SoldierTest
    {

        [Fact]
        public void Construction_ASoldierMustHaveAName()
        {
            var soldier = new Soldier("name");

            soldier.Name.Should().Be("name");
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void Construction_ASoldierMustHaveAName_CannotBeBlank(string name)
        {
            Action act = () => new Soldier(name);
             
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Construction_ASoldierMustHaveBareFistWeaponsByDefault()
        {
            var soldier = new Soldier("name");
            soldier.Weapon.Should().BeEquivalentTo(new BareFist());
        }

        [Fact]
        public void EquipWeapon_GivenAnAxe_WeaponIsSet()
        {
            // GIVEN
            var soldier = new Soldier("Ann");
            var weapon = new Axe();

            // WHEN
            soldier.EquipWeapon(weapon);

            // THEN
            soldier.Weapon.Should().Be(weapon);
        }

        [Fact]
        public void Attack_SoldierCannotAttackHimself_ThrowsException()
        {
            // GIVEN
            var soldier1 = new Soldier("1");

            // WHEN
            Assert.Throws<Exception>(() => soldier1.Attack(soldier1));
        }

        [Fact]
        public void Attack_Given_DefenderHasBetterWeapon_DefenderWins()
        {
            // GIVEN
            var attacker = new Soldier("Attacker");
            attacker.EquipWeapon(new BareFist());

            var defender = new Soldier("Defender");
            defender.EquipWeapon(new Axe());

            // WHEN
            var winnerName = attacker.Attack(defender);

            // THEN
            winnerName.Should().Be(defender.Name);
        }

        [Fact]
        public void Attack_IfSameWeaponDamage_AttackerWins()
        {
            // GIVEN
            var attacker = new Soldier("Attacker");
            attacker.EquipWeapon(new BareFist());

            var defender = new Soldier("Defender");
            defender.EquipWeapon(new BareFist());

            // WHEN
            var winnerName = attacker.Attack(defender);

            // THEN
            winnerName.Should().Be(attacker.Name);
        }

        [Fact]
        public void Attack_AttackerHasBetterWeapon_AttackerWins()
        {
            // GIVEN
            var attacker = new Soldier("Attacker");
            attacker.EquipWeapon(new Axe());

            var defender = new Soldier("Defender");
            defender.EquipWeapon(new BareFist());

            // WHEN
            var winnerName = attacker.Attack(defender);

            // THEN
            winnerName.Should().Be(attacker.Name);
        }
    }
}