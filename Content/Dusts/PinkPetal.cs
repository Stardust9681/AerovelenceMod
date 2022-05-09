﻿#region Using directives

using Terraria;
using Terraria.ModLoader;

#endregion

namespace AerovelenceMod.Content.Dusts
{
	public sealed class PinkPetal : ModDust
	{

		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = false;

			dust.velocity *= 0.5f;
			dust.velocity.Y -= 0.1f;

			dust.frame.X = 0;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.1f;
			
			dust.scale -= 0.02f;
			if (dust.scale < 0.2f)
			{
				dust.active = false;
			}
			
			return (false);
		}
	}
}
