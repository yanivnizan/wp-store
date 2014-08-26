/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SoomlaWpStore.billing.wp.store
{
    /// <summary>
    /// Handles dispatching to the UI and App Threads
    /// </summary>
    public static class Dispatcher
    {
        // needs to be set via the app so we can invoke onto App Thread (see App.xaml.cs)
        public static Action<Action> InvokeOnAppThread
        { get; set; }

        // needs to be set via the app so we can invoke onto UI Thread (see App.xaml.cs)
        public static Action<Action> InvokeOnUIThread
        { get; set; }
    }
}
