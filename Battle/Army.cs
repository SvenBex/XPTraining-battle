using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Battle
{
    public class Army
    {
        private IHeadquarter _headquarter;
        private List<Soldier> _enlistedSoldiers = new List<Soldier>();
        public IEnumerable<Soldier> EnlistedSoldiers => _enlistedSoldiers;
        public bool HasEnlistedSoldiers => _enlistedSoldiers.Any();
        public string Name { get; private set; }

        public Soldier Frontman
        {
            get 
            {
                return _enlistedSoldiers.Any() 
                    ? _enlistedSoldiers.First() 
                    : throw new Exception("We should draft more people");
            }
        }

        public Army(string name, IHeadquarter headquarter)
        {
            Name = name;
            _headquarter = headquarter;
        }

        public void Enlist(Soldier soldier)
        {
            _enlistedSoldiers.Add(soldier);
            soldier.SetId(_headquarter.ReportEnlistment(soldier.Name));
        }

        public void KillSoldier(Soldier soldier)
        {
            if (!_enlistedSoldiers.Contains(soldier))
            {
                throw new Exception("Doesn't matter, not ours.");
            }
            _headquarter.ReportCasualty(soldier.Id);
            _enlistedSoldiers.Remove(soldier);
        }

        internal string EngageWar(Army defendingArmy)
        {
            if (!HasEnlistedSoldiers && !defendingArmy.HasEnlistedSoldiers)
            {
                throw new Exception("*tumbleweed_rolling_by.gif*");
            }

            if (!defendingArmy.HasEnlistedSoldiers)
            {
                return Name;
            }

            if (!HasEnlistedSoldiers)
            {
                return defendingArmy.Name;
            }

            ResolveAttack(defendingArmy);

            return EngageWar(defendingArmy);
        }

        private void ResolveAttack(Army defendingArmy)
        {
            var survivingSoldier = Frontman.Attack(defendingArmy.Frontman);
            if (survivingSoldier == Frontman)
            {
                defendingArmy.KillSoldier(defendingArmy.Frontman);
            }
            else if (survivingSoldier == defendingArmy.Frontman)
            {
                KillSoldier(Frontman);
            }
        }
    }
}