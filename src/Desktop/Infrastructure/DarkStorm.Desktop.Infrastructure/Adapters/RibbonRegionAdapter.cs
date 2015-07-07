using Fluent;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections;
using System.Windows;

namespace DarkStorm.Desktop.Infrastructure
{


    public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
    {

        private static readonly Hashtable RibbonTabs = new Hashtable();

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="behaviorFactory">Allows the registration
        /// of the default set of RegionBehaviors.</param>
        public RibbonRegionAdapter(IRegionBehaviorFactory behaviorFactory)
            : base(behaviorFactory) { }

        /// <summary>
        /// Adapts a WPF control to serve as a Prism IRegion. 
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The WPF control to adapt.</param>
        protected override void Adapt(IRegion region, Ribbon regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (FrameworkElement element in e.NewItems)
                        {
                            if (element is Ribbon)
                            {

                                Ribbon rb = element as Ribbon;
                                var tabList = new List<RibbonTabItem>();

                                var items = rb.Tabs;

                                for (int i = rb.Tabs.Count - 1; i >= 0; i--)
                                {
                                    if (!(rb.Tabs[i] is RibbonTabItem)) continue;
                                    RibbonTabItem rt = (rb.Tabs[i] as RibbonTabItem);
                                    rb.Tabs.Remove(rt); // remove from existing view ribbon
                                    regionTarget.Tabs.Add(rt); // add to target region ribbon
                                    tabList.Add(rt); // add to tracking list

                                    // Without these next 3 lines the tabs datacontext would end up being inherited from the Ribbon to which 
                                    // it has been transferred.
                                    // Not sure if this is the best place to do this but it works for my purposes at the moment
                                    if (rt.DataContext.Equals(regionTarget.DataContext))
                                    { // then it is inherited
                                        rt.DataContext = rb.DataContext; // so set it explicitly to the original parent ribbons datacontext
                                    }

                                }
                                // store tracking list in hashtable using string key (= the view type name)
                                var key = rb.GetType().Name;
                                RibbonTabs[key] = tabList;

                            }
                            else if (element is RibbonTabItem)
                            {
                                // the datacontext should already be set in  these circumstances
                                regionTarget.Tabs.Add((RibbonTabItem)element);
                            }
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (UIElement elementLoopVariable in e.OldItems)
                        {

                            var element = elementLoopVariable;

                            if (element is Ribbon)
                            {

                                Ribbon rb = element as Ribbon;
                                var key = rb.GetType().Name;
                                if (!RibbonTabs.ContainsKey(key)) continue; // no ribbon tabs have been tracked

                                var tabList = (RibbonTabs[key] as List<RibbonTabItem>) ?? new List<RibbonTabItem>();
                                foreach (RibbonTabItem rt in tabList)
                                {
                                    if (!regionTarget.Tabs.Contains(rt)) continue; // this shouldn't happen
                                    regionTarget.Tabs.Remove(rt); // remove from  target region ribbon
                                    rb.Tabs.Add(rt); // restore to view ribbon
                                }
                                RibbonTabs.Remove(key); // finished tracking so remove from hashtable

                            }
                            else if (regionTarget.Tabs.Contains(element))
                            {
                                regionTarget.Tabs.Remove((RibbonTabItem)element);
                            }
                        }
                        break;
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }





    //    public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
    //    {
    //        public RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory) { }

    //        private Ribbon ribbonRegionTarget;

    //        protected override void Adapt(IRegion region, Ribbon regionTarget)
    //        {
    //            ribbonRegionTarget = regionTarget;
                
    //            region.ActiveViews.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(OnActiveViewsChanged);
               
    //            foreach (RibbonTabItem ribbonTabView in region.ActiveViews)
    //            {
    //                regionTarget.Tabs.Add(ribbonTabView);
    //            }
    //        }

    //        private void OnActiveViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
    //        {
                
    //            switch (e.Action)
    //            {
    //                // Hinzufügen von Ribboncontrols
    //                case NotifyCollectionChangedAction.Add:
    //                    // für jedes neue Item
    //                    foreach (object ribbonView in e.NewItems)
    //                    {
    //                        // wenn es ein Ribbon ist - SPEZIALFALL
    //                        if (ribbonView is Ribbon)
    //                        {
    //                            Ribbon rb = ribbonView as Ribbon;
    //                            for (int i = rb.Tabs.Count - 1; i >= 0; i--)
    //                            {
    //                                if (rb.Tabs[i] is RibbonTabItem)
    //                                {
    //                                    // Umhängen aller Ribbontab in das Ribbon des Adapters
    //                                    RibbonTabItem rt = (rb.Tabs[i] as RibbonTabItem);
    //                                    rb.Tabs.Remove(rt);
    //                                    ribbonRegionTarget.Tabs.Add(rt);
    //                                }
    //                            }
    //                        }
    //                        else
    //                            // wenn es ein Ribbontab ist
    //                            if (ribbonView is RibbonTabItem)
    //                            {
    //                                ribbonRegionTarget.Tabs.Add((RibbonTabItem)ribbonView);
    //                            }
    //                            else
    //                                // wenn es ein Ribbonbutton ist
    //                                if (ribbonView is Fluent.Button)
    //                                {
    //                                    bool alreadyInserted = false;

    //                                    foreach (object ot in ribbonRegionTarget.Tabs)
    //                                    {
    //                                        // in das erste Ribbontab
    //                                        if (ot is RibbonTabItem && !alreadyInserted)
    //                                        {
    //                                            foreach (object og in ((RibbonTabItem)ot).Groups)
    //                                            {
    //                                                // in die erste Ribbon group
    //                                                if (og is RibbonGroupBox)
    //                                                {
    //                                                    ((RibbonGroupBox)og).Items.Add(ribbonView);
    //                                                    alreadyInserted = true;
    //                                                    break;
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    throw new ArgumentException("unsupported type " + ribbonView.GetType().Name);
    //                                }
    //                    }
    //                    break;
    //                // entfernen von Ribboncontrols 
    //                case NotifyCollectionChangedAction.Remove:
    //                    {
    //                        if (e.NewItems != null)
    //                            foreach (object ribbonView in e.NewItems)
    //                            {
    //                                // wenn es ein Ribbon ist - SPEZIALFALL
    //                                if (ribbonView is Ribbon)
    //                                {
    //                                    Ribbon rb = ribbonView as Ribbon;
    //                                    for (int i = rb.Tabs.Count - 1; i >= 0; i--)
    //                                    {
    //                                        if (rb.Tabs[i] is RibbonTabItem)
    //                                        {
    //                                            RibbonTabItem rt = (rb.Tabs[i] as RibbonTabItem);
    //                                            rb.Tabs.Remove(rt);
    //                                            ribbonRegionTarget.Tabs.Remove(rt);
    //                                        }
    //                                    }
    //                                }
    //                                else
    //                                    // wenn es ein Ribbontab ist
    //                                    if (ribbonView is RibbonTabItem)
    //                                    {
    //                                        ribbonRegionTarget.Tabs.Remove((RibbonTabItem)ribbonView);
    //                                    }
    //                                    else
    //                                        // wenn es ein Ribbonbutton ist
    //                                        if (ribbonView is Fluent.Button)
    //                                        {
    //                                            // doesn't work - strange!
    //                                            ((Fluent.Button)ribbonView).Visibility = System.Windows.Visibility.Hidden;
    //                                            // doesn't work - strange!

    //                                            ribbonRegionTarget.Tabs.Remove((RibbonTabItem)ribbonView);
    //                                        }
    //                                        else
    //                                        {
    //                                            throw new ArgumentException("unsupported type " + ribbonView.GetType().Name);
    //                                        }
    //                            }
    //                        break;
    //                    }
    //            }
    //        }
    //    protected override IRegion CreateRegion()
    //    {
    //        return new AllActiveRegion();
    //    }
    //}
}
