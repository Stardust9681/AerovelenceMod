using Terraria.ID;
using Terraria;
using Terraria.GameInput;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using AerovelenceMod.Common.Systems;

namespace AerovelenceMod.Content.Items.Weapons.Misc.Magic
{
    public class CursedFlameStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            Tooltip.SetDefault("Unleashes a barrage of cursed fire.");
        }
        public override void SetDefaults()
        {
            Item.crit = 6;
            Item.damage = 38;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 28;
            Item.height = 44;
            Item.useTime = 20;
            Item.useAnimation = 60;
            Item.UseSound = SoundID.Item34;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.value = 25000 * 5;
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.CursedDartFlame;
            Item.shootSpeed = 9f;
        }
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.SoulofNight, 15).
                AddIngredient(ItemID.CursedFlame, 15).
                AddTile(TileID.Anvils).
                Register();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            int[] types = new int[] { ProjectileID.CursedDartFlame, ProjectileID.CursedFlameFriendly, ProjectileID.CursedDart };
            velocity = velocity.RotatedByRandom(0.5236f) * Main.rand.NextFloat(.95f, 1.05f);
            int index = Main.rand.Next(3);
            velocity = velocity * (((2-index) * .25f) + 1);
            type = types[index];
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(type == ProjectileID.CursedDart)
            {
                Projectile p = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                p.ai[1] = float.MinValue;
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
