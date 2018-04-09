using System.Collections.Generic;
using System.Linq;
using Gico.Config;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RMenu : BaseReadModel
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string ParentId { get; set; }
        [ProtoMember(3)]
        public EnumDefine.MenuTypeEnum Type { get; set; }
        [ProtoMember(4)]
        public string Url { get; set; }
        [ProtoMember(5)]
        public string Condition { get; set; }
        [ProtoMember(6)]
        public EnumDefine.MenuPositionEnum Position { get; set; }
        [ProtoMember(7)]
        public int Priority { get; set; }

        public static Dictionary<string, IList<RMenu>> BuildTree(RMenu[] menus)
        {
            Dictionary<string, IList<RMenu>> dictionary = new Dictionary<string, IList<RMenu>>();
            foreach (var rMenu in menus)
            {
                string key = string.IsNullOrEmpty(rMenu.ParentId) ? "#" : rMenu.ParentId;
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, new List<RMenu>());
                }
                dictionary[key].Add(rMenu);
            }
            return dictionary;
        }

        public static RMenu[] RemoveCurrentTree(RMenu[] menus, RMenu menu)
        {
            Dictionary<string, IList<RMenu>> dictionary = BuildTree(menus);
            IList<RMenu> menusRemove = new List<RMenu>();
            if (dictionary.ContainsKey(menu.Id))
            {
                var childs = dictionary[menu.Id];
                foreach (var child in childs)
                {
                    //dictionary[menu.Id].Remove(child);
                    menusRemove.Add(child);
                    RemoveChild(dictionary, child, ref menusRemove);
                }
            }
            if (dictionary.ContainsKey(menu.ParentId))
            {
                menusRemove.Add(menu);
                RemoveChild(dictionary, menu, ref menusRemove);
            }
                
            var rootItem = dictionary["#"].FirstOrDefault(p => p.Id == menu.Id);
            if (rootItem != null)
            {
                dictionary["#"].Remove(rootItem);
            }
            if (menusRemove.Count > 0)
            {
                foreach (var rMenu in menusRemove)
                {
                    dictionary[rMenu.ParentId].Remove(rMenu);
                }
            }
            return dictionary.Values.SelectMany(p => p).ToArray();
        }

        private static void RemoveChild(Dictionary<string, IList<RMenu>> dictionary, RMenu menu, ref IList<RMenu> menusRemove)
        {
            if (dictionary.ContainsKey(menu.Id))
            {
                var childs = dictionary[menu.Id];
                foreach (var child in childs)
                {
                    //dictionary[menu.Id].Remove(child);
                    menusRemove.Add(child);
                    RemoveChild(dictionary, child, ref menusRemove);
                }
            }
        }
    }
}