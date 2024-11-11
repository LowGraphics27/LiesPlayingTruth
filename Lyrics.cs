using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security.Cryptography;
using StorybrewCommon.Storyboarding3d;

namespace StorybrewScripts
{
    public class Lyrics : StoryboardObjectGenerator
    {
        FontGenerator chorusLargeFont, chorusItalicFont, chorusFont, smooshFont, engFont, engCursiveFont, engCursiveFontLarge, chorusFontLarge;
        StoryboardLayer TextBehind, Text, Box, TextFront, TextMoreFront, engText, engTextReversed, verseText, creditLayer, diffSpecificLayer;
        [Configurable] public Color4 RedFont; //DA1D45, 

        private void Initialiser()
        {   
            TextBehind = GetLayer("Text Behind");
            Box = GetLayer("Box");
            Text = GetLayer("Text");
            TextFront = GetLayer("Text Front");
            TextMoreFront = GetLayer("Text More Front");
            verseText = GetLayer("Text Verse");
            engText = GetLayer("English Text");
            engTextReversed = GetLayer("Eng Test Rotated");
            creditLayer = GetLayer("Credits");
            diffSpecificLayer = GetLayer("Diff");

            chorusLargeFont = LoadFont("sb/f/large", new FontDescription()
            {
                FontPath = "fontlibrary/TsukuAOldMincho.ttf",
                FontSize = 600,
                Color = Color4.White,
                Padding = new Vector2(200,50),
                FontStyle = System.Drawing.FontStyle.Italic,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow() {Radius = 0, Color = Color4.White },
            new FontOutline(){ Thickness = 0, Color = Color4.White },
            new FontShadow() { Thickness = 0, Color = Color4.Black });

            chorusFont = LoadFont("sb/f/reg", new FontDescription()
            {
                FontPath = "fontlibrary/TsukuAOldMincho.ttf",
                FontSize = 200,
                Color = Color4.White,
                Padding = new Vector2(50,50),
                FontStyle = System.Drawing.FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow() {Radius = 0, Color = Color4.White },
            new FontOutline(){ Thickness = 0, Color = Color4.White },
            new FontShadow() { Thickness = 0, Color = Color4.Black });

            chorusFontLarge = LoadFont("sb/f/reg_large", new FontDescription()
            {
                FontPath = "fontlibrary/TsukuAOldMincho.ttf",
                FontSize = 400,
                Color = Color4.White,
                Padding = new Vector2(100,100),
                FontStyle = System.Drawing.FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow() {Radius = 0, Color = Color4.White },
            new FontOutline(){ Thickness = 0, Color = Color4.White },
            new FontShadow() { Thickness = 0, Color = Color4.Black });

            chorusItalicFont = LoadFont("sb/f/italic", new FontDescription()
            {
                FontPath = "fontlibrary/TsukuAOldMincho.ttf",
                FontSize = 200,
                Color = Color4.White,
                Padding = new Vector2(60,50),
                FontStyle = System.Drawing.FontStyle.Italic,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow() {Radius = 0, Color = Color4.Black },
            new FontOutline(){ Thickness = 0, Color = Color4.Black },
            new FontShadow() { Thickness = 0, Color = Color4.Black });

            smooshFont = LoadFont("sb/f/s", new FontDescription()
            {
                FontPath = "fontlibrary/smoosh.ttf",
                FontSize = 800,
                Color = Color4.White,
                Padding = new Vector2(50,50),
                FontStyle = System.Drawing.FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow() {Radius = 0, Color = Color4.White },
            new FontOutline(){ Thickness = 0, Color = Color4.White },
            new FontShadow() { Thickness = 0, Color = Color4.Black });

            engFont = LoadFont("sb/f/e", new FontDescription()
            {
                FontPath = "fontlibrary/TsukuAOldMincho.ttf",
                FontSize = 40,
                Color = Color4.White,
                Padding = new Vector2(0,0),
                FontStyle = System.Drawing.FontStyle.Regular,
                TrimTransparency = false,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow() {Radius = 0, Color = Color4.White },
            new FontOutline(){ Thickness = 0, Color = Color4.White },
            new FontShadow() { Thickness = 0, Color = Color4.Black });

            engCursiveFont = LoadFont("sb/f/c", new FontDescription()
            {
                FontPath = "fontlibrary/Creattion Demo.otf",
                FontSize = 100,
                Color = Color4.White,
                Padding = new Vector2(50,0),
                FontStyle = System.Drawing.FontStyle.Regular,
                TrimTransparency = false,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow() {Radius = 0, Color = Color4.White },
            new FontOutline(){ Thickness = 0, Color = Color4.White },
            new FontShadow() { Thickness = 0, Color = Color4.Black });

            engCursiveFontLarge = LoadFont("sb/f/c_large", new FontDescription()
            {
                FontPath = "fontlibrary/Creattion Demo.otf",
                FontSize = 200,
                Color = Color4.White,
                Padding = new Vector2(100,0),
                FontStyle = System.Drawing.FontStyle.Regular,
                TrimTransparency = false,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow() {Radius = 0, Color = Color4.White },
            new FontOutline(){ Thickness = 0, Color = Color4.White },
            new FontShadow() { Thickness = 0, Color = Color4.Black });

            var q = GetLayer("Debug").CreateSprite("sb/q.png");
            q.ScaleVec(0, 290011, 1, 480, 1, 480);

            q = GetLayer("Debug").CreateSprite("sb/q.png");
            q.ScaleVec(0, 290011, 854, 1, 854, 1);
        }

        public override void Generate()
        {
		    Initialiser();

            /*LYRICS
            Verse 1
            期待外れのことばかりで
            ドラマのようなラスト求めた
            喜劇か悲劇か 決めるのは誰だ？
            誰もが纏うポーカーフェイス

            都合のいい物語で構わない
            嘘だらけの世界の中で

            Chorus 1
            重ねたメモリーはフィクション
            演じてたんだ
            嘘じゃない眩しさに 怯えて

            ニセモノに包まれた
            空っぽな心なんだ
            ああ 僕を壊さないで
            簡単に絆されて Lost my way
            自分を失わぬよう

            Verse 2
            有象無象のような
            枯れてゆくもの
            そうなりたくはないから
            ズルしたっていい
            信じられるなら
            ホンモノを描け 一度の人生(ストーリー)

            現実は残酷か 決めるのは誰だ？
            答はわからない？あーあ

            思考停止と言う
            毒に侵されて
            自分を委ね続けて
            流されていく
            そんな自分じゃニセモノのようだ

            Chorus 2
            見せかけばかりのフィクション
            演じてたんだ
            たとえ嘘で塗り固めたって

            何が正しい、なんて
            思ったもん勝ちでしょ？
            ああ もっと踊らされて！
            簡単に堕ちてゆく
            "自分"が無いのならば

            Chorus 3
            重ねたメモリーはフィクション
            演じてたんだ
            嘘じゃない眩しさが怖かった
            騙されてしまうなら
            僕は騙し続けよう
            真実と嘘の間で

            1度きりの舞台[ステージ]で
            ホンモノを探して
            演じ続けているんだ
            それぞれ描くフィクションで

            Ending
            この世界の中でずっと
            負けないよう 手探りの僕は
            たとえ真実が嘘となっても
            生きていくために 騙されよう
            
            */


            //----------------------------------------- Chorus 1 --------------------------------------------------//
            
            //重ねたメモリーはフィクション
            GenerateLyrics(Text, chorusLargeFont, "重ねた", 59091, 60233, 0.5f, -120f, new Vector2(-40,7),
                Entry: 3, inLetterOffset: 152/2, inTimeOffset: 1000, inMoveOffset: 80f, Exit: 99);
            var q = Box.CreateSprite("sb/q.png");
            q.ScaleVec(OsbEasing.OutExpo, 60233, 60233 + 500, 270, 16, 290, 16);
            q.Color(62060, Color4.Black);
            q = Box.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(-107, 240));
            q.ScaleVec(OsbEasing.OutExpo, 60233, 62060 , 200, 0.5, 250, 0.5);
            q = Box.CreateSprite("sb/q.png", OsbOrigin.CentreRight, new Vector2(747, 240));
            q.ScaleVec(OsbEasing.OutExpo, 60233, 62060, 200, 0.5, 250, 0.5);
            GenerateLyrics(Text, chorusFont, "重ねたメモリーはフィクション", 60233, 62060, 0.06f, 5f, new Vector2(0,1),
                Entry: 4, inLetterOffset: 0, inTimeOffset: 750, Exit: 99);
            GenerateLyrics(Text, engFont, "All these piles of memories are fiction", 60233, 62060, 0.18f, 3.7f, new Vector2(0,22),
                Entry: 0, inLetterOffset: 0, inTimeOffset: 0, Exit: 99);
            GenerateLyrics(TextBehind, smooshFont, "MEMORY", 60233, 61451, 0.8f, -20f, new Vector2(0,120),
                Entry: 5, inLetterOffset: 0, inTimeOffset: 750, Exit: 4);
            GenerateLyrics(TextBehind, smooshFont, "FICTION", 61451, 62060, 0.8f, -20f, new Vector2(0,120),
                Entry: 6, inLetterOffset: 0, inTimeOffset: 500, Exit: 99);

            //演じてたんだ
            GenerateLyrics(TextFront, chorusItalicFont, "演じてたんだ", 62060, 64192, 0.5f, -50f, new Vector2(-10,-25), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "Just an act, it was", 62060, 64192, 0.45f, 5f, new Vector2(0,25), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            //嘘じゃない眩しさに 怯えて
            var posOffset = new Vector2(0,-35);
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(-51, 241) + posOffset);
            q.ScaleVec(OsbEasing.OutCubic, 64192, 65487+1000, 80, 20, 246, 20);
            q.Color(64192, 64192+33, Color4.Black, Color4.Black);
            q.Color(64192+33, RedFont);
            q.Color(67847, RedFont);
            GenerateLyrics(TextFront, chorusFont, "嘘じゃない", 64192, 67847, 0.2f, -5f, new Vector2(-240,0) + posOffset,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(34, 314) + posOffset);
            q.ScaleVec(OsbEasing.OutCubic, 65487, 67847, 50, 20, 198, 20);
            q.Color(65487, 65487+33, Color4.Black, Color4.Black);
            q.Color(65487+33, RedFont);
            q.Color(67847, RedFont);
            GenerateLyrics(TextFront, chorusFont, "眩しさに", 65487, 67847, 0.2f, -5f, new Vector2(-180,70) + posOffset,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(TextFront, engFont, "It's not a lie, I'm terrified to face the blinding truth", 64192, 67847, 0.18f, 2f, new Vector2(-210,2),
                Entry: 0, inLetterOffset: 10, inTimeOffset: 0, Exit: 99);

            GenerateLyrics(TextFront, chorusItalicFont, "怯えて", 67847, 69370, 0.3f, 10f, new Vector2(210,0), Vertical:true, Angle: -0.2f,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 99, outTimeOffset: 250, outMoveOffset: 10f);
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreRight, new Vector2(477, 314-35));
            q.ScaleVec(OsbEasing.OutExpo, 67847, 68228, 250, 20, 0, 20);
            q.Color(68228, RedFont);
            GenerateLyrics(TextFront, engCursiveFont, "Afraid", 67847, 69370, 0.6f, 4f, new Vector2(285,10), Angle: 0.2f - (float)Math.PI/2,
                Entry: 2, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 10f, Exit: 99);

            //ニセモノに包まれた
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(TextFront, chorusFont, "ニセモノに", 69294, 71807, 0.22f, -5f, new Vector2(-200,-70) + posOffset,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 4, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusItalicFont, "包", 70055, 71807, 0.5f, 0f, new Vector2(-20,-10) + posOffset, Angle: -0.2f,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1500, inMoveOffset: 15f, Exit: 4, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "まれた", 70055+100, 71807, 0.22f, 10f, new Vector2(190,60) + posOffset,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 4, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateLyrics(TextMoreFront, engCursiveFont, "I was lost and trapped in a fantasy", 69370, 71807, 0.35f, 3f, new Vector2(0,20), Angle: -0.2f,
                Entry: 0, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            //空っぽな心なんだ
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(TextFront, chorusFont, "空っぽな", 71807, 74243, 0.22f, 20f, new Vector2(40,-5) + posOffset, Vertical: true,
                Entry: 0, inLetterOffset: 152, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 2, outLetterOffset: 250, outMoveOffset: 10f);
            GenerateNoiseLyrics(TextFront, chorusFont, "心なんだ", 71807+400, 74243, 0.22f, 20f, new Vector2(-56,-5) + posOffset, Vertical: true,
                Entry: 0, inLetterOffset: 152, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 3, outLetterOffset: 250, outMoveOffset: 10f);
            GenerateLyrics(TextMoreFront, engFont, "My  heart  was  just", 71807, 74243, 0.18f, 4f, new Vector2(-210,5),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);
            GenerateLyrics(TextMoreFront, engFont, "running  on  empty", 71807+600, 74243, 0.18f, 4f, new Vector2(210,5),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);

            //ああ 僕を壊さないで
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(TextFront, chorusFont, "ああ", 74243, 75233, 0.25f, 0f, new Vector2(0, 0) + posOffset,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 6, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "僕を", 75233, 76299, 0.45f, -10f, new Vector2(0, 0) + posOffset,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1100, inMoveOffset: 30f, Exit: 99, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateCharacterSeparation("壊", 76299-200, 77593, 0.3f, 0f, new Vector2(320, 240));
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(161, 176) + posOffset);
            q.ScaleVec(OsbEasing.OutExpo, 77593, 79116, 0, 0.5, 180, 0.5);
            q.Color(79116, Color4.Black);
            GenerateNoiseLyrics(TextFront, chorusFont, "ああ ", 77593, 79116, 0.15f, -10f, new Vector2(-210, -42) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 10f, Exit: 99, outLetterOffset: 0, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "僕を ", 77593, 79116, 0.4f, -10f, new Vector2(180, -10) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 20f, Exit: 99, outLetterOffset: 0, outMoveOffset: 20f);
            GenerateLyrics(TextMoreFront, engFont, "Ah, I can't take much more - don't break me now", 77593, 79116, 0.18f, 1f, new Vector2(150,17),
                Entry: 1, inLetterOffset: 10, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);
            GenerateNoiseLyrics(TextFront, chorusFont, "壊さないで", 77593, 79116, 0.4f, -10f, new Vector2(0,60) + posOffset,
                Entry: 1, inLetterOffset: 50, inTimeOffset: 1000, inMoveOffset: 50f, Exit: 99);

            //簡単に絆されて Lost my way
            GenerateLyrics(TextFront, chorusItalicFont, "簡単に絆されて", 79421, 82238, 0.5f, -50f, new Vector2(0,-30), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "I was so easily swept away", 79421, 82238, 0.45f, 8f, new Vector2(0,30), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(TextBehind, smooshFont, "LOST MY WAY", 82162, 84065-300, 0.8f, -40f, new Vector2(0,335),
                Entry: 5, inLetterOffset: 0, inTimeOffset: 750, Exit: 99);

            //自分を失わぬよう
            GenerateLyrics(TextFront, chorusItalicFont, "自分を失わぬよう", 84218-100, 88939, 0.5f, -60f, new Vector2(-5,-40), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "I'm struggling not to lose myself", 84218, 88939, 0.45f, 7f, new Vector2(10,30), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            //----------------------------------------- Chorus 2 --------------------------------------------------//

            //見せかけばかりのフィクション
            GenerateLyrics(GetLayer("OVER OVER"), chorusLargeFont, "見せかけ", 134472, 135766, 0.5f, -200f, new Vector2(-50,10),
                Entry: 3, inLetterOffset: 152/2, inTimeOffset: 1000, inMoveOffset: 80f, Exit: 99);
            GenerateLyrics(Text, chorusFont, "見せかけばかりのフィクション", 135766, 137593, 0.06f, 5f, new Vector2(0,1),
                Entry: 4, inLetterOffset: 0, inTimeOffset: 750, Exit: 99);
            q = Box.CreateSprite("sb/q.png");
            q.ScaleVec(OsbEasing.OutExpo, 135766, 135766 + 500, 270, 16, 290, 16);
            q.Color(137593, Color4.Black);
            q = Box.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(-107, 240));
            q.ScaleVec(OsbEasing.OutExpo, 135766, 137593 , 200, 0.5, 250, 0.5);
            q = Box.CreateSprite("sb/q.png", OsbOrigin.CentreRight, new Vector2(747, 240));
            q.ScaleVec(OsbEasing.OutExpo, 135766, 137593, 200, 0.5, 250, 0.5);
            GenerateLyrics(Text, engFont, "See this semblance of delusion's fiction", 135766, 137593, 0.18f, 3.5f, new Vector2(0,22),
                Entry: 0, inLetterOffset: 0, inTimeOffset: 0, Exit: 99);
            GenerateLyrics(TextBehind, smooshFont, "DELUSION", 135766, 136984, 0.8f, -20f, new Vector2(0,120),
                Entry: 5, inLetterOffset: 0, inTimeOffset: 750, Exit: 4);
            GenerateLyrics(TextBehind, smooshFont, "FICTION", 136984, 137593, 0.8f, -20f, new Vector2(0,120),
                Entry: 6, inLetterOffset: 0, inTimeOffset: 500, Exit: 99);

            //演じてたんだ
            GenerateLyrics(TextFront, chorusItalicFont, "演じてたんだ", 137593, 139725, 0.5f, -50f, new Vector2(-10,-25), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "All an act, it was", 137593, 139725, 0.45f, 5f, new Vector2(5,25), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            //たとえ嘘で塗り固めたって
            posOffset = new Vector2(0,0);
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(-51, 240) + posOffset);
            q.ScaleVec(OsbEasing.OutCubic, 139725, 142390-500, 80, 20, 340, 20);
            q.Color(139725, 139725+33, Color4.Black, Color4.Black);
            q.Color(139725+33, RedFont);
            q.Color(142466, RedFont);
            GenerateLyrics(TextFront, chorusFont, "たとえ嘘で塗り", 139725, 142466, 0.2f, -5f, new Vector2(-200,0) + posOffset,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(TextFront, engFont, "There is no hope, this pretense is -", 139725, 142466, 0.18f, 2f, new Vector2(-260,30),
                Entry: 0, inLetterOffset: 20, inTimeOffset: 0, Exit: 99);

            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(394, 240) + posOffset);
            q.ScaleVec(OsbEasing.OutExpo, 142466, 144827, 235-200, 20, 235, 20);
            q.Color(144903, RedFont);
            GenerateLyrics(TextFront, chorusFont, "固めたって", 142466, 144903, 0.2f, -5f, new Vector2(200,0) + posOffset,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(TextFront, engFont, "Entirely built on lies", 142466, 144903, 0.18f, 2f, new Vector2(140,30),
                Entry: 0, inLetterOffset: 20, inTimeOffset: 0, Exit: 99);
            
            //何が正しい、なんて
            posOffset = new Vector2(40,20);
            GenerateNoiseLyrics(TextFront, chorusFont, "何が正しい、", 144903, 147340, 0.22f, 20f, new Vector2(-100,-30) + posOffset,
                Entry: 0, inLetterOffset: 120, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 4, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "なんて", 145893, 147340, 0.22f, 10f, new Vector2(140,30) + posOffset,
                Entry: 1, inLetterOffset: 50, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 4, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateLyrics(TextFront, engFont, "I don't know if I have the right to say", 144903, 147340, 0.18f, 2f, new Vector2(-170, 55),
                Entry: 1, inLetterOffset: 15, inTimeOffset: 1000, Exit: 99);
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft);
            q.Move(145741, 273, 293);
            q.ScaleVec(OsbEasing.OutExpo, 145741-50, 147340, 0, 0.6, 120, 0.6);
            q.Fade(145741, .5f);

            //思ったもん勝ちでしょ
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(TextFront, chorusFont, "思ったもん", 147340, 149776, 0.22f, 20f, new Vector2(40,-5) + posOffset, Vertical: true,
                Entry: 0, inLetterOffset: 152, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 2, outLetterOffset: 250, outMoveOffset: 10f);
            GenerateNoiseLyrics(TextFront, chorusFont, "勝ちでしょ", 148177-200, 149776, 0.22f, 15f, new Vector2(-56,-15) + posOffset, Vertical: true,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 3, outLetterOffset: 250, outMoveOffset: 10f);
            GenerateLyrics(TextMoreFront, engFont, "Shouldn't  the  winners", 147340, 149776, 0.18f, 4f, new Vector2(-220,5),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);
            GenerateLyrics(TextMoreFront, engFont, "just  get  their  way?", 147340+400, 149776, 0.18f, 4f, new Vector2(210,5),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);

            //ああ もっと踊らされて
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(TextFront, chorusFont, "ああ", 149776, 150842, 0.25f, 0f, new Vector2(0, 0) + posOffset,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 6, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "もっと", 150842, 151908, 0.45f, -25f, new Vector2(0, 0) + posOffset,
                Entry: 12, inLetterOffset: 0, inTimeOffset: 1100, inMoveOffset: 30f, Exit: 99, outLetterOffset: 250, outMoveOffset: 20f);
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(161, 176) + posOffset);
            q.ScaleVec(OsbEasing.OutExpo, 153126, 154649, 0, 0.5, 160, 0.5);
            q.Color(153126, Color4.Black);
            GenerateNoiseLyrics(TextFront, chorusFont, "ああ ", 153126, 154649, 0.15f, -10f, new Vector2(-210, -42) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 10f, Exit: 99, outLetterOffset: 0, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "もっと ", 153126, 154649, 0.4f, -25f, new Vector2(140, 0) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 20f, Exit: 99, outLetterOffset: 0, outMoveOffset: 20f);
            GenerateCharacterSeparation("踊", 151908-100, 153126, 0.4f, 0f, new Vector2(320, 240));
            GenerateNoiseLyrics(TextFront, chorusFont, "踊らされて", 153126, 154649, 0.4f, -10f, new Vector2(0,60) + posOffset,
                Entry: 1, inLetterOffset: 50, inTimeOffset: 1000, inMoveOffset: 50f, Exit: 99);
            GenerateLyrics(TextMoreFront, engFont, "Ah, I've lost all control - exploit me now", 153126, 154649, 0.18f, 1f, new Vector2(138,17),
                Entry: 1, inLetterOffset: 10, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);
            
            //簡単に堕ちてゆく
            GenerateLyrics(TextFront, chorusItalicFont, "簡単に堕ちてゆく", 154954, 157923, 0.5f, -50f, new Vector2(0,-30), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "It's easy to fall underway", 154954, 157923, 0.5f, 8f, new Vector2(0,35), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(TextBehind, smooshFont, "FALL UNDER", 157847, 159446-300, 0.8f, -40f, new Vector2(0,335),
                Entry: 5, inLetterOffset: 0, inTimeOffset: 750, Exit: 99);

            //"自分"が無いのならば
            GenerateLyrics(TextFront, chorusItalicFont, "\"自分\"が無いのならば", 159827, 164472, 0.45f, -50f, new Vector2(-50,-45), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "I'm trying hard to right myself", 159827, 164472, 0.45f, 8f, new Vector2(10,30), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            //----------------------------------------- Chorus 3 --------------------------------------------------//
            
            //重ねたメモリーはフィクション
            posOffset = new Vector2(0,0);
            GenerateLyrics(Text, chorusLargeFont, "重ねた", 194243, 195461, 0.5f, -120f, new Vector2(-40,7),
                Entry: 3, inLetterOffset: 152/2, inTimeOffset: 1000, inMoveOffset: 80f, Exit: 99);
            GenerateLyrics(verseText, chorusFontLarge, "メモリーは", 195461, 196680, 0.3f, -60f, new Vector2(0,0) + posOffset,
                Entry: 3, inLetterOffset: 0, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(TextBehind, smooshFont, "MEMORY", 195461, 196680, 0.8f, -20f, new Vector2(0,120),
                Entry: 5, inLetterOffset: 0, inTimeOffset: 750, Exit: 4);
            GenerateLyrics(GetLayer("EVEN MORE OVER"), chorusFontLarge, "フィクション", 196680, 197289, 0.4f, -120f, new Vector2(0,0) + posOffset,
                Entry: 3, inLetterOffset: 0, inTimeOffset: 750, Exit: 99);
            GenerateLyrics(GetLayer("EVEN MORE OVER"), engCursiveFontLarge, "fiction", 196680, 197289, 0.5f, 25f, new Vector2(30,45),
                Entry: 1, inLetterOffset: 0, inTimeOffset: 750, inMoveOffset: 0, Exit: 99);
            
            //演じてたんだ
            GenerateLyrics(TextFront, chorusItalicFont, "演じてたんだ", 197289, 199421, 0.5f, -50f, new Vector2(-10,-25), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "What an act, it was", 197289, 199421, 0.45f, 5f, new Vector2(0,25), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            //嘘じゃない眩しさが 怖かった
            posOffset = new Vector2(0,-35);
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(-51, 241) + posOffset);
            q.ScaleVec(OsbEasing.OutCubic, 199421, 200791+1000, 80, 20, 246, 20);
            q.Color(199421, 199421+33, Color4.Black, Color4.Black);
            q.Color(199421+33, RedFont);
            q.Color(202923, RedFont);
            GenerateLyrics(TextFront, chorusFont, "嘘じゃない", 199421, 202923, 0.2f, -5f, new Vector2(-240,0) + posOffset,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(34, 314) + posOffset);
            q.ScaleVec(OsbEasing.OutCubic, 200791, 202923, 50, 20, 198, 20);
            q.Color(200791, 200791+33, Color4.Black, Color4.Black);
            q.Color(200791+33, RedFont);
            q.Color(202923, RedFont);
            GenerateLyrics(TextFront, chorusFont, "眩しさが", 200791, 202923, 0.2f, -5f, new Vector2(-180,70) + posOffset,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(TextFront, engFont, "I cannot lie, I'm paralysed before the blinding truth", 199421, 202923, 0.18f, 2f, new Vector2(-210,2),
                Entry: 0, inLetterOffset: 10, inTimeOffset: 0, Exit: 99);

            GenerateLyrics(TextFront, chorusItalicFont, "怖かった", 202923, 204598, 0.3f, 10f, new Vector2(210,20), Vertical:true, Angle: -0.2f,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 99, outTimeOffset: 250, outMoveOffset: 10f);
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreRight, new Vector2(477, 314-35));
            q.ScaleVec(OsbEasing.OutExpo, 202923, 202923+500, 250, 20, 0, 20);
            q.Color(202923, RedFont);
            GenerateLyrics(TextFront, engCursiveFont, "Scared", 202923, 204598, 0.6f, 4f, new Vector2(285,30), Angle: 0.1f - (float)Math.PI/2,
                Entry: 2, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 10f, Exit: 99);

            //騙されてしまうなら
            posOffset = new Vector2(-100,-20);
            GenerateNoiseLyrics(TextFront, chorusFont, "騙されて", 204598, 207035, 0.22f, 10f, new Vector2(10,-10) + posOffset,
                Entry: 0, inLetterOffset: 120, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 4, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "しまうなら", 204598, 207035, 0.22f, 5f, new Vector2(210,90) + posOffset,
                Entry: 0, inLetterOffset: 120, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 4, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateLyrics(TextFront, engFont, "If         I'm         going         to         be         deceived         right         then", 204598, 207035, 0.18f, 2f, new Vector2(0, 24),
                Entry: 1, inLetterOffset: 5, inTimeOffset: 500, inMoveOffset:0, Exit: 99);

            //僕は騙し続けよう
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(TextFront, chorusFont, "僕は騙し", 207035, 209472, 0.22f, 20f, new Vector2(50,5) + posOffset, Vertical: true,
                Entry: 0, inLetterOffset: 152, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 2, outLetterOffset: 250, outMoveOffset: 10f);
            GenerateNoiseLyrics(TextFront, chorusFont, "続けよう", 207873-100, 209472, 0.22f, 15f, new Vector2(-56,-5) + posOffset, Vertical: true,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 3, outLetterOffset: 250, outMoveOffset: 10f);
            GenerateLyrics(TextMoreFront, engFont, "I'll  just  have  to", 207035, 209472, 0.18f, 4f, new Vector2(-200,5),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);
            GenerateLyrics(TextMoreFront, engFont, "keep  up  the  lie  again", 207035+500, 209472, 0.18f, 4f, new Vector2(220,5),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);

            //真実と嘘の間で
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(TextFront, chorusFont, "真", 209472, 210537, 0.5f, 0f, new Vector2(0, 0) + posOffset,
                Entry: 5, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 30f, Exit: 99, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "実", 210537, 211299-100, 0.3f, 0f, new Vector2(0, 0) + posOffset,
                Entry: 5, inLetterOffset: 0, inTimeOffset: 700, inMoveOffset: 30f, Exit: 6, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateNoiseLyrics(TextFront, chorusFont, "と", 211299-100, 211603, 0.25f, -25f, new Vector2(0, 0) + posOffset,
                Entry: 3, inLetterOffset: 0, inTimeOffset: 500, inMoveOffset: 10f, Exit: 99, outLetterOffset: 250, outMoveOffset: 20f);
            GenerateCharacterSeparation("嘘", 211603-100, 212822, 0.45f, 0f, new Vector2(320, 240));
            GenerateLyrics(TextMoreFront, engFont, "Here, right between the line of truth and lies", 212822, 214345, 0.18f, 1f, new Vector2(135,-5),
                Entry: 1, inLetterOffset: 10, inTimeOffset: 750, inMoveOffset: 20f, Exit: 99);
            GenerateNoiseLyrics(TextFront, chorusFont, "嘘の間で", 212822, 214345, 0.4f, 23f, new Vector2(0,60) + posOffset,
                Entry: 1, inLetterOffset: 50, inTimeOffset: 1000, inMoveOffset: 50f, Exit: 99);
            q = TextFront.CreateSprite("sb/q.png", OsbOrigin.CentreLeft, new Vector2(330-200, 176) + posOffset);
            q.ScaleVec(OsbEasing.OutExpo, 212822, 214345, 0, 0.5, 200, 0.5);
            q.Color(212822, Color4.Black);
            q = TextFront.CreateSprite("sb/star.png");
            q.ScaleVec(212822, 0.05, 0.07);
            q.Color(214345, Color4.Black);
            q.Move(212822, 72, 176);
            q = TextFront.CreateSprite("sb/star.png");
            q.ScaleVec(212822, 0.05, 0.07);
            q.Color(214345, Color4.Black);
            q.Move(212822, 72+15, 176);
            q = TextFront.CreateSprite("sb/star.png");
            q.ScaleVec(212822, 0.05, 0.07);
            q.Color(214345, Color4.Black);
            q.Move(212822, 72+30, 176);
            GenerateNoiseLyrics(TextFront, chorusFont, "真実と", 212822, 214345, 0.25f, 5f, new Vector2(150, -65) + posOffset,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 20f, Exit: 99, outLetterOffset: 0, outMoveOffset: 20f);

            //度きりの舞台で
            GenerateLyrics(TextFront, chorusItalicFont, "一度きりの舞台で", 214649, 219142, 0.5f, -60f, new Vector2(-20,-20), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "Taking my chance on this one-time stage", 214649, 219142, 0.4f, 6f, new Vector2(0,30), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            //ホンモノを探して
            GenerateLyrics(TextFront, chorusItalicFont, "ホンモノを探して", 219522, 224167, 0.5f, -60f, new Vector2(-20,-10), Angle: -0.2f,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1000, inMoveOffset: 40f, Exit: 99);
            GenerateLyrics(TextFront, engCursiveFont, "I'll try to search for the real truth", 219522, 224167, 0.4f, 6f, new Vector2(0,30), Angle: -0.2f,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            //演じ続けているんだ
            var EndTime = 229345;
            posOffset = new Vector2(290,-50);
            GenerateLyrics(verseText, chorusFont, "演", 224319, EndTime, 0.4f, -15f, new Vector2(-0, 40) + posOffset,
                Entry: 3, inLetterOffset: 0, inTimeOffset: 1200, Exit: 5, outTimeOffset: 500);
            GenerateLyrics(verseText, chorusFont, "じ", 224624, EndTime, 0.2f, -15f, new Vector2(70, -50) + posOffset,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1200, Exit: 5);
            GenerateLyrics(verseText, chorusFont, "続", 224776, EndTime, 0.4f, -15f, new Vector2(-120, -60) + posOffset,
                Entry: 2, inLetterOffset: 0, inTimeOffset: 1200, Exit: 5);
            GenerateLyrics(verseText, chorusFont, "けて", 225081, EndTime, 0.2f, 30f, new Vector2(-130, 90) + posOffset, Vertical: true,
                Entry: 3, inLetterOffset: 100, inTimeOffset: 1200, Exit: 5);
            GenerateLyrics(verseText, chorusFont, "いるんだ", 225537, EndTime, 0.2f, 30f, new Vector2(-220, 110) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1200, Exit: 5);
            GenerateLyrics(engTextReversed, engFont, "I'll     carry     on     acting     till     the     end", 224319, 233913, 0.22f, 1f, new Vector2(238,-720),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);

            //それぞれ描くフィクションで
            posOffset = new Vector2(30,10);
            GenerateLyrics(verseText, chorusFont, "それぞれ", 229192, 233913, 0.3f, 0f, new Vector2(-230, -160) + posOffset,
                Entry: 1, inLetterOffset: 150, inTimeOffset: 1200, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "描", 229954, 233913, 0.5f, 30f, new Vector2(-320, -10) + posOffset, Vertical: true,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1200, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "く", 229954+152, 233913, 0.4f, 30f, new Vector2(-280, 120) + posOffset, Vertical: true,
                Entry: 3, inLetterOffset: 100, inTimeOffset: 1200, Exit: 99);
            GenerateLyrics(verseText, chorusFontLarge, "フィクション", 230563, 233913, 0.3f, -95f, new Vector2(10, 30) + posOffset,
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1200, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "で", 231172-50, 233913, 0.4f, 30f, new Vector2(260, 50) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1200, Exit: 99);
            GenerateLyrics(verseText, engCursiveFontLarge, "Fiction", 230563, 233913, 0.5f, 22f, new Vector2(35, 90),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(engText, engFont, "to     create     our     own     fiction     here", 229192, 233913, 0.22f, 1f, new Vector2(238,-720),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);



            //----------------------------------------- Verse 1 ---------------------------------------------------//
            
            engText.Origin = new Vector2(320,240);
            engText.RotationDegrees = 90;

            //期待外れのことばかりで
            posOffset = new Vector2(180,-10);
            GenerateLyrics(verseText, chorusFont, "期待外れ", 23837, 27568, 0.1f, 15f, new Vector2(50,-60) + posOffset, Vertical: true,
                Entry: 2, inLetterOffset: 120, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "のことばかりで", 24598, 27568, 0.1f, 10f, new Vector2(-50,40) + posOffset, Vertical: true,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(engText, engFont, "I've known, disappointments follow me everywhere", 23837, 27568, 0.18f, 1.5f, new Vector2(238,-500),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 500, inMoveOffset: 5f, Exit: 99);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.TopCentre);
            q.ScaleVec(OsbEasing.OutExpo, 23837, 27568, 0.6, 0, 0.6, 320);
            q.Move(27568, 320+90+180, 80);
            q.Fade(27568, .5f);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.BottomCentre);
            q.ScaleVec(OsbEasing.OutExpo, 23837, 27568, 0.6, 0, 0.6, 320);
            q.Move(27568, 320-90+180, 400);
            q.Fade(27568, .5f);

            //ドラマのようなラスト求めた
            posOffset = new Vector2(80,-30);
            GenerateLyrics(verseText, chorusFont, "ドラマ", 27568, 31223, 0.3f, 0f, new Vector2(0,0) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(verseText, engCursiveFont, "dramatic", 27568, 31223, 0.45f, 15f, new Vector2(0,25) + posOffset,
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "のようなラスト", 28253, 31223, 0.12f, 5f, new Vector2(170,75) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(verseText, engFont, "All I ever sought was to find one dramatic end", 27568, 31223, 0.18f, 1f, new Vector2(0,80),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 5f, Exit: 99);
            posOffset = new Vector2(0,0);
            GenerateLyrics(verseText, chorusLargeFont, "求めた", 31299, 32746, 0.4f, -80f, new Vector2(-45,5) + posOffset,
                Entry: 2, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 50f, Exit: 99);

            //喜劇か悲劇か 決めるのは誰だ？
            posOffset = new Vector2(-2,0);
            GenerateLyrics(verseText, chorusFont, "喜劇", 33126, 35487, 0.25f, 10f, new Vector2(-100,-50) + posOffset,
                Entry: 3, inLetterOffset: 150, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "か", 33507, 35487, 0.25f, 5f, new Vector2(5,-70) + posOffset,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1500, Exit: 99);
            GenerateLyrics(verseText, engFont, "Comedy", 33888, 35487, 0.25f, 10f, new Vector2(35,-15) + posOffset,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1500, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "きげき", 33126, 35487, 0.07f, 5f, new Vector2(-200,-50) + posOffset, Vertical:true,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 750, inMoveOffset: 5f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "悲劇", 34192, 35487, 0.25f, 10f, new Vector2(100,50) + posOffset,
                Entry: 2, inLetterOffset: 150, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "か", 34725, 35487, 0.25f, 5f, new Vector2(205,35) + posOffset,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(verseText, engFont, "Tragedy", 34268, 35487, 0.25f, 10f, new Vector2(245,85) + posOffset,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 1500, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "ひげき", 34040, 35487, 0.07f, 5f, new Vector2(0,50) + posOffset, Vertical:true,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 750, inMoveOffset: 5f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "【", 33126, 35487, 0.07f, 0f, new Vector2(-204,-20) + posOffset, Angle: - (float)Math.PI/2,
                Entry: 2, inLetterOffset: 0, inTimeOffset: 750, inMoveOffset:5f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "】", 33126, 35487, 0.07f, 0f, new Vector2(-192,-95) + posOffset, Angle: - (float)Math.PI/2,
                Entry: 3, inLetterOffset: 0, inTimeOffset: 750, inMoveOffset:5f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "【", 34040, 35487, 0.07f, 0f, new Vector2(-3,80) + posOffset, Angle: - (float)Math.PI/2,
                Entry: 2, inLetterOffset: 0, inTimeOffset: 750, inMoveOffset:5f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "】", 34040, 35487, 0.07f, 0f, new Vector2(8, 5) + posOffset, Angle: - (float)Math.PI/2,
                Entry: 3, inLetterOffset: 0, inTimeOffset: 750, inMoveOffset:5f, Exit: 99);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.CentreLeft);
            q.ScaleVec(OsbEasing.OutCubic, 33507, 35487, 0, 0.6, 200, 0.6);
            q.Move(33507, 365, 176);
            q.Fade(33507, .5f);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.CentreRight);
            q.ScaleVec(OsbEasing.OutCubic, 34040, 35487, 0, 0.6, 200, 0.6);
            q.Move(34192, 275, 282);
            q.Fade(34192, .5f);
            q = verseText.CreateSprite("sb/f/reg_outline/6c7a.png");
            var fScale = new Vector2(0.4f, 0.6f);
            q.ScaleVec(35487, fScale);
            q.ScaleVec(OsbEasing.OutExpo, 35487+50, 35487+1000, fScale*.45f, fScale*.35f);
            q.Fade(35867, 35867, 1, 0);
            GenerateLyrics(verseText, chorusFont, "決めるのは誰だ？", 35867, 37466, 0.15f, 10f, new Vector2(5,0) + posOffset,
                Entry: 5, inLetterOffset: 0, inTimeOffset: 37466-35867+1000, Exit: 99);
            GenerateLyrics(verseText, engFont, "Who chooses if it's comedy? Who chooses a tragedy?", 35867, 37466, 0.18f, 1f, new Vector2(0,50),
                Entry: 5, inLetterOffset: 0, inTimeOffset: 37466-35867+500, inMoveOffset: 5f, Exit: 99);

            //誰もが纏うポーカーフェイス
            posOffset = new Vector2(0,0);
            GenerateLyrics(verseText, chorusFont, "誰もが纏う", 38076, 41350, 0.1f, 20f, new Vector2(0,0) + posOffset, Vertical:true,
                Entry: 2, inLetterOffset: 304, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(engText, engFont, "Who doesn't hide behind a poker face", 38076, 41350, 0.18f, 1.3f, new Vector2(238,-345),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 500, inMoveOffset: 5f, Exit: 99);
            GenerateLyrics(verseText, chorusFontLarge, "ポーカーフェイス", 41350, 42644, 0.25f, -60f, new Vector2(0,0) + posOffset,
                Entry: 3, inLetterOffset: 0, inTimeOffset: 750, Exit: 99);
            GenerateLyrics(verseText, engCursiveFontLarge, "poker face", 41350, 42644, 0.5f, 25f, new Vector2(30,45),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 750, inMoveOffset: 10f, Exit: 99);

            
            //都合のいい物語で構わない
            GenerateLyrics(verseText, chorusFont, "都合のいい", 45233, 46223, 0.08f, 10f, new Vector2(-262,40) + posOffset,
                Entry: 0, inLetterOffset: 100, inTimeOffset: 750, Exit: 99);
            GenerateLyrics(verseText, engFont, "Whenever it's too good to be my truth", 45233, 46223, 0.18f, 1f, new Vector2(-240,70),
                Entry: 1, inLetterOffset: 12, inTimeOffset: 1000, inMoveOffset: 5f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "物語", 46223, 47517, 0.4f, 40f, new Vector2(0,0) + posOffset, Vertical:true,
                Entry: 3, inLetterOffset: 100, inTimeOffset: 750, Exit: 99);
            GenerateLyrics(verseText, engCursiveFontLarge, "story", 46223, 47517, 0.5f, 10f, new Vector2(80,5) + posOffset, Angle: -0.2f - (float)Math.PI/2,
                Entry: 2, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 10f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "都合のいい物語で構わない", 47517, 52390, 0.08f, 10f, new Vector2(180,40) + posOffset,
                Entry: 3, inLetterOffset: 0, inTimeOffset: 2000, Exit: 99);
            GenerateLyrics(verseText, engFont, "Whenever it's too good to be my truth. I'm just not bothered", 47517, 52390, 0.18f, 1f, new Vector2(148,70),
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 5f, Exit: 99);

            //嘘だらけの世界の中で
            GenerateLyrics(verseText, chorusItalicFont, "嘘だらけの", 52695, 55664, 0.2f, -15f, new Vector2(-16,0) + posOffset,
                Entry: 12, inLetterOffset: 100, inTimeOffset: 1200, Exit: 5);
            GenerateLyrics(verseText, chorusItalicFont, "世界の中で", 55664, 57873, 0.4f, -20f, new Vector2(70,40) + posOffset, Vertical:true,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 2000, Exit: 6, outTimeOffset: 750);
            GenerateLyrics(verseText, engFont, "Because our world deludes,", 55817, 58482, 0.18f, 1f, new Vector2(-120,65),
                Entry: 1, inLetterOffset: 10, inTimeOffset: 1000, inMoveOffset: 5f, Exit: 99);
            GenerateLyrics(verseText, engFont, "its lies are playing truth", 55817, 58482, 0.18f, 1f, new Vector2(-126,80),
                Entry: 1, inLetterOffset: 10, inTimeOffset: 1000, inMoveOffset: 5f, Exit: 99);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.TopCentre);
            q.ScaleVec(OsbEasing.OutExpo, 55817, 58482, 0.6, 0, 0.6, 26);
            q.Move(55817, 123, 297);
            q.Fade(55817, 0.5f);

            //----------------------------------------- Verse 2 ---------------------------------------------------//
            
            //有象無象のような
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(verseText, chorusFont, "有象", 98685, 99675, 0.3f, 80f, new Vector2(-100,0) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1000, inMoveOffset: 60f, Exit: 99);
            GenerateNoiseLyrics(verseText, chorusFont, "無象", 98685, 99675, 0.3f, 80f, new Vector2(100,0) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 0, inTimeOffset: 1000,inMoveOffset: 60f, Exit: 99);
            GenerateNoiseLyrics(verseText, chorusFont, "のような", 98685+100, 99675, 0.1f, 20f, new Vector2(0,0) + posOffset, Vertical: true,
                Entry: 3, inLetterOffset: 50, inTimeOffset: 1000,inMoveOffset: 10f, Exit: 99);
           
            //枯れてゆくもの
            GenerateNoiseLyrics(verseText, chorusFont, "枯れてゆくもの", 99675, 100893, 0.2f, 10f, new Vector2(0,0) + posOffset,
                Entry: 3, inLetterOffset: 50, inTimeOffset: 750,inMoveOffset: 20f, Exit: 3, outLetterOffset: 100);
            q = verseText.CreateSprite("sb/f/reg_outline/67af.png");
            fScale = new Vector2(0.4f, 0.6f);
            q.ScaleVec(99675, fScale*1.5f);
            q.ScaleVec(OsbEasing.OutExpo, 99675+50, 100893+1000, fScale * .85f, fScale*.7f);
            q.Fade(100893, 100893, 1, 0);
            
            //そうなりたくはないから
            GenerateNoiseLyrics(verseText, chorusFont, "そうなりたくはないから", 100893, 103329, 0.4f, -40f, new Vector2(0,0) + posOffset,
                Entry: 1, inLetterOffset: 50, inTimeOffset: 1000,inMoveOffset: 30f, Exit: 3, outLetterOffset: 0);

            //ズルしたっていい
            GenerateNoiseLyrics(verseText, chorusFont, "ズルしたっていい", 103329, 104548, 0.35f, -20f, new Vector2(0,0) + posOffset,
                Entry: 13, inLetterOffset: 0, inTimeOffset: 1200,inMoveOffset: 10f, Exit: 99, outLetterOffset: 0);

            //信じられるなら
            GenerateNoiseLyrics(verseText, chorusFont, "信じ", 104548, 105766, 0.5f, 0f, new Vector2(-150,20) + posOffset, Vertical: true,
                Entry: 3, inLetterOffset: 50, inTimeOffset: 750,inMoveOffset: 20f, Exit: 3, outLetterOffset: 400);
            GenerateNoiseLyrics(verseText, chorusFont, "られるなら", 104548+100, 105766, 0.5f, -50f, new Vector2(100,-10) + posOffset, Vertical: true,
                Entry: 2, inLetterOffset: 50, inTimeOffset: 750,inMoveOffset: 20f, Exit: 2, outLetterOffset: 150);
            posOffset = new Vector2(30,0);

            //ホンモノを描け
            GenerateNoiseLyrics(verseText, chorusFont, "ホンモノを", 105766, 106984, 0.4f, -40f, new Vector2(-130,-40) + posOffset,
                Entry: 2, inLetterOffset: 0, inTimeOffset: 500,inMoveOffset: 20f, Exit: 3, outLetterOffset: 400);
            GenerateNoiseLyrics(verseText, chorusFont, "描け", 106527, 106984, 0.5f, -20f, new Vector2(150,0) + posOffset,
                Entry: 1, inLetterOffset: 50, inTimeOffset: 1000,inMoveOffset: 20f, Exit: 999, outLetterOffset: 0);

            q = verseText.CreateSprite("sb/f/reg_outline/63cf.png");
            q.StartLoopGroup(106527-150, 2);
            q.Fade(0,50,0,1);
            q.EndGroup();
            q.ScaleVec(OsbEasing.OutCubic, 106527-152, 106527, 0.3f * 1.1f * 1.8f, 0.4f * 1.1f * 1.8f, 0.3f * 0.9f * 1.8f, 0.4f * 0.9f  * 1.8f);
            q.Move(OsbEasing.InExpo, 106527-152, 106527, 320-110, 240, 320-100, 240);
            
            //一度の人生(ストーリー)
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(verseText, chorusFont, "一度の人生", 106984, 108126, 0.5f, 0f, new Vector2(0,0) + posOffset,
                Entry: 0, inLetterOffset: 50, inTimeOffset: 750,inMoveOffset: 20f, Exit: 3, outLetterOffset: 50);
            GenerateNoiseLyrics(verseText, chorusFont, "ストーリー", 106984+150, 108126, 0.1f, 15f, new Vector2(200,80) + posOffset,
                Entry: 3, inLetterOffset: 25, inTimeOffset: 750,inMoveOffset: 10f, Exit: 3, outLetterOffset: 50);
            GenerateNoiseLyrics(verseText, chorusFont, "【】", 106984+100, 108126, 0.1f, 230f, new Vector2(200,80) + posOffset,
                Entry: 3, inLetterOffset: 150, inTimeOffset: 750,inMoveOffset: 10f, Exit: 3, outLetterOffset: 50);

            //English Translation
            GenerateLyrics(engText, engFont, "Just like the common masses", 98685, 99675, 0.18f, 1f, new Vector2(142,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engText, engFont, "All easily forgotten", 99675, 100893, 0.18f, 1f, new Vector2(120,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engText, engFont, "I don't want to be a blind follower", 100893, 103329, 0.18f, 1f, new Vector2(158,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engText, engFont, "It's fine to deceive", 103329, 104548, 0.18f, 1f, new Vector2(118,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engText, engFont, "If you can believe", 104548, 105766, 0.18f, 1f, new Vector2(116,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engText, engFont, "Just paint me a once-in-a-lifetime story", 105766, 107974, 0.18f, 1f, new Vector2(170,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            
            GenerateLyrics(engTextReversed, engFont, "Just like the common masses", 98685, 99675, 0.18f, 1f, new Vector2(142,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engTextReversed, engFont, "All easily forgotten", 99675, 100893, 0.18f, 1f, new Vector2(120,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engTextReversed, engFont, "I don't want to be a blind follower", 100893, 103329, 0.18f, 1f, new Vector2(158,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engTextReversed, engFont, "It's fine to deceive", 103329, 104548, 0.18f, 1f, new Vector2(118,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engTextReversed, engFont, "If you can believe", 104548, 105766, 0.18f, 1f, new Vector2(116,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engTextReversed, engFont, "Just paint me a once-in-a-lifetime story", 105766, 107974, 0.18f, 1f, new Vector2(170,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            engTextReversed.Origin = new Vector2(320,240);
            engTextReversed.Position = new Vector2(640,480);
            engTextReversed.RotationDegrees = -90;
            
            //現実は残酷か 決めるのは誰だ？
            posOffset = new Vector2(-60,0);
            GenerateLyrics(verseText, chorusFont, "現実", 108583, 112999, 0.3f, 30f, new Vector2(-230,-80) + posOffset,
                Entry: 3, inLetterOffset: 100, inTimeOffset: 2000, inMoveOffset: 30f, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "はか", 109345, 112999, 0.18f, -15f, new Vector2(-220, 40) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(verseText, chorusItalicFont, "残酷", 109649, 112999, 0.3f, 25f, new Vector2(-110, 90) + posOffset,
                Entry: 2, inLetterOffset: 150, inTimeOffset: 1200, Exit: 99);

            posOffset = new Vector2(0,-20);
            GenerateLyrics(verseText, chorusItalicFont, "決", 111020, 112999, 0.3f, -15f, new Vector2(100, -30) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "めるのはだ", 111020+100, 112999, 0.18f, 0f, new Vector2(280,-60) + posOffset,
                Entry: 0, inLetterOffset: 0, inTimeOffset: 1000, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "誰", 112314, 112999, 0.3f, -15f, new Vector2(280,70) + posOffset,
                Entry: 11, inLetterOffset: 100, inTimeOffset: 1000, Exit: 99);

            GenerateLyrics(engText, engFont, "Who       chooses       our       destiny?", 111020, 112999, 0.18f, 1f, new Vector2(238,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(engTextReversed, engFont, "Who       chooses       our       reality?", 108735, 112999, 0.18f, 1f, new Vector2(238,-730),
                Entry: 1, inLetterOffset: 20, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);

            posOffset = new Vector2(0,0);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.TopCentre, new Vector2(326, 134));
            q.ScaleVec(OsbEasing.OutExpo, 113456, 117416-200, 18, 50, 18, 220);
            q.Color(113456, 113456+33, Color4.Black, Color4.Black);
            q.Color(113456+33, RedFont);

            q = verseText.CreateSprite("sb/q.png", OsbOrigin.BottomCentre, new Vector2(326, 134+220));
            q.ScaleVec(OsbEasing.InExpo, 117416-200, 117416, 18, 220, 18, 0);
            q.Color(117416-200, RedFont);

            //答はわからない？
            GenerateLyrics(verseText, chorusFont, "答はわからない？", 113609, 117416, 0.11f, 6f, new Vector2(0,0) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1200, Exit: 99);
            GenerateNoiseLyrics(verseText, chorusFont, "あーあ", 117340, 118177, 0.35f, 0f, new Vector2(0,0) + posOffset,
                Entry: 12, inLetterOffset: 0, inTimeOffset: 1000, Exit: 999);
            GenerateLyrics(engText, engFont, "Who doesn't know the answer to it all?", 113609, 117416, 0.18f, 1f, new Vector2(240,-345),
                Entry: 1, inLetterOffset: 13, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);

            //思考停止と言う毒に侵されて
            posOffset = new Vector2(-165,-50);
            GenerateLyrics(verseText, chorusFont, "思考停止と言う", 118482, 123050, 0.06f, 10f, new Vector2(0,0) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1200, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "毒に侵されて", 120994, 123050, 0.06f, 10f, new Vector2(-65,82) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1200, Exit: 99);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.TopCentre);
            q.ScaleVec(OsbEasing.OutExpo, 118177, 118177+4000, 0.6, 0, 0.6, 250);
            q.Move(123050, 320+70-200, 100);
            q.Fade(123050, .5f);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.BottomCentre);
            q.ScaleVec(OsbEasing.OutExpo, 118177, 118177+4000, 0.6, 0, 0.6, 250);
            q.Move(123050, 320-70-200, 350);
            q.Fade(123050, .5f);
            GenerateLyrics(verseText, engFont, "Can't make a thought or decision", 118482, 123050, 0.18f, 1f, new Vector2(-200,140),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(verseText, engFont, "A poison that afflicts the mind", 120994, 123050, 0.18f, 1f, new Vector2(-200,150),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);

            //自分を委ね続けて 
            posOffset = new Vector2(230,-49);
            GenerateLyrics(verseText, chorusFont, "自分を委ね続けて", 123050, 127923, 0.06f, 10f, new Vector2(0,0) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1200, Exit: 99);
            //流されていく
            GenerateLyrics(verseText, chorusFont, "流されていく", 125944, 127923, 0.06f, 10f, new Vector2(-65,93) + posOffset, Vertical: true,
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1200, Exit: 99);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.TopCentre);
            q.ScaleVec(OsbEasing.InExpo, 127923-1000, 127923, 0.6, 250, 0.6, 0);
            q.Move(123050, 320+70+200, 100);
            q.Fade(123050, .5f);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.BottomCentre);
            q.ScaleVec(OsbEasing.InExpo, 127923-1000, 127923, 0.6, 250, 0.6, 0);
            q.Move(123050, 320-70+200, 350);
            q.Fade(123050, .5f);
            GenerateLyrics(verseText, engFont, "I feel myself subjugated within", 123050, 127923, 0.18f, 1f, new Vector2(200,140),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(verseText, engFont, "Sweeping my will aside", 125944, 127923, 0.18f, 1f, new Vector2(200,150),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);

            //そんな自分じゃニセモノのようだ
            posOffset = new Vector2(0,0);
            GenerateNoiseLyrics(verseText, chorusFont, "そんな自分じゃ", 128380, 131121, 0.35f, 0f, new Vector2(0,5) + posOffset,
                Entry: 10, inLetterOffset: 50, inTimeOffset: 1500, inMoveOffset: 30f, Exit: 99);
            GenerateLyrics(verseText, engFont, "Because                  of                  all                  my                  flaws", 128380, 134472, 0.18f, 1f, new Vector2(0,-150),
                Entry: 3, inLetterOffset: 0, inTimeOffset: 2000, inMoveOffset: 3f, Exit: 99);
            GenerateNoiseLyrics(verseText, chorusFont, "ニセモノのようだ", 131121, 134624, 0.4f, -40f, new Vector2(0,5) + posOffset,
                Entry: 11, inLetterOffset: 0, inTimeOffset: 750, inMoveOffset: 40f, Exit: 6);
            GenerateLyrics(verseText, engFont, "I                  kept                  lying                  to                  play                  the                  truth", 131121, 134472, 0.18f, 1f, new Vector2(0,160),
                Entry: 2, inLetterOffset: 0, inTimeOffset: 2000, inMoveOffset: 3f, Exit: 99);

            
                
            //----------------------------------------- Ending ----------------------------------------------------//

            //この世界の中でずっと
            //負けないよう 手探りの僕は
            posOffset = new Vector2(-1,-20);
            GenerateLyrics(verseText, chorusFont, "この世界の中でずっと", 234065, 243659, 0.06f, 10f, new Vector2(40,0) + posOffset, Vertical: true,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1500, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "負けないよう", 238939, 243659, 0.06f, 10f, new Vector2(0,-43) + posOffset, Vertical: true,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1500, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "手探りの僕は", 240766, 243659, 0.06f, 10f, new Vector2(-40,-40) + posOffset, Vertical: true,
                Entry: 2, inLetterOffset: 100, inTimeOffset: 1500, Exit: 99);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.TopCentre);
            q.ScaleVec(233913, 0.6, 240);
            q.Move(243659, 320+70, 120-20);
            q.Fade(243659, .5f);
            q = verseText.CreateSprite("sb/q.png", OsbOrigin.TopCentre);
            q.ScaleVec(233913, 0.6, 240);
            q.Move(243659, 320-70, 120-20);
            q.Fade(243659, .5f);
            posOffset = new Vector2(0,0);
            GenerateLyrics(verseText, engFont, "Forever in this world, as if though time preserved", 234065, 243659, 0.18f, 1f, new Vector2(0,130),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            GenerateLyrics(verseText, engFont, "So not to lose myself, I hesitantly search around", 238939, 243659, 0.18f, 1f, new Vector2(0,145),
                Entry: 1, inLetterOffset: 30, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);

            //たとえ真実が嘘となっても
            GenerateLyrics(verseText, chorusFont, "たとえ真実が", 243812, 248989, 0.4f, 10f, new Vector2(-80,-90) + posOffset, 
                Entry: 0, inLetterOffset: 100, inMoveOffset: 50f, inTimeOffset: 1200, Exit: 7, outTimeOffset:500, outMoveOffset: 50f, outLetterOffset: 50);
            GenerateLyrics(verseText, chorusFont, "嘘となっても", 246248, 248989, 0.4f, -30f, new Vector2(160,110) + posOffset, 
                Entry: 1, inLetterOffset: 100, inTimeOffset: 1200, inMoveOffset: 20f, Exit: 8, outTimeOffset:500, outMoveOffset: 50f, outLetterOffset: 50);
            GenerateLyrics(verseText, engFont, "Even            if            the            truth            were            told", 243812, 248685, 0.18f, 1f, new Vector2(0,-160),
                Entry: 3, inLetterOffset: 0, inTimeOffset: 2000, inMoveOffset: 3f, Exit: 99);
            GenerateLyrics(verseText, engFont, "Revealed            to            be            a            lie", 246248, 248685, 0.18f, 1f, new Vector2(0,168),
                Entry: 2, inLetterOffset: 0, inTimeOffset: 2000, inMoveOffset: 3f, Exit: 99);

            //生きていくために 騙されよう
            GenerateLyrics(verseText, chorusFont, "生きていくために", 248685, 251654, 0.5f, -70f, new Vector2(25,0) + posOffset, 
                Entry: 0, inLetterOffset: 100, inMoveOffset: 30f, inTimeOffset: 1500, Exit: 99);
            GenerateLyrics(verseText, chorusFont, "騙されよう", 251654, 253253, 0.08f, 5f, new Vector2(-1.2f,0) + posOffset, 
                Entry: 5, inLetterOffset: 0, inMoveOffset: 50f, inTimeOffset: 3000, Exit: 99);
            GenerateLyrics(verseText, engFont, "I   must   continue   to   deceive,   as   my   way   to   survive", 248685, 251654, 0.18f, 4f, new Vector2(0,60),
                Entry: 2, inLetterOffset: 12, inTimeOffset: 1000, inMoveOffset: 2f, Exit: 99);
            
            //----------------------------------------- Credits ----------------------------------------------------//

            posOffset = new Vector2(-280,10);
            var engOffset = 40;
            var japOffset = 50;
            GenerateLyrics(creditLayer, engFont, "Music / Lyrics", 272898, 282644, 0.3f, 1f, new Vector2(0,0) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Illustrator", 272898 + 100, 282644, 0.3f, 1f, new Vector2(-14,20) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Vocal", 272898 + 200, 282644, 0.3f, 1f, new Vector2(-32,40) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Piano", 272898 + 300, 282644, 0.3f, 1f, new Vector2(-32,60) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Bass", 272898 + 400, 282644, 0.3f, 1f, new Vector2(-37,80) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Mixing Engineer", 272898 + 500, 282644, 0.3f, 1f, new Vector2(9,100) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);

            posOffset = new Vector2(-90,10);
            GenerateLyrics(creditLayer, chorusFont, "みゅー", 272898 + 400, 282644, 0.07f, 1f, new Vector2(-31,-5) + posOffset,
                Entry: 3, inLetterOffset: japOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, chorusFont, "おにねこ", 272898 + 500, 282644, 0.07f, 1f, new Vector2(-22,20-5) + posOffset,
                Entry: 3, inLetterOffset: japOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, chorusFont, "棗いつき", 272898 + 600, 282644, 0.07f, 1f, new Vector2(-22,40-5) + posOffset,
                Entry: 3, inLetterOffset: japOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Isshiki", 272898 + 700, 282644, 0.3f, 1f, new Vector2(-33,60) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, chorusFont, "土井達也", 272898 + 800, 282644, 0.07f, 1f, new Vector2(-22,80-5) + posOffset,
                Entry: 3, inLetterOffset: japOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, chorusFont, "内田恭祐", 272898 + 900, 282644, 0.07f, 1f, new Vector2(-22,100-5) + posOffset,
                Entry: 3, inLetterOffset: japOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);

            posOffset = new Vector2(80,10);
            GenerateLyrics(creditLayer, engFont, "Graphic Design", 272898 + 1000 + 800, 282644, 0.3f, 1f, new Vector2(0,0) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Storyboard", 272898 + 1100 + 800, 282644, 0.3f, 1f, new Vector2(-16,20) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Video", 272898 + 1200 + 800, 282644, 0.3f, 1f, new Vector2(-36,40) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Design Coordinator", 272898 + 1300 + 800, 282644, 0.3f, 1f, new Vector2(16,60) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Mappers", 272898 + 1400 + 800, 282644, 0.3f, 1f, new Vector2(-27,80) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Lyrics Translators", 272898 + 1500 + 800, 282644, 0.3f, 1f, new Vector2(10,100) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);

            posOffset = new Vector2(240,10);
            GenerateLyrics(creditLayer, engFont, "Panthullu", 272898 + 1400 + 800, 282644, 0.3f, 1f, new Vector2(0,0) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "LowGraphics", 272898 + 1500 + 800, 282644, 0.3f, 1f, new Vector2(13,20) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Iyouka", 272898 + 1600 + 800, 282644, 0.3f, 1f, new Vector2(-10,40) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "Sakura006", 272898 + 1700 + 800, 282644, 0.3f, 1f, new Vector2(4,60) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);
            GenerateLyrics(creditLayer, engFont, "mangomizer, Sakura006 & Kai", 272898 + 1900 + 800, 282644, 0.3f, 1f, new Vector2(79,100) + posOffset,
                Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f);

            creditLayer.Scale = .7f;
            creditLayer.Position = new Vector2(105,90);

            if (Beatmap.Name.Equals("Facade")) {
            GenerateLyrics(diffSpecificLayer, engFont, "Garden & Ryuusei Aika", 272898 + 1800 + 800, 282644, 0.3f, 1f, new Vector2(53,80) + posOffset,
            Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f); }
            else if (Beatmap.Name.Equals("Hard") | Beatmap.Name.Equals("Normal")) {
            GenerateLyrics(diffSpecificLayer, engFont, "Garden", 272898 + 1800 + 800, 282644, 0.3f, 1f, new Vector2(-9,80) + posOffset,
            Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f); }
            else if (Beatmap.Name.Equals("Trio Collab Extreme")) {
            GenerateLyrics(diffSpecificLayer, engFont, "iBell, Amateurre & captin1", 272898 + 1800 + 800, 282644, 0.3f, 1f, new Vector2(69,80) + posOffset,
            Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f); }
            else if (Beatmap.Name.Equals("Trio Collab Expert")) {
            GenerateLyrics(diffSpecificLayer, engFont, "KoldNoodl, Reioli & Laurier", 272898 + 1800 + 800, 282644, 0.3f, 1f, new Vector2(73,80) + posOffset,
            Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f); }
            else if (Beatmap.Name.Equals("Wispy's Insane")) {
            GenerateLyrics(diffSpecificLayer, engFont, "Wispy", 272898 + 1800 + 800, 282644, 0.3f, 1f, new Vector2(-13,80) + posOffset,
            Entry: 3, inLetterOffset: engOffset, inTimeOffset: 1500, Exit: 99, inMoveOffset: 5f); }

            diffSpecificLayer.Scale = .7f;
            diffSpecificLayer.Position = new Vector2(105,90);
            
        }

        public void GenerateLyrics(StoryboardLayer layer, FontGenerator font, string lyrics, int StartTime, int EndTime, float FontScale, float Kerning, Vector2 PositionOffset,
            float Angle = 0f, bool Vertical = false, bool Box = false, bool AfterImage = false,
            int Entry = 0, float inTimeOffset = 500f, float inLetterOffset = 0, float inMoveOffset = 20f,
            int Exit = 0, float outTimeOffset = 500f, float outLetterOffset = 0, float outMoveOffset = 20f)
        {
            
            float lineWidth = 0f, lineHeight = 0f, verticalHeight = 0f, verticalWidth = 0f, textureOffset = 0f;
            int y = 0, totalY = 0;

            //Calculate sprite sizes and *attempts* to centre them in the screen
            foreach (var letter in lyrics)
            {
                var texture = font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * FontScale + Kerning;
                lineHeight = Math.Max(lineHeight, texture.Height * FontScale);
                verticalHeight += texture.Height * FontScale + Kerning;
                verticalWidth = Math.Max(verticalWidth, texture.Width * FontScale);
                textureOffset = Math.Max(textureOffset, texture.OffsetFor(OsbOrigin.Centre).Y * FontScale);
                totalY++;
            }
            lineWidth -= Kerning;
            verticalHeight -= Kerning;
            float width = Vertical? verticalWidth : lineWidth;
            float height = Vertical? verticalHeight: Math.Abs(lineHeight) + lineWidth * (float)Math.Sin(Angle);
            float letterX = 320 - width * 0.5f;
            float letterY = 240 - (height + textureOffset) * 0.5f;

            foreach (var letter in lyrics)
            {
                var fontScale = IsKanji(letter) ? FontScale * 1f : FontScale * 0.75f;                
                var letterScale = new Vector2(fontScale, fontScale);
                if (font == chorusItalicFont) letterScale = new Vector2(fontScale, fontScale*1.2f);
                else if (font == engCursiveFont) letterScale = new Vector2(fontScale*1.8f, fontScale);

                switch (lyrics) //Customised Scaling
                {
                    case "棗いつき":
                    case "土井達也":
                    case "内田恭祐":
                        fontScale = FontScale * 0.75f;
                        letterScale = new Vector2(fontScale, fontScale);
                        break;
                    case "重ねた":
                        fontScale = FontScale;
                        letterScale = new Vector2(fontScale*0.8f, fontScale*1.5f);
                        break;
                    case "見せかけ":
                        fontScale = FontScale;
                        letterScale = new Vector2(fontScale*0.7f, fontScale*1.5f);
                        break;
                    case "重ねたメモリーはフィクション":
                    case "見せかけばかりのフィクション":
                        fontScale = FontScale;
                        letterScale = new Vector2(fontScale, fontScale);
                        break;
                    case "怯えて":
                        letterScale = IsKanji(letter) ? new Vector2(fontScale * 1.4f, fontScale) : new Vector2(fontScale*1.6f, fontScale);
                        break;
                    case "嘘じゃない":
                    case "たとえ嘘で塗り":
                    case "眩しさに":
                    case "眩しさが":
                    case "固めたって":
                    case "ニセモノに":
                    case "壊さないで":
                        fontScale = IsKanji(letter) ? FontScale * 1f : FontScale * 0.5f;
                        letterScale = IsKanji(letter) ? new Vector2(fontScale * 1.2f, fontScale) : new Vector2(fontScale*2f, fontScale);
                        break;
                    case "ドラマ":
                    case "現実":
                    case "残酷":
                    case "決":
                    case "誰":
                        letterScale = new Vector2(fontScale, fontScale*2f);
                        break;
                    case "dramatic":
                    case "poker face":
                    case "story":
                    case "Fiction":
                    case "fiction":
                    case "memory":
                        letterScale = new Vector2(fontScale*2.5f, fontScale);
                        break;
                    case "求めた":
                        fontScale = FontScale;
                        letterScale = new Vector2(fontScale, fontScale*1.4f);
                        break;
                    case "喜劇":
                    case "悲劇":
                    case "はか":
                    case "演":
                    case "続":
                    case "描":
                    case "めるのはだ":
                        letterScale = new Vector2(fontScale, fontScale*1.4f);
                        break;
                    case "Comedy":
                    case "Tragedy":
                    case "それぞれ":
                        letterScale = new Vector2(fontScale*1.5f, fontScale);
                        break;
                    case "決めるのは誰だ？":
                    case "都合のいい":
                    case "都合のいい物語で構わない":
                    case "じ":
                    case "けて":
                    case "いるんだ":
                    case "く":
                    case "で":
                    case "騙されよう":
                        letterScale = new Vector2(fontScale, fontScale * 1.3f);
                        break;
                    case "ポーカーフェイス":
                        letterScale = new Vector2(fontScale, fontScale*3f);
                        break;
                    case "嘘だらけの":
                        letterScale = new Vector2(fontScale*1.3f, fontScale);
                        break;
                    case "世界の中で":
                        fontScale = IsKanji(letter) ? FontScale * 1f : FontScale * 0.9f;
                        if (y==4) fontScale = IsKanji(letter) ? FontScale * 1f : FontScale * 0.7f;
                        letterScale = new Vector2(fontScale*1.4f, fontScale);
                        break;
                    case "答はわからない？":
                        if (y==0) letterScale = new Vector2(fontScale*1.4f, fontScale);
                        break;
                    case "思考停止と言う":
                        if (y==2) letterScale = new Vector2(fontScale*2f, fontScale);
                        break;
                    case "毒に侵されて":
                    case "流されていく":
                        if (y==0) letterScale = new Vector2(fontScale*2f, fontScale);
                        break;
                    case "自分を委ね続けて":
                        if (y==3) letterScale = new Vector2(fontScale*2f, fontScale);
                        break;
                    case "フィクション":
                    case "メモリーは":
                        letterScale = new Vector2(fontScale, fontScale*2f);
                        break;
                    case "たとえ真実が":
                        if (y==3 | y==4) letterScale = new Vector2(fontScale*1.6f, fontScale*1.1f);
                        else letterScale = new Vector2(fontScale*2f*0.5f, fontScale*0.5f);
                        break;
                    case "嘘となっても":
                        if (y == 0) letterScale = new Vector2(fontScale*1.6f, fontScale*1.1f);
                        else letterScale = new Vector2(fontScale*2f*0.5f, fontScale*0.5f);
                        break;
                    case "生きていくために":
                        if (y == 0) letterScale = new Vector2(fontScale*2f*0.7f, fontScale*0.7f);
                        else letterScale = new Vector2(fontScale*2f*0.5f, fontScale*0.5f);
                        break;   
                }

                var texture = font.GetTexture(letter.ToString());

                if (!texture.IsEmpty)
                {
                    var startTime = StartTime;
                    var endTime = EndTime;

                    var position = new Vector2(letterX, letterY)
                        + texture.OffsetFor(OsbOrigin.Centre) * FontScale + PositionOffset;

                    var sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    var letterOffset = y*inLetterOffset;

                    switch (lyrics) //Customised Effects
                    {
                        case "重ねたメモリーはフィクション":
                        case "見せかけばかりのフィクション":
                        case "メモリーは":
                            Entry = y%2==0 ? 3 : 2;
                            break;
                        case "重ねた":
                        case "見せかけ":
                            Entry = y%2==0 ? 3 : 2;
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            sprite.StartLoopGroup(endTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                        case "MEMORY":
                        case "DELUSION":
                            sprite.Color(startTime, RedFont);
                            Exit = 4;
                            outLetterOffset = 300;
                            outMoveOffset = 50f;
                            break;
                        case "FICTION":
                            Entry = 6;
                            sprite.Color(startTime, RedFont);
                            outLetterOffset = 500;
                            sprite.StartLoopGroup(endTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                        case "演じてたんだ":
                            Entry = y%2==0 ? 3 : 2;
                            if (y==1) {
                                position += new Vector2(5,40);}
                            else if (y==2) {
                                position += new Vector2(-5,10);}
                            else if (y==3) {
                                position += new Vector2(-5,30);}
                            else if (y==4) {
                                position += new Vector2(-2,14);}
                            else if (y==5) {
                                position += new Vector2(-5,30);}
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, RedFont, RedFont);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "Afraid":
                        case "Scared":
                        case "Just an act, it was":
                        case "I've been acting all along":
                        case "I was lost and trapped in a fantasy":
                            sprite.Color(startTime + letterOffset, RedFont);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "All an act, it was":
                            if (y==0) position += new Vector2(-8,0);
                            sprite.Color(startTime + letterOffset, RedFont);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "What an act, it was":
                            if (y==0) position += new Vector2(-13,0);
                            sprite.Color(startTime + letterOffset, RedFont);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "怯えて":
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            Entry = y%2==0 ? 1 : 0;
                            Exit = y%2==0 ? 1 : 0;
                            sprite.Color(EndTime - 50, EndTime, RedFont, RedFont);
                            sprite.StartLoopGroup(endTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                        case "怖かった":
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            Entry = y%2==0 ? 1 : 0;
                            Exit = y%2==0 ? 1 : 0;
                            sprite.Color(EndTime - 50, EndTime, RedFont, RedFont);
                            sprite.StartLoopGroup(endTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            if (y==1) position += new Vector2(0,-10);
                            else if (y==2) position += new Vector2(0,-30);

                            break;
                        case "嘘じゃない":
                            Entry = Random(7, 11);
                            if (!IsKanji(letter)) position += new Vector2(0,5);
                            sprite.Color(StartTime, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            sprite.StartLoopGroup(StartTime  + letterOffset, 2);
                            sprite.Color(0, 50, RedFont, Color4.White);
                            sprite.EndGroup();
                            break;
                        case "たとえ嘘で塗り":
                            Entry = Random(7, 11);
                            if (!IsKanji(letter)) position += new Vector2(0,2);
                            sprite.Color(StartTime, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            sprite.StartLoopGroup(StartTime  + letterOffset, 2);
                            sprite.Color(0, 50, RedFont, Color4.White);
                            sprite.EndGroup();
                            if (y == 4) position += new Vector2(5,0);
                            else if (y == 5) position += new Vector2(10,0);
                            else if (y == 6) position += new Vector2(10,-2);
                            break;
                        case "固めたって":
                            Entry = Random(7, 11);
                            if (!IsKanji(letter)) position += new Vector2(0,5);
                            sprite.Color(StartTime, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            letterOffset = 0;
                            if (y == 3) position += new Vector2(0,-5);
                            if (y == 1) letterOffset += 143380 - StartTime;
                            else if (y == 2) letterOffset += 143532 - StartTime;
                            else if (y == 3) letterOffset += 143837 - StartTime;
                            else if (y == 4) letterOffset += 144142 - StartTime;
                            sprite.StartLoopGroup(StartTime  + letterOffset, 2);
                            sprite.Color(0, 50, RedFont, Color4.White);
                            sprite.EndGroup();

                            break;
                        case "眩しさに":
                            Entry = Random(7, 11);
                            inTimeOffset = 1000;
                            if (!IsKanji(letter)) position += new Vector2(0,5);
                            sprite.Color(StartTime, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            if (y == 1) letterOffset += 66248 - StartTime;
                            else if (y == 2) letterOffset += 66553 - StartTime;
                            else if (y == 3) letterOffset += 66857 - StartTime;
                            sprite.StartLoopGroup(StartTime  + letterOffset, 2);
                            sprite.Color(0, 50, RedFont, Color4.White);
                            sprite.EndGroup();
                            break;
                        case "眩しさが":
                            Entry = Random(7, 11);
                            inTimeOffset = 1000;
                            if (!IsKanji(letter)) position += new Vector2(0,5);
                            sprite.Color(StartTime, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            if (y == 1) letterOffset += 201553 - StartTime;
                            else if (y == 2) letterOffset += 201857 - StartTime;
                            else if (y == 3) letterOffset += 202162 - StartTime;
                            sprite.StartLoopGroup(StartTime  + letterOffset, 2);
                            sprite.Color(0, 50, RedFont, Color4.White);
                            sprite.EndGroup();
                            break;
                    
                        case "僕を壊さないで":
                            sprite.Color(77593, 77593, Color4.White, Color4.Black);
                            if (y > 2) letterOffset += 77517 - StartTime - 100;
                            break;
                        case "Wrapped up in falseness":
                            sprite.Color(startTime + letterOffset, RedFont);
                            sprite.Color(EndTime - 50, EndTime, Color4.White, Color4.White);
                            sprite.Fade(EndTime - 100, EndTime - 50, 0, 1);
                            break;
                        case "My heart is empty":
                            sprite.Color(startTime + letterOffset, Color4.White);
                            if (y >= 3 && y <= 7)
                            {
                                sprite.StartLoopGroup(72492, 4);
                                sprite.Color(0, 25, Color4.Black, Color4.Black);
                                sprite.Color(25, 50, RedFont, RedFont);
                                sprite.EndGroup();
                            }
                            sprite.Color(EndTime - 50, EndTime, Color4.White, Color4.White);
                            sprite.Fade(EndTime - 100, EndTime - 50, 0, 1);
                            break;
                        case "Ah, I can't take much more - don't break me now":
                        case "Ah, I've lost all control - exploit me now":
                        case "Here, right between the line of truth and lies":
                            sprite.Color(startTime, Color.Black);
                            break;
                        case "fiction":
                        case "memory":
                            sprite.Color(startTime, RedFont);
                            break;
                        case "簡単に絆されて":
                            Entry = y%2==0 ? 3 : 2;
                            if (y == 1) position += new Vector2(10,50);
                            else if (y == 2) position += new Vector2(15,20);
                            else if (y == 3) position += new Vector2(25,40);
                            else if (y == 4) position += new Vector2(40,0);
                            else if (y == 5) position += new Vector2(25,50);
                            else if (y == 6) position += new Vector2(0,10);
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, RedFont, RedFont);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "簡単に堕ちてゆく":
                            Entry = y%2==0 ? 3 : 2;
                            if (y == 0) position += new Vector2(0,20);
                            else if (y == 1) position += new Vector2(10,70);
                            else if (y == 2) position += new Vector2(15,40);
                            else if (y == 3) position += new Vector2(25,40);
                            else if (y == 4) position += new Vector2(40,20);
                            else if (y == 5) position += new Vector2(25,70);
                            else if (y == 6) position += new Vector2(20,0);
                            else if (y == 7) position += new Vector2(0,50);
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, RedFont, RedFont);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "\"自分\"が無いのならば":
                            Entry = y%2==0 ? 3 : 2;
                            if (y == 0) position += new Vector2(-25,20);
                            else if (y == 1) position += new Vector2(10,50);
                            else if (y == 2) position += new Vector2(40,20);
                            else if (y == 3) position += new Vector2(60,0);
                            else if (y == 4) position += new Vector2(70,40);
                            if (y >= 5) position += new Vector2(90,30);
                            if (y == 5) position += new Vector2(10,-20);
                            else if (y == 6) position += new Vector2(30,40);
                            else if (y == 7) position += new Vector2(10,-20);
                            else if (y == 8) position += new Vector2(10,20);
                            else if (y == 9) position += new Vector2(10,-10);
                            else if (y == 10) position += new Vector2(10,30);
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, RedFont, RedFont);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "I was so easily swept away":
                        case "Taking my chance on this one-time stage":
                        case "I'll try to search for the real truth":
                            sprite.Color(startTime + letterOffset, RedFont);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "It's easy to fall underway":
                            if (y==2) position += new Vector2(10,0);
                            sprite.Color(startTime + letterOffset, RedFont);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "I'm trying hard to right myself":
                            if (y==1) position += new Vector2(10,0);
                            sprite.Color(startTime + letterOffset, RedFont);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "I'm struggling not to lose myself":
                            if (y==1) position += new Vector2(10,0);
                            sprite.Color(startTime + letterOffset, RedFont);
                            sprite.Color(EndTime - 100, EndTime, Color4.Black, Color4.Black);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "LOST MY WAY":
                        case "FALL UNDER":
                            sprite.Color(startTime, RedFont);
                            outLetterOffset = 500;
                            sprite.StartLoopGroup(endTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                        case "自分を失わぬよう":
                            Entry = y%2==0 ? 3 : 2;
                            if (y == 0) position += new Vector2(-10,20);
                            else if (y == 1) position += new Vector2(15,60);
                            else if (y == 2) position += new Vector2(20,-10);
                            else if (y == 3) position += new Vector2(20,50);
                            else if (y == 4) position += new Vector2(50,30- 20);
                            else if (y == 5) position += new Vector2(50,80- 20);
                            else if (y == 6) position += new Vector2(50,40- 20);
                            else if (y == 7) position += new Vector2(30,100-20);
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, RedFont, RedFont);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "一度きりの舞台で":
                            Entry = y%2==0 ? 3 : 2;
                            if (y == 0) position += new Vector2(-50,20);
                            else if (y == 2) position += new Vector2(10,40);
                            else if (y == 3) position += new Vector2(-5,0);
                            else if (y == 4) position += new Vector2(-10,40);
                            else if (y == 5) position += new Vector2(30,30);
                            else if (y == 6) position += new Vector2(70,-20);
                            else if (y == 7) position += new Vector2(80,40);

                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, RedFont, RedFont);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "ホンモノを探して":
                            Entry = y%2==0 ? 3 : 2;
                            if (y == 1) position += new Vector2(0,30);
                            else if (y == 2) position += new Vector2(0,-20);
                            else if (y == 3) position += new Vector2(-10,30);
                            else if (y == 4) position += new Vector2(-15,-15);
                            else if (y == 5) position += new Vector2(5,35);
                            else if (y == 6) position += new Vector2(35,-20);
                            else if (y == 7) position += new Vector2(25,-5);

                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            sprite.Color(EndTime - 100, EndTime, RedFont, RedFont);
                            sprite.Fade(EndTime - 50, EndTime, 1, 0);
                            break;
                        case "期待外れのことばかりで":
                            Entry = Random(0, 4);
                            letterScale = IsKanji(letter) ? new Vector2(fontScale * 1.5f, fontScale) : new Vector2(fontScale*1.5f, fontScale);
                            break;
                        case "のことばかりで":
                            Entry = Random(0, 4);
                            break;
                        case "ドラマ":
                            Entry = y%2==0 ? 3 : 2;
                            break;
                        case "dramatic":
                        case "poker face":
                        case "story":
                        case "Fiction":
                            sprite.Color(EndTime, RedFont);
                            break;
                        case "のようなラスト":
                            Entry = y%2==0 ? 3 : 2;
                            if (y == 1) letterOffset += 28406 - StartTime;
                            else if (y == 2) letterOffset += 28710 - StartTime;
                            else if (y == 3) letterOffset += 29015 - StartTime;
                            else if (y >= 4) 
                            {
                                letterOffset += 29928 - StartTime + (y-5) * 100;
                                Entry = 0;
                            }
                            break;
                        case "求めた":
                        case "ポーカーフェイス":
                        case "それぞれ":
                        case "フィクション":
                            Entry = y%2==0 ? 3 : 2;
                            break;
                        case "きげき":
                        case "ひげき":
                        case "いるんだ":
                            Entry = y%2==0 ? 0 : 1;
                            break;
                        case "誰もが纏う":
                            if (y == 3) 
                            {
                                letterOffset += 39827 - StartTime - y*inLetterOffset;
                                Entry = 14;
                            }
                            else if (y == 4) 
                            {
                                letterOffset += 40664 - StartTime - y*inLetterOffset;
                                Entry = 11;
                            }
                            
                            if (y==0) 
                            {
                                sprite.ScaleVec(startTime + letterOffset, startTime + letterOffset + 100, letterScale * 2f, letterScale * 2f);
                                sprite.ScaleVec(startTime + letterOffset + 100, startTime + letterOffset + 100, letterScale, letterScale);
                            }
                            break;
                        case "都合のいい物語で構わない":
                            if (y <= 6) Entry = 999;
                            else if (y == 7) Entry = 0;
                            else if (y == 8) 
                            {
                                letterOffset = 49573 - StartTime-100;
                                Entry = 11;
                            }
                            else if (y == 9) 
                            {
                                letterOffset = 50487 - StartTime-100;
                                Entry = 0;
                            }
                            else if (y == 10) letterOffset = 50944 - StartTime-100;
                            else if (y == 11) letterOffset = 51553 - StartTime-100;
                            break;
                        case "嘘だらけの":
                            Entry = y%2==0 ? 12 : 13;
                            sprite.StartLoopGroup(StartTime + letterOffset, 2);
                            sprite.Fade(0, 50, 0, 1);
                            sprite.EndGroup();
                            sprite.StartLoopGroup(EndTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                        case "世界の中で":
                        Entry = Random(0,4);
                            if (y == 1) position += new Vector2(-170,0);
                            else if (y == 2) 
                            {
                                position += new Vector2(-80,-50);
                                sprite.Color(EndTime, RedFont);
                            }
                            else if (y == 3) position += new Vector2(-30,-60);
                            else if (y == 4) position += new Vector2(-130,-70);
                            sprite.StartLoopGroup(EndTime+500, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                        case "決めるのは誰だ？":
                        case "Who chooses if it's comedy? Who chooses a tragedy?":
                            sprite.StartLoopGroup(EndTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                        case "はか":
                            if (y == 1) 
                            {
                                position += new Vector2(230,70);
                                letterOffset += 109954 - StartTime-50;
                            }
                            break;
                        case "めるのはだ":
                            Entry = 3;
                            if (y==1)
                            {
                                letterOffset += 111324 - StartTime;
                                Entry = 2;
                            }
                            else if (y==2)
                            {
                                letterOffset += 111781 - StartTime - 50;
                                Entry = 0;
                            }
                            else if (y==3) 
                            {
                                position += new Vector2(-130,80);
                                letterOffset += 112086 - StartTime - 50;
                            }
                            else if (y==4) 
                            {
                                position += new Vector2(-20,170);
                                letterOffset += 112466 - StartTime;

                            }
                            break;
                        case "答はわからない？":
                            if (y == 6) position += new Vector2(0,-5);
                            if (y > 1) 
                            {
                                letterOffset += 115284 - StartTime-200;
                                Entry = y%2==0 ? 1 : 0;
                            }
                            sprite.StartLoopGroup(StartTime  + letterOffset, 2);
                            sprite.Color(0, 50, RedFont, Color4.White);
                            sprite.EndGroup();
                            sprite.Color(EndTime-50, EndTime, Color4.White, Color4.Black);
                            break;
                        case "毒に侵されて":
                            Entry = Random(0,4);
                            if (y==1) position += new Vector2(0,-3);
                            break;
                        case "この世界の中でずっと":
                            Entry = Random(0,4);
                            if (y>=5) letterOffset += 236502 - StartTime - 500;
                            if (y == 8) position += new Vector2(0,-5);
                            break;
                        case "たとえ真実が":
                            if (y == 3 | y == 4)
                            {
                                sprite.ScaleVec(StartTime + letterOffset,StartTime + letterOffset + 100, letterScale*1.5f, letterScale*1.5f);
                                sprite.ScaleVec(StartTime + letterOffset + 100, letterScale);
                                inMoveOffset = 60f;
                            }
                            else 
                            {
                                inMoveOffset = 30f;
                                position += new Vector2(0, -40);
                            }
                            if (y==1) position += new Vector2(-30,0);
                            else if (y==2) position += new Vector2(-80,0);
                            else if (y==3) position += new Vector2(-80,0);
                            else if (y==5) 
                            {
                                position += new Vector2(20,0);
                                Entry = 2;
                            }
                            if (y==0) position += new Vector2(10,0);
                            break;
                        case "嘘となっても":
                            if (y==0) 
                            {
                                position += new Vector2(-60,0);
                                sprite.ScaleVec(StartTime + letterOffset,StartTime + letterOffset + 100, letterScale*1.5f, letterScale*1.5f);
                                sprite.ScaleVec(StartTime + letterOffset + 100, letterScale);
                                inMoveOffset = 60f;
                                Entry = 3;
                            }
                            else 
                            {
                                inMoveOffset = 30f;
                                Entry = 0;
                                position += new Vector2(0, 30);
                            }
                            if (y==3) position += new Vector2(0,-10);
                            break;
                        case "生きていくために":
                            Entry = y%2==0 ? 12 : 13;
                            break;
                        case "騙されよう":
                            if (y==0) position += new Vector2(-4, 0);
                            break;
                        case "手探りの僕は":
                        case "思考停止と言う":
                        case "自分を委ね続けて":
                        case "流されていく":
                        case "負けないよう":
                            Entry = Random(0,4);
                            break;
                        case "Whenever it's too good to be my truth. I'm just not bothered":
                            if (y>=39) 
                            {   
                                letterOffset += 49573 - StartTime + (y-41) * 50;
                                Entry = 3;
                                inMoveOffset = 1f;
                            }
                            else {
                                inMoveOffset = 0f;
                            }
                            break;
                        case "Because our world deludes,":
                        case "its lies are playing truth":
                            sprite.StartLoopGroup(EndTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                    }

                    //Extra switch cases because it's easier than re-organising
                    switch (lyrics) {
                        case "現実":
                        case "はか":
                        case "残酷":
                        case "決":
                        case "めるのはだ":
                        case "誰":
                        case "演":
                        case "じ":
                        case "続":
                        case "けて":
                        case "いるんだ":
                        case "それぞれ":
                        case "く":
                        case "フィクション":
                        case "で":
                        case "Who       chooses       our       reality?":
                        case "Who       chooses       our       destiny?":
                        case "If         I'm         going         to         be         deceived         right         then":
                        case "I'll     carry     on     acting     till     the     end":
                        case "to     create     our     own     fiction     here":
                        case "Who doesn't know the answer to it all?":
                            sprite.Color(startTime + letterOffset, startTime + letterOffset + 100, RedFont, RedFont);
                            sprite.Color(OsbEasing.OutExpo, startTime + letterOffset + 100, startTime + letterOffset + 500, RedFont, Color4.White);
                            break;
                        case "Just like the common masses":
                        case "All easily forgotten":
                        case "I don't want to be a blind follower":
                        case "It's fine to just deceive":
                        case "If you can believe":
                        case "Just paint me a once-in-a-lifetime story":
                            sprite.StartLoopGroup(EndTime-200, 4);
                            sprite.Fade(0, 50, 1, 0);
                            sprite.EndGroup();
                            break;
                        case "Can't make a thought or decision":
                        case "A poison that afflicts the mind":
                        case "I feel myself subjugated within":
                        case "Sweeping my will aside":
                            Entry = Random(0,4);
                            break;
                        case "Forever in this world, as if though time preserved":
                            Entry = Random(0,4);
                            if (y >= 23) letterOffset += 236502 - StartTime + y - 750;
                            break;
                        case "So not to lose myself, I hesitantly search around":
                            Entry = Random(0,4);
                            if (y >= 23) letterOffset += 240766 - StartTime + y - 750;
                            break;
                    }

                    switch (Entry) //Animates the entry animation (case 0 to 3 are my defaults, everything else is written according to what I need)
                    {
                        case 0: //from RIGHT
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(inMoveOffset,0), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 1: //from LEFT
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(-inMoveOffset,0), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 2: //from BELOW
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(0,inMoveOffset), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 3: //from ABOVE
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(0,-inMoveOffset), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 4: //Move towards centre (unused)
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, MoveCentre(position, 1.5f), position);
                            sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, letterScale * 1.5f, letterScale);
                            break;
                        case 5: //Move towards centre (smaller)
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, MoveCentre(position, 1.4f).X, position.Y, position.X, position.Y);
                            sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, letterScale, letterScale);
                            break;
                        case 6: //
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, MoveCentre(position, 0.9f).X, position.Y, position.X, position.Y);
                            sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, letterScale, letterScale);
                            break;
                        case 7: //from RIGHT
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(inMoveOffset,0), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 8: //from LEFT
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(-inMoveOffset,0), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 9: //from BELOW
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(0,inMoveOffset), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 10: //from ABOVE
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(0,-inMoveOffset), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 11:
                            sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, letterScale * 1.5f, letterScale);
                            break;
                        case 12:
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(-inMoveOffset,inMoveOffset), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 13:
                            sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(inMoveOffset,-inMoveOffset), position);
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        case 14:
                            sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset, startTime + letterOffset+70, letterScale * 2f, letterScale * 2f);
                            sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset+70, startTime + inTimeOffset + letterOffset, letterScale * 1.3f, letterScale);
                            break;
                        case 999:
                            sprite.ScaleVec(startTime + letterOffset, letterScale);
                            break;
                        default:
                            break;
                    }

                    sprite.Fade(startTime + letterOffset, startTime + letterOffset, 0, 1);

                    letterOffset = (totalY - y) * outLetterOffset;
                    
                    switch (lyrics) {
                        case "怯えて":
                            letterOffset = 250;
                            break;
                    }
                    switch (Exit) //Exit Animation, this part isn't fully developed yet, might not work correctly
                    {
                        case 0: //to LEFT
                            sprite.Move(OsbEasing.InExpo, endTime - letterOffset + outLetterOffset, endTime, position, position+new Vector2(-outMoveOffset,0));
                            break;
                        case 1: //to RIGHT
                            sprite.Move(OsbEasing.InExpo, endTime - letterOffset + outLetterOffset, endTime, position, position+new Vector2(outMoveOffset,0));
                            break;
                        case 2: //to UP
                            sprite.Move(OsbEasing.InExpo, endTime - outLetterOffset, endTime, position, position+new Vector2(0,-outMoveOffset));
                            break;
                        case 3: //to DOWN
                            sprite.Move(OsbEasing.InExpo, endTime - outLetterOffset, endTime, position, position+new Vector2(0,outMoveOffset));
                            break;
                        case 4: 
                            sprite.Move(OsbEasing.InExpo, endTime - outLetterOffset, endTime, position.X, position.Y, MoveCentre(position, 0.4f).X, position.Y);
                            break;
                        case 5:
                            sprite.Move(OsbEasing.InExpo, EndTime-1000, EndTime, MoveCentre(position, 1f).X, position.Y, MoveCentre(position, 2f).X, position.Y);
                            break;
                        case 6:
                            sprite.Move(OsbEasing.InExpo, EndTime + y*50, EndTime + y*50+outTimeOffset, MoveCentre(position, 1f), MoveCentre(position, 3f));
                            sprite.ScaleVec(OsbEasing.InExpo, EndTime + y*50, EndTime + y*50+outTimeOffset, letterScale, letterScale * 3f);
                            break;
                        case 7: //to LEFT
                            sprite.Move(OsbEasing.InExpo, EndTime - 500 - letterOffset, EndTime - letterOffset, position, position+new Vector2(-outMoveOffset,0));
                            sprite.Fade(EndTime - letterOffset-50,EndTime - letterOffset,1,0);
                            break;
                        case 8: //to RIGHT
                            sprite.Move(OsbEasing.InExpo, EndTime + (totalY-y)*50 - 400 - 500, EndTime + (totalY-y)*50 - 500, position, position+new Vector2(outMoveOffset,0));
                            sprite.Fade(EndTime + (totalY-y)*50 - 650, EndTime + (totalY-y)*50 - 500, 1, 0);
                            break;
                        case 99:
                            sprite.Fade(endTime, endTime, 1, 0);
                            break;
                    }

                    if (Angle != 0) sprite.Rotate(startTime + letterOffset, Angle);
                    if (layer == engText | layer == engTextReversed) sprite.Rotate(startTime + letterOffset, Angle);
                }         

                if (Vertical) letterY += texture.Height * FontScale + Kerning;
                else
                {
                    var distance = texture.BaseWidth * FontScale + Kerning;
                    switch (lyrics) {
                        case "嘘じゃない":
                        case "眩しさに":
                        case "眩しさに ":
                            if (IsKanji(letter)) distance += 6;
                            break;
                        case "嘘だらけの":
                            if (y == 0) distance += 10;
                            break;
                    }
                    letterX += distance * (float)Math.Cos(Angle);
                    letterY += distance * (float)Math.Sin(Angle);
                }

                y++;
            }
        }

        public void GenerateNoiseLyrics(StoryboardLayer layer, FontGenerator font, string lyrics, int StartTime, int EndTime, float FontScale, float Kerning, Vector2 PositionOffset,
            float Angle = 0f, bool Vertical = false, bool Box = false, bool AfterImage = false,
            int Entry = 0, float inTimeOffset = 500f, float inLetterOffset = 0, float inMoveOffset = 20f,
            int Exit = 0, float outTimeOffset = 500f, float outLetterOffset = 0, float outMoveOffset = 20f)
        {
            float lineWidth = 0f, lineHeight = 0f, verticalHeight = 0f, verticalWidth = 0f, textureOffset = 0f;
            int y = 0, totalY = 0;

            foreach (var letter in lyrics)
            {
                var texture = font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * FontScale + Kerning;
                lineHeight = Math.Max(lineHeight, texture.Height * FontScale);
                verticalHeight += texture.Height * FontScale + Kerning;
                verticalWidth = Math.Max(verticalWidth, texture.Width * FontScale);
                textureOffset = Math.Max(textureOffset, texture.OffsetFor(OsbOrigin.Centre).Y * FontScale);
                totalY++;
            }

            lineWidth -= Kerning;
            verticalHeight -= Kerning;

            float width = Vertical? verticalWidth : lineWidth;
            float height = Vertical? verticalHeight: Math.Abs(lineHeight) + lineWidth * (float)Math.Sin(Angle);
            float letterX = 320 - width * 0.5f;
            float letterY = 240 - (height + textureOffset) * 0.5f;

            foreach (var letter in lyrics)
            {
                var fontScale = IsKanji(letter) ? FontScale * 1f : FontScale * 0.75f;
                var letterScale = new Vector2(fontScale, fontScale);
                switch (lyrics) {
                    case "ニセモノに":
                    case "まれた":
                    case "なんて":
                    case "しまうなら":
                        letterScale = new Vector2(fontScale * 1.5f, fontScale);
                        break;
                    case "騙されて":
                        if (y==0) letterScale = new Vector2(fontScale * 1.5f, fontScale * 1.8f);
                        else letterScale = new Vector2(fontScale * 1.5f, fontScale);
                        break;
                    case "何が正しい、":
                        if (y==0|y==2) letterScale = new Vector2(fontScale * 1.5f, fontScale * 1.2f);
                        else letterScale = new Vector2(fontScale * 1.5f, fontScale);
                        break;
                    case "心なんだ":
                    case "勝ちでしょ":
                        letterScale = new Vector2(fontScale * 0.8f, fontScale);
                        if (y == 0)  letterScale = new Vector2(fontScale * 1.5f, fontScale);
                        break;
                    case "僕は騙し":
                        letterScale = new Vector2(fontScale * 0.8f, fontScale);
                        if (y == 2)  letterScale = new Vector2(fontScale * 1.5f, fontScale);
                        break;
                    case "続けよう":
                    case "空っぽな":
                    case "思ったもん":
                        fontScale = FontScale * 0.75f;
                        letterScale = new Vector2(fontScale * 0.8f, fontScale);
                        break;
                    case "ああ ":
                        if (y==1) letterScale = new Vector2(fontScale*0.8f, fontScale*0.8f);
                        break;
                    case "ああ":
                        if (y==1) letterScale = new Vector2(fontScale*0.8f * 1.5f, fontScale*0.8f);
                        else letterScale = new Vector2(fontScale * 1.5f, fontScale);
                        break;
                    case "壊":
                        fontScale = IsKanji(letter) ? FontScale * 1.1f : FontScale * 0.8f;
                        letterScale = IsKanji(letter) ? new Vector2(fontScale * 1.6f, fontScale) : new Vector2(fontScale*2f, fontScale);
                        break;
                    case "僕を ":
                    case "真実と":
                    case "僕を":
                    case "もっと":
                    case "もっと ":
                    case "真":
                    case "実":
                        fontScale = IsKanji(letter) ? FontScale * 1.1f : FontScale * 0.8f;
                        letterScale = IsKanji(letter) ? new Vector2(fontScale * 1.2f, fontScale) : new Vector2(fontScale*2f, fontScale);
                        break;
                    case "壊さないで":
                    case "踊らされて":
                    case "そんな自分じゃ":
                        fontScale = IsKanji(letter) ? FontScale * 1.5f : FontScale * 0.8f;
                        letterScale = IsKanji(letter) ? new Vector2(fontScale * 1.2f, fontScale) : new Vector2(fontScale*2f, fontScale);
                        break;
                    case "嘘の間で":
                        fontScale = IsKanji(letter) ? FontScale * 1.5f : FontScale * 0.8f;
                        letterScale = IsKanji(letter) ? new Vector2(fontScale * 1.2f, fontScale) : new Vector2(fontScale*2f, fontScale);
                        if (y==2) letterScale = new Vector2(fontScale * 1.2f, fontScale * 0.7f);
                        break;
                    case "有象":
                    case "無象":
                        letterScale = new Vector2(fontScale, fontScale*1.5f);
                        break;
                    case "枯れてゆくもの":
                        letterScale = new Vector2(fontScale, fontScale*1.25f);
                        break;
                    case "そうなりたくはないから":
                    case "ズルしたっていい":
                        letterScale = new Vector2(fontScale, fontScale*2f);
                        break;
                    case "あーあ":
                        letterScale = new Vector2(fontScale * 1.5f, fontScale);
                        break;
                    case "ニセモノのようだ":
                        letterScale = new Vector2(fontScale, fontScale * 2f);
                        break;
                    case "信じ":
                        if (y==0) letterScale = new Vector2(fontScale * 1.5f, fontScale);
                        break;
                    case "ホンモノを":
                        Entry = y%2==0 ? 3 : 2;
                        letterScale = new Vector2(fontScale * 1.2f, fontScale);
                        break;
                    case "描け":
                        if (y==1) letterScale = new Vector2(fontScale * 1.2f * 0.8f, fontScale * 0.8f);
                        else letterScale = new Vector2(fontScale*1.1f, fontScale * 1.7f);
                        break;



                }
                var texture = font.GetTexture(letter.ToString());

                if (!texture.IsEmpty)
                {
                    var startTime = StartTime;
                    var endTime = EndTime;

                    var position = new Vector2(letterX, letterY)
                        + texture.OffsetFor(OsbOrigin.Centre) * FontScale + PositionOffset;

                    var letterOffset = y*inLetterOffset;

                    switch (lyrics) {
                        case "ニセモノに":
                            Entry = Random(0, 4);
                            if (y == 0) Entry = 0;
                            else if (y == 1) Entry = 2;
                            else if (y == 2) Entry = 3;

                            if (y == 1) position += new Vector2(0, 15);
                            else if (y == 3) position += new Vector2(-5, 15);
                            break;
                        case "まれた":
                            Entry = y%2==0 ? 3 : 2;
                            if (y == 1) position += new Vector2(5, 0);
                            else if (y == 2) position += new Vector2(20, 0);
                            break;
                        case "空っぽな":
                            Entry = y%2==0 ? 0 : 1;
                            if (y == 1) position += new Vector2(0, -15);
                            else if (y == 2) position += new Vector2(0, 18);
                            else if (y == 3) position += new Vector2(0, 14);
                            break;
                        case "心なんだ":
                            Entry = y%2==0 ? 0 : 1;
                            if (y >= 1) position += new Vector2(0, 0);
                            if (y == 2) position += new Vector2(0, -2);
                            break;
                        case "思ったもん":
                            Entry = y%2==0 ? 0 : 1;
                            if (y == 1) position += new Vector2(0,-10);
                            else if (y == 2) position += new Vector2(0, 18);
                            else if (y == 3) position += new Vector2(0, 13);
                            break;
                        case "続けよう":
                            Entry = y%2==0 ? 0 : 1;
                            if (y == 2) position += new Vector2(0, 10);
                            else if (y == 3) position += new Vector2(0, 20);
                            break;
                        case "勝ちでしょ":
                            Entry = y%2==0 ? 0 : 1;
                            if (y == 3) position += new Vector2(0,10);
                            break;
                        case "なんだ":
                            if (y == 1) position += new Vector2(0, -5);
                            break;
                        case "ああ":
                            if (y == 0) {Entry = 3; Exit = 99;}
                            else {Entry = 2; Exit = 99;}
                            break;
                        case "真実":
                            if (y == 0) {Entry = 3; Exit = 99;}
                            else {Entry = 2; Exit = 99;}
                            break;
                        case "僕を":
                            if (y == 0) Entry = 9;
                            else Entry = 8;
                            Exit = 6;
                            inMoveOffset = 100f;
                            break;
                        case "僕を ":
                        case "真実と":
                            Entry = 0;
                            inMoveOffset = 50f;
                            if (y==2) position += new Vector2(-19,0);
                            break;
                        case "もっと ":
                            Entry = 0;
                            inMoveOffset = 50f;
                            if (y==2) position += new Vector2(-10, 0);
                            break;
                        case "もっと":
                            if (y==2) position += new Vector2(-10, 0);
                            break;
                        case "壊さないで":
                        case "踊らされて":
                            Entry = y%2==0 ? 7 : 6;
                            if (y >= 1) position += new Vector2(25, 23);
                            break;
                        case "嘘の間で":
                            Entry = y%2==0 ? 7 : 6;
                            if (y == 1|y==3) position += new Vector2(0, 23);
                            else if (y == 2) position += new Vector2(0, 8);
                            break;
                        case "有象":
                            if (y == 1) Entry = 2;
                            break;
                        case "無象":
                            if (y == 0) Entry = 3;
                            else Entry = 0;
                            break;
                        case "そうなりたくはないから":
                            Entry = y%2==0 ? 2 : 3;

                            break;
                        case "そんな自分じゃ":
                        case "しまうなら":
                            Entry = y%2==0 ? 7 : 6;
                            break;
                        case "信じ":
                            if (y == 1) position += new Vector2(40, 0);
                            break;
                        case "られるなら":
                            if (y == 1) position += new Vector2(70, 0);
                            else if (y == 3) position += new Vector2(70, 0);
                            else if (y == 4) position += new Vector2(25, 10);
                            break;
                        case "騙されてしまうなら":
                            Entry = y%2==0 ? 7 : 6;
                            if (y == 0) position += new Vector2(-15, 0);
                            break;
                        case "騙されて":
                            Entry = y%2==0 ? 7 : 6;
                            if (y == 0) 
                            {
                                position += new Vector2(-15, -20);
                                Entry = 5;
                            }
                            break;
                        case "ホンモノを":
                            if (y == 1) position += new Vector2(-10, 50);
                            else if (y == 2) position += new Vector2(-10, 10);
                            else if (y == 3) position += new Vector2(-30, 70);
                            else if (y == 4) position += new Vector2(-25, 30);
                            break;
                        case "描け":
                            if (y==1)
                            {
                                Entry = 2;
                                inMoveOffset = 20f;
                            }
                            break;
                        
                    }

                    var outMovement = new Vector2(Random(-outMoveOffset,outMoveOffset), Random(-outMoveOffset,outMoveOffset));
                    var randomScaleX = Random(0.5f, 2f);
                    var randomScaleY = Random(0.5f, 2f);

                    for (int i = 0; i < 8; i++)
                    {
                        if (i == 0) layer = GetLayer("Noise Text Front");
                        else layer = GetLayer("Noise Text Behind");

                        startTime += i * 4;
                        endTime += i * 4;
                        string imgPath = texture.Path;
                        if (i != 0) 
                        {
                            if (font == chorusFont) imgPath = NoiseImage("f/reg_noise", texture.Path);
                            else if (font == chorusItalicFont) imgPath = NoiseImage("f/italic_noise", texture.Path);
                        }
                        var sprite = layer.CreateSprite(imgPath, OsbOrigin.Centre, position);

                        switch (lyrics) {
                            case "包":
                                if (i == 0) Entry = 5;
                                else Entry = 4;
                                break;
                            case "心なんだ":
                                if (y == 0) {
                                    if (i == 0) Entry = 5;
                                    else Entry = 4;
                                }
                                break;
                            case "僕は騙し":
                                if (y == 2) {
                                    if (i == 0) Entry = 5;
                                    else Entry = 4;
                                }
                                break;
                            case "勝ちでしょ":
                                if (y == 0) {
                                    if (i == 0) Entry = 5;
                                    else Entry = 4;
                                }
                                break;
                            case "壊":
                                if (i == 0) Entry = 5;
                                else Entry = 4;
                                break;
                            case "一度の人生":
                                if (y==0) Entry = 1;
                                else if (y==1) Entry = 2;
                                else if (y==2) Entry = 0;
                                else if (y==3) Entry = 3;
                                else if (y==4) Entry = 3;
                                break;
                            case "描け":
                                break;
                            case "何が正しい、":
                                if (y==2) 
                                {
                                    Entry = 5;
                                    if (i!=0) Entry =4;
                                }
                                else Entry = 0;
                                break;
                            case "騙されて":
                                if (y==0)
                                {
                                    if (i!=0) Entry = 4;
                                }
                                break;
                            
                        }

                        switch (Entry)
                        {
                            case 0: //from RIGHT
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(inMoveOffset,0), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 1: //from LEFT
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(-inMoveOffset,0), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 2: //from BELOW
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(0,inMoveOffset), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 3: //from ABOVE
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(0,-inMoveOffset), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 4:
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position + new Vector2(Random(-inMoveOffset,inMoveOffset), Random(-inMoveOffset,inMoveOffset)), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 5:
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position, position);
                                sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, letterScale * 1.5f, letterScale);
                                break;
                            case 6: //from LEFTDOWN
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(-inMoveOffset,inMoveOffset/2), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 7: //from UPRIGHT
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(inMoveOffset,-inMoveOffset/2), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 8: //from RIGHT
                                if (i==0) 
                                {
                                    sprite.Fade(startTime + letterOffset+50, startTime + letterOffset+50, 1, 0);
                                    sprite.Fade(startTime + letterOffset+100, startTime + letterOffset+100, 0, 1);
                                }
                                sprite.Move(OsbEasing.OutQuart, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(inMoveOffset,0), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 9: //from LEFT
                                if (i==0) 
                                {
                                    sprite.Fade(startTime + letterOffset+50, startTime + letterOffset+50, 1, 0);
                                    sprite.Fade(startTime + letterOffset+100, startTime + letterOffset+100, 0, 1);
                                }
                                sprite.Move(OsbEasing.OutQuart, startTime + letterOffset, startTime + inTimeOffset + letterOffset, position+new Vector2(-inMoveOffset,0), position);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 10:
                                sprite.Move(startTime + letterOffset, startTime + letterOffset + 75, MoveCentre(position, 2f), MoveCentre(position, 2f));
                                sprite.ScaleVec(startTime + letterOffset, startTime + letterOffset + 75, letterScale * 2f, letterScale * 2f);
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset + 75, startTime + inTimeOffset + letterOffset, MoveCentre(position, 1.2f), position);
                                sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset + 75, startTime + inTimeOffset + letterOffset, letterScale * 1.2f, letterScale);
                                break;
                            case 11:
                                sprite.Move(startTime + letterOffset, startTime + letterOffset + 75, MoveCentre(position, 2.5f), MoveCentre(position, 2.5f));
                                sprite.ScaleVec(startTime + letterOffset, startTime + letterOffset + 75, letterScale * 2.5f, letterScale * 2.5f);
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset + 75, startTime + inTimeOffset + letterOffset, MoveCentre(position, 1.5f), position);
                                sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset + 75, startTime + inTimeOffset + letterOffset, letterScale * 1.5f, letterScale);
                                if (i==0)
                                {
                                    sprite.Move(startTime + inTimeOffset + letterOffset, EndTime-200, position, MoveCentre(position, 0.95f));
                                    sprite.ScaleVec(startTime + inTimeOffset + letterOffset, EndTime-200, letterScale, letterScale * 0.95f);
                                }
                                break;
                            case 12:
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, MoveCentre(position, 1.5f), position);
                                sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, letterScale * 1.5f, letterScale);
                                break;
                            case 13:
                                sprite.Move(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, MoveCentre(position, 0.5f), position);
                                sprite.ScaleVec(OsbEasing.OutExpo, startTime + letterOffset, startTime + inTimeOffset + letterOffset, letterScale.X * .5f, letterScale.Y, letterScale.X, letterScale.Y);
                                break;
                            default:
                                break;
                        }

                        if (i != 0) sprite.Fade(OsbEasing.InExpo, StartTime + letterOffset, startTime + letterOffset, 0, 1);
                        else sprite.Fade(startTime + letterOffset, startTime + letterOffset, 0, 1);

                        //letterOffset = (totalY - y) * outLetterOffset;

                        switch (Exit)
                        {
                            case 0: //to LEFT
                                sprite.Move(OsbEasing.InExpo, endTime - letterOffset + outLetterOffset, endTime, position, position+new Vector2(-outMoveOffset,0));
                                break;
                            case 1: //to RIGHT
                                sprite.Move(OsbEasing.InExpo, endTime - letterOffset + outLetterOffset, endTime, position, position+new Vector2(outMoveOffset,0));
                                break;
                            case 2: //to UP
                                sprite.Move(OsbEasing.InExpo, endTime - outLetterOffset, endTime, position, position+new Vector2(0,-outMoveOffset));
                                break;
                            case 3: //to DOWN
                                sprite.Move(OsbEasing.InExpo, endTime - outLetterOffset, endTime, position, position+new Vector2(0,outMoveOffset));
                                break;
                            case 4:
                                sprite.Move(OsbEasing.InExpo, endTime - outLetterOffset, endTime, position, position + outMovement);
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 5:
                                sprite.Move(OsbEasing.InExpo, endTime - outLetterOffset, endTime, position, position + new Vector2(-outMoveOffset, outMoveOffset/2));
                                sprite.ScaleVec(startTime + letterOffset, letterScale);
                                break;
                            case 6:
                                if (i == 0)
                                {
                                    sprite.StartLoopGroup(EndTime-200, 4);
                                    sprite.Fade(0, 50, 1, 0);
                                    sprite.EndGroup();
                                }
                                break;
                            case 99:
                                if (i == 0) sprite.Fade(endTime, endTime, 1, 0);
                                break;
                            case 999:
                                sprite.Fade(EndTime, EndTime, 1, 0);
                                break;
                        }
                        
                        if (Angle != 0) sprite.Rotate(startTime + letterOffset, Angle);
                        if (i != 0) sprite.Color(startTime, RedFont);

                        //Um...I don't know a better way to do this...
                        if (lyrics == "あーあ" && i == 0) sprite.Color(startTime + letterOffset,Color4.White);
                        else if (lyrics == "心なんだ" && y == 0 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/2, RedFont, Color4.White);
                        else if (lyrics == "包" && y == 0 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/3, RedFont, Color4.White);
                        else if (lyrics == "何が正しい、" && y == 2 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/3, RedFont, Color4.White);
                        else if (lyrics == "勝ちでしょ" && y == 0 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/3, RedFont, Color4.White);
                        else if (lyrics == "僕は騙し" && y == 2 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/3, RedFont, Color4.White);
                        else if (lyrics == "騙されて" && y == 0 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/3, RedFont, Color4.White);
                        else if (lyrics == "壊" && y == 0 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/3, RedFont, Color4.White);
                        else if (lyrics == "壊さないで" && y == 0 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/2, RedFont, RedFont);
                        else if (lyrics == "嘘の間で" && y != 0 && i == 0) sprite.Color(startTime, Color.Black);
                        else if (lyrics == "嘘の間で" && y == 0 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/2, RedFont, RedFont);
                        else if (lyrics == "壊さないで" && y != 0 && i == 0) sprite.Color(startTime, Color.Black);
                        else if (lyrics == "踊らされて" && y == 0 && i == 0) sprite.Color(startTime + letterOffset, startTime + letterOffset + inTimeOffset/2, RedFont, RedFont);
                        else if (lyrics == "踊らされて" && y != 0 && i == 0) sprite.Color(startTime, Color.Black);
                        else if (lyrics == "僕を " && i == 0) sprite.Color(startTime, Color.Black);
                        else if (lyrics == "真実と" && i == 0) sprite.Color(startTime, Color.Black);
                        else if (lyrics == "もっと " && i == 0) sprite.Color(startTime, Color.Black);
                        else if (lyrics == "ああ " && i == 0) sprite.Color(startTime, Color.Black);
                        else if (lyrics == "僕を" && i != 0) sprite.Fade(EndTime-200, EndTime-200, 1, 0);

                    }
                }         

                if (Vertical) letterY += texture.Height * FontScale + Kerning;
                else
                {
                    var distance = texture.BaseWidth * FontScale + Kerning;
                    switch (lyrics) {
                        case "嘘じゃない":
                        case "眩しさに":
                        case "眩しさに ":
                            if (IsKanji(letter)) distance += 6;
                            break;
                    }
                    letterX += distance * (float)Math.Cos(Angle);
                    letterY += distance * (float)Math.Sin(Angle);
                }


                y++;
            }
        }

        public void GenerateCharacterSeparation(string Character, int startTime, int endTime, float FontScale, double Angle, Vector2 position)
        {
            char ch = Character[0];
            int unicodeValue = (int)ch;
            string hexValue = unicodeValue.ToString("x4");
            var y = 0;
            StoryboardLayer layer;

            foreach (string file in LyricPath(hexValue))
            {
                var StartTime = startTime + y * 75;
                var EndTime = endTime;
                var MovementOffset = Random(10f,30f);
                var Entry = Random(0,4);
                if (y == 0) Entry = 0;
                else if (y == 1) Entry = 3;
                else if (y == 2) Entry = 1;

                var zoomScale = Random(1f, 1.3f) * 1.5f;

                for (int i = 0; i < 8; i++)
                {
                    StartTime += i * 6;
                    EndTime += i * 6;
                    string imgPath = file;

                    if (i != 0) 
                    {
                        imgPath = NoiseImage("f/sep_noise", file);
                    }

                    if (i == 0) layer = GetLayer("Noise Text Front");
                    else layer = GetLayer("Noise Text Behind");
                    
                    var sprite = layer.CreateSprite(imgPath);

                    if (i != 0) 
                    {
                        sprite.Color(StartTime, RedFont);
                        sprite.Fade(OsbEasing.InExpo, startTime, StartTime, 0, 0.8);
                    }

                    sprite.ScaleVec(OsbEasing.OutExpo, StartTime, StartTime+100, FontScale * 1.6f * zoomScale, FontScale * zoomScale, FontScale * 1.6f * zoomScale, FontScale * zoomScale);
                    sprite.ScaleVec(OsbEasing.OutExpo, StartTime+100, EndTime-250, FontScale * 1.6f * zoomScale / 2 , FontScale * zoomScale / 2, FontScale * 1.6f, FontScale);

                    switch (Entry) {
                        case 0: //from TOP RIGHT
                            sprite.Move(StartTime, StartTime+100, MoveCentre(position + new Vector2(-MovementOffset, MovementOffset),zoomScale), MoveCentre(position + new Vector2(-MovementOffset, MovementOffset),zoomScale));
                            sprite.Move(OsbEasing.OutExpo, StartTime+100, EndTime-250, MoveCentre(position + new Vector2(-MovementOffset, MovementOffset),zoomScale/2), position);
                            break;  
                        case 1: //from TOP LEFT  
                            sprite.Move(StartTime, StartTime+100, MoveCentre(position + new Vector2(MovementOffset, MovementOffset),zoomScale), MoveCentre(position + new Vector2(MovementOffset, MovementOffset),zoomScale));
                            sprite.Move(OsbEasing.OutExpo, StartTime+100, EndTime-250, MoveCentre(position + new Vector2(MovementOffset, MovementOffset),zoomScale/2), position);
                            break;  
                        case 2: //from DOWN RIGHT  
                            sprite.Move(OsbEasing.OutExpo, StartTime, EndTime-250, MoveCentre(position + new Vector2(-MovementOffset, -MovementOffset),zoomScale), position);
                            break;  
                        case 3: //from DOWN LEFT  
                            sprite.Move(StartTime, StartTime+100, MoveCentre(position + new Vector2(MovementOffset, -MovementOffset),zoomScale), MoveCentre(position + new Vector2(MovementOffset, -MovementOffset),zoomScale));
                            sprite.Move(OsbEasing.OutExpo, StartTime+100, EndTime-250, MoveCentre(position + new Vector2(MovementOffset, -MovementOffset),zoomScale/2), position);
                            break; 
                    }

                    sprite.Move(OsbEasing.InExpo, EndTime-250, EndTime-50, position, position + new Vector2(-MovementOffset/2,MovementOffset/4));
                    sprite.Fade(endTime, endTime, 1, 0);

                }
                y++;
            }
        }

        public Vector2 MoveCentre(Vector2 startPosition, double factor)
        {
            var centre = new Vector2(320, 240);
            var angle = Math.Atan2(centre.Y - startPosition.Y, centre.X - startPosition.X);
            var newFactor = Math.Sqrt(Math.Pow(centre.X - startPosition.X, 2) + Math.Pow(centre.Y - startPosition.Y, 2)) * (1 - factor);
            return new Vector2((float)(startPosition.X + Math.Cos(angle) * newFactor), (float)(startPosition.Y + Math.Sin(angle) * newFactor));
        }

        public Vector2 MoveAngle(Vector2 startPosition, float angle, float distance)
        {
            var y = distance * (float)Math.Sin(angle);
            var x = distance * (float)Math.Cos(angle);
            startPosition += new Vector2(x, y);
            return startPosition;
        }

        static bool IsKanji(char c)
        {
            return c >= '\u4E00' && c <= '\u9FFF';
        }

        public IEnumerable<string> LyricPath (string spriteBaseName)
        {
            string MapsetPath = StoryboardObjectGenerator.Current.MapsetPath;
            string folderName = "sb/f/sep";
            string folderPath = Path.Combine(MapsetPath, folderName);
            string [] files = Directory.GetFiles(folderPath);

            var spriteFiles = files
                .Where(file => Path.GetFileName(file).StartsWith(spriteBaseName + "_") && Path.GetFileName(file).EndsWith(".png"))
                .Select(file => Path.GetRelativePath(MapsetPath, file));
            
            return spriteFiles;
        }

        public string NoiseImage(string folder, string imagePath)
        {
            var bitmap = GetMapsetBitmap(imagePath);
            string fileName = Path.GetFileName(imagePath);
            string MapsetPath = StoryboardObjectGenerator.Current.MapsetPath;
            string outputFolderPath = Path.Combine(MapsetPath, "sb", folder);
            string outputImagePath = Path.Combine(outputFolderPath, fileName);
            string returnPath = Path.Combine("sb", folder, fileName);

            DirectoryInfo d = System.IO.Directory.CreateDirectory(outputFolderPath);
            
            if (File.Exists(outputImagePath))
            {
                return returnPath;
            }

            Random random  = new Random();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x <  bitmap.Width; x++)
                {
                    int newAlpha = 0;
                    Color originalColor = bitmap.GetPixel(x, y);
                    if (originalColor.A == 0) continue;
                    int noise = random.Next(0, 255);
                    if (originalColor.A == 255) newAlpha = Math.Clamp(originalColor.A - noise, 0, 255);
                    Color noisyColor = Color.FromArgb(newAlpha, 255, 255, 255);
                    bitmap.SetPixel(x, y, noisyColor);
                }
            }
            bitmap.Save(outputImagePath);
            return returnPath;
        }
    }
}
