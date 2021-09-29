# MonitorControl
A small Windows program that allows you to put your monitors to sleep via HTTP.  Also offers the ability to turn monitors back on, although this ability does not work on all systems.

## Usage

Download a release [from the releases tab](https://github.com/bp2008/MonitorControl/releases) and extract it.  Run MonitorControl.exe.

Find the system tray icon that looks like this, and double-click or right-click it to access the configuration window.

![application icon](https://i.imgur.com/8nPtNH5.png)

Configure the application as you desire, and use the web interface to remotely control your computer monitors.

![screenshot](https://i.imgur.com/YfDzXkt.png)

Configuration changes take effect immediately.

Closing the configuration window will not exit the program unless you click "Exit Program", or choose the "Exit" option from the tray icon's context menu.

## Web Interface

The web server responds with a basic guide explaining the available commands:

![screenshot](https://i.imgur.com/O1NmDge.png)

## Building From Source

To build from source, clone the repository and also my [BPUtil repository](https://github.com/bp2008/BPUtil).  Open in Visual Studio 2019 community edition.  You may need to remove and re-add the `BPUtil` project to the solution, and add `BPUtil` as a reference in the `MonitorControl` project.
