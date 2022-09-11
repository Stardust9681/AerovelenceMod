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

namespace AerovelenceMod.Content.Projectiles.Weapons.Magic
{
    public class CrystalGlade_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 18;
            Projectile.alpha = 80;
            Projectile.penetrate = 2;
            Projectile.friendly = true;
            Projectile.scale = .94f;
            Projectile.timeLeft = 180;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = 0.15708f * Main.rand.Next(30);
                Projectile.ai[1] = Main.rand.NextFloat(9f, 13.5f);
            }
            Projectile.ai[0] += 0.15708f; //~pi/30

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Main.rand.NextBool())
            {
                Vector2 offset = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(MathHelper.PiOver2);
                float sinAI0 = MathF.Sin(Projectile.ai[0]);
                float cosAI0 = MathF.Cos(Projectile.ai[0]);
                offset = (offset * sinAI0) * Projectile.ai[1];
                Vector2 spawnPos = Projectile.Center + offset;
                float scale = (cosAI0 + 1.7f)* 1.4816f; //
                Dust.NewDustDirect(spawnPos, 1, 1, DustID.GreenTorch, Scale: scale).noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.position -= oldVelocity;
            Projectile.velocity = oldVelocity;
            Projectile.ai[1] = (MathF.Sin(MathHelper.ToRadians(Projectile.timeLeft * 1.5f))+3)*6;
            if(Projectile.timeLeft < 2)
            {
                for(int i = 0; i < 6; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.GreenTorch, Scale: Main.rand.NextFloat(2f, 4f));
                    d.noGravity = true;
                    d.velocity = oldVelocity.RotatedByRandom(0.15708f) * Main.rand.Next(new int[] { -1, 1 });
                }
                Projectile.active = false;
            }
            return false;
        }
    }
}
