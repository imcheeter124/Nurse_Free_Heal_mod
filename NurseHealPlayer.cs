using Terraria.ModLoader;

namespace Nurse_Free_Heal_mod
{
    public class NurseHealPlayer : ModPlayer
    {
        public int healCooldown = 0;

        public override void PostUpdate()
        {
            if (healCooldown > 0)
                healCooldown--;
        }
    }
}
