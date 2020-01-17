using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json.Bson;
using NSubstitute;
using Xunit;

namespace Battle.Tests
{
    public class ArmyTest
    {
        private IHeadquarter _headquarter;
        public ArmyTest()
        {
            _headquarter = Substitute.For<IHeadquarter>();
        }

        private Army CreateArmy(string name = "test")
        {
            return new Army(name, _headquarter);
        }

        [Fact]
        public void Construction_NoSoldiersEnlisted()
        {
            var army = CreateArmy();
            army.EnlistedSoldiers.Should().HaveCount(0);
        }

        [Fact]
        public void Enlist_GivenASoldier_SoldierEnlisted()
        {
            // GIVEN
            var army = CreateArmy();
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
            var army = CreateArmy();
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
            var army = CreateArmy();

            // THEN
            Assert.Throws<Exception>(() => army.Frontman);
        }

        [Fact]
        public void Name_ArmyHasAName()
        {
            // GIVEN
            var army = CreateArmy("Test");

            // WHEN THEN
            army.Name.Should().Be("Test");
        }

        [Fact]
        public void EngageWar_GivenTwoEmptyArmies_ThrowsException()
        {
            // GIVEN
            var army1 = CreateArmy("Test1");
            var army2 = CreateArmy("Test2");

            // THEN
            Assert.Throws<Exception>(() => army1.EngageWar(army2));
        }

        [Fact]
        public void EngageWar_DefendingArmyHasNoSoldiers_AttackingArmyWins()
        {
            // GIVEN
            var attackers = CreateArmy("Rushers");
            attackers.Enlist(new Soldier("Rene"));
            var defenders = CreateArmy("Campers");

            // WHEN
            var winner = attackers.EngageWar(defenders);

            // THEN
            winner.Should().Be(attackers.Name);
        }

        [Fact]
        public void EngageWar_DefendingArmyHasSoldiers_DefendingArmyWins()
        {
            // GIVEN
            var attackers = CreateArmy("Rushers");
            var defenders = CreateArmy("Campers");
            defenders.Enlist(new Soldier("René"));

            // WHEN
            var winner = attackers.EngageWar(defenders);

            // THEN
            winner.Should().Be(defenders.Name);
        }

        [Fact]
        public void EngageWar_GivenTwoArmiesWithSoldierWithSameWeapon_AttackingArmyWins()
        {
            // GIVEN
            var attackers = CreateArmy("Rushers");
            attackers.Enlist(new Soldier("Dirk", new Sword()));
            var defenders = CreateArmy("Campers");
            defenders.Enlist(new Soldier("René", new Sword()));

            // WHEN
            var winner = attackers.EngageWar(defenders);

            // THEN
            winner.Should().Be(attackers.Name);
        }

        [Fact]
        public void EngageWar_GivenTwoArmiesWithSoldierWithAttackingSoldierHasStrongerWeapon_AttackingArmyWins()
        {
            // GIVEN
            var attackers = CreateArmy("Rushers");
            attackers.Enlist(new Soldier("Dirk", new Axe()));
            var defenders = CreateArmy("Campers");
            defenders.Enlist(new Soldier("René", new BareFist()));

            // WHEN
            var winner = attackers.EngageWar(defenders);

            // THEN
            winner.Should().Be(attackers.Name);
        }

        [Fact]
        public void EngageWar_GivenTwoArmiesWithSoldierWithDefendingSoldierHasStrongerWeapon_DefendingArmyWins()
        {
            // GIVEN
            var attackers = CreateArmy("Rushers");
            attackers.Enlist(new Soldier("Dirk", new Sword()));
            var defenders = CreateArmy("Campers");
            defenders.Enlist(new Soldier("René", new Axe()));

            // WHEN
            var winner = attackers.EngageWar(defenders);

            // THEN
            winner.Should().Be(defenders.Name);
        }

        [Fact]
        public void EngageWar_GivenTwoArmiesWith2SoldiersWithSameWeapons_AttackingArmyWins()
        {
            // GIVEN
            var attackers = CreateArmy("Rushers");
            attackers.Enlist(new Soldier("Dirk", new Sword()));
            attackers.Enlist(new Soldier("Michel", new Sword()));
            var defenders = CreateArmy("Campers");
            defenders.Enlist(new Soldier("René", new Sword()));
            defenders.Enlist(new Soldier("LaMarcuzsz", new Sword()));

            // WHEN
            var winner = attackers.EngageWar(defenders);

            // THEN
            winner.Should().Be(attackers.Name);
        }

        [Fact]
        public void Enlist_EnlistedSoldierGetsReportedToTheHQ()
        {
            // GIVEN
            var army = CreateArmy();
            var soldierName = "bobby";

            // WHEN
            army.Enlist(new Soldier(soldierName));

            // THEN
            _headquarter.Received().ReportEnlistment(soldierName);
        }

        [Fact]
        public void Enlist_GivenASoldier_SoldierGetAnId()
        {
            // GIVEN
            var army = CreateArmy();
            var soldier = new Soldier("Bobby-B");
            var expectedId = 9001;
            _headquarter
                .ReportEnlistment(Arg.Any<string>())
                .Returns(expectedId);

            // WHEN
            army.Enlist(soldier);

            // THEN
            soldier.Id.Should().Be(expectedId);
        }

        [Fact]
        public void KillSoldier_GivenADiedSoldier_HeadquarterGetsNotified()
        {
            // GIVEN
            var army = CreateArmy();
            var soldierId = 24;
            var kobe = new Soldier("Briant");
            _headquarter.ReportEnlistment(kobe.Name)
                .Returns(soldierId);
            army.Enlist(kobe);

            // WHEN
            army.KillSoldier(kobe);

            // THEN
            _headquarter.Received().ReportCasualty(soldierId);
        }

        [Fact]
        public void KillSoldier_GivenUnenlistedSoldier_ThrowsException()
        {
            // GIVEN
            var army = CreateArmy();
            var kobe = new Soldier("Briant");

            // WHEN THEN
            Assert.Throws<Exception>(() => army.KillSoldier(kobe));
        }
    }
}
