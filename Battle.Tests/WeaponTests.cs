using FluentAssertions;
using Xunit;

namespace Battle.Tests
{
    public class WeaponTests
    {
        [Fact]
        public void BareFist_DamageIs1()
        {
            // GIVEN
            var weapon = new BareFist();

            // WHEN THEN
            weapon.Damage.Should().Be(1);
        }

        [Fact]
        public void Spear_DamageIs2()
        {
            // GIVEN
            var weapon = new Spear();

            // WHEN THEN
            weapon.Damage.Should().Be(2);
        }

        [Fact]
        public void Sword_DamageIs2()
        {
            // GIVEN
            var weapon = new Sword();

            // WHEN THEN
            weapon.Damage.Should().Be(2);
        }

        [Fact]
        public void Axe_DamageIs3()
        {
            // GIVEN
            var weapon = new Axe();

            // WHEN THEN
            weapon.Damage.Should().Be(3);
        }
    }
}
