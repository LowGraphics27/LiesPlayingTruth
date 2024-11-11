using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES10;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Diagnostics;
using System.Drawing;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Animations;
using StorybrewCommon.Storyboarding.CommandValues;
using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using System.Security;
using StorybrewCommon.Storyboarding.Commands;

namespace StorybrewScripts
{
    public class ChorusScript : StoryboardObjectGenerator
    {
        StoryboardLayer bgLayer, mainLayer, fgLayer;
        public string bgPath, bgBlurPath, girlPath, framePath, featherPath, glassPath;
        public override void Generate()
        {
            bgLayer = GetLayer("Background");
            mainLayer = GetLayer("Main");
            fgLayer = GetLayer("Foreground");

            bgPath = "sb/background.jpg";
            bgBlurPath = "sb/background_blur.jpg";
            girlPath = "sb/girl.png";
            framePath = "sb/frame.png";
            featherPath = "sb/feather.png";
            glassPath = "sb/glass.png";

            //----------------------------------------- Chorus 1 --------------------------------------------------//

            var StartTime = 59700;
            var EndTime = 64192;
            var q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, 854, 480);
            q.Fade(OsbEasing.OutCubic, StartTime, StartTime+1000, 1, 0);
            Scene1(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            StartTime = 64192;
            EndTime = 66933;
            Scene2(StartTime, EndTime);
            var light = GetLayer("Light").CreateSprite("sb/light.png");
            light.Scale(StartTime, 4/9f);
            light.Fade(EndTime, 0.9);
            CreateLightOverlay("sb/overlay/light/6.jpg",StartTime, EndTime, 0.2f);
            
            StartTime = 66933;
            EndTime = 69370;
            Scene3(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            StartTime = 66705;
            EndTime = 67010;
            q = GetLayer("Light").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, EndTime, 854, 480, 854, 480);
            q.Color(StartTime, Color4.Black);
            q.StartLoopGroup(StartTime, 4);
            q.Fade(0, 52, 0, 0.5);
            q.EndGroup();
            q.Fade(StartTime+(52*4), 1);

            StartTime = 69370; 
            EndTime = 71807;
            SceneA(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/5.jpg",StartTime, EndTime, 0.1f, flipH: true);

            StartTime = 71807; 
            EndTime = 74243;
            SceneB(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/2.jpg",StartTime, EndTime, 0.2f);
            
            StartTime = 77593;
            EndTime = 79116;
            q = mainLayer.CreateSprite("sb/q.png");
            q.ScaleVec(EndTime, 854, 480);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0,1);

            q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(EndTime, 854, 480);
            q.Fade(OsbEasing.InExpo, EndTime-500, EndTime, 0,1);
            
            StartTime = 74243;
            EndTime = 79116;
            Scene4(StartTime, EndTime);  
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard1.png", 0.22f, 0.3f, 1.1f, 1.5f, new Vector2(1050, 480));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard2.png", 0.23f, 0.4f, 1.12f, 1.7f, new Vector2(-140, -150));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard3.png", 0.29f, 0.47f, 1.2f, 2.8f, new Vector2(400, 560));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard4.png", 0.27f, 0.4f, 1.15f, 2f, new Vector2(-220, 670));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard5.png", 0.18f, 0.24f, 1.1f, 1.5f, new Vector2(-80, 950));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard6.png", 0.30f, 0.45f, 1.15f, 2f, new Vector2(790, -130));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard7.png", 0.34f, 0.53f, 1.15f, 2.6f, new Vector2(1030, 110));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard8.png", 0.27f, 0.4f, 1.1f, 1.5f, new Vector2(-500, 390));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard9.png", 0.27f, 0.45f, 1.21f, 2.5f, new Vector2(570, -310));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard10.png", 0.3f, 0.45f, 1.12f, 2f, new Vector2(-350, 0));
            CreateLightOverlay("sb/overlay/light/4.jpg",StartTime, EndTime, 0.2f);

            StartTime = 79116;
            EndTime = 88939;
            Scene5(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            //----------------------------------------- Chorus 2 --------------------------------------------------//
            var offset = 135233 - 59700;
            StartTime = 59700 + offset;
            EndTime = 64192 + offset;
            q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, 854, 480);
            q.Fade(OsbEasing.OutCubic, StartTime, StartTime+1000, 1, 0);
            Scene1(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            StartTime = 64192 + offset;
            EndTime = 66933 + offset;
            Scene2(StartTime, EndTime);
            light = GetLayer("Light").CreateSprite("sb/light.png");
            light.Scale(StartTime, 4/9f);
            light.Fade(EndTime, 0.9);
            CreateLightOverlay("sb/overlay/light/6.jpg",StartTime, EndTime, 0.2f);

            StartTime = 66933 + offset;
            EndTime = 69370 + offset;
            Scene3(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            StartTime = 66705 + offset;
            EndTime = 67010 + offset;
            q = GetLayer("Light").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, EndTime, 854, 480, 854, 480);
            q.Color(StartTime, Color4.Black);
            q.StartLoopGroup(StartTime, 4);
            q.Fade(0, 52, 0, 0.5);
            q.EndGroup();
            q.Fade(StartTime+(52*4), 1);

            StartTime = 69370 + offset; 
            EndTime = 71807 + offset;
            SceneA(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/5.jpg",StartTime, EndTime, 0.1f, flipH: true);

            StartTime = 71807 + offset; 
            EndTime = 74243 + offset;
            SceneB(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/2.jpg",StartTime, EndTime, 0.2f);
            
            StartTime = 77593 + offset;
            EndTime = 79116 + offset;
            q = mainLayer.CreateSprite("sb/q.png");
            q.ScaleVec(EndTime, 854, 480);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0,1);

            q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(EndTime, 854, 480);
            q.Fade(OsbEasing.InExpo, EndTime-500, EndTime, 0,1);
            
            StartTime = 74243 + offset;
            EndTime = 79116 + offset;
            Scene4(StartTime, EndTime);  
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard1.png", 0.22f, 0.3f, 1.1f, 1.5f, new Vector2(1050, 480));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard2.png", 0.23f, 0.4f, 1.12f, 1.7f, new Vector2(-140, -150));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard3.png", 0.29f, 0.47f, 1.2f, 2.8f, new Vector2(400, 560));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard4.png", 0.27f, 0.4f, 1.15f, 2f, new Vector2(-220, 670));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard5.png", 0.18f, 0.24f, 1.1f, 1.5f, new Vector2(-80, 950));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard6.png", 0.30f, 0.45f, 1.15f, 2f, new Vector2(790, -130));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard7.png", 0.34f, 0.53f, 1.15f, 2.6f, new Vector2(1030, 110));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard8.png", 0.27f, 0.4f, 1.1f, 1.5f, new Vector2(-500, 390));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard9.png", 0.27f, 0.45f, 1.21f, 2.5f, new Vector2(570, -310));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard10.png", 0.3f, 0.45f, 1.12f, 2f, new Vector2(-350, 0));
            CreateLightOverlay("sb/overlay/light/4.jpg",StartTime, EndTime, 0.2f);

            StartTime = 79116 + offset;
            EndTime = 88939 + offset;
            Scene5(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            //----------------------------------------- Solo ------------------------------------------------------//

            StartTime = 188837;
            EndTime = 193253;
            var posOffset = new Vector2(50,0);
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard1.png", 0.22f, 0.3f, 1.1f, 1.5f,   posOffset + new Vector2(1050, 480));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard2.png", 0.23f, 0.4f, 1.12f, 1.7f,  posOffset + new Vector2(-140, -150));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard3.png", 0.29f, 0.47f, 1.2f, 2.8f,  posOffset + new Vector2(400, 560));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard4.png", 0.27f, 0.4f, 1.15f, 2f,    posOffset + new Vector2(-220, 670));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard5.png", 0.18f, 0.24f, 1.1f, 1.5f,  posOffset + new Vector2(-80, 950));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard6.png", 0.30f, 0.45f, 1.15f, 2f,   posOffset + new Vector2(790, -130));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard7.png", 0.34f, 0.53f, 1.15f, 2.6f, posOffset + new Vector2(1030, 110));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard8.png", 0.27f, 0.4f, 1.1f, 1.5f,   posOffset + new Vector2(-500, 390));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard9.png", 0.27f, 0.45f, 1.21f, 2.5f, posOffset + new Vector2(570, -310));
            GlassShard2(StartTime, EndTime, "sb/particles/glass/shard10.png", 0.3f, 0.45f, 1.12f, 2f,   posOffset + new Vector2(-350, 0));

            //----------------------------------------- Chorus 3 --------------------------------------------------//
            offset = 194928 - 59700;
            StartTime = 59700 + offset;
            EndTime = 64192 + offset;
            q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, 854, 480);
            q.Fade(OsbEasing.OutCubic, StartTime, StartTime+1000, 1, 0);
            Scene1(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            StartTime = 196680;
            EndTime = 197289;
            q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, 854, 480);
            q.Color(EndTime, Color4.Black);

            q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(EndTime, 854, 480);
            q.Fade(OsbEasing.OutExpo, EndTime, EndTime+500, 1, 0);

            StartTime = 64192 + offset;
            EndTime = 66933 + offset;
            Scene2(StartTime, EndTime);
            light = GetLayer("Light").CreateSprite("sb/light.png");
            light.Scale(StartTime, 4/9f);
            light.Fade(EndTime, 0.9);
            CreateLightOverlay("sb/overlay/light/6.jpg",StartTime, EndTime, 0.2f);

            StartTime = 66933 + offset;
            EndTime = 69370 + offset;
            Scene3(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            StartTime = 66705 + offset;
            EndTime = 67010 + offset;
            q = GetLayer("Light").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, EndTime, 854, 480, 854, 480);
            q.Color(StartTime, Color4.Black);
            q.StartLoopGroup(StartTime, 4);
            q.Fade(0, 52, 0, 0.5);
            q.EndGroup();
            q.Fade(StartTime+(52*4), 1);

            StartTime = 69370 + offset; 
            EndTime = 71807 + offset;
            SceneA(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/5.jpg",StartTime, EndTime, 0.1f, flipH: true);

            StartTime = 71807 + offset; 
            EndTime = 74243 + offset;
            SceneB(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/2.jpg",StartTime, EndTime, 0.2f);
            
            StartTime = 77593 + offset;
            EndTime = 79116 + offset;
            q = mainLayer.CreateSprite("sb/q.png");
            q.ScaleVec(EndTime, 854, 480);
            q.Fade(OsbEasing.InExpo, StartTime-250, StartTime, 0,1);

            q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(EndTime, 854, 480);
            q.Fade(OsbEasing.InExpo, EndTime-500, EndTime, 0,1);
            
            StartTime = 74243 + offset;
            EndTime = 79116 + offset;
            Scene4(StartTime, EndTime);  
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard1.png", 0.22f, 0.3f, 1.1f, 1.5f, new Vector2(1050, 480));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard2.png", 0.23f, 0.4f, 1.12f, 1.7f, new Vector2(-140, -150));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard3.png", 0.29f, 0.47f, 1.2f, 2.8f, new Vector2(400, 560));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard4.png", 0.27f, 0.4f, 1.15f, 2f, new Vector2(-220, 670));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard5.png", 0.18f, 0.24f, 1.1f, 1.5f, new Vector2(-80, 950));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard6.png", 0.30f, 0.45f, 1.15f, 2f, new Vector2(790, -130));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard7.png", 0.34f, 0.53f, 1.15f, 2.6f, new Vector2(1030, 110));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard8.png", 0.27f, 0.4f, 1.1f, 1.5f, new Vector2(-500, 390));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard9.png", 0.27f, 0.45f, 1.21f, 2.5f, new Vector2(570, -310));
            GlassShard(StartTime, EndTime, "sb/particles/glass/shard10.png", 0.3f, 0.45f, 1.12f, 2f, new Vector2(-350, 0));
            CreateLightOverlay("sb/overlay/light/4.jpg",StartTime, EndTime, 0.2f);

            StartTime = 214345;
            EndTime = 224319;
            Scene5(StartTime, EndTime);
            CreateLightOverlay("sb/overlay/light/1.jpg",StartTime, EndTime, 0.2f);

            StartTime = 224167;
            EndTime = 233913;
            q = GetLayer("Overlay").CreateSprite("sb/q.png");
            q.ScaleVec(StartTime, 854, 480);
            q.Color(StartTime+152, Color4.Black);

            var gScale = 480f/GetMapsetBitmap("sb/girl.png").Height;
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/girl.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 1f * gScale, startScale : gScale * 1.55f, endScale: gScale * 1.65f, startPosition: new Vector2(440, 295),
                zoomRotation: 0f, startRotation: 0f, endRotation: 0.03f);
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/background_blur.jpg", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 1.3f * gScale, startScale : gScale * 1.5f, endScale: gScale*1.52f);
            CreateAnimation(fgLayer, StartTime, EndTime, "sb/frame.png", Easing: OsbEasing.OutExpo, easingTime: 750,
                zoomScale: 0.9f * gScale, startScale : gScale * 1.5f, endScale: gScale*1.8f, startPosition: new Vector2(480, 280));


            //----------------------------------------- Ending ----------------------------------------------------//

            Scene1(253253, 258126);
            Scene2(258126, 262999);

            StartTime = 262999;
            EndTime = 267873;
            CreateAnimation(mainLayer, StartTime, EndTime, "sb/girl.png", Easing: OsbEasing.OutExpo, easingTime: 1000,
                zoomScale: 3f * gScale, startScale : gScale * 1.4f, endScale: gScale * 1.35f, startPosition: new Vector2(420, 300));
            CreateAnimation(bgLayer, StartTime, EndTime, "sb/background_blur.jpg", Easing: OsbEasing.OutExpo, easingTime: 1000,
                zoomScale: 1.75f * gScale, startScale : gScale * 1.41f, endScale: gScale*1.4f);
            CreateAnimation(fgLayer, StartTime, EndTime, "sb/frame.png", Easing: OsbEasing.OutExpo, easingTime: 1000,
                zoomScale: 4f * gScale, startScale : gScale * 1.45f, endScale: gScale*1.3f, startPosition: new Vector2(420, 300));

            //Unorganised Light Overlays
            CreateLightOverlay("sb/overlay/light/2.jpg",113152, 118177, 0.2f);
            CreateLightOverlay("sb/overlay/light/2.jpg",174218, 179091, 0.2f);
            CreateLightOverlay("sb/overlay/light/5.jpg",179091, 181527, 0.1f);
            CreateLightOverlay("sb/overlay/light/1.jpg",181527, 183964, 0.2f);
            CreateLightOverlay("sb/overlay/light/7.jpg",183964, 186400, 0.2f);
            CreateLightOverlay("sb/overlay/light/6.jpg",186400, 188837, 0.2f);
            CreateLightOverlay("sb/overlay/light/4.jpg",188837, 193253, 0.2f);
            CreateLightOverlay("sb/overlay/light/2.jpg",224319, 233913, 0.2f);

            CreateLightOverlay("sb/overlay/light/1.jpg",253253, 257974, 0.2f);
            CreateLightOverlay("sb/overlay/light/2.jpg",258126, 262999, 0.2f);
            CreateLightOverlay("sb/overlay/light/5.jpg",262999, 267796, 0.1f);
            CreateLightOverlay("sb/overlay/light/5.jpg",267873, 270309, 0.1f);

        }

        public void CreateLightOverlay(string imagePath, int startTime, int endTime, float opacity, bool? flipH = false)
        {
            var sprite = GetLayer("Overlay").CreateSprite(imagePath);
            sprite.Scale(startTime, 854f / GetMapsetBitmap(imagePath).Width);
            sprite.Fade(startTime, opacity);
            sprite.Additive(endTime);
            if (flipH.Value) sprite.FlipH(startTime);
        }

        public void GlassShard(int StartTime, int EndTime, string Path, float zoomScale, float startScale, float endScale, float endZoom, Vector2 position)
        {
            startScale *= 1.1f;
            endScale = startScale * ((endScale - 1f) * 0.65f + 1f);
            endZoom = (endZoom - 1f) * 0.4f + 1f;

            var beat = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration;
            var glass = mainLayer.CreateSprite(Path);
            glass.Scale(OsbEasing.OutExpo, StartTime, StartTime + beat*2, zoomScale, startScale);
            glass.Scale(StartTime + beat*2, EndTime - beat*1, startScale, endScale);
            glass.Scale(OsbEasing.InExpo, EndTime - beat*1, EndTime, endScale, endScale*endZoom);

            glass.Move(OsbEasing.OutExpo, StartTime, StartTime + beat*2, MoveCentreOrigin(position, zoomScale), MoveCentreOrigin(position, startScale));
            glass.Move(StartTime + beat*2, EndTime - beat*1, MoveCentreOrigin(position, startScale), MoveCentreOrigin(position, endScale));
            glass.Move(OsbEasing.InExpo, EndTime - beat*1, EndTime, MoveCentreOrigin(position, endScale), MoveCentreOrigin(position, endScale*endZoom));

            glass.Color(OsbEasing.InExpo, StartTime+3350-100, StartTime+3350, Color4.White, Color4.Black);
            glass.Fade(OsbEasing.InExpo, StartTime+3350-100, StartTime+3350, 0.8f, 0.2f);            
        }

        public void GlassShard2(int StartTime, int EndTime, string Path, float zoomScale, float startScale, float endScale, float endZoom, Vector2 position)
        {
            startScale *= 1.1f;
            endScale = startScale * ((endScale - 1f) * 0.2f + 1f);
            endZoom = (endZoom-endScale) * 0.1f + endScale;

            var glass = GetLayer("Glass").CreateSprite(Path);
            glass.Scale(OsbEasing.OutExpo, StartTime, StartTime + 500, endZoom, endScale);
            glass.Scale(StartTime + 500, EndTime , endScale, startScale);

            glass.Move(OsbEasing.OutExpo, StartTime, StartTime + 500, MoveCentreOrigin(position, endZoom), MoveCentreOrigin(position, endScale));
            glass.Move(StartTime + 500, EndTime, MoveCentreOrigin(position, endScale), MoveCentreOrigin(position, startScale));
            glass.Fade(StartTime, 0.8f);
        }

        public void Scene1(int StartTime, int EndTime)
        {
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scale)
            {
                sprite.Layer = layer;
                sprite.SpritePath = path;
                sprite.UseDistanceFade = false;
                sprite.ConfigureGenerators(s =>
                {
                    s.ScaleDecimals = 4;
                    s.ScaleTolerance = 0.2f;
                });
                sprite.PositionX.Add(StartTime, posX);
                sprite.PositionY.Add(StartTime, posY);
                sprite.PositionZ.Add(StartTime, posZ);
                sprite.ScaleX.Add(StartTime, scale);
                sprite.ScaleY.Add(StartTime, scale);
                scene.Add(sprite);
            }

            ConfigureSprite(new Sprite3d(), mainLayer, girlPath, 0, 0, 0, 0.4f); // girl
            ConfigureSprite(new Sprite3d(), fgLayer, framePath, 0, 0, 10, 0.22f); // frame
            ConfigureSprite(new Sprite3d(), bgLayer, bgPath, 0, 0, -40, 1.12f); // bg

            scene.Root.PositionZ.Add(StartTime, 39);
            scene.Root.PositionZ.Add(EndTime, 18, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.Rotation.Add(StartTime, -0.3f);
            scene.Root.Rotation.Add(EndTime, 0, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 8);
        }

        public void Scene2(int StartTime, int EndTime)
        {
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scale)
            {
                sprite.Layer = layer;
                sprite.SpritePath = path;
                sprite.UseDistanceFade = false;
                sprite.ConfigureGenerators(s =>
                {
                    s.ScaleDecimals = 4;
                    s.ScaleTolerance = 0.2f;
                });
                sprite.PositionX.Add(StartTime, posX);
                sprite.PositionY.Add(StartTime, posY);
                sprite.PositionZ.Add(StartTime, posZ);
                sprite.ScaleX.Add(StartTime, scale);
                sprite.ScaleY.Add(StartTime, scale);
                scene.Add(sprite);
            }

            ConfigureSprite(new Sprite3d(), mainLayer, "sb/girl_close.png", 380, -50, 0, 0.55f); // girl
            ConfigureSprite(new Sprite3d(), bgLayer, bgBlurPath, 1200, 100, -40, 3f); // bg

            void ConfigureFeather(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scale, float Rotate)
            {
                sprite.Layer = layer;
                sprite.SpritePath = path;
                sprite.UseDistanceFade = false;
                sprite.ConfigureGenerators(s =>
                {
                    s.ScaleDecimals = 4;
                    s.ScaleTolerance = 0.2f;
                });
                sprite.PositionX.Add(StartTime, posX);
                sprite.PositionY.Add(StartTime, posY);
                sprite.PositionZ.Add(StartTime, posZ);
                sprite.ScaleX.Add(StartTime, scale);
                sprite.ScaleY.Add(StartTime, scale);
                sprite.Rotation.Add(StartTime, Rotate);
                scene.Add(sprite);
            }

            ConfigureFeather(new Sprite3d(), fgLayer, "sb/particles/feather/6.png", 170, 30, 20, 0.15f, 0.7f);
            ConfigureFeather(new Sprite3d(), fgLayer, "sb/particles/feather/7.png", -180, -70, 17, 0.2f, -0.3f);
            ConfigureFeather(new Sprite3d(), fgLayer, "sb/particles/feather/8.png", -140, 100, 10, 0.3f, 0f);

            scene.Root.PositionZ.Add(StartTime, 10);
            scene.Root.PositionZ.Add(EndTime - 533, 10);
            scene.Root.PositionZ.Add(EndTime, 14, EasingFunctions.ToEasingFunction(OsbEasing.InExpo));
            scene.Root.PositionX.Add(StartTime, -250);
            scene.Root.PositionX.Add(StartTime+1000, -10, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionX.Add(EndTime-50, 0);
            scene.Root.PositionX.Add(EndTime, 0, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);
        }

        public void Scene3(int StartTime, int EndTime)
        {
            var end = EndTime;
            EndTime += 500;
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scale, float opacity)
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
                sprite.ScaleX.Add(StartTime, scale);
                sprite.ScaleY.Add(StartTime, scale);
                sprite.Opacity.Add(end, opacity);
                sprite.Opacity.Add(end, 0);
                scene.Add(sprite);
            }

            ConfigureSprite(new Sprite3d(), mainLayer, girlPath, 0, 0, 0, 0.4f, 1); // girl
            

            Sprite3d frame = new Sprite3d
            {
                Layer = fgLayer,
                SpritePath = framePath,
                UseDistanceFade = false
            };
            frame.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            var scale = 0.22f;
            frame.PositionX.Add(StartTime, 0);
            frame.PositionY.Add(StartTime, 0);
            frame.PositionY.Add(StartTime, -20);
            frame.PositionZ.Add(StartTime, 10);
            frame.ScaleX.Add(StartTime, scale);
            frame.ScaleY.Add(StartTime, scale);
            frame.Opacity.Add(67923, 1);
            frame.Opacity.Add(end, 0.5f);
            frame.Opacity.Add(end, 0f);

            Sprite3d bg = new Sprite3d
            {
                Layer = bgLayer,
                SpritePath = bgBlurPath,
                UseDistanceFade = false
            };
            bg.ConfigureGenerators(s => {
                s.ScaleDecimals = 6;
                s.ScaleTolerance = 0.1f;
            });
            scale = 1.21f;
            bg.PositionX.Add(StartTime, 0);
            bg.PositionY.Add(StartTime, 0);
            bg.PositionZ.Add(StartTime, -40);
            bg.ScaleX.Add(StartTime, scale);
            bg.ScaleY.Add(StartTime, scale);
            bg.Opacity.Add(end, 1);
            bg.Opacity.Add(end, 0);

            scene.Add(bg);
            scene.Add(frame);

            scene.Root.PositionZ.Add(StartTime, 18f);
            scene.Root.PositionZ.Add(EndTime, 26, EasingFunctions.ToEasingFunction(OsbEasing.OutQuad));
            scene.Root.PositionY.Add(EndTime, 60, EasingFunctions.ToEasingFunction(OsbEasing.OutQuad));

            scene.Root.PositionX.Add(EndTime, 60, EasingFunctions.ToEasingFunction(OsbEasing.OutQuad));

            scene.Root.Rotation.Add(StartTime, 0f);
            scene.Root.Rotation.Add(EndTime, 0.12f, EasingFunctions.ToEasingFunction(OsbEasing.OutQuad));

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 4);
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

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scale)
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
                sprite.ScaleX.Add(StartTime, scale);
                sprite.ScaleY.Add(StartTime, scale);
                scene.Add(sprite);
            }

            ConfigureSprite(new Sprite3d(), mainLayer, "sb/a2.png", 10, 0, 0, 0.45f); // girl
            ConfigureSprite(new Sprite3d(), fgLayer, "sb/a3.png", 40, 0, 10, 0.25f); // frame
            ConfigureSprite(new Sprite3d(), bgLayer, "sb/a1.png", 0, 0, -40, 1.4f); // bg

            scene.Root.PositionZ.Add(StartTime, 22.1f);
            scene.Root.PositionX.Add(StartTime, 25);
            scene.Root.PositionX.Add(StartTime+1000, -56, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionX.Add(EndTime-250, -60);
            scene.Root.PositionX.Add(EndTime, -70, EasingFunctions.ToEasingFunction(OsbEasing.InExpo));

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

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scale)
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
                sprite.ScaleX.Add(StartTime, scale);
                sprite.ScaleY.Add(StartTime, scale);
                scene.Add(sprite);
            }

            ConfigureSprite(new Sprite3d(), mainLayer, "sb/b2.png", 40, 0, 0, 0.45f); // girl
            ConfigureSprite(new Sprite3d(), fgLayer, "sb/b3.png", 50, -20, 8, 0.27f); // frame
            ConfigureSprite(new Sprite3d(), bgLayer, "sb/b1.png", 0, 0, -40, 1.36f); // bg

            scene.Root.PositionZ.Add(StartTime, 24f);
            scene.Root.PositionZ.Add(EndTime-250, 24f);
            scene.Root.PositionZ.Add(EndTime, 29f, EasingFunctions.ToEasingFunction(OsbEasing.InExpo));
            scene.Root.PositionX.Add(StartTime, 20);
            scene.Root.PositionX.Add(StartTime+1000, -56, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionX.Add(EndTime, -60);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 16);
        }

        public void Scene4(int StartTime, int EndTime)
        {
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            Sprite3d bg = new Sprite3d
            {
                Layer = bgLayer,
                SpritePath = "sb/girl2.jpg",
                UseDistanceFade = false
            };
            bg.ConfigureGenerators(s => {
                s.ScaleDecimals = 4;
                s.ScaleTolerance = 0.2f;
            });
            var scale = 2.35f;
            bg.PositionX.Add(StartTime, 0);
            bg.PositionY.Add(StartTime, 0);
            bg.PositionZ.Add(StartTime, -100);
            bg.Opacity.Add(StartTime, 0.2f);
            bg.ScaleX.Add(StartTime, scale);
            bg.ScaleY.Add(StartTime, scale);
            bg.Opacity.Add(StartTime+3350, 0.1f);
            bg.Opacity.Add(StartTime+3350, 0);
            scene.Add(bg);

            var beat = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration;
            scene.Root.PositionZ.Add(StartTime, 5f);
            scene.Root.PositionZ.Add(StartTime + beat*2, 20f, EasingFunctions.ToEasingFunction(OsbEasing.OutExpo));
            scene.Root.PositionZ.Add(EndTime-beat, 24f);
            scene.Root.PositionX.Add(StartTime, 30);

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 4);
        }

        public void Scene5(int StartTime, int EndTime)
        {
            Scene3d scene = new Scene3d();
            scene.Root.PositionX.Add(StartTime, 0);
            scene.Root.PositionY.Add(StartTime, 0);
            scene.Root.PositionZ.Add(StartTime, 30);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.PositionX.Add(StartTime, 0);
            camera.PositionY.Add(StartTime, 0);
            camera.PositionZ.Add(StartTime, 40);

            void ConfigureSprite(Sprite3d sprite, StoryboardLayer layer, string path, int posX, int posY, int posZ, float scale)
            {
                sprite.Layer = layer;
                sprite.SpritePath = path;
                sprite.UseDistanceFade = false;
                sprite.ConfigureGenerators(s =>
                {
                    s.ScaleDecimals = 4;
                    s.ScaleTolerance = 0.2f;
                });
                sprite.PositionX.Add(StartTime, posX);
                sprite.PositionY.Add(StartTime, posY);
                sprite.PositionZ.Add(StartTime, posZ);
                sprite.ScaleX.Add(StartTime, scale);
                sprite.ScaleY.Add(StartTime, scale);
                scene.Add(sprite);
            }

            ConfigureSprite(new Sprite3d(), mainLayer, girlPath, 0, 0, 0, 0.4f); // girl
            ConfigureSprite(new Sprite3d(), fgLayer, framePath, 0, 0, 10, 0.22f); // frame
            ConfigureSprite(new Sprite3d(), bgLayer, bgPath, 0, 0, -40, 1.12f); // bg

            scene.Root.PositionZ.Add(StartTime, 39);
            scene.Root.PositionZ.Add(StartTime+4000, 19f, EasingFunctions.ToEasingFunction(OsbEasing.OutQuint));
            scene.Root.PositionZ.Add(EndTime, 18.5f);
            scene.Root.Rotation.Add(StartTime, -0.3f);
            scene.Root.Rotation.Add(StartTime+4000, 0, EasingFunctions.ToEasingFunction(OsbEasing.OutQuint));

            var beat = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration * 2;
            for (int i = 0; i < (EndTime-StartTime)/beat; i++)
            {
                scene.Root.PositionX.Add(StartTime + i * beat, Random(-1.5f, 1.5f), EasingFunctions.ToEasingFunction(OsbEasing.InOutSine));
                scene.Root.PositionY.Add(StartTime + i * beat, Random(-1f, 1f), EasingFunctions.ToEasingFunction(OsbEasing.InOutSine));
            }

            scene.Generate(camera, GetLayer(""), StartTime, EndTime, Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 8);
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

            if (spritePath == featherPath) sprite.Color(startTime, Color4.Black);
            if (spritePath == girlPath && startTime == 77593) sprite.Color(startTime, Color4.Black);
        }

        public Vector2 MoveCentreOrigin(Vector2 startPosition, double factor, Vector2? centre = null)
        {
            if (!centre.HasValue) centre = new Vector2(320, 240);
            var angle = Math.Atan2(centre.Value.Y - startPosition.Y, centre.Value.X - startPosition.X);
            var newFactor = Math.Sqrt(Math.Pow(centre.Value.X - startPosition.X, 2) + Math.Pow(centre.Value.Y - startPosition.Y, 2)) * (1 - factor);
            return new Vector2((float)(startPosition.X + Math.Cos(angle) * newFactor), (float)(startPosition.Y + Math.Sin(angle) * newFactor));
        }
    }
}

