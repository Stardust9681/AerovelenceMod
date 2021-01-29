using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Aerovelence.Items.Weapons.Melee
{
    public class Cyverthrow : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 48;
            item.damage = 11;// change this later
            item.knockBack = 2f;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.rare = 0;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("CyverthrowProj");
        }
    }
    public class CyverthrowProj : ModProjectile
    {
        private int shootTimer;
        private int explosionTimer;
        private bool npcNearby = true;
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.light = 0.5f;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 380f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 16.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyverthrow");
        }
        public override void AI()
        {

            if (Main.rand.NextFloat() < 0.8289474f)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, 0f, 0f, 0, new Color(255, 50, 0), 1.118421f);
                Dust dust = Main.dust[dustIndex];
                dust.shader = GameShaders.Armor.GetSecondaryShader(38, Main.LocalPlayer);
                dust.noGravity = true;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }

            shootTimer++;
            explosionTimer++;


            if (shootTimer >= Main.rand.Next(7, 19))
            {
                float speed = 5f;
                int type = mod.ProjectileType("CyverthrowBolt");
                Vector2 velocity = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(projectile.Center, velocity, type, projectile.damage, 5f, projectile.owner);
                shootTimer = 0;
            }
            if (explosionTimer >= 200)
            {
                for (int i = 0; i < 12; i++)
                {
                    int dustType = 170;
                    int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType);
                    Dust dust = Main.dust[dustIndex];
                    dust.velocity.X = dust.velocity.X + Main.rand.Next(-10, 10) * 0.01f;
                    dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-65, -48) * 0.01f;
                    dust.scale *= 2f + Main.rand.Next(-30, 31) * 0.01f;
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(29, Main.LocalPlayer);
                }
                for (int a = 0; a < 9; a++)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, new Vector2(7, 7).RotatedBy(MathHelper.ToRadians(360 / 8 * a)), ProjectileID.MartianTurretBolt, projectile.damage, 0f, projectile.owner)];
                    proj.hostile = false;
                    proj.friendly = true;
                }
                explosionTimer = 0;
            }
        }
    }

}
public class CyverthrowBolt : ModProjectile
{

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        ProjectileID.Sets.TrailingMode[projectile.type] = 0;
    }

    public override void SetDefaults()
    {
        projectile.width = 30;
        projectile.height = 10;
        projectile.aiStyle = -1;
        projectile.friendly = true;
        projectile.hostile = false;
        projectile.melee = true;
        projectile.penetrate = 3;
        projectile.timeLeft = 400;
        projectile.light = 0.5f;
        projectile.ignoreWater = true;
        projectile.tileCollide = false;
        projectile.extraUpdates = 1;


    }
    public override void AI()
    {
        if (Main.rand.NextFloat() < 0.5f)
        {
            int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, 0f, 0f, 0, new Color(255, 50, 0), 1.118421f);
            Dust dust = Main.dust[dustIndex];
            dust.shader = GameShaders.Armor.GetSecondaryShader(38, Main.LocalPlayer);
            dust.noGravity = true;
            dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
        }
        projectile.rotation = projectile.velocity.ToRotation();
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
    {
        Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
        for (int k = 0; k < projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            Color color = projectile.GetAlpha(Color.LightPink) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
            spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
        }
        return true;
    }
}


