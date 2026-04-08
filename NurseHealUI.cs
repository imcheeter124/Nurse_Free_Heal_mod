using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Nurse_Free_Heal_mod
{
    public class NurseHealUI : ModSystem
    {
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<NurseHealPlayer>();
            if (modPlayer.healCooldown <= 0) return;

            int secsLeft = (modPlayer.healCooldown + 59) / 60;
            string text = Language.GetTextValue("Mods.Nurse_Free_Heal_mod.UI.CooldownDisplay", secsLeft);

            Vector2 pos = new Vector2(Main.screenWidth - 200, 80);
            Utils.DrawBorderStringFourWay(
                spriteBatch,
                FontAssets.MouseText.Value,
                text,
                pos.X, pos.Y,
                Color.LightPink, Color.DarkRed,
                Vector2.Zero, 1f
            );
        }
    }
}
