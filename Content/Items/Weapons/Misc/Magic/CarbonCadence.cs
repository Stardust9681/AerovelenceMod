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
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AerovelenceMod.Content.Items.Weapons.Misc.Magic
{
    public class CarbonCadence : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Casts an explosive Crystal Mine, which unleashes shards upon detonating.");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.rare = 1;
            Item.value = 2080 * 5;
            Item.mana = 25;
            Item.damage = 23;
            Item.knockBack = 6f;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.reuseDelay = 4;
            Item.useStyle = 5;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.shootSpeed = 8;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.Magic.CarbonCadence_Proj>();
            Item.UseSound = SoundID.Item9;
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player) && player.altFunctionUse != 2 ? true : player.ownedProjectileCounts[Item.shoot] > 0;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
            int foundProjs = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.owner == player.whoAmI && p.type == type && p.ai[0] == 1)
                {
                    foundProjs++; //Increase number of found projectiles by 1
                    p.ai[1] = 2;
                    p.netUpdate = true;
                }
                if (foundProjs >= player.ownedProjectileCounts[type]) break;
            }
            return false;
        }
    }
}
