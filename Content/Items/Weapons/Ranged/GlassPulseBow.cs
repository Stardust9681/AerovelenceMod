using AerovelenceMod.Content.Items.Placeables.Blocks;
using AerovelenceMod.Content.Items.Placeables.CrystalCaverns;
using AerovelenceMod.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace AerovelenceMod.Content.Items.Weapons.Ranged
{
    public class GlassPulseBow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glass Pulse Bow");
            Tooltip.SetDefault("Fires a bolt of energy");
        }

        public override void SetDefaults()
        {
            Item.crit = 8;
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 88;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item5;
            Item.shootSpeed = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 0, 35, 20);
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item72);
            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0f));
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<PrismaticBolt>(), damage, knockback, Main.myPlayer);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddIngredient(ModContent.ItemType<CavernCrystal>(), 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}