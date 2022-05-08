using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AerovelenceMod.Content.Items.Weapons.Melee
{
    public class Ornament : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ornament");
        }
        public override void SetDefaults()
        {
            Item.channel = true;		
            Item.crit = 20;
            Item.damage = 46;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 8;
            Item.value = 10000;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 2f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(2))
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("OrnamentProjectileRed").Type, damage, knockback, player.whoAmI);
            } else
            {
                Projectile.NewProjectile(source, position.X + 10, position.Y + 10, velocity.X + 4, velocity.Y + 4, Mod.Find<ModProjectile>("OrnamentProjectileGreen").Type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }

    public class OrnamentProjectileGreen : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 40;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 440f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 24f;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }

            if (Main.rand.NextFloat() <= 0.03)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, new Vector2(Main.rand.NextFloat(-2.5f, 2.5f), Main.rand.NextFloat(-2f, -4.5f)), ProjectileID.OrnamentFriendly, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }

    }

    public class OrnamentProjectileRed : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 40;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 440f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 24f;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() <= 0.03)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, new Vector2(Main.rand.NextFloat(-2.5f, 2.5f), Main.rand.NextFloat(-2f, -4.5f)), ProjectileID.OrnamentFriendly, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}