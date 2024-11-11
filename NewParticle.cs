using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;
using System.Linq;

namespace StorybrewScripts
{
    public class NewParticle : StoryboardObjectGenerator
    {
        [Configurable] public string Path = "sb/pl.png";
        [Configurable] public int StartTime;
        [Configurable] public int EndTime;
        [Configurable] public int FadeIn;
        [Configurable] public int FadeOut;

        [Configurable] public double ParticleDuration = 2000;
        [Configurable] public double StartXOffset = 0;
        [Configurable] public double StartYOffset = 0;
        [Configurable] public double EndXRandomMin = 0;
        [Configurable] public double EndXRandomMax = 0;
        [Configurable] public double EndYRandomMin = 0;
        [Configurable] public double EndYRandomMax = 0;
        [Configurable] public double RandomDurationRange = 1000;
        [Configurable] public double ParticleAmount = 16;
        [Configurable] public bool RandomEasing;
        [Configurable] public Color4 Color = new Color4(1, 1, 1, 0.6f);
        [Configurable] public float ColorVariance = 0.6f;
        [Configurable] public double StartScale = 0.2;
        [Configurable] public double RandomScaleRange = 0.1;
        [Configurable] public bool Rotation;
        [Configurable] public bool Additive;
        [Configurable] public OsbOrigin Origin = OsbOrigin.Centre;

        public override void Generate()
        {
            var End = EndTime+ParticleDuration;
            var Start = StartTime-ParticleDuration;

            var RealStart = StartTime;
            var RealEndTime = EndTime;

            StartTime += -(int)ParticleDuration;
            EndTime += (int)ParticleDuration;
            
            if (StartTime == EndTime)
            {
                StartTime = (int)Beatmap.HitObjects.First().StartTime;
                EndTime = (int)Beatmap.HitObjects.Last().EndTime;
            }
            EndTime = Math.Min(EndTime, (int)AudioDuration);
            StartTime = Math.Min(StartTime, EndTime);

            var particleDuration = ParticleDuration > 0 ? ParticleDuration :
                Beatmap.GetTimingPointAt(StartTime).BeatDuration * 4;

            var layer = GetLayer("");
            using (var pool = new OsbSpritePool(layer, Path, Origin, (sprite, startTime, endTime) =>
            {
                var color = Color;
                if (ColorVariance > 0)
                {
                    ColorVariance = MathHelper.Clamp(ColorVariance, 0, 1);

                    var hsba = Color4.ToHsl(color);
                    var sMin = Math.Max(0, hsba.Y - ColorVariance * 0.5f);
                    var sMax = Math.Min(sMin + ColorVariance, 1);
                    var vMin = Math.Max(0, hsba.Z - ColorVariance * 0.5f);
                    var vMax = Math.Min(vMin + ColorVariance, 1);

                    color = Color4.FromHsl(new Vector4(
                        hsba.X,
                        (float)Random(sMin, sMax),
                        (float)Random(vMin, vMax),
                        hsba.W));
                }
                if (color != Color4.White) sprite.Color(RealStart, color);

                sprite.Fade(StartTime, RealStart-FadeIn, 0, 0);
                sprite.Fade(RealStart-FadeIn, RealStart, 0, Color.A);
                sprite.Fade(RealEndTime-FadeOut, RealEndTime, Color.A, 0);
                sprite.Fade(RealEndTime, EndTime, 0, 0);

                if (Additive) sprite.Additive(StartTime);

            }))
            {
                
                var timeStep = particleDuration / ParticleAmount;
                for (var startTime = (double)StartTime; startTime <= EndTime - particleDuration; startTime += timeStep)
                {
                    var newparticleDuration = particleDuration + Random(RandomDurationRange, -RandomDurationRange);
                    var endTime = startTime + newparticleDuration;

                    var sprite = pool.Get(startTime, endTime);

                    var startX = Random(-100, 740) + StartXOffset;
                    var startY = Random(0, 480) + StartYOffset;
                    var endX = startX + Random(EndXRandomMin, EndXRandomMax);
                    var endY = startY + Random(EndYRandomMin, EndYRandomMax);

                    if(RandomEasing) sprite.MoveY(OsbEasing.InOutSine, startTime, endTime, startY, endY);
                    else sprite.MoveY(startTime, endTime, startY, endY);
                    sprite.MoveX(startTime, endTime, startX, endX);

                    var randomscale = Random(-RandomScaleRange, RandomScaleRange);
                    
                    sprite.Scale(OsbEasing.OutSine, startTime, startTime + 500, 0, StartScale + randomscale);
                    sprite.Scale(OsbEasing.InSine, endTime - 500, endTime, StartScale + randomscale, 0);


                    if (Rotation) sprite.Rotate(startTime, Random(-Math.PI, Math.PI));
                }
            }
        }
    }
}
