using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json.Bson;
using Xunit;

namespace Battle.Tests
{
    public class ArmyTest
    {
        [Fact]
        public void Construction_NoSoldiersEnlisted()
        {
            var army = new Army();
            army.EnlistedSoldiers.Should().HaveCount(0);
        }

        [Fact]
        public void Enlist_GivenASoldier_SoldierEnlisted()
        {
            // GIVEN
            var army = new Army();
            var dirk = new Soldier("dirk");

            // WHEN
            army.Enlist(dirk);

            // THEN
            army.EnlistedSoldiers.Should().HaveCount(1);
            army.EnlistedSoldiers.First().Should().Be(dirk);
        }

        [Fact]
        public void Enlist_GivenTwoSoldiers_FirstSoldierShouldIsFrontman()
        {
            // GIVEN
            var army = new Army();
            var soldier1 = new Soldier("Bob");
            var soldier2 = new Soldier("Bort");

            // WHEN
            army.Enlist(soldier1);
            army.Enlist(soldier2);

            // THEN
            army.Frontman.Should().Be(soldier1);
        }

        [Fact]
        public void Frontman_GivenNoSoldiersEnlisted_ThrowsException()
        {
            // GIVEN
            var army = new Army();

            // THEN
            Assert.Throws<Exception>(() => army.Frontman);
        }
    }
}
