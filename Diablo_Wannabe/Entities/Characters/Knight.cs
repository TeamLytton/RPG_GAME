namespace Diablo_Wannabe.Entities.Characters
{
    public class Knight : Player
    {
        private const int KnightDefaultMoveSpeed = 3;
        private const int KnightDefaultWeaponRange = 70;
        private const int KnightDefaultHealth = 120;
        private const int KnightDefaultArmor = 40;
        private const int KnightDefaultDamage = 50;

        public Knight(string path) 
            : base(path, KnightDefaultMoveSpeed, KnightDefaultWeaponRange, KnightDefaultHealth, KnightDefaultArmor, KnightDefaultDamage)
        {
        }
    }
}