using Diablo_Wannabe.Entities.Characters;

namespace Diablo_Wannabe.Interfaces
{
    public interface IPickable
    {
        bool GotPicked { get; set; }

        void GetPicked(Player player);
    }
}