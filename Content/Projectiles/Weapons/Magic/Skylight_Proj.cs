using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using System.IO;

namespace AerovelenceMod.Content.Projectiles.Weapons.Magic
{

    public class Skylight_Proj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_0";
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 70;
            Projectile.extraUpdates = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            if (Projectile.localAI[1] == 0)
            {
                Projectile.localAI[1] = 1.5f;
            }
            Vector2 dustPos = Projectile.Center;
            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.UltraBrightTorch);
                d.noGravity = true;
                d.velocity = Vector2.Zero;
                dustPos -= Projectile.velocity * .333f;
            }
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] >= Main.rand.Next(3, 24) && Projectile.velocity != Vector2.Zero)
            {
                if (Projectile.localAI[1] != 0)
                    Projectile.localAI[1] *= Main.rand.NextFloat(-0.9f, -1.1f);
                if (Math.Abs(Projectile.localAI[1]) > 2f)
                    Projectile.localAI[1] = 1.5f;
                Projectile.localAI[0] = 0;

                Vector2 aimPos = new Vector2(Projectile.ai[0], Projectile.ai[1]);
                aimPos = (aimPos - Projectile.Center).SafeNormalize(Vector2.Zero) * 12f;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity.RotatedBy(Projectile.localAI[1]), aimPos, .4f).SafeNormalize(Vector2.Zero)*12f;
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.localAI[0]);
            writer.Write(Projectile.localAI[1]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadSingle();
            Projectile.localAI[1] = reader.ReadSingle();
        }
    }
}
