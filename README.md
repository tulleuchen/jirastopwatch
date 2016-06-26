## Summary

A Windows desktop tool for recording time spent on different Jira tasks.

![](http://gehling.dk/jirastopwatch/screenshot4.png)

## Features:

### Easy time tracking of Jira issues

* Switch time tracking between issues with just one click
* Configure how many time-tracking slots you want available
* Time can be manually edited (eg. if you forgot to start the timer when starting work)
* Optionally pause timer when locking your PC

### Integration with Jira REST API

* Select issue keys from a list based on one of your favorite JQL filters or type it manually
* Displays issue description when key has been selected or entered manually
* Post spent time into Jira as a worklog with comments

### Automatically save program state on exit

* Jira issue keys are saved on program exit
* Optionally remember login credentials
* Optionally save time-tracking state, so your stopwatch continue to "run" even if you need to quit the program (e.g. you need to reboot, but still want to keep on recording time)

Feature-requests are more than welcome :-)

## Download & installation

A setup file with the latest release can be [downloaded here](https://github.com/carstengehling/jirastopwatch/releases).

## Usage

Watch the tutorial screencast: [https://vimeo.com/146107370](https://vimeo.com/146107370)

After install, start the application and click the settings icon (gears icon). Enter the real base URL for your Jira server and press OK. Then click on the padlock to login to Jira.

Now you can either write Jira issue id's manually into the the textbox. Or you can choose one of your favorite JQL filters in the drop-down list in the bottom of the windows, and afterwards select Jia issues from drop-down lists.

Press the green PLAY button next to the issue, that you want to work on. The time-tracking textbox will now turn green, and after the first minute has passed, the time elapsed will change from "0m" to "1m".

If you press PLAY on another task, the previous timer will automatically pause.

The button to the right of the time box lets you post the time directly on Jira as a worklog along with a comment. If the posting is successful, the timer will automatically reset, otherwise the timer will not be changed.

The rightmost button on each row reset the timer to 0m.

That's pretty much it!

## Mac OSX and Linux users

Jira StopWatch has been compiled and tested to work on Linux Mint 17.0 with the [Xamarin packages](http://www.mono-project.com/download/#download-lin).

Anyone with a MacOSX available: I would love to know if everything works out of the box.

## License

Apache License version 2.0 - please read [LICENSE.txt](LICENSE.txt)

## Feedback

Bug reports, feature requests etc. are welcome. Please use Github for this.

## Externals

The application depend on [RestSharp](https://github.com/restsharp/RestSharp) for all communication with Jira.

All icons on buttons were downloaded from [Icons8](https://icons8.com).

## Changelog

<pre>
1.7.0     2016-06-25     New features/improvement
                           - Edit timer is now always enabled - you edit by double-clicking
                             the time field. 
                           - StopWatch now only runs single instance - it detects at startup
                             if another instance is running, and brings it into focus.
                           - New icon that is visible on dark Windows 10 taskbar theme.
                           - Timers now also support days - eg. 2d 13h 34m

                         Bugfixes:
                           - After version 1.6.1 the Jira session might  timeout. If this
                             happens, StopWatch will now automatically re-authenticate and
                             retry the requests. 
                           - When you activate many timers, the StopWatch window might be
                             too big for the screen. Now the window will not be higher than
                             desktop size, and instead a scrollbar gives access to the
                             remaining issues. 
                           - Minimize to tray sometimes did not show the system tray icon.
                           - Dropdown box with issue list only updated the description below
                             when leaving the field. Now it happens on selection + on <enter>
                             if you manually write a key.
                           - Requests to Jira API did not work, if the issue key had leading
                             spaces eg. " OPS-14". This has been fixed.

1.6.1     2016-04-18     Changed filter- and issue-loading from Jira API, to only happen
                         when comboboxes are opened, instead of every 30 seconds as before.

1.6.0     2016-04-09     New features:
                           - Allow multiple timers to run at the same time
                           - Choose if worklog text should be posted on worklog track or
                             comment track
                           - Display total time in bottom of window

                         Bugfixes:
                          - Issue description was not updated when manually typing an
                            issue key or deleting a key
                          - notifyIcon is not available on Mono, so on non-Windows
                            platforms, disable all minimze-to-tray code
                          - Fixed thread UI issue

                         Remade project structure to make crossplatform building easier

                         Refactoring of internal Jira communication (including NUnit
                         test-coverage)

1.5.0     2016-02-07     Option for pausing timer when locking your PC 
                         (eg. for lunch breaks)

                         Application can now be minimized to the system tray

                         Several bugfixes - for details see the commit history

1.4.1     2016-01-02     Added About dialog

1.4.0     2015-12-25     Worklog comments can now be saved without posting to Jira immediately.  Useful
                         if your task takes a long time and you want to note down your progress while
						 waiting to submit the worklog until the end.

						 New option to enable timers to be editable. Useful if you forgot to start the
						 timer when starting work. Times can be entered both Jira style like 1h 15m and
						 the "classic" way like 1.25h.

						 Thanks goes to [Seth Feldkamp](https://github.com/sfeldkamp) for the ideas to
						 both features and for testing.

1.3.1     2015-12-16     Cosmetic UI tweaks. My computer was running with a default zoom-level of 125%,
                         which meant that things did not look correct on default zoom level.

1.3.0     2015-11-11     Issues can now be selected from a list of available issues  - this list is
                         controlled by selecting between your favourite JQL filters

1.2.0     2015-10-19     Save time-tracking state, so stopwatch continue to "run" after quitting program

                         Automatic re-login, if Jira session has expired

                         Visual Jira connection status

                         Fixed tab-order on controls

1.1.1     2015-10-09     Fixed problems with main window being "Always on top" and the applications
                         other dialog boxes

1.1.0     2015-10-09     Changed all icons to https://icons8.com

                         New feature: Post worklog to Jira with a comment

1.0.5     2015-10-07     Nicer buttons + tooltips

1.0.4     2015-09-30     Clear summary label when issue key is empty

1.0.3     2015-09-28     Remember login credentials with DPAPI

1.0.2     2015-09-28     Integration with Jira: Async load issue summary

1.0.1     2015-09-25     First release with setup program
</pre>
