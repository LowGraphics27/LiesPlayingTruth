using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Commands;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Animations;
using System.Runtime.CompilerServices;
using System.Drawing.Printing;
using System.Threading;
using System.Drawing;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace StorybrewScripts
{
    public class VerseScript : StoryboardObjectGenerator
    {
        StoryboardLayer bgLayer, mainLayer, fgLayer, overLayer;
        [Configurable] public Color4 DarkGrey;
        [Configurable] public Color4 DarkRed;
        
        [Configurable] public int seed;
        [Configurable] public int seed2;
        [Configurable] public int seed3;

        public override void Generate()
        {
            bgLayer = GetLayer("Background");
            mainLayer = GetLayer("Main");
            fgLayer = GetLayer("Foreground");
            overLayer = GetLayer("Overlay");

            var screenSize = new Vector2(854, 480);

            //----------------------------------------- 1A --------------------------------------------------//
            var StartTime = 22999;
            var MidTime = 27568;
            var EndTime = 32746;
            
            CreateLightOverlay("sb/overlay/light/2.jpg",StartTime, MidTime, .1f);
            CreateLightOverlay("sb/overlay/light/1.jpg",MidTime, EndTime, .1f);

            var q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime-50, screenSize);
            q.Color(StartTime, Color4.Black);
            q.Fade(StartTime-250, StartTime, 0, 1);
            q.Fade(StartTime, 23456, 1, 0);

            var bg = bgLayer.CreateSprite("sb/grad.jpg");
            bg.Scale(StartTime, 4/9f);
            bg.Fade(EndTime, .4f);
    
            var girl = mainLayer.CreateSprite("sb/asset/1a_face.png");
            var imgScale = 480f / GetMapsetBitmap("sb/asset/1a_face.png").Height;
            girl.Scale(StartTime, MidTime, imgScale * 1.55f, imgScale* 1.45f);
            girl.Scale(MidTime, EndTime, imgScale * 1.1f, imgScale);
            girl.Rotate(StartTime, MidTime, -0.06, -0.04);
            girl.Rotate(MidTime, EndTime, -0.04, 0);
            girl.Fade(31299-50, 1);
            girl.Fade(31299-50, 31299, 0, 0);
            girl.Fade(31299, 1);
            girl.Fade(32136, 32136+50, 0, 0);
            girl.Fade(32136+50, 32441, 1, 1);
            girl.Fade(32441, 32441+50, 0, 0);
            girl.Fade(32441+50, 1);
            girl.Fade(EndTime-100, EndTime, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                girl.Move(32441 + i * 152/4, Random(320-1f, 320+1f), Random(240-1f, 240+1f));
            }

            var r = girl.RotationAt(31299-50);
            var s = girl.ScaleAt(31299-50);
            var girl2 = mainLayer.CreateSprite("sb/asset/1a_face_blur.png");
            girl2.Scale(31299-50, s.X * 2f);
            girl2.Rotate(31299, r);

            r = girl.RotationAt(32136);
            s = girl.ScaleAt(32136);
            girl2 = mainLayer.CreateSprite("sb/asset/1a_face_blur.png");
            girl2.Scale(32136, s.X * 2f);
            girl2.Rotate(32136 + 50, r);

            r = girl.RotationAt(32441);
            s = girl.ScaleAt(32441);
            girl2 = mainLayer.CreateSprite("sb/asset/1a_face_blur.png");
            girl2.Scale(32441, s.X * 2f);
            girl2.Rotate(32441 + 50, r);

            r = girl.RotationAt(EndTime-100);
            s = girl.ScaleAt(EndTime-100);
            girl2 = mainLayer.CreateSprite("sb/asset/1a_face_blur.png");
            girl2.Scale(EndTime-100, s.X * 2f);
            girl2.Rotate(EndTime-50, r);

            var frame = mainLayer.CreateSprite("sb/asset/1a_frame.png");
            var position = new Vector2(220, 340);
            frame.Scale(StartTime, MidTime, imgScale * 1.5f * 1.5f, imgScale* 1.35f * 1.5f);
            frame.Move(StartTime, MidTime, MoveCentreOrigin(position, 1.5f), MoveCentreOrigin(position, 1.35f));
            position = new Vector2(200, 360);
            frame.Scale(MidTime, EndTime, imgScale * 1.1f * 1.5f, imgScale * 1.5f);
            frame.Move(MidTime, EndTime, MoveCentreOrigin(position, 1.1f), position);

            var feather = mainLayer.CreateSprite("sb/asset/1a_feather1.png");
            position = new Vector2(600, 150);
            feather.Scale(StartTime, MidTime, imgScale * 1.45f, imgScale* 1.4f);
            feather.Scale(MidTime, EndTime, imgScale * 1.05f, imgScale);
            feather.Move(StartTime, MidTime, MoveCentreOrigin(position, 1.45f), MoveCentreOrigin(position, 1.4f));
            feather.Move(MidTime, EndTime, MoveCentreOrigin(position, 1.05), position);

            feather = mainLayer.CreateSprite("sb/asset/1a_feather2.png");
            position = new Vector2(70, 320);
            feather.Scale(StartTime, MidTime, imgScale * 1.8f * 1.4f, imgScale* 1.5f * 1.4f);
            feather.Move(StartTime, MidTime, MoveCentreOrigin(position, 1.8f), MoveCentreOrigin(position, 1.5f));
            position = new Vector2(80, 330);
            feather.Scale(MidTime, EndTime, imgScale * 1.3f * 1.6f, imgScale*1.2f  * 1.6f);
            feather.Move(MidTime, EndTime, MoveCentreOrigin(position, 1.3f), MoveCentreOrigin(position, 1.2f));

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(MidTime, screenSize);
            q.Color(MidTime-50, Color4.Black);
            q.StartLoopGroup(MidTime-200, 4);
            q.Fade(0, 50, 0 ,1);
            q.EndGroup();

            StartTime = 31299;
            EndTime = 32746-100;
            var glass = fgLayer.CreateSprite("sb/asset/crack.png");
            glass.Scale(StartTime, EndTime, 4/9f, 4/9f);
            for (int i = 0; i < 4; i++)
            {
                glass.Move(32441 + i * 152/4, Random(320-1f, 320+1f), Random(240-1f, 240+1f));
            }
            glass.Fade(32136, 1);
            glass.Fade(32136, 32136+50, 0, 0);
            glass.Fade(32136+50, 32441, 1, 1);
            glass.Fade(32441, 32441+50, 0, 0);
            glass.Fade(32441+50, 1);
            glass.Color(StartTime, Color4.White);

            StartTime = 31299-50;
            EndTime = 31299;
            q = fgLayer.CreateSprite("sb/asset/crack_blur.png");
            q.Scale(StartTime, EndTime, 4/9f, 4/9f);
            q.Color(StartTime, Color4.White);
            StartTime = 32136;
            q = fgLayer.CreateSprite("sb/asset/crack_blur.png");
            q.Scale(StartTime, StartTime+50, 4/9f, 4/9f);
            q.Color(StartTime, Color4.White);
            StartTime = 32441;
            q = fgLayer.CreateSprite("sb/asset/crack_blur.png");
            q.Scale(StartTime, StartTime+50, 4/9f, 4/9f);
            q.Color(StartTime, Color4.White);
            StartTime = 32746-100;
            EndTime = 32746;
            q = fgLayer.CreateSprite("sb/asset/crack_blur.png");
            q.Scale(StartTime, EndTime, 4/9f, 4/9f);
            q.Color(StartTime, Color4.White);

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(EndTime-50, 854, 480);
            q.Color(EndTime, Color4.White);

            //----------------------------------------- 1B --------------------------------------------------//

            StartTime = 32746;
            EndTime = 37619-152;

            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            Sprite3d g = new Sprite3d
            {
                Layer = mainLayer,
                SpritePath = "sb/asset/full_s.png",
                UseDistanceFade = false
            };
            g.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            var scale = 0.55f;
            g.PositionX.Add(StartTime, 50);
            g.PositionY.Add(StartTime, 100);
            g.PositionZ.Add(StartTime, -1);
            g.ScaleX.Add(StartTime, scale);
            g.ScaleY.Add(StartTime, scale);
            g.Coloring.Add(StartTime, DarkGrey);
            g.Rotation.Add(StartTime, .45f);
            scene.Add(g);

            List<string> spriteList = new()
                {
                    "sb/particles/shard2/0.png",
                    "sb/particles/shard2/1.png",
                    "sb/particles/shard2/2.png",
                    "sb/particles/shard2/3.png",
                    "sb/particles/shard2/4.png",
                    "sb/particles/shard2/5.png",
                    "sb/particles/shard2/6.png",
                    "sb/particles/shard2/7.png",
                };
            
            Random random = new Random(seed);

            for (int i = 0; i < 20; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = fgLayer,
                    SpritePath = spriteList[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.4f - 0.1f) + 0.1f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-500, 501));
                p.PositionY.Add(StartTime, random.Next(-300, 301));
                p.PositionZ.Add(StartTime, random.Next(5, 25));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, DarkRed);
                scene.Add(p);
            }

            List<string> spriteList2 = new()
                {
                    "sb/particles/shard/1.png",
                    "sb/particles/shard/2.png",
                    "sb/particles/shard/3.png",
                    "sb/particles/shard/4.png",
                    "sb/particles/shard/5.png",
                    "sb/particles/shard/6.png",
                    "sb/particles/shard/7.png",
                    "sb/particles/shard/8.png",
                };
        
            random = new Random(seed2);

            for (int i = 0; i < 64; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = fgLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-500, 501));
                p.PositionY.Add(StartTime, random.Next(-300, 301));
                p.PositionZ.Add(StartTime, random.Next(5, 25));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, DarkRed);
                scene.Add(p);
            }

            scene.Root.PositionZ.Add(StartTime, 37);
            scene.Root.PositionZ.Add(StartTime+750, -2, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionZ.Add(EndTime, -5);
            scene.Root.Rotation.Add(StartTime, -0.02f);
            scene.Root.Rotation.Add(EndTime, 0.03f);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);

            StartTime = 37619;
            EndTime = 42644;

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            g = new Sprite3d
            {
                Layer = mainLayer,
                SpritePath = "sb/asset/full_s.png",
                UseDistanceFade = false
            };
            g.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            scale = 0.55f;
            g.PositionX.Add(StartTime, 150);
            g.PositionY.Add(StartTime, 220);
            g.PositionZ.Add(StartTime, -1);
            g.ScaleX.Add(StartTime, scale);
            g.ScaleY.Add(StartTime, scale);
            g.Coloring.Add(StartTime, DarkGrey);
            g.Rotation.Add(StartTime, .45f);
            scene.Add(g);

            Sprite3d m = new Sprite3d
            {
                Layer = mainLayer,
                SpritePath = "sb/asset/mask.png",
                UseDistanceFade = false
            };
            m.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            scale = 0.55f/4f;
            m.PositionX.Add(StartTime, -132);
            m.PositionY.Add(StartTime, 1);
            m.PositionZ.Add(StartTime, -1);
            m.ScaleX.Add(StartTime, scale);
            m.ScaleY.Add(StartTime, scale);
            m.Rotation.Add(StartTime, .45f);
            m.Coloring.Add(StartTime, DarkRed);
            scene.Add(m);
            
            random = new Random(seed3);

            for (int i = 0; i < 10; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = fgLayer,
                    SpritePath = spriteList[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.4f - 0.1f) + 0.1f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-400, 401));
                p.PositionY.Add(StartTime, random.Next(-200, 201));
                p.PositionZ.Add(StartTime, random.Next(5, 10));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, DarkRed);
                scene.Add(p);
            }
        
            for (int i = 0; i < 32; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = fgLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-300, 301));
                p.PositionY.Add(StartTime, random.Next(-200, 201));
                p.PositionZ.Add(StartTime, random.Next(5, 8));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, DarkRed);
                scene.Add(p);
            }

            scene.Root.PositionZ.Add(StartTime, 16);
            scene.Root.PositionZ.Add(StartTime+500, 20, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionZ.Add(EndTime, 21);
            scene.Root.Rotation.Add(EndTime, 0.03f);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);

            //----------------------------------------- 1C --------------------------------------------------//

            StartTime = 42644;
            EndTime = 46223;
            
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, .2f);
            var girlScale = 480f/GetMapsetBitmap("sb/asset/1c_girl.png").Height;
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/background_blur.jpg", OsbEasing.OutExpo,
                easingTime: 750, zoomScale: girlScale * 1.2f, startScale : girlScale * 1.05f, endScale: girlScale * 1f);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/1c_girl.png", OsbEasing.OutExpo,
                easingTime: 750, zoomScale: girlScale * 1.6f, startScale : girlScale * 1.15f, endScale: girlScale * 1.1f);
            CreateAnimation(fgLayer, StartTime, EndTime, "sb/asset/1a_frame.png", OsbEasing.OutExpo,
                easingTime: 750, zoomScale: 2f, startScale : 1.2f, endScale: 1.1f, startPosition: new Vector2(0, 230), startRotation: 2.5f);
            CreateAnimation(fgLayer, StartTime, EndTime, "sb/asset/1a_feather2.png", OsbEasing.OutExpo,
                easingTime: 750, zoomScale: 3f, startScale : 1.25f, endScale: 1.1f, startPosition: new Vector2(580, 210), startRotation: 3f);
            bg = bgLayer.CreateSprite("sb/q.png");
            bg.Scale(StartTime, 4/9f);
            bg.Fade(EndTime, 1f);

            StartTime = 46223; 
            EndTime = 47517;
            bg = bgLayer.CreateSprite("sb/asset/break/grad_bw.jpg");
            bg.Scale(StartTime, 4/9f);
            bg.Fade(EndTime, .5f);

            var noise = fgLayer.CreateAnimation("sb/overlay/noise/noise.jpg", 4, 152/2, OsbLoopType.LoopForever);
            noise.Scale(StartTime, EndTime, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width);
            noise.Fade(StartTime, 0.25);

            StartTime = 47517;
            EndTime = 52390;
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, .2f);
            bg = bgLayer.CreateSprite("sb/background_blur.jpg");
            bg.Scale(StartTime, EndTime, 4/9f*1.05f, 4/9f);
            bg.Fade(EndTime, .5f);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/1c_girl.png",
                startScale : girlScale * 1.1f, endScale: girlScale * 1f,  startRotation: 3.14f);
            CreateAnimation(fgLayer, StartTime, EndTime, "sb/asset/1a_frame.png",
                startScale : 1.4f, endScale: 1.1f, startPosition: new Vector2(620, 200), startRotation: -1.2f);
            CreateAnimation(fgLayer, StartTime, EndTime, "sb/asset/1a_feather2.png",
                startScale : 1.5f, endScale: 1.1f, startPosition: new Vector2(150, 400), startRotation: 0);


            StartTime = 52390; 
            EndTime = 57263;
            bg = bgLayer.CreateSprite("sb/asset/break/grad_bw.jpg");
            bg.Scale(StartTime, 4/9f);
            bg.Fade(StartTime, 56959, .5f, .8f);
            bg.Fade(56959, 57111, .8f, .5f);
            bg.Fade(57111, EndTime, .2f, .2f);
            bg.Fade(EndTime, 58482, .2f, .2f);

            EndTime = 58482;
            noise = fgLayer.CreateAnimation("sb/overlay/noise/noise.jpg", 4, 152/2, OsbLoopType.LoopForever);
            noise.Scale(StartTime, EndTime, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width);
            noise.Fade(57263, EndTime, .25f, 0);

            StartTime = 52390;
            EndTime = 58482;

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);
            
            for (int i = 0; i < 200; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-300, 301));
                p.PositionY.Add(StartTime, random.Next(-300, 1601));
                p.PositionZ.Add(StartTime, random.Next(5, 10));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Opacity.Add(StartTime, Random(0.1f,0.3f));
                scene.Add(p);
            }
            scene.Root.PositionZ.Add(StartTime, 20f);
            scene.Root.PositionY.Add(StartTime-2000, 500);
            scene.Root.PositionY.Add(EndTime, -1500f, EasingFunctions.ToEasingFunction(OsbEasing.InExpo));

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);

            //----------------------------------------- 2A --------------------------------------------------//

            StartTime = 88939;
            EndTime = 98685;

            bg = bgLayer.CreateSprite("sb/asset/break/grad_bw.jpg");
            bg.Scale(StartTime, EndTime, 4/9f, 4/9f);

            for (int i = 0; i < 32; i++)
            {
                position = new Vector2(Random(-107, 747),Random(0, 480));
                q = mainLayer.CreateSprite(spriteList2[Random(0, 8)]);
                q.Move(StartTime, EndTime, position, position + new Vector2(0, Random(-40, -5)));
                q.Scale(StartTime, Random(0.03, 0.05));
                q.Fade(StartTime, Random(.5, .8));
            }

            var sprite = mainLayer.CreateSprite("sb/asset/break/6.png");
            position = new Vector2(90, 450);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-30));
            sprite.Scale(StartTime, 0.3f);
            sprite.FlipH(StartTime);

            sprite = mainLayer.CreateSprite("sb/asset/break/0.png");
            position = new Vector2(100, 150);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-90));
            sprite.Scale(StartTime, 0.35f);

            sprite = mainLayer.CreateSprite("sb/asset/break/1.png");
            position = new Vector2(320, 320);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60));
            sprite.Scale(StartTime, 0.45f);

            sprite = mainLayer.CreateSprite("sb/asset/break/2.png");
            position = new Vector2(-80, 360);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60));
            sprite.Scale(StartTime, 0.6f);
            sprite.Rotate(StartTime, 4.14);

            sprite = mainLayer.CreateSprite("sb/asset/break/5.png");
            position = new Vector2(590, 110);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-40));
            sprite.Scale(StartTime, 0.5f);

            sprite = mainLayer.CreateSprite("sb/particles/shard2/4.png");
            position = new Vector2(650, 390);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60));
            sprite.Scale(StartTime, 0.6f);

            sprite = mainLayer.CreateSprite("sb/particles/shard2/7.png");
            position = new Vector2(580, 280);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-70));
            sprite.Scale(StartTime, 0.3f);
            sprite.Rotate(StartTime, .9f);

            
            noise = fgLayer.CreateAnimation("sb/overlay/noise/noise.jpg", 4, 152/2, OsbLoopType.LoopForever);
            noise.Scale(StartTime, EndTime, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width);
            noise.Fade(StartTime, 0.25);
            
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0, 1);
            q.Fade(StartTime, StartTime+250, 1, 0);
            q.Color(StartTime, Color4.Black);

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, EndTime-500, EndTime, 0, 1);

            //----------------------------------------- 2B --------------------------------------------------//

            
            StartTime = 98685;
            EndTime = 100893;
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, 107974, .1f);
            var gScale = 480f/GetMapsetBitmap("sb/asset/bw.png").Height;
            var bScale = 480f/GetMapsetBitmap("sb/asset/bg_dark.jpg").Height;
            position = new Vector2(220, 530);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/bw.png", Easing: OsbEasing.OutExpo, easingTime: 1000,
                startScale : 2f * gScale, zoomPosition: position + new Vector2(200, 0),startPosition: position, endPosition: position + new Vector2(-5, 0));
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/asset/bg_dark.jpg", Easing: OsbEasing.OutExpo, easingTime: 1000,
                startScale : 2f * bScale, zoomPosition: position + new Vector2(100, 0),startPosition: position, endPosition: position + new Vector2(-2, 0));


            StartTime = 100893;
            EndTime = 103329;
            position = new Vector2(100, 0);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/bw.png", Easing: OsbEasing.OutExpo, easingTime: 1000,
                startScale : 2f * gScale, zoomPosition: position + new Vector2(-200, 0),startPosition: position, endPosition: position + new Vector2(5, 0));
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/asset/bg_dark.jpg", Easing: OsbEasing.OutExpo, easingTime: 1000,
                startScale : 2f * bScale, zoomPosition: position + new Vector2(-100, 0),startPosition: position, endPosition: position + new Vector2(2, 0));

            StartTime = 103329;
            EndTime = 105766;
            position = new Vector2(650, -100);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/bw.png", Easing: OsbEasing.OutExpo, easingTime: 1000,
                startScale : 2.2f * gScale, zoomPosition: position + new Vector2(0, 100),startPosition: position, endPosition: position + new Vector2(0, -3));
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/asset/bg_dark.jpg", Easing: OsbEasing.OutExpo, easingTime: 1000,
                startScale : 2.2f * bScale, zoomPosition: position + new Vector2(0, 50),startPosition: position, endPosition: position + new Vector2(0, -1));

            StartTime = 105766;
            EndTime = 106984;
            position = new Vector2(500, 330);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/bw.png", Easing: OsbEasing.OutExpo, easingTime: 1000,
                startScale : 1.8f * gScale, zoomPosition: position + new Vector2(0, -100),startPosition: position, endPosition: position + new Vector2(0, 1));
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/asset/bg_dark.jpg", Easing: OsbEasing.OutExpo, easingTime: 1000,
                startScale : 1.8f * bScale, zoomPosition: position + new Vector2(0, -50),startPosition: position, endPosition: position + new Vector2(0, 1));


            StartTime = 106984;
            EndTime = 108279;
            position = new Vector2(350, 260);
            var colorScale = 480f/GetMapsetBitmap("sb/asset/colour.png").Height;
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/colour.png", Easing: OsbEasing.OutExpo, easingTime: 1000,
                zoomScale: 1.4f * colorScale, startScale : colorScale, endScale: colorScale, startPosition: position);
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/asset/bg_dark.jpg", Easing: OsbEasing.OutExpo, easingTime: 1000,
                zoomScale: 1.2f * bScale, startScale : bScale, endScale: gScale);

            StartTime = 106984;
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.OutCubic, StartTime, StartTime+500, 1, 0);

            StartTime = 98685;
            EndTime = 108279;
            var vign = fgLayer.CreateSprite("sb/v.png");
            vign.Scale(StartTime, EndTime, 4/9f, 4/9f);
            vign.Color(StartTime, Color4.Black);
            vign.Fade(105766, 105766, 1, 0);

            StartTime = 107974;
            EndTime = 108126;
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Color(EndTime, DarkGrey);

            StartTime = 108126;
            EndTime = 108279;
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Color(EndTime, DarkRed);

            //----------------------------------------- 2C --------------------------------------------------//
            StartTime = 108279;
            EndTime = 112999;

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            g = new Sprite3d
            {
                Layer = mainLayer,
                SpritePath = "sb/asset/full_s.png",
                UseDistanceFade = false
            };
            g.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            scale = 0.55f;
            g.PositionX.Add(StartTime, 50);
            g.PositionY.Add(StartTime, 80);
            g.PositionZ.Add(StartTime, -1);
            g.ScaleX.Add(StartTime, scale);
            g.ScaleY.Add(StartTime, scale);
            g.Coloring.Add(StartTime, DarkGrey);
            g.Rotation.Add(StartTime, .2f);
            scene.Add(g);

            random = new Random(9);
            for (int i = 0; i < 20; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = fgLayer,
                    SpritePath = spriteList[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.4f - 0.1f) + 0.1f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-500, 501));
                p.PositionY.Add(StartTime, random.Next(-300, 301));
                p.PositionZ.Add(StartTime, random.Next(5, 25));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, DarkRed);
                scene.Add(p);
            }

            for (int i = 0; i < 64; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = fgLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-500, 501));
                p.PositionY.Add(StartTime, random.Next(-300, 301));
                p.PositionZ.Add(StartTime, random.Next(5, 25));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, DarkRed);
                scene.Add(p);
            }

            scene.Root.PositionZ.Add(StartTime, 37);
            scene.Root.PositionZ.Add(StartTime+750, -2, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionZ.Add(EndTime-152, -5);
            scene.Root.PositionZ.Add(EndTime, -10, EasingFunctions.ToEasingFunction(OsbEasing.InExpo));

            scene.Root.Rotation.Add(StartTime, 0);
            scene.Root.Rotation.Add(EndTime, 0.05f);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);


            StartTime = 112999;
            EndTime = 113152;
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Color(EndTime, Color4.Black);

            StartTime = 113152;
            EndTime = 118177;
            gScale = 480f/GetMapsetBitmap("sb/asset/2c_face.png").Height;
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/2c_face.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 2f * gScale, startScale : gScale * 1.05f, endScale: gScale);
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/a1.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 1.35f * gScale, startScale : gScale * 1.01f, endScale: gScale);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/particles/feather/6.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 3f * gScale, startScale : gScale * 1.1f, endScale: gScale, startPosition: new Vector2(520, 100), startRotation: 1f);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/particles/feather/8.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 3f * gScale, startScale : gScale * 1.1f, endScale: gScale, startPosition: new Vector2(720, 350), startRotation: 1f);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/particles/feather/7.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 3f * gScale, startScale : gScale * 1.1f, endScale: gScale, startPosition: new Vector2(-50, 200), startRotation: -1f);

            for (int i = 0; i < 16; i++)
            {
                position = new Vector2(Random(-107, 747),Random(0, 480));
                scale = Random(0.03f, 0.05f);
                q = mainLayer.CreateSprite(spriteList2[Random(0, 8)]);
                q.Move(OsbEasing.OutExpo, StartTime, StartTime + 750, MoveCentreOrigin(position, 3f), MoveCentreOrigin(position, 1.1f));
                q.Move(StartTime + 750, EndTime, MoveCentreOrigin(position, 1.1f), position);
                q.Scale(OsbEasing.OutExpo, StartTime, StartTime + 750, scale * 3f, scale * 1.1f);
                q.Scale(StartTime + 750, EndTime, scale * 1.1f, scale);
            }

            vign = fgLayer.CreateSprite("sb/v.png");
            vign.Scale(StartTime, EndTime, 4/9f, 4/9f);
            vign.Color(StartTime, Color4.Black);
            vign.Fade(StartTime, .5f);

            //----------------------------------------- 2D --------------------------------------------------//

            StartTime = 118177;
            EndTime = 127923;
            bg = fgLayer.CreateSprite("sb/asset/break/grad_bw.jpg");
            bg.Scale(StartTime, 4/9f);
            bg.Fade(EndTime, .5f);
            noise = overLayer.CreateAnimation("sb/overlay/static/static.jpg", 83, 1000/30, OsbLoopType.LoopForever);
            noise.Scale(StartTime, 480f/GetMapsetBitmap("sb/overlay/static/static0.jpg").Height);
            noise.Fade(StartTime, .3f);
            noise.Additive(EndTime);
            noise = overLayer.CreateAnimation("sb/overlay/noise/noise.jpg", 4, 152/2, OsbLoopType.LoopForever);
            noise.Scale(StartTime, EndTime, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width);
            noise.Fade(StartTime, 0.2);

            StartTime = 118177;
            var vhs = overLayer.CreateAnimation("sb/overlay/vhs/vhs.jpg", 8, 1000/30, OsbLoopType.LoopForever);
            vhs.Additive(StartTime);
            vhs.Scale(StartTime, 480f/GetMapsetBitmap("sb/overlay/vhs/vhs0.jpg").Height);
            vhs.Fade(StartTime, StartTime + 250, .5f, 0);

            StartTime = 123050;
            vhs = overLayer.CreateAnimation("sb/overlay/vhs/vhs.jpg", 8, 1000/30, OsbLoopType.LoopForever);
            vhs.Additive(StartTime);
            vhs.Scale(StartTime, 480f/GetMapsetBitmap("sb/overlay/vhs/vhs0.jpg").Height);
            vhs.Fade(StartTime, StartTime + 250, .5f, 0);

            StartTime = 118177;
            EndTime = 123050;

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            g = new Sprite3d
            {
                Layer = mainLayer,
                SpritePath = "sb/asset/full_s.png",
                UseDistanceFade = false
            };
            g.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            scale = 0.65f;
            g.PositionX.Add(StartTime, 50);
            g.PositionY.Add(StartTime, 80);
            g.PositionZ.Add(StartTime, -1);
            g.ScaleX.Add(StartTime, scale);
            g.ScaleY.Add(StartTime, scale);
            g.Coloring.Add(StartTime, Color4.DarkGray);
            g.Rotation.Add(StartTime, .2f);
            scene.Add(g);

            random = new Random(12334);
            for (int i = 0; i < 20; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.4f - 0.1f) + 0.1f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-700, 201));
                p.PositionY.Add(StartTime, random.Next(-300, 301));
                p.PositionZ.Add(StartTime, random.Next(5, 14));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, Color4.White);
                scene.Add(p);
            }

            for (int i = 0; i < 64; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-600, 50));
                p.PositionY.Add(StartTime, random.Next(-200, 201));
                p.PositionZ.Add(StartTime, random.Next(5, 20));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, Color4.White);
                scene.Add(p);
            }

            scene.Root.PositionZ.Add(StartTime, 15);
            scene.Root.PositionY.Add(StartTime, 100+20);
            scene.Root.PositionY.Add(StartTime+750, 20+20, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionY.Add(EndTime, 0+20);

            scene.Root.PositionX.Add(StartTime, 250);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);


            StartTime = 123050;
            EndTime = 127923;

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            g = new Sprite3d
            {
                Layer = mainLayer,
                SpritePath = "sb/asset/full_s.png",
                UseDistanceFade = false
            };
            g.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            scale = 0.65f;
            g.PositionX.Add(StartTime, 50);
            g.PositionY.Add(StartTime, 80);
            g.PositionZ.Add(StartTime, -1);
            g.ScaleX.Add(StartTime, scale);
            g.ScaleY.Add(StartTime, scale);
            g.Coloring.Add(StartTime, Color4.DarkGray);
            g.Rotation.Add(StartTime, .2f);
            scene.Add(g);

            random = new Random(7);
            for (int i = 0; i < 20; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.4f - 0.1f) + 0.1f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-700, 201));
                p.PositionY.Add(StartTime, random.Next(-300, 301));
                p.PositionZ.Add(StartTime, random.Next(5, 14));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, Color4.White);
                scene.Add(p);
            }

            for (int i = 0; i < 64; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-600, 50));
                p.PositionY.Add(StartTime, random.Next(-200, 201));
                p.PositionZ.Add(StartTime, random.Next(5, 20));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Coloring.Add(StartTime, Color4.White);
                scene.Add(p);
            }

            scene.Root.PositionZ.Add(StartTime, 15);
            scene.Root.PositionY.Add(StartTime, -50);
            scene.Root.PositionY.Add(EndTime, -80);
            scene.Root.Rotation.Add(StartTime, (float)Math.PI);

            scene.Root.PositionX.Add(StartTime, -250);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);

            //----------------------------------------- 2E --------------------------------------------------//
            
            StartTime = 127923;
            EndTime = 134015;
            

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, 854, 480);
            q.Color(StartTime, Color4.Black);
            q.Fade(OsbEasing.InExpo, StartTime-500, StartTime, 0, 1);
            AnimationSection2E("sb/asset/2e/1.png", new Vector2(-30, 65), 1);
            AnimationSection2E("sb/asset/2e/0.png", new Vector2(51, 145), 2);
            AnimationSection2E("sb/asset/2e/2.png", new Vector2(5, 290), 15);
            AnimationSection2E("sb/asset/2e/3.png", new Vector2(154, 359), 4);
            AnimationSection2E("sb/asset/2e/4.png", new Vector2(448, 120), 12);
            AnimationSection2E("sb/asset/2e/5.png", new Vector2(52, 407), 6);
            AnimationSection2E("sb/asset/2e/6.png", new Vector2(217, 370), 17);
            AnimationSection2E("sb/asset/2e/7.png", new Vector2(448, 252), 8);
            AnimationSection2E("sb/asset/2e/8.png", new Vector2(265, 435), 9);
            AnimationSection2E("sb/asset/2e/9.png", new Vector2(550, 360), 10);

            noise = overLayer.CreateAnimation("sb/overlay/static/static.jpg", 83, 1000/30, OsbLoopType.LoopForever);
            noise.Scale(130360, 480f/GetMapsetBitmap("sb/overlay/static/static0.jpg").Height);
            noise.Fade(130360, .3f);
            noise.Additive(EndTime);

            StartTime = 130360;
            vhs = overLayer.CreateAnimation("sb/overlay/vhs/vhs.jpg", 8, 1000/30, OsbLoopType.LoopForever);
            vhs.Additive(StartTime);
            vhs.Scale(StartTime, 480f/GetMapsetBitmap("sb/overlay/vhs/vhs0.jpg").Height);
            vhs.Fade(StartTime, StartTime + 250, .5f, 0);

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(134319, 854, 480);
            q.Color(134319, Color4.Black);
            q.Fade(134319, EndTime, 0, 1);

            //----------------------------------------- 3A --------------------------------------------------//
            StartTime = 164472;
            EndTime = 174218;

            bg = bgLayer.CreateSprite("sb/asset/break/grad_bw.jpg");
            bg.Scale(StartTime, EndTime, 4/9f, 4/9f);
            bg.Fade(StartTime, 169345, .5f, .7f);
            bg.Fade(169345, EndTime, 1f, 1f);

            sprite = mainLayer.CreateSprite("sb/asset/break/6.png");
            position = new Vector2(90, 450);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-30/2));
            sprite.Scale(StartTime, 0.3f);
            sprite.Fade(StartTime, 169345, .5f, .7f);
            sprite.Fade(169345, EndTime, 1f, 1f);
            sprite.FlipH(StartTime);

            sprite = mainLayer.CreateSprite("sb/asset/break/0.png");
            position = new Vector2(100, 150);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-120/2));
            sprite.Scale(StartTime, 0.35f);
            sprite.Fade(StartTime, 169345, .5f, .7f);
            sprite.Fade(169345, EndTime, 1f, 1f);

            sprite = mainLayer.CreateSprite("sb/asset/break/1.png");
            position = new Vector2(320, 320);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60/2));
            sprite.Scale(StartTime, 0.45f);
            sprite.Fade(StartTime, 169345, .5f, .7f);
            sprite.Fade(169345, EndTime, 1f, 1f);

            sprite = mainLayer.CreateSprite("sb/asset/break/2.png");
            position = new Vector2(-80, 360);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60/2));
            sprite.Scale(StartTime, 0.6f);
            sprite.Rotate(StartTime, 4.14);
            sprite.Fade(StartTime, 169345, .5f, .7f);
            sprite.Fade(169345, EndTime, 1f, 1f);

            sprite = mainLayer.CreateSprite("sb/asset/break/5.png");
            position = new Vector2(590, 110);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-40/2));
            sprite.Scale(StartTime, 0.5f);
            sprite.Fade(StartTime, 169345, .5f, .7f);
            sprite.Fade(169345, EndTime, 1f, 1f);

            sprite = mainLayer.CreateSprite("sb/particles/shard2/4.png");
            position = new Vector2(650, 390);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60/2));
            sprite.Scale(StartTime, 0.6f);
            sprite.Fade(StartTime, 169345, .5f, .7f);
            sprite.Fade(169345, EndTime, 1f, 1f);

            sprite = mainLayer.CreateSprite("sb/particles/shard2/7.png");
            position = new Vector2(580, 280);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-70/2));
            sprite.Scale(StartTime, 0.3f);
            sprite.Rotate(StartTime, .9f);
            sprite.Fade(StartTime, 169345, .5f, .7f);
            sprite.Fade(169345, EndTime, 1f, 1f);

            noise = fgLayer.CreateAnimation("sb/overlay/noise/noise.jpg", 4, 152/2, OsbLoopType.LoopForever);
            noise.Scale(StartTime, EndTime, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width);
            noise.Fade(StartTime, 0.25);
            
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(169345, screenSize);
            q.Fade(OsbEasing.InExpo, 169345-250, 169345, 0, 0.5);
            q.Fade(OsbEasing.OutCubic, 169345, 169345+250, 0.5, 0);

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0, 1);
            q.Fade(OsbEasing.OutCubic, StartTime, 169345, 1, 0);
            q.Color(169345, Color4.Black);

            StartTime = 174218;
            FeatherTransition(StartTime);
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0, 1);
            q.Fade(OsbEasing.OutCubic, StartTime, StartTime+500, 1, 0);
            
            //----------------------------------------- 3B --------------------------------------------------//
            StartTime = 174218;
            EndTime = 179091; 
            SceneC(StartTime, EndTime);

            StartTime = 179091;
            EndTime = 181527;
            SceneA(StartTime, EndTime);

            StartTime = 181527;
            EndTime = 183964;
            SceneB(StartTime, EndTime);

            StartTime = 183964;
            EndTime = 186400;

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0, .5f);
            q.Fade(OsbEasing.OutExpo, StartTime, StartTime+250, .5f, 0);
            q.Additive(StartTime);
            q.Color(StartTime, DarkRed);

            girl = bgLayer.CreateSprite("sb/asset/3b_base.jpg");
            girl.Scale(StartTime, 4/9f);
            girl.Move(OsbEasing.OutExpo, StartTime, StartTime+500, 320, 240-30, 320, 240-2);
            girl.Move(StartTime+500, EndTime, 320, 240-2, 320, 240+2);
            girl.Fade(StartTime, .8f);

            glass = mainLayer.CreateSprite("sb/asset/3b_glass1.png", OsbOrigin.TopLeft);
            position = new Vector2(-107, 10);
            glass.Scale(StartTime, 4/9f);
            glass.Move(OsbEasing.OutExpo, StartTime, StartTime+500, position + new Vector2(0, -40), position + new Vector2(0, -8));
            glass.Move(StartTime+500, EndTime, position + new Vector2(0, -8), position);

            glass = mainLayer.CreateSprite("sb/asset/3b_glass2.png", OsbOrigin.BottomRight);
            position = new Vector2(747, 460);
            glass.Scale(StartTime, 4/9f);
            glass.Move(OsbEasing.OutExpo, StartTime, StartTime+500, position + new Vector2(0, -40), position + new Vector2(0, -8));
            glass.Move(StartTime+500, EndTime, position + new Vector2(0, -8), position);

            StartTime = 186400;
            EndTime = 188837;

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0, .5f);
            q.Fade(OsbEasing.OutExpo, StartTime, StartTime+250, .5f, 0);
            q.Additive(StartTime);
            q.Color(StartTime, DarkRed);
            
            girl = bgLayer.CreateSprite("sb/asset/3c_base.jpg");
            girl.Scale(StartTime, 4/9f);
            girl.Move(OsbEasing.OutExpo, StartTime, StartTime+500, 320, 240-20, 320, 240-2);
            girl.Move(StartTime+500, EndTime, 320, 240-2, 320, 240+2);
            girl.Fade(StartTime, .9f);

            glass = mainLayer.CreateSprite("sb/asset/3c_glass1.png", OsbOrigin.BottomLeft);
            position = new Vector2(-107, 500);
            glass.Scale(StartTime, 4/9f * 0.9f);
            glass.Move(OsbEasing.OutExpo, StartTime, StartTime+500, position + new Vector2(0, -50), position + new Vector2(0, -8));
            glass.Move(StartTime+500, EndTime, position + new Vector2(0, -8), position);

            glass = mainLayer.CreateSprite("sb/asset/3c_glass2.png", OsbOrigin.TopRight);
            position = new Vector2(747, 0);
            glass.Scale(StartTime, 4/9f * 0.9f);
            glass.Move(OsbEasing.OutExpo, StartTime, StartTime+500, position + new Vector2(0, -50), position + new Vector2(0, -8));
            glass.Move(StartTime+500, EndTime, position + new Vector2(0, -8), position);

            StartTime = 188837;
            EndTime = 191274;

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0, .5f);
            q.Fade(OsbEasing.OutExpo, StartTime, StartTime+250, .5f, 0);
            q.Additive(StartTime);
            q.Color(StartTime, DarkRed);

            bg = bgLayer.CreateSprite("sb/asset/break/grad_bw.jpg");
            bg.Scale(StartTime, 4/9f);
            bg.Fade(EndTime+2000, 0.8f);
            
            girl = mainLayer.CreateSprite("sb/asset/3c_base.png");
            girl.Scale(OsbEasing.OutExpo, StartTime, StartTime+500, 4/9f*1.6f, 4/9f*1.05f);
            girl.Scale(StartTime+500, EndTime + 2000, 4/9f*1.05f, 4/9f);

            StartTime = 183964;
            EndTime = 191274;
            FeatherTransition(StartTime-300);

            StoryboardLayer titleLayer = GetLayer("Title");

            q = titleLayer.CreateSprite("sb/title.png");
            q.Scale(OsbEasing.OutSine, StartTime, EndTime+2000, 1/9f, 1/9f);
            q.Move(StartTime, 320, 310);

            q = titleLayer.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(-107, 310));
            q.ScaleVec(StartTime, 200, 0.75);
            q.Color(EndTime + 2000, DarkRed);
            q = titleLayer.CreateSprite("sb/q.png", OsbOrigin.CentreRight, new Vector2(747, 310));
            q.ScaleVec(StartTime, 200, 0.75);
            q.Color(EndTime + 2000, DarkRed);
            q = titleLayer.CreateSprite("sb/star.png", OsbOrigin.Centre, new Vector2(-50, 310));
            q.Scale(StartTime, 0.08f);
            q.Color(EndTime + 2000, DarkRed);
            q = titleLayer.CreateSprite("sb/star.png", OsbOrigin.Centre, new Vector2(-30, 310));
            q.Scale(StartTime, 0.08f);
            q.Color(EndTime + 2000, DarkRed);
            q = titleLayer.CreateSprite("sb/star.png", OsbOrigin.Centre, new Vector2(-10, 310));
            q.Scale(StartTime, 0.08f);
            q.Color(EndTime + 2000, DarkRed);
            q = titleLayer.CreateSprite("sb/star.png", OsbOrigin.Centre, new Vector2(640+50, 310));
            q.Scale(StartTime, 0.08f);
            q.Color(EndTime + 2000, DarkRed);
            q = titleLayer.CreateSprite("sb/star.png", OsbOrigin.Centre, new Vector2(640+30, 310));
            q.Scale(StartTime, 0.08f);
            q.Color(EndTime + 2000, DarkRed);
            q = titleLayer.CreateSprite("sb/star.png", OsbOrigin.Centre, new Vector2(640+10, 310));
            q.Scale(StartTime, 0.08f);
            q.Color(EndTime + 2000, DarkRed);

            q = GetLayer("OVER").CreateSprite("sb/q.png");
            q.ScaleVec(EndTime, screenSize);
            q.Fade(OsbEasing.InCubic, EndTime, EndTime + 2000, 0, 1);
            q.Color(EndTime, Color4.Black);

            //-----------------------------------------------------------------------------------------------//

            StartTime = 250664;
            EndTime = 250969;
            position = new Vector2(350, 260);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/colour.png", Easing: OsbEasing.OutExpo, easingTime: 1000,
                zoomScale: 1.4f * colorScale, startScale : colorScale, endScale: colorScale, startPosition: position);
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/asset/bg_dark.jpg", Easing: OsbEasing.OutExpo, easingTime: 1000,
                zoomScale: 1.2f * gScale, startScale : gScale, endScale: gScale);
            
            StartTime = 52390;
            EndTime = 54827;
            q = GetLayer("").CreateAnimation("sb/overlay/noise/noise.jpg", 4, 152/2, OsbLoopType.LoopForever);
            q.Scale(StartTime, EndTime, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width);
            q.Fade(StartTime, 0.1);

            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0, 1);
            q.Fade(OsbEasing.OutExpo, StartTime, StartTime + 500, 1, 0);
            q.Color(StartTime, Color4.Black);
            
            //----------------------------------------- 4A --------------------------------------------------//

            StartTime = 233913;
            EndTime = 243659;

            q = GetLayer("OVER").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-500, StartTime, 0, 1);
            q.Fade(OsbEasing.OutExpo, StartTime, StartTime + 500, 1, 0);
            q.Color(StartTime, Color4.White);

            bg = bgLayer.CreateSprite("sb/asset/break/grad_bw.jpg");
            bg.Scale(StartTime, EndTime, 4/9f, 4/9f);

            sprite = mainLayer.CreateSprite("sb/asset/break/6.png");
            position = new Vector2(90, 450);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-30));
            sprite.Scale(StartTime, 0.3f);
            sprite.FlipH(StartTime);

            sprite = mainLayer.CreateSprite("sb/asset/break/0.png");
            position = new Vector2(100, 150);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-90));
            sprite.Scale(StartTime, 0.35f);

            sprite = mainLayer.CreateSprite("sb/asset/break/2.png");
            position = new Vector2(-80, 360);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60));
            sprite.Scale(StartTime, 0.6f);
            sprite.Rotate(StartTime, 4.14);

            sprite = mainLayer.CreateSprite("sb/asset/break/5.png");
            position = new Vector2(590, 110);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-40));
            sprite.Scale(StartTime, 0.5f);

            sprite = mainLayer.CreateSprite("sb/particles/shard2/4.png");
            position = new Vector2(650, 390);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60));
            sprite.Scale(StartTime, 0.6f);

            sprite = mainLayer.CreateSprite("sb/particles/shard2/7.png");
            position = new Vector2(580, 280);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-70));
            sprite.Scale(StartTime, 0.3f);
            sprite.Rotate(StartTime, .9f);
            
            noise = fgLayer.CreateAnimation("sb/overlay/noise/noise.jpg", 4, 152/2, OsbLoopType.LoopForever);
            noise.Scale(StartTime, 250588, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width);
            noise.Fade(StartTime, 0.25);
            noise.Fade(243659, .2);

            StartTime = 243050;
            EndTime = 243659;
            vhs = overLayer.CreateAnimation("sb/overlay/vhs/vhs.jpg", 8, 1000/30, OsbLoopType.LoopForever);
            vhs.Additive(StartTime);
            vhs.Scale(StartTime, 480f/GetMapsetBitmap("sb/overlay/vhs/vhs0.jpg").Height);
            vhs.Fade(StartTime, EndTime, .5f, .2f);

            StartTime = 250360;
            EndTime = 250664;
            vhs = overLayer.CreateAnimation("sb/overlay/vhs/vhs.jpg", 8, 1000/30, OsbLoopType.LoopForever);
            vhs.Additive(StartTime);
            vhs.Scale(StartTime, 480f/GetMapsetBitmap("sb/overlay/vhs/vhs0.jpg").Height);
            vhs.Fade(StartTime, EndTime, .5f, .2f);

            StartTime = 262999;
            FeatherTransition(StartTime-200);
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-500, StartTime, 0, 1);
            q.Fade(OsbEasing.OutExpo, StartTime, StartTime + 500, 1, 0);
            q.Color(StartTime, Color4.White);

            StartTime = 267873;
            EndTime = 270309;
            gScale = 480f/GetMapsetBitmap("sb/asset/2c_face.png").Height;
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/2c_face.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 1.5f * gScale, startScale : gScale * 1.02f, endScale: gScale);
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/c1.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 1.75f * gScale, startScale : gScale * 1.41f, endScale: gScale*1.4f);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/particles/feather/6.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 2f * gScale, startScale : gScale * 1.03f, endScale: gScale, startPosition: new Vector2(530, 90), startRotation: 1f);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/particles/feather/8.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 2f * gScale, startScale : gScale * 1.03f, endScale: gScale, startPosition: new Vector2(720, 350), startRotation: 1f);
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/particles/feather/7.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 2f * gScale, startScale : gScale * 1.03f, endScale: gScale, startPosition: new Vector2(-80, 200), startRotation: -1f);
            q = fgLayer.CreateSprite("sb/light.png");
            q.Scale(StartTime, 4/9f);
            q.Fade(EndTime, .8);

            StartTime = 258126;
            EndTime = 262999;
            q = fgLayer.CreateSprite("sb/light.png");
            q.Scale(StartTime, 4/9f);
            q.Fade(EndTime, .8);

            StartTime = 270309;
            EndTime = 272898;
            gScale = 480f/GetMapsetBitmap("sb/asset/glow.png").Height * 1.1f;
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/asset/glow.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 2f * gScale, startScale : gScale * 1.04f, endScale: gScale, startPosition: new Vector2(330, 240));
            q = bgLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, EndTime, screenSize, screenSize);
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-500, StartTime, 0, 1);
            q.Fade(OsbEasing.OutSine, StartTime, StartTime+750, 1, 0);
            q.Fade(OsbEasing.InExpo, EndTime-500, EndTime, 0, 1);

            StartTime = 243659;
            EndTime = 250588;

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);
            
            g = new Sprite3d
            {
                Layer = bgLayer,
                SpritePath = "sb/asset/bg_dark.jpg",
                UseDistanceFade = false
            };
            g.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            scale = 1.38f;
            g.PositionX.Add(StartTime, 0);
            g.PositionY.Add(StartTime, 0);
            g.PositionZ.Add(StartTime, -30);
            g.ScaleX.Add(StartTime, scale);
            g.ScaleY.Add(StartTime, scale);
            scene.Add(g);

            g = new Sprite3d
            {
                Layer = mainLayer,
                SpritePath = "sb/asset/full_s.png",
                UseDistanceFade = false
            };
            g.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            scale = 0.55f;
            g.PositionX.Add(StartTime, 50);
            g.PositionY.Add(StartTime, 80);
            g.PositionZ.Add(StartTime, -1);
            g.ScaleX.Add(StartTime, scale);
            g.ScaleY.Add(StartTime, scale);
            g.Coloring.Add(StartTime, DarkRed);
            g.Rotation.Add(StartTime, .2f);
            scene.Add(g);

            random = new Random(9);
            for (int i = 0; i < 20; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.4f - 0.1f) + 0.1f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-500, 501));
                p.PositionY.Add(StartTime, random.Next(-300, 301));
                p.PositionZ.Add(StartTime, random.Next(5, 25));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Opacity.Add(StartTime, .5f);
                scene.Add(p);
            }

            for (int i = 0; i < 64; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-500, 501));
                p.PositionY.Add(StartTime, random.Next(-300, 301));
                p.PositionZ.Add(StartTime, random.Next(5, 25));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                scene.Add(p);
            }

            scene.Root.PositionZ.Add(StartTime, 37);
            scene.Root.PositionZ.Add(StartTime+750, -2, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionZ.Add(EndTime-152, -5);
            scene.Root.PositionZ.Add(EndTime, -10, EasingFunctions.ToEasingFunction(OsbEasing.InExpo));

            scene.Root.Rotation.Add(StartTime, 0);
            scene.Root.Rotation.Add(EndTime, 0.03f);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);

            noise = overLayer.CreateAnimation("sb/overlay/static/static.jpg", 83, 1000/30, OsbLoopType.LoopForever);
            noise.Scale(StartTime, 480f/GetMapsetBitmap("sb/overlay/static/static0.jpg").Height);
            noise.Fade(StartTime, .15f);
            noise.Additive(EndTime);
            //-----------------------------------------------------------------------------------------------//
            var vScale = 480f/GetMapsetBitmap("sb/overlay/v1.jpg").Height;

            StartTime = 57263;
            q = GetLayer("").CreateSprite("sb/t2.jpg");
            q.Scale(StartTime, StartTime+152, vScale, vScale);
            q.Fade(StartTime, .4f);
            StartTime = 57416;
            q = GetLayer("").CreateSprite("sb/overlay/v2.jpg");
            q.Scale(StartTime, StartTime+152, vScale, vScale);
            q.Fade(StartTime, .4f);
            StartTime = 57568;
            q = GetLayer("").CreateSprite("sb/t1.jpg");
            q.Scale(StartTime, StartTime+152, vScale, vScale);
            q.Fade(StartTime, .4f);
            StartTime = 57720;
            q = GetLayer("").CreateSprite("sb/overlay/v1.jpg");
            q.Scale(StartTime, StartTime+152, vScale, vScale);
            q.Fade(StartTime, .4f);
            q.Color(StartTime, DarkRed);
           
            StartTime = 57873;
            q = GetLayer("").CreateSprite("sb/t3.jpg");
            q.Scale(StartTime, StartTime+152, vScale, vScale);
            q.Fade(StartTime, .4f);
            StartTime = 58025;
            q = GetLayer("").CreateSprite("sb/overlay/v2.jpg");
            q.Scale(StartTime, StartTime+152, vScale, vScale);
            q.Fade(StartTime, .4f);
            StartTime = 58177;
            q = GetLayer("").CreateSprite("sb/t1.jpg");
            q.Scale(StartTime, StartTime+152, vScale, vScale);
            q.Fade(StartTime, .4f);
            StartTime = 58329;
            q = GetLayer("").CreateSprite("sb/overlay/v1.jpg");
            q.Scale(StartTime, StartTime+152, vScale, vScale);
            q.Fade(StartTime, .4f);


            StartTime = 258126-152;
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Color(StartTime+152, Color4.Black);

            StartTime = 267873-152;
            q = overLayer.CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Color(StartTime+152, Color4.Black);

            //First Verse
            StartTime = 22999;
            EndTime = 58482;
            var letterboxHeight = 60;
            q = GetLayer("Bar").CreateSprite("sb/q.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            q.ScaleVec(StartTime, 900, letterboxHeight);
            q.Color(EndTime, Color4.Black);

            q = GetLayer("Bar").CreateSprite("sb/q.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            q.ScaleVec(StartTime, 900, letterboxHeight);
            q.Color(EndTime, Color4.Black);

            //Second Verse
            StartTime = 88939;
            EndTime = 135233;
            q = GetLayer("Bar").CreateSprite("sb/q.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            q.ScaleVec(StartTime, 900, letterboxHeight);
            q.Color(EndTime, Color4.Black);

            q = GetLayer("Bar").CreateSprite("sb/q.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            q.ScaleVec(StartTime, 900, letterboxHeight);
            q.Color(EndTime, Color4.Black);

            //Third Verse
            StartTime = 164472;
            EndTime = 193710;
            q = GetLayer("Bar").CreateSprite("sb/q.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            q.ScaleVec(StartTime, 900, letterboxHeight);
            q.Color(EndTime, Color4.Black);

            q = GetLayer("Bar").CreateSprite("sb/q.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            q.ScaleVec(StartTime, 900, letterboxHeight);
            q.Color(EndTime, Color4.Black);

            StartTime = 233913;
            EndTime = 251883;
            q = GetLayer("Bar").CreateSprite("sb/q.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            q.ScaleVec(StartTime, 900, letterboxHeight);
            q.Color(EndTime, Color4.Black);

            q = GetLayer("Bar").CreateSprite("sb/q.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            q.ScaleVec(StartTime, 900, letterboxHeight);
            q.Color(EndTime, Color4.Black);

            StartTime = 272898;
            EndTime = 284472;

            q = GetLayer("OVER").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime-500, StartTime, 0, 1);
            q.Fade(OsbEasing.OutExpo, StartTime, StartTime + 500, 1, 0);
            q.Color(StartTime, Color4.White);

            bg = bgLayer.CreateSprite("sb/asset/break/grad_bw.jpg");
            bg.Scale(StartTime, EndTime, 4/9f, 4/9f);

            for (int i = 0; i < 32; i++)
            {
                position = new Vector2(Random(-107, 747),Random(0, 480));
                q = mainLayer.CreateSprite(spriteList2[Random(0, 8)]);
                q.Move(StartTime, EndTime, position, position + new Vector2(0, Random(-40, -5)));
                q.Scale(StartTime, Random(0.03, 0.05));
                q.Fade(StartTime, Random(.2, .5));
            }


            sprite = mainLayer.CreateSprite("sb/asset/break/6.png");
            position = new Vector2(90, 450);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-30));
            sprite.Scale(StartTime, 0.3f);
            sprite.FlipH(StartTime);
            sprite.Fade(StartTime, .5f);

            sprite = mainLayer.CreateSprite("sb/asset/break/0.png");
            position = new Vector2(100, 150);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-90));
            sprite.Scale(StartTime, 0.35f);
            sprite.Fade(StartTime, .5f);

            sprite = mainLayer.CreateSprite("sb/asset/break/1.png");
            position = new Vector2(320, 320);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60));
            sprite.Scale(StartTime, 0.45f);
            sprite.Fade(StartTime, .5f);

            sprite = mainLayer.CreateSprite("sb/asset/break/2.png");
            position = new Vector2(-80, 360);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60));
            sprite.Scale(StartTime, 0.6f);
            sprite.Rotate(StartTime, 4.14);
            sprite.Fade(StartTime, .5f);

            sprite = mainLayer.CreateSprite("sb/asset/break/5.png");
            position = new Vector2(590, 110);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-40));
            sprite.Scale(StartTime, 0.5f);
            sprite.Fade(StartTime, .5f);

            sprite = mainLayer.CreateSprite("sb/particles/shard2/4.png");
            position = new Vector2(650, 390);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-60));
            sprite.Scale(StartTime, 0.6f);
            sprite.Fade(StartTime, .5f);

            sprite = mainLayer.CreateSprite("sb/particles/shard2/7.png");
            position = new Vector2(580, 280);
            sprite.Move(StartTime, EndTime, position, position + new Vector2(0,-70));
            sprite.Scale(StartTime, 0.3f);
            sprite.Rotate(StartTime, .9f);
            sprite.Fade(StartTime, .5f);

            noise = fgLayer.CreateAnimation("sb/overlay/noise/noise.jpg", 4, 152/2, OsbLoopType.LoopForever);
            noise.Scale(StartTime, StartTime, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width, 854f/GetMapsetBitmap("sb/overlay/noise/noise1.jpg").Width);
            noise.Fade(EndTime, 0.15);

            

            StartTime = 233913;
            EndTime = 243659;

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);
            

            for (int i = 0; i < 200; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-300, 301));
                p.PositionY.Add(StartTime, random.Next(-300, 1801));
                p.PositionZ.Add(StartTime, random.Next(5, 10));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Opacity.Add(StartTime, Random(0.3f,0.5f));
                scene.Add(p);
            }
            scene.Root.PositionZ.Add(StartTime, 20f);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionY.Add(EndTime, -1700f);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);

            StartTime = 169345;
            EndTime = 174218;

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);
            

            for (int i = 0; i < 200; i++)
            {
                Sprite3d p = new Sprite3d
                {
                    Layer = mainLayer,
                    SpritePath = spriteList2[random.Next(0, 8)],
                    UseDistanceFade = false
                };
                p.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                scale = (float)(random.NextDouble() * (0.04f - 0.01f) + 0.01f);
                var angle = Math.Round(random.NextDouble() * (2 * Math.PI) - Math.PI, 1);
                p.PositionX.Add(StartTime, random.Next(-300, 301));
                p.PositionY.Add(StartTime, random.Next(-300, 1601));
                p.PositionZ.Add(StartTime, random.Next(5, 10));
                p.Rotation.Add(StartTime, (float)angle);
                p.ScaleX.Add(StartTime, scale);
                p.ScaleY.Add(StartTime, scale);
                p.Opacity.Add(StartTime, Random(0.5f,0.8f));
                scene.Add(p);
            }
            scene.Root.PositionZ.Add(StartTime, 20f);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionY.Add(EndTime, -1400f);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);


            StartTime = 283558;
            EndTime = 284472;

            q = GetLayer("OVER").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.InExpo, StartTime, EndTime, 0, 1);
            q.Color(StartTime, Color4.Black);

            StartTime = 253253;
            q = GetLayer("OVER").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, screenSize);
            q.Fade(OsbEasing.OutExpo, StartTime, StartTime+2000, 1, 0);

            StartTime = 272898;
            EndTime = 282644;
            q = titleLayer.CreateSprite("sb/title.png");
            q.Scale(StartTime, .7/9f);
            q.Move(EndTime, 320, 160);

            StartTime = 282644;
            EndTime = 284472;
            q = titleLayer.CreateSprite("sb/owc_logo.png");
            q.Scale(OsbEasing.OutExpo, StartTime, EndTime, 0.25, 0.2);

        }

        public Vector2 MoveCentreOrigin(Vector2 startPosition, double factor, Vector2? centre = null)
        {
            if (!centre.HasValue) centre = new Vector2(320, 240);
            var angle = Math.Atan2(centre.Value.Y - startPosition.Y, centre.Value.X - startPosition.X);
            var newFactor = Math.Sqrt(Math.Pow(centre.Value.X - startPosition.X, 2) + Math.Pow(centre.Value.Y - startPosition.Y, 2)) * (1 - factor);
            return new Vector2((float)(startPosition.X + Math.Cos(angle) * newFactor), (float)(startPosition.Y + Math.Sin(angle) * newFactor));
        }

        public void CreateLightOverlay(string imagePath, int startTime, int endTime, float opacity, bool? flipH = false)
        {
            var sprite = GetLayer("Overlay").CreateSprite(imagePath);
            sprite.Scale(startTime, 854f / GetMapsetBitmap(imagePath).Width);
            sprite.Fade(startTime, opacity);
            sprite.Additive(endTime);
            if (flipH.Value) sprite.FlipH(startTime);
        }

        public void CreateAnimation(StoryboardLayer layer, int startTime, int endTime, string spritePath, 
            OsbEasing Easing = OsbEasing.None, float? easingTime = null,
            Vector2? zoomPosition = null, Vector2? startPosition = null, Vector2? endPosition = null,
            float? zoomScale = null, float? startScale = null, float? endScale = null, Vector2? scaleOrigin = null,
            float? zoomRotation = null, float? startRotation = null, float? endRotation = null
            )
        {
            if (!startPosition.HasValue) startPosition = new Vector2(320, 240);
            if (!scaleOrigin.HasValue) scaleOrigin = new Vector2(320, 240);
            var sprite = layer.CreateSprite(spritePath, OsbOrigin.Centre, startPosition.Value);
            

            if (Easing == OsbEasing.None)
            {  
                if (startScale.HasValue && endScale.HasValue) 
                {
                    sprite.Scale(startTime, endTime, startScale.Value, endScale.Value);
                    sprite.Move(startTime, endTime, MoveCentreOrigin(startPosition.Value, startScale.Value/endScale.Value, scaleOrigin.Value), MoveCentreOrigin(startPosition.Value, endScale.Value/endScale.Value, scaleOrigin.Value));
                }
                else if (startScale.HasValue) sprite.Scale(startTime, startScale.Value);

                if (startPosition.HasValue && endPosition.HasValue && !scaleOrigin.HasValue) sprite.Move(startTime, endTime, startPosition.Value, endPosition.Value);

                if (startRotation.HasValue && endRotation.HasValue) sprite.Rotate(startTime, endTime, startRotation.Value, endRotation.Value);
                else if (startRotation.HasValue) sprite.Rotate(endTime, startRotation.Value);
            }
            else
            {
                if (startScale.HasValue && endScale.HasValue) 
                {
                    sprite.Scale(Easing, startTime, startTime + easingTime.Value, zoomScale.Value, startScale.Value);
                    sprite.Scale(startTime + easingTime.Value, endTime, startScale.Value, endScale.Value);
                    sprite.Move(Easing, startTime, startTime + easingTime.Value, MoveCentreOrigin(startPosition.Value, zoomScale.Value/endScale.Value, scaleOrigin.Value), MoveCentreOrigin(startPosition.Value, startScale.Value/endScale.Value, scaleOrigin.Value));
                    sprite.Move(startTime + easingTime.Value, endTime, MoveCentreOrigin(startPosition.Value, startScale.Value/endScale.Value, scaleOrigin.Value), MoveCentreOrigin(startPosition.Value, endScale.Value/endScale.Value, scaleOrigin.Value));
                }
                else if (startScale.HasValue) sprite.Scale(startTime, startScale.Value);

                if (startPosition.HasValue && endPosition.HasValue) 
                {
                    sprite.Move(OsbEasing.OutExpo, startTime, startTime + easingTime.Value, zoomPosition.Value, startPosition.Value);
                    sprite.Move(startTime + easingTime.Value, endTime, startPosition.Value, endPosition.Value);
                }

                if (startRotation.HasValue && endRotation.HasValue) sprite.Rotate(startTime, endTime, startRotation.Value, endRotation.Value);
                else if (startRotation.HasValue) sprite.Rotate(endTime, startRotation.Value);
            }
        }

        public void AnimationSection2E(string Path, Vector2 position, int seed)
        {
            // Initialize Random with a specific seed
            Random random = new Random(seed);

            var StartTime = 127923;
            var EndTime = 134015;
            var RandomTime = random.Next(0, 500);
            var RandomInterval = random.Next(2500, 3500);

            var sprite = mainLayer.CreateSprite(Path);
            sprite.Fade(StartTime + RandomTime, StartTime + RandomTime + 500, 0, 1);
            sprite.Move(
                OsbEasing.InOutSine, StartTime, StartTime + RandomInterval,
                position.X + (float)(random.NextDouble() * 10 - 5),
                position.Y + (float)(random.NextDouble() * 5 - 0.5),
                position.X, position.Y
            );
            sprite.Move(
                OsbEasing.InOutSine, StartTime + RandomInterval, EndTime + RandomTime - 250,
                position.X, position.Y,
                position.X + (float)(random.NextDouble() * 10 - 5),
                position.Y + (float)(random.NextDouble() * 10 - 5)
            );
            var newPos = sprite.PositionAt(EndTime + RandomTime - 250);

            sprite.StartLoopGroup(EndTime + RandomTime, 4);
            sprite.Fade(0, 50, 1, 0);
            sprite.EndGroup();
            sprite.Scale(OsbEasing.InExpo, EndTime + RandomTime - 250, EndTime + RandomTime + 250, 4 / 9f, 4 / 9f * 1.5f);
            sprite.Move(OsbEasing.InExpo, EndTime + RandomTime - 250, EndTime + RandomTime + 250, MoveCentreOrigin(newPos, 1f), MoveCentreOrigin(newPos, 1.5f));
        }


        public void SceneA(int StartTime, int EndTime)
        {
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scaleX, float scaleY)
            {
                sprite.Layer = layer;
                sprite.SpritePath = path;
                sprite.UseDistanceFade = false;
                sprite.ConfigureGenerators(s =>
                {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                sprite.PositionX.Add(StartTime, posX);
                sprite.PositionY.Add(StartTime, posY);
                sprite.PositionZ.Add(StartTime, posZ);
                sprite.ScaleX.Add(StartTime, scaleX);
                sprite.ScaleY.Add(StartTime, scaleY);
                scene.Add(sprite);
            }

            ConfigureSprite(new Sprite3d(), mainLayer,  "sb/a2.png", 10, 0, 0, 0.45f, 0.45f); // girl
            ConfigureSprite(new Sprite3d(), fgLayer, "sb/a3.png", 40, 0, 10, 0.25f, 0.25f); // frame
            ConfigureSprite(new Sprite3d(), bgLayer, "sb/a1.png", 0, 0, -40, 1.4f, 1.4f);  // bg

            scene.Root.PositionZ.Add(StartTime, 22.1f);
            scene.Root.PositionX.Add(StartTime, -20);
            scene.Root.PositionX.Add(StartTime+750, -56, EasingFunctions.ToEasingFunction(OsbEasing.OutQuint));
            scene.Root.PositionX.Add(EndTime-300, -58);
            scene.Root.PositionX.Add(EndTime, -70, EasingFunctions.ToEasingFunction(OsbEasing.InQuint));

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);
        }

        public void SceneB(int StartTime,  int EndTime)
        {
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, float posX, float posY, float posZ, float scaleX, float scaleY)
            {
                sprite.Layer = layer;
                sprite.SpritePath = path;
                sprite.UseDistanceFade = false;
                sprite.ConfigureGenerators(s =>
                {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                sprite.PositionX.Add(StartTime, posX);
                sprite.PositionY.Add(StartTime, posY);
                sprite.PositionZ.Add(StartTime, posZ);
                sprite.ScaleX.Add(StartTime, scaleX);
                sprite.ScaleY.Add(StartTime, scaleY);
                scene.Add(sprite);
            }

            // Usage for each sprite
            ConfigureSprite(new Sprite3d(), mainLayer, "sb/b2.png", 40, 0, 0, 0.45f, 0.45f); // girl
            ConfigureSprite(new Sprite3d(), fgLayer, "sb/b3.png", 50, -20, 8, 0.27f, 0.27f); // frame
            ConfigureSprite(new Sprite3d(), bgLayer, "sb/b1.png", 0, 0, -40, 1.36f, 1.36f); // bg
            
            scene.Root.PositionZ.Add(StartTime, 24f);
            scene.Root.PositionY.Add(EndTime-250, 0);
            scene.Root.PositionY.Add(EndTime, 20, EasingFunctions.ToEasingFunction(OsbEasing.InExpo));
            scene.Root.PositionX.Add(StartTime, 20);
            scene.Root.PositionX.Add(StartTime+750, -56, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionX.Add(EndTime, -60);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);
        }

        public void SceneC(int StartTime, int EndTime)
        {
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scaleX, float scaleY)
            {
                sprite.Layer = layer;
                sprite.SpritePath = path;
                sprite.UseDistanceFade = false;
                sprite.ConfigureGenerators(s =>
                {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                sprite.PositionX.Add(StartTime, posX);
                sprite.PositionY.Add(StartTime, posY);
                sprite.PositionZ.Add(StartTime, posZ);
                sprite.ScaleX.Add(StartTime, scaleX);
                sprite.ScaleY.Add(StartTime, scaleY);
                scene.Add(sprite);
            }

            ConfigureSprite(new Sprite3d(), mainLayer, "sb/c2.png", 50, 20, 0, 0.45f, 0.45f); // girl
            ConfigureSprite(new Sprite3d(), bgLayer, "sb/c1.png", 0, 0, -40, 1.4f, 1.4f); // bg

            scene.Root.PositionZ.Add(StartTime, 22.1f);
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionX.Add(EndTime-250, -20);
            scene.Root.PositionX.Add(EndTime, -40, EasingFunctions.ToEasingFunction(OsbEasing.InQuint));

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);
        }

        public void FeatherTransition(int StartTime)
        {
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            List<string> spriteList = new()
                {
                    "sb/particles/transition/0.png",
                    "sb/particles/transition/1.png",
                    "sb/particles/transition/2.png",
                    "sb/particles/transition/3.png",
                    "sb/particles/transition/4.png",
                };

            for (int i = 0; i < 32; i++)
            {
                Sprite3d f = new Sprite3d
                {
                    Layer = overLayer,
                    SpritePath =spriteList[Random(0,5)],
                    UseDistanceFade = false
                };
                f.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                var scale = Random(0.1f, 0.2f);
                f.PositionX.Add(StartTime, Random(-700, 1000));
                f.PositionY.Add(StartTime, Random(-70, 70));
                f.PositionZ.Add(StartTime, Random(0, 10));
                f.ScaleX.Add(StartTime, scale);
                f.ScaleY.Add(StartTime, scale);
                f.Rotation.Add(StartTime, (float)Random(-Math.PI, Math.PI));
                f.Opacity.Add(StartTime, Random(0.6f,0.8f));
                scene.Add(f);
            }

            for (int i = 0; i < 32; i++)
            {
                Sprite3d f = new Sprite3d
                {
                    Layer = overLayer,
                    SpritePath =spriteList[Random(0,5)],
                    UseDistanceFade = false
                };
                f.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                var scale = Random(0.05f, 0.1f);
                f.PositionX.Add(StartTime, Random(-700, 1000));
                f.PositionY.Add(StartTime, Random(-70, 70));
                f.PositionZ.Add(StartTime, Random(0, 10));
                f.ScaleX.Add(StartTime, scale);
                f.ScaleY.Add(StartTime, scale);
                f.Rotation.Add(StartTime, (float)Random(-Math.PI, Math.PI));
                f.Opacity.Add(StartTime, Random(0.6f,0.8f));
                scene.Add(f);
            }
            
            scene.Root.PositionX.Add(StartTime-700, -1100);
            scene.Root.PositionX.Add(StartTime+700, 1100);

            scene.Generate(camera, GetLayer(""), StartTime-700, StartTime+700, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);

            scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);
            
            camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            for (int i = 0; i < 32; i++)
            {
                Sprite3d f = new Sprite3d
                {
                    Layer = overLayer,
                    SpritePath =spriteList[Random(0,5)],
                    UseDistanceFade = false
                };
                f.ConfigureGenerators(s => {
                    s.ScaleDecimals = 6;
                    s.ScaleTolerance = 0.1f;
                });
                var scale = Random(0.02f, 0.05f);
                f.PositionX.Add(StartTime, Random(-700, 1000));
                f.PositionY.Add(StartTime, Random(-70, 70));
                f.PositionZ.Add(StartTime, Random(0, 10));
                f.ScaleX.Add(StartTime, scale);
                f.ScaleY.Add(StartTime, scale);
                f.Rotation.Add(StartTime, (float)Random(-Math.PI, Math.PI));
                f.Opacity.Add(StartTime, Random(0.75f,1f));
                scene.Add(f);
            }
            
            scene.Root.PositionX.Add(StartTime-1000, -1100);
            scene.Root.PositionX.Add(StartTime+1200, 1100);

            scene.Generate(camera, GetLayer(""), StartTime-1000, StartTime+1200, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);
        }

    }
}
