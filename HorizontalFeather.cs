using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class HorizontalFeather : StoryboardObjectGenerator
    {
        [Group("Timing")]
        [Configurable] public int StartTime;
        [Configurable] public int TrueStartTime;
        [Configurable] public int EndTime;

        [Group("Sprite")]
        [Configurable] public OsbOrigin Origin = OsbOrigin.Centre;
        [Configurable] public Vector2 Scale = new Vector2(1, 1);
        [Configurable] public Color4 Color = Color4.White;
        [Configurable] public float ColorVariance = 0.6f;
        [Configurable] public bool Additive = false;
        [Configurable] public bool Rotation = false;

        [Group("Spawn")]
        [Configurable] public int ParticleCount = 32;
        [Configurable] public float Lifetime = 1000;
        [Configurable] public float LifetimeRandom = 200;
        [Configurable] public OsbEasing Easing = OsbEasing.None;

        [Group("Changes")]
        [Configurable] public double FinalScale = 0.12;
        [Configurable] public double RandomScale = 0.02;
        [Configurable] public double Opacity = 0.02;


        public override void Generate()
        {
            var duration = (double)(EndTime - StartTime);
            
            //var bitmap = GetMapsetBitmap(Path);

            var layer = GetLayer("");

            List<string> spriteList = new List<string>
            {
                "sb/particles/transition/0.png",
                "sb/particles/transition/1.png",
                "sb/particles/transition/2.png",
                "sb/particles/transition/3.png",
                "sb/particles/transition/4.png",
            };

		    for (var i = 0; i < ParticleCount; i++)
            { 
                var frameCount = 1000/20;
                var loopDuration = Lifetime + Random(-LifetimeRandom, LifetimeRandom);
                loopDuration = (int)Math.Floor(loopDuration/frameCount)*frameCount;
                var loopCount = Math.Max(1, (int)Math.Floor(duration / loopDuration));

                var startTime = Random(StartTime, StartTime +  loopDuration);
                var endTime = startTime + loopDuration * loopCount;

                var angle = Random(-Math.PI, Math.PI);

                var startPosition = new Vector2(-200, Random(0-100, 480+100));
                var endPosition = startPosition + new Vector2(854+200, Random(-100, 100));

                var particle = layer.CreateSprite(spriteList[Random(0,5)]);

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

                if (color.R != 1 || color.G != 1 || color.B != 1)
                    particle.Color(startTime, color);
                if(startTime < TrueStartTime)
                    particle.Fade(TrueStartTime, TrueStartTime, 0 , Opacity);

                if(endTime > EndTime)
                    particle.Fade(EndTime, EndTime, Opacity, 0);

                

                var startRotation = Random(-Math.PI, Math.PI);
                var endRotation = startRotation+Random(Math.PI/4, Math.PI/2);
                
                particle.Scale(startTime, Random(FinalScale - RandomScale, FinalScale + RandomScale));

                particle.StartLoopGroup(startTime, loopCount);
                //for (int y = 0; y < loopDuration/frameCount; y++)
                //{
                //    particle.Move(y * frameCount, (y+1) * frameCount, startPosition+changeVector*y, startPosition+changeVector*y);
                //    particle.Rotate(y * frameCount, (y+1) * frameCount, startRotation+changeRotation*y, startRotation+changeRotation*y);
                //}
                particle.MoveX(0, loopDuration, startPosition.X, endPosition.X);
                particle.MoveY(OsbEasing.InOutSine, 0, loopDuration, startPosition.Y, endPosition.Y);
                particle.Rotate(0, loopDuration, startRotation, endRotation);
                particle.EndGroup();
            }
        }
    }
}
