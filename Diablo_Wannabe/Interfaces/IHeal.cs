
using Diablo_Wannabe.Entities.Characters;

namespace Diablo_Wannabe.Interfaces
{
    public interface IHeal : IItem
    {
        int HealingPower { get; set; }

        void Heal(Player player);
    }
}