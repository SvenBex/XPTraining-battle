using System;
using System.Collections.Generic;
using System.Text;

namespace Battle
{
    public interface IWeapon
    {
        int Damage { get; }
    }

    public class BareFist : IWeapon
    {
        public int Damage => 1;
    }

    public class Spear : IWeapon
    {
        public int Damage => 2;
    }

    public class Sword : IWeapon
    {
        public int Damage => 2;
    }

    public class Axe : IWeapon
    {
        public int Damage => 3;
    }
}
