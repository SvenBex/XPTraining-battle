using System;

namespace Battle
{
    public class Soldier
    {
        private readonly IWeapon _defaultWeapon = new BareFist();
        public IWeapon Weapon { get; private set; }
        public string Name { get; }
        private int? _id { get; set; }
        internal int Id => _id.GetValueOrDefault(-1);

        public Soldier(string name)
        {
            ValidateNameisNotBlank(name);
            Name = name;
            Weapon = _defaultWeapon;
        }

        public Soldier(string name, IWeapon weapon) : this(name)
        {
            EquipWeapon(weapon);
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
            if (weapon == null)
            {
                Weapon = _defaultWeapon;
                return;
            }
            Weapon = weapon;
        }

        public int GetWeaponDamage()
        {
            return Weapon.Damage;
        }

        internal Soldier Attack(Soldier defendingSoldier)
        {
            if (this == defendingSoldier)
            {
                throw new Exception("Stop hitting yourself.");
            }

            return GetWeaponDamage() >= defendingSoldier.GetWeaponDamage()
                ? this
                : defendingSoldier;
        }

        public void SetId(int id)
        {
            if (_id.HasValue)
            {
                throw new Exception("Soldier already has an id.");
            }

            _id = id;
        }
    }
}