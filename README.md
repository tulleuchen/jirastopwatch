## Summary

A tool for recording time spent on different Jira tasks.

![](http://gehling.dk/jirastopwatch/screenshot3.png)

## Features:

* Configure how many time-tracking slots you want available
* Integration to Jira REST API: Fetch task description when task-key has been entered (requires login)
* Time is reported in Jira time-logging format (eg. 2h 31m) to easily copy/paste into time-logging
* Jira issue keys are saved on program exit
* Posting spent time into Jira as a worklog with comment (using Jira REST API)
* Save time-tracking state, so your stopwatch continue to "run" even if you need to quit the program (e.g. you need to reboot, but still want to keep on recording time)
* Automatic re-login, if Jira session has expired

## Planned features

* Adding issue keys with auto-complete/selecting from list - list provided by current JQL filter

* Better visual feedback when posting worklog to Jira (to indicate success)

Feature-requests are more than welcome :-)

## Mac OSX and Linux users

Jira StopWatch has been compiled and tested to work on Linux Mint 17.0 with the [Xamarin packages](http://www.mono-project.com/download/#download-lin).

Anyone with a MacOSX available: I would love to know if everything works out of the box.

## Download

A setup file with the latest release can be [downloaded here](https://github.com/carstengehling/jirastopwatch/releases).

## Usage

After install, start the application and click the settings icon (gears icon). Enter the real base URL for your Jira server and press OK. Then click on the padlock to login to Jira.

Now write a Jira task id in one of the white textboxes. When you leave the textbox, the task description will be fetched from your Jira server and displayed below the textbox. Repeat this for the tasks you are currently working on.

Press the green PLAY button next to your task-id. The time-tracking textbox will now turn green, and after the first minute has passed, the time elapsed will change from "0m" to "1m".

If you press PLAY on another task, the previous will automatically pause.

The button to the right of the time box lets you post the time directly on Jira as a worklog along with a comment. If the posting is successful, the timer will automatically reset, otherwise the timer will not be changed. **Note that since the smallest time unit in StopWatch is minutes, the button will be disabled until at least 1 minute has passed.**

Click on the rightmost button to reset the time to 0m.

That's pretty much it!

## License

Apache License version 2.0 - please read [LICENSE.txt](LICENSE.txt)

## Feedback

Bug reports, feature requests etc. are welcome. Please use Github for this.

## Externals

The application depend on [RestSharp](https://github.com/restsharp/RestSharp) for all communication with Jira.

All icons on buttons were downloaded from [Icons8](https://icons8.com).

## Changelog

<pre>
1.0.1     2015-09-25     First release with setup program
1.0.2     2015-09-28     Integration with Jira: Async load issue summary
1.0.3     2015-09-28     Remember login credentials with DPAPI
1.0.4     2015-09-30     Clear summary label when issue key is empty
1.0.5     2015-10-07     Nicer buttons + tooltips

1.1.0     2015-10-09     Changed all icons to https://icons8.com
                         New feature: Post worklog to Jira with a comment

1.1.1     2015-10-09     Fixed problems with main window being "Always on top" and the applications other dialog boxes

1.2.0     2015-10-19     Save time-tracking state, so stopwatch continue to "run" after quitting program
                         Automatic re-login, if Jira session has expired
                         Visual Jira connection status
                         Fixed tab-order on controls
</pre>
