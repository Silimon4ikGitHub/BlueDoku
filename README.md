0.86 Fixes
Added strategy pattern to the Analytics system by IAnalyticsProvider.
Added debug log analytics provider to test events. To add a new provider you only need to insert it into _providers.
Deleted analytics dependency from GameController.
Refactored event subscriptions: deleted lambda constructions and added safe unsubscriptions.
Deleted dependency on ScoreSystem from ScoreBoostSystem.
Fixed combo viewer counter bug.

0.85
Implemented infinity combo system, with saving all combo data to prefs
Genereted and prepared resources for updting UI and Animations
Add Ui to show currend combo state and animate every increasing
Add combo atmmosphere animation of snow due to whiite-blue game setting
Add animtion for figures splsh for boosted figure placing
Fix bug in settigns panel (icons of vibrtion and sound was missplced)
Add anlytics service and add apropriate events to implement it
