using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AerovelenceMod.Items.Weapons.Melee
{
    public class Ornament : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ornament");
		}
        public override void SetDefaults()
        {
            item.channel = true;		
			item.crit = 20;
            item.damage = 46;
            item.ranged = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 24;
            item.useAnimation = 24;
			item.UseSound = SoundID.Item1;
            item.useStyle = 5;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.knockBack = 8;
            item.value = 10000;
            item.rare = 7;
            item.autoReuse = false;
            item.shoot = 1;
            item.shootSpeed = 2f;
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.rand.Next(2) == 0)
			{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("OrnamentProjectileRed"), damage, knockBack, player.whoAmI);
			} else
			{
			Projectile.NewProjectile(position.X + 10, position.Y + 10, speedX + 4, speedY + 4, mod.ProjectileType("OrnamentProjectileGreen"), damage, knockBack, player.whoAmI);
			}
			return false;
		}
    }
}