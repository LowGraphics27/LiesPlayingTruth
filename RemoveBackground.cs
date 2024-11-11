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
    public class RemoveBackground : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var q = GetLayer("").CreateSprite(Beatmap.BackgroundPath);
            q.Fade(0,0);            
        }
    }
}
