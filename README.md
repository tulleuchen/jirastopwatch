## Summary

A tool for recording time spent on different Jira tasks.

![](http://gehling.dk/jirastopwatch/screenshot.png)

### Features:

* Configure how many time-tracking slots you want available
* Integration to Jira REST API: Fetch task description when task-key has been entered (requires login)
* Time is reported in Jira time-logging format (eg. 2h 31m) to easily copy/paste into time-logging
* Jira issue keys are saved on program exit

### Planned features

* Posting spent time into Jira API
* Save time-tracking state, so your stopwatch continue to "run" even if you need to quit the program (e.g. you need to reboot, but still want to keep on recording time)
* Automatic re-login, if Jira session has expired

Feature-requests are more than welcome :-)

## License

Apache License version 2.0 - please read [LICENSE.TXT]

## Download

You can download a setup file with the latest version of Jira StopWatch on [SourceForge](https://sourceforge.net/projects/jirastopwatch/files)

## Usage

After install, start the application and click the settings icon (gears icon). Enter the real base URL for your Jira server and press OK. Then click on the padlock to login to Jira.

Now write a Jira task id in one of the white textboxes. When you leave the textbox, the task description will be fetched from your Jira server and displayed below the textbox. Repeat this for the tasks you are currently working on.

Press "Start" on one of your tasks. The time-tracking textbox will now turn green, and after the first minute has passed, the time elapsed will change from "0m" to "1m".

If you press "Start" on another task, the previous will automatically pause.

Click on "Reset" to... well... reset the time to 0m. :-)

Click on "Open" to open the task in your browser

That's it!


## Feedback

Bug reports, feature requests etc. are welcome. Please use Github for this.

## Changelog

<pre>
1.0.1     2015-09-25     First release with setup program
1.0.2     2015-09-28     Integration with Jira: Async load issue summary
1.0.3     2015-09-28     Remember login credentials with DPAPI
1.0.4     2015-09-30     Clear summary label when issue key is empty
1.0.5     2015-10-07     Nicer buttons + tooltips
</pre>
