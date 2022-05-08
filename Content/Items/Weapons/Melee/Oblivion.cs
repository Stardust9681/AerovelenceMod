using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AerovelenceMod.Content.Items.Weapons.Melee
{
    public class Oblivion : ModItem
    {
        public bool NPCHit;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oblivion");
        }
        public override void SetDefaults()
        {
            Item.crit = 6;
            Item.damage = 75;
            Item.DamageType = DamageClass.Melee;
            Item.width = 60;
            Item.height = 68;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.shoot = ProjectileID.FlamingArrow;
            Item.shootSpeed = 60f;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
        }
        public override bool? UseItem(Player player)
        {
            player.direction = (Main.MouseWorld.X - player.Center.X > 0) ? 1 : -1;
            return true;
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<OblivionsWrath>();
            {
                for (int i = -4; i < 4; i++)
                {
                    position = Main.MouseWorld + new Vector2(i * 20, -850);
                    Vector2 velocity1 = (Main.MouseWorld - position).SafeNormalize(Vector2.Zero).RotatedByRandom(0.05f) * Item.shootSpeed;
                    Projectile.NewProjectile(source, position, velocity1, type, damage, 2f, player.whoAmI);
                }
                return false;
            }
        }
    }

    public class OblivionsWrath : ModProjectile
    {
        int i;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oblivion's Wrath");
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            DrawOffsetX = -45;
            Projectile.alpha = 255;
            DrawOriginOffsetY = 0;
            Projectile.damage = 65;
            DrawOriginOffsetX = 23;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            i++;
            if (i % 1 == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height / 2, 164);
            }
        }
    }
}