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
            soldier.Weapon.Should().Be(Weapon.BareFist);
        }

        [Fact]
        public void SetWeapon_GivenAnAxe_WeaponIsSet()
        {
            // GIVEN
            var soldier = new Soldier("Ann");

            // WHEN
            soldier.SetWeapon(Weapon.Axe);

            // THEN
            soldier.Weapon.Should().Be(Weapon.Axe);
        }
    }
}