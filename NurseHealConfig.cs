using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Nurse_Free_Heal_mod
{
    public class NurseHealConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(60)]
        [Range(5, 3600)]
        [Label("Cooldown (seconds)")]
        [Tooltip("How long (in seconds) before the nurse heals for free again.\nSet to 0 to always heal for free.")]
        public int CooldownSeconds { get; set; }
    }
}
