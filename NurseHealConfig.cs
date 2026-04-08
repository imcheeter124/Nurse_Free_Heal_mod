using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Nurse_Free_Heal_mod
{
    public class NurseHealConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(60)]
        [Range(5, 3600)]
        public int CooldownSeconds { get; set; }

        [DefaultValue(100)]
        [Range(1, 100)]
        public int HealPercent { get; set; }

        [DefaultValue(false)]
        public bool ResetCooldownOnBossKill { get; set; }
    }
}
