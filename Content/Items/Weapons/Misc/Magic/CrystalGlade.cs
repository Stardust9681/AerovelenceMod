using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.Audio;
using ReLogic.Content;
using Terraria.DataStructures;

namespace AerovelenceMod.Content.Items.Weapons.Misc.Magic
{
    public class CrystalGlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Legends say it's just a rewritten Plant Fiber Cordage.");
            //Surely we don't ACTUALLY want this tooltip?
        }
        public override void SetDefaults()
        {
            Item.crit = 11;
            Item.damage = 97;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.width = 30;
            Item.height = 34;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.UseSound = SoundID.Item21;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.value = 205000 * 5;
            Item.rare = 11;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.Magic.CrystalGlade_Proj>(); //Naming consistency
            Item.shootSpeed = 11.4f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(0.15708f);
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.First(x => x.Name == "ItemName" && x.Mod == "Terraria").OverrideColor = new Color(255, 132, 0);
        }
    }
}
