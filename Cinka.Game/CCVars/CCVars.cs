using Robust.Shared;
using Robust.Shared.Configuration;

namespace Cinka.Game.CCVars
{
    [CVarDefs]
    public sealed class CCVars : CVars
    {
        /*
            * VIEWPORT
            */

        public static readonly CVarDef<bool> ViewportStretch =
            CVarDef.Create("viewport.stretch", true, CVar.CLIENTONLY | CVar.ARCHIVE);

        public static readonly CVarDef<int> ViewportFixedScaleFactor =
            CVarDef.Create("viewport.fixed_scale_factor", 2, CVar.CLIENTONLY | CVar.ARCHIVE);

        // This default is basically specifically chosen so fullscreen/maximized 1080p hits a 2x snap and does NN.
        public static readonly CVarDef<int> ViewportSnapToleranceMargin =
            CVarDef.Create("viewport.snap_tolerance_margin", 64, CVar.CLIENTONLY | CVar.ARCHIVE);

        public static readonly CVarDef<int> ViewportSnapToleranceClip =
            CVarDef.Create("viewport.snap_tolerance_clip", 32, CVar.CLIENTONLY | CVar.ARCHIVE);

        public static readonly CVarDef<bool> ViewportScaleRender =
            CVarDef.Create("viewport.scale_render", true, CVar.CLIENTONLY | CVar.ARCHIVE);

        public static readonly CVarDef<int> ViewportMinimumWidth =
            CVarDef.Create("viewport.minimum_width", 15, CVar.REPLICATED);

        public static readonly CVarDef<int> ViewportMaximumWidth =
            CVarDef.Create("viewport.maximum_width", 21, CVar.REPLICATED);

        public static readonly CVarDef<int> ViewportWidth =
            CVarDef.Create("viewport.width", 21, CVar.CLIENTONLY | CVar.ARCHIVE);

        /*
         * Save?
         */
        public static readonly CVarDef<string> LastScenePrototype = 
            CVarDef.Create("game.last_scene", "default");

    }
}