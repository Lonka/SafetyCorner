using System.Collections.Generic;

namespace SafetyCorner.ViewModels
{
    public class MenuItem
    {
        public string Name { get; set; }
        public List<NavigationItem> Navigations { get; set; }
    }
}