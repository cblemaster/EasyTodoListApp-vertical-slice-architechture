
# EasyTodoListApp
## About
+ An application that provides todo functionality, nothing fancy, just the basics
+ My first time using vertical slice architecture and the MediatR library
## Acknowledgement
+ Many thanks to Ziggy Rafiq, whose write up of VSA caught my attention because it was the first I encountered that included code examples (in fact, a complete application)
+ https://github.com/ziggyrafiq/ECommerceApp-VerticalSlice/tree/main
## Objectives
+ Complete my first project that uses VSA
+ Understand some advantages and disadvantages of using VSA
+ Make the app extensible so that adding features is easy
## Use cases
|#|Use case|Description|
|-|-|-|
|1|See all todos|As a user, I want to see an interactive list of my todos<br />All information related to a todo should be available in this view<br />The list should be sorted by due date desc, then description<br />This list excludes completed todos, which are visible elsewhere|
|2|See due today todos|As a user, I want to see an interactive list of my todos that are due today<br />All information related to a todo should be available in this view<br />The list should be sorted by description<br />This list excludes completed todos, which are visible elsewhere|
|3|See important todos|As a user, I want to see an interactive list of my todos that are important<br />All information related to a todo should be available in this view<br />The list should be sorted by due date desc, then description<br />This list excludes completed todos, which are visible elsewhere|
|4|See completed todos|As a user, I want to see an interactive list of my todos that are complete<br />All information related to a todo should be available in this view<br />The list should be sorted by due date desc, then description|
|5|See overdue todos|As a user, I want to see an interactive list of my todos that are due prior to today<br />All information related to a todo should be available in this view<br />The list should be sorted by due date desc, then description<br />This list excludes completed todos, which are visible elsewhere|
|6|Create todo|As a user, I want to create todos<br />The description for the new todo is required, cannot be all whitespace characters, and must be 100 or fewer characters in length<br />The due date for the new todo is optional, and if provided can be past, present, or future<br />The create date for the new todo is set and never subsequently modified|
|7|Update todo|As a user, I want to update todos<br />I want to be able to update the todo decription and/or the due date<br />The description for the todo is required, cannot be all whitespace characters, and must be 100 or fewer characters in length<br />The due date for the todo is optional, and if provided can be past, present, or future<br />Completed todos cannot be updated, except to toggle back to incomplete<br />The update date for the todo is set |
|8|Mark todo incomplete|As a user, I want to mark completed todos as not complete<br />The update date for the todo is set|
|9|Delete todo|As a user, I want to delete todos<br />Important todos that are not complete cannot be deleted|
## Built with
+ .NET 9/ C# 13
+ Microsoft Visual Studio Community 2022
+ MediatR 12.4.1 https://www.nuget.org/packages/MediatR/12.4.1
+ Entity Framework Core 9.0.0
+ SQLite database provider for EF 9.0.0
+ Microsoft CommunityToolkit.Mvvm 8.4.0
+ Microsoft.Extensions.DependencyInjection 9.0.0
+ Visual Studio Code (for jupyter notebooks)
+ Mermaid js (for architecture diagram)
## Improvement opportunities / new features
+ Keep .NET version up to date
+ Keep nuget package versions up to date
+ Update, or even ditch, the documentation
+ Test suite: unit tests, db integration tests, api endpoint tests, what else?
+ Desktop project
	+ Look for external style/theme lib to apply to UI elements, to improve overall appearance and styling
    + Improve UI appearance with grid layout, sorting, paging, filtering
    + Arrange the UI nav vertically and decide if it looks better
    + The 'responses' models returned from the client data service are implemented clumsily: used (exclusively?) to pass string messages back to the caller
    + Serializing and deserializing json could be moved from the client data service class to a separate class
    + Too much 'UI stuff' is happening in the page models, move this into command processors
    + Getting a sequence of todos could be a common operation with different predicates passed in (decided with something more elegant than the switch statement used now)
    + Add web UI (Blazor? Angular/TS?)
+ App project
   + Ditch the controller in favor of endpoints
   + Implement domain services, where all of the business rules and validation rules live
   + Currently business logic and validation are in the request handlers; instead the request handlers should call domain services for business rule and validation checks
   + Dig into MediatR and how I can get more out of it
