using Estimote;
using Foundation;
using UIKit;

namespace HowColdIsMyBeer
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        private TriggerManager _triggerManager;

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var userNotificationSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert, new NSSet());
            UIApplication.SharedApplication.RegisterUserNotificationSettings(userNotificationSettings);

            _triggerManager = new TriggerManager();
            var wrongTemperatureRule = TemperatureRule.TemperatureBetween(2, 8, "841a40679cd3f7d4");
            var tempTrigger = new Trigger(new Rule[] { wrongTemperatureRule }, "WrongTemperture");
            _triggerManager.ChangedState += TriggerManagerOnChangedState;
            _triggerManager.StartMonitoringForTrigger(tempTrigger);

            return true;
        }

        private void TriggerManagerOnChangedState(object sender, TriggerChangedStateEventArgs args)
        {
            var notification = new UILocalNotification
            {
                AlertAction = "Your beer is at the wrong temperature!",
                SoundName = UILocalNotification.DefaultSoundName
            };

            UIApplication.SharedApplication.PresentLocalNotificationNow(notification);
        }

        //
        // This method is invoked when the application is about to move from active to inactive state.
        //
        // OpenGL applications should use this method to pause.
        //
        public override void OnResignActivation(UIApplication application)
        {
        }

        // This method should be used to release shared resources and it should store the application state.
        // If your application supports background exection this method is called instead of WillTerminate
        // when the user quits.
        public override void DidEnterBackground(UIApplication application)
        {
        }

        // This method is called as part of the transiton from background to active state.
        public override void WillEnterForeground(UIApplication application)
        {
        }

        // This method is called when the application is about to terminate. Save data, if needed. 
        public override void WillTerminate(UIApplication application)
        {
        }
    }
}