<div class="row">
  <div class="column">

  </div>
  <div class="column">

  </div>
</div>
<p float="left">
    <a href="https://revolve.no/"><img align=left src="https://raw.githubusercontent.com/RevolveNTNU/RevolveTestDiary/tribe/.github/revolve.svg" width="30%"/></a>
</p>
<br>
<br>

# Revolve Test Diary
This is an offline, cross-platform .NET application, based on the Avalonia UI Framework. The purpose is to provide a lightweight diary, that can keep track of happenings over the course of a track day.

## How to Guide
Below you can find an introduction to the application, and a run-through of all the buttons in the toolbar.

### Getting Started
Simply download the executable from the latest version on Github: https://github.com/RevolveNTNU/RevolveTestDiary/releases

Launch the application and you will be greeted by the following screen:
<img align=left src="https://raw.githubusercontent.com/RevolveNTNU/RevolveTestDiary/tribe/.github/startscreen.png" width="100%"/>

You are now ready to go!

### Creating goals
The first section allows you to keep track of your goals for the day. Simply check them off as you reach them throughout the day.

<img align=left src="https://raw.githubusercontent.com/RevolveNTNU/RevolveTestDiary/tribe/.github/goals.png" width="100%"/>

### Sessions
Your day will be divided into sessions, for example one for setting up the track, one for a specific test run and one for cleaning up the track. Each session has a list of entries, where you can explain what is going on. You can choose between 4 types of entries:

* Comment - Just a simple comment
* Test run - A test run of the car
* Event - Something that has happened, but is not a test run
* Issue - An issue that you have encountered

<img align=left src="https://raw.githubusercontent.com/RevolveNTNU/RevolveTestDiary/tribe/.github/sessions.png" width="100%"/>

### Debrief
At the bottom you find the debrief section. You fill this in at the end of your day, answering three questions togehter:

1. What has gone well today?
2. What could have gone better today?
3. Issues discovered today?

<img align=left src="https://raw.githubusercontent.com/RevolveNTNU/RevolveTestDiary/tribe/.github/debrief.png" width="100%"/>

### Extra functionality
There are some buttons in the toolbar that allow for additional features.
<img align=left src="https://raw.githubusercontent.com/RevolveNTNU/RevolveTestDiary/tribe/.github/toolbar.png" width="100%"/>

#### Saving
All your logs are autosaved whenever you make changes to them. They are saved as JSON files to /My Documents/RevolveTestDiary. You can also force save, by pressing the "Save all TestDays" button or pressing Ctrl+S.

#### Loading
You can load a JSON file that was saved using the "Load a TestDay" button, or pressing Ctrl+O.

#### Creating a new TestDay
An additional tab, with a new test day can be created with the "Create a new TestDay" button, or pressing Ctrl+N.

#### Exporting to MarkDown
Your log can be exported to MarkDown with the "Export selected TestDay" button, or pressing Ctrl+E. This will create a .md file with your log, great for sharing!

```
# 24.07.2021 15:16:33

## Goals
- [X] Do two full Endurance events
- [ ] Do five full Skidpad events

## Sessions
### 24.07.2021 15:18:51 - Setting up the track
* 24.07.2021 15:22:44 - COMMENT:	A lot of brooming required today
* 24.07.2021 15:22:50 - COMMENT:	Slow start, people are just looking around
* 24.07.2021 15:22:56 - EVENT:		Track is ready!

### 24.07.2021 15:18:53 - Endurance 1
* 24.07.2021 15:19:05 - COMMENT:	Fresh tyres
* 24.07.2021 15:19:11 - COMMENT:	Mathias and Herbert are driving
* 24.07.2021 15:19:21 - TEST_RUN:	Endurance stint 1
* 24.07.2021 15:19:30 - COMMENT:	Driver change went very well, 2 min 45 sec
* 24.07.2021 15:19:37 - TEST_RUN:	Endurance stint 2
* 24.07.2021 15:19:50 - COMMENT:	Endurance completed, max cell temp 58.3

## Debrief

### What went well today?
Got to drive two endurance events

### What can be improved on from today?
Could be more efficient when setting up the track

### What issues did we discover?
The FL tyre has some leaking

```

#### Uploading to Revolve TestLog
Your log can also be uploaded to Revolve Testlog: vault.revolve.no/testlog. This is done with the "Upload selected TestDay" button, or pressing Ctrl+U. In order for this to work, you will have to specify the Test Phase you want to add your log to. You can find this ID on Testlog's season page, as demonstrated in the screenshot below: 
<img align=left src="https://raw.githubusercontent.com/RevolveNTNU/RevolveTestDiary/tribe/.github/testlog.png" width="100%"/>

When uploading, a new TrackDay is created, and all your entries, debrief notes and goals are added to that TrackDay.

<img align=left src="https://raw.githubusercontent.com/RevolveNTNU/RevolveTestDiary/tribe/.github/upload_testlog.png" width="100%"/>