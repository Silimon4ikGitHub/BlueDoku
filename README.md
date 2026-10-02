Implemented infinite combo system, saving all combo data to PlayerPrefs; updated the score saving system to handle complex combo saves, and added reverse optimization for deprecated saves.
Generated and prepared resources for updating UI and animations.
Added UI to show current combo state and animate every increase.
Added combo atmosphere animation (snow) matching the white-blue game setting; animation speed increases with combo count.
Added animation for figure splash on boosted figure placement.
Fixed bug in settings panel (vibration and sound icons were misplaced).
Added analytics service and appropriate events to implement tracking.

I left a simple DI system using a singleton and manual source setup. Changed the score calculation, viewing, and saving sequence (calculate -> show -> save result). Chose an event system to implement analytics; this system will help integrate other services and event subscriptions in the future. In the future, it would be better to create a separate EventManager for better DI.

If I had more development time, I would create an EventManager for isolated integration of different SDKs, add mesh particles to selected figures, introduce figure colors and color score quests (goals) for gamification (perhaps adding blocked figures that can be crushed by only one color).