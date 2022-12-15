using System.Collections.Generic;
using System.Linq;
using fluXis.Game.Audio;
using fluXis.Game.Graphics.Background;
using fluXis.Game.Map;
using fluXis.Game.Screens.Select.UI;
using osu.Framework.Allocation;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;

namespace fluXis.Game.Screens.Select
{
    public class SelectScreen : Screen
    {
        public BackgroundStack Backgrounds;
        private List<MapSet> mapSets;
        public MapSet MapSet;
        public MapInfo MapInfo;

        public MapList MapList;

        private readonly Dictionary<MapSet, MapListEntry> lookup = new Dictionary<MapSet, MapListEntry>();

        [BackgroundDependencyLoader]
        private void load(MapStore maps, BackgroundStack background)
        {
            Backgrounds = background;
            mapSets = maps.GetMapSets();

            AddInternal(MapList = new MapList());

            int i = 0;

            foreach (var set in mapSets)
            {
                lookup[set] = MapList.AddMap(this, set, i);
                i++;
            }

            MapSet firstSet = mapSets.First();
            MapInfo firstMap = firstSet.Maps.First();
            MapSet = firstSet;
            Backgrounds.AddBackgroundFromMap(firstMap);
        }

        public void SelectMapSet(MapSet set)
        {
            MapInfo map = set.Maps.First();
            MapSet = set;
            Backgrounds.AddBackgroundFromMap(map);
            Conductor.PlayTrack(map, true, map.Metadata.PreviewTime);

            MapList.ScrollTo(lookup[set]);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.Left)
            {
                changeSelection(-1);
            }
            else if (e.Key == Key.Right)
            {
                changeSelection(1);
            }

            return base.OnKeyDown(e);
        }

        private void changeSelection(int by = 0)
        {
            int current = mapSets.IndexOf(MapSet);
            current += by;

            if (current < 0)
                current = mapSets.Count - 1;
            else if (current >= mapSets.Count)
                current = 0;

            SelectMapSet(mapSets[current]);
        }
    }
}
