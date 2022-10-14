[![Gitter chat](https://badges.gitter.im/jirastopwatch.png)](https://gitter.im/jirastopwatch "Gitter chat")

#######################################################
# Looking for a new maintainer for this repository!! #
# Contact me at tulleuchen@gmail.com
#######################################################

## Summary

A Windows desktop tool for recording time spent on different Jira tasks.

![](http://jirastopwatch.com/img/screen2.png)

## Features, download and installation:

Read all about features on [the product homepage](http://jirastopwatch.com).

Documentaion about [how to download, install, and use Jira StopWatch](http://jirastopwatch.com/doc/)
is also available.

Feature-requests are more than welcome :-)

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
2.2.0     2017-10-31     New features/improvements:
                           - Log work using actual start time (Thanks to Adam Conway)
                           - Use Jira's hour/day configuration when presenting time
                             (eg. 8 hours = 1 day) (Again thank you Adam Conway)
                           - Posting worklog will always round up recorded time to nearest minute.

2.1.0     2017-08-16     New features/improvements
                           - HTTPS connections are now forced to use TLS 1.1 or newer.
						   - Optional display of issue's project name.
						   - Added ellipsis for long issue summaries.

                         Bugfixes
						   - Fixed random disappearing issue summaries.
						   - Submit worklog accepts 0 without any unit as remaining time.
 
2.0.1     2017-04-10     Bugfixes
                           - System tray icon missing when minimizing to tray.
                           - Always use current screen instead of main screen for calculating
                             maximum height of main window.

2.0.0     2017-03-23     New features/improvements
                           - Added keyboard shortcuts to operate most important functions.
                           - Moved user credentials into settings window instead of
                             separate login window.
                           - Added Jira's default filter "My open issues" as hard-coded
                             first filter.
                           - Added a help button, that links to the new documentation homepage.

                         Bugfixes
                           - If the user config file got corrupt, StopWatch would not start.
                           - Worklog could not be submitted, if user's regional setting was
                             set to eg. Swedish because of a different date format.
                           - When manually editing timer, total time was not updated instantly.

1.9.0     2017-01-24     New features/improvements
                           - Issue rows are now added/removed directly from the UI instead of
                             from inside the settings dialog. (Thanks to Adam Conway for this)
                           - When posting worklog, remaining estimate can now be edited the same
                             way as in Jira. (Thanks to Adam Conway for this)
                           - Display version info on titlebar.
                           - Keep existing settings on version upgrade.
                           - Misc. UI improvements (coloring, repositioning UI items, etc.)
                           - Optionally autostart an issue (setting "In progress") when pressing play
                             on timer.
                           - Optional logging of Jira API communication for debugging purposes.
                           - Copy/paste of Jira URL into issue combobox now extracts the issue key.
                           - Click on "Not connected" will now display the connection problems in a dialog.

                         Bugfixes:
                           - Active timers total time did not get stored when using the setting
                             "pause active timer".

1.8.0     2016-10-05     New features/improvements:
                           - When submitting worklog, StartTime will also be set. The value
                             will be equal to "now minus logged work-time".
                             Kudos to [Lee Houghton](https://github.com/asztal)
                             for making this PR.
                           - UI now handles Windows' zoom settings much better.
                           - Timers and settings are now saved continously instead of only
                             at program exit. So no data loss if you get power-outs or PC crash.
                           - Issue dropdown box will now retrieve up to 200 issues, instead of
                             the API-default of 50.
							 
                         Bugfixes:
                           - Fixed random startup crashes.
                           - If issue keys are very long, they could be cropped in the dropdown
                             box. This has been fixed, so the key column width adapts to the
                             widest key.
                      
1.7.0     2016-06-25     New features/improvements:
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
