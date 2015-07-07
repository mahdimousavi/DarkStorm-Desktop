using Fluent;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Infrastructure
{
    public class RibbonItemRegionAdapter : RegionAdapterBase<RibbonTabItem>
    {
        public RibbonItemRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, RibbonTabItem regionTarget)
        {
            region.ActiveViews.CollectionChanged += ((x, y) =>
            {
                switch (y.Action)
                {
                    case
                        System.Collections.Specialized
                              .NotifyCollectionChangedAction.Add:
                        foreach (Fluent.RibbonGroupBox group in y.NewItems)
                        {
                            regionTarget.Groups.Add(group);
                        }
                        break;
                    case
                    System.Collections.Specialized
                          .NotifyCollectionChangedAction.Remove:
                        foreach (Fluent.RibbonGroupBox group in y.NewItems)
                        {
                            regionTarget.Groups.Remove(group);
                        }
                        break;
                }
            });

        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
        //static void AddViewToRegion(object view,)
    }
}
