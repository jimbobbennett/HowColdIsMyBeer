using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Estimote;
using Foundation;
using UIKit;

namespace HowColdIsMyBeer
{
    public partial class RootViewController : UIViewController
    {
        private readonly NearableManager _nearableManager;
        private readonly TriggerManager _triggerManager;
        private readonly HashSet<string> _foundNearables = new HashSet<string>(); 

        public RootViewController(IntPtr handle) : base(handle)
        {
            _nearableManager = new NearableManager();
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TemperatureLabel.Text = "Locating...";

            _nearableManager.RangedNearable += NearableManagerOnRangedNearable;
            _nearableManager.RangedNearables += NearableManagerOnRangedNearables;
            _nearableManager.StartRangingForIdentifier("a764931d560a883f");
            _nearableManager.StartRangingForType(NearableType.Car);
            _nearableManager.StartRangingForType(NearableType.Bed);
        }

        private void NearableManagerOnRangedNearables(object sender, DidRangeNearablesEventArgs didRangeNearablesEventArgs)
        {
            foreach (var nearable in didRangeNearablesEventArgs.Nearables)
            {
                _foundNearables.Add(nearable.Type.ToString());
            }

            Debug.WriteLine($"Found {_foundNearables.Count} nearables");
        }

        private void NearableManagerOnRangedNearable(object sender, DidRangeNearableEventArgs args)
        {
            const char degree = (char) 176;
            TemperatureLabel.Text = $"{args.Nearable.Temperature:N}{degree}C";
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}