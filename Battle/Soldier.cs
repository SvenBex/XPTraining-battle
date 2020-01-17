using System;

namespace Battle
{
    public class Soldier
    {
        public IWeapon Weapon { get; private set; } = new BareFist();
        public string Name { get; }

        public Soldier(string name)
        {
            ValidateNameisNotBlank(name);
            Name = name;
        }

        private void ValidateNameisNotBlank(string name)
        {
            if (IsBlank(name))
            {
                throw new ArgumentException("name can not be blank");
            }
        }

        private bool IsBlank(string name) => string.IsNullOrEmpty(name?.Trim());
        
        public void EquipWeapon(IWeapon weapon)
        {
            Weapon = weapon;
        }

        public int GetWeaponDamage()
        {
            return Weapon.Damage;
        }

        internal string Attack(Soldier defendingSoldier)
        {
            if (this == defendingSoldier)
            {
                throw new Exception("Stop hitting yourself.");
            }

            return GetWeaponDamage() >= defendingSoldier.GetWeaponDamage()
                ? Name
                : defendingSoldier.Name;
        }
    }
}