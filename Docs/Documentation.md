# Video Browser Application

## General

Video Browser Application is a Windows Desktop application to fetch, filter and brows videos.

## Requirements

### Narrative 1: User Controls in Main Window
 ```
As a User of VBA
I want a UI for the application
So that
	- there is uniform Button style
	- there is Buttons for Previous and Next page
	- there is Button for Load Videos
	- there is Info about current and all pages
	- there is a List for Videos
	- there is a combobox for Filter options
```

## Architecture Baselines

There is four module in Application
- VideoBrowser.Itf for Interfaces (lazy reference coupling)
- VideoBrowser.App for UI logic
- VideoBrowser.Common for common BusinessLogic infrastructure
- VideoBrowser.Client for BusinessLogic

### VideoBrowser.App

This module is responsible for the UI logic and the necessary Startup logic.