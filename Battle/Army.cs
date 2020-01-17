using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Battle
{
    public class Army
    {
        private List<Soldier> _enlistedSoldiers = new List<Soldier>();
        public IEnumerable<Soldier> EnlistedSoldiers => _enlistedSoldiers;

        public Soldier Frontman
        {
            get 
            {
                return _enlistedSoldiers.Any() 
                    ? _enlistedSoldiers.First() 
                    : throw new Exception("We should draft more people");
            }
        }

        public void Enlist(Soldier soldier)
        {
            _enlistedSoldiers.Add(soldier);
        }
    }
}