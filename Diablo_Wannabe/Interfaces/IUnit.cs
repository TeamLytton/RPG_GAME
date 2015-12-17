
namespace Diablo_Wannabe.Interfaces
{
    public interface IUnit
    {
        int MaxHealth { get; set; }

        int Health { get; set; }

        int Armor { get; set; }

        int Damage { get; set; }

        int WeaponRange { get; set; }
    }
}