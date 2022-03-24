# CPS_Backend_API
This project is built to hit CPS' API, rename a field, and return it as a JSON via my own endpoint.

This project is built using Microsoft's .Net Core 3.1.
To run this API, you will need to go into the following folder path and run the CPS_Backend_API.exe. 
This will have the API running, and the end point to hit is http://localhost:5000/CPS. If running the application via VisualStudio 2019/2022, the end point to hit will be https://localhost:44354/CPS.

Installation requirements:
.Net Core 3.1
IIS

What to expect within the project:
The project is using the CPSController to hit CPS' API to receive a list of calendars and their properties as a JSON response. This controller first hits the API, catches the response and checks its status.
If it has a success status code, it will go ahead and parse the JSON response in another method (GetCalendars). The "GetCalendars" will then return a list of the newly modified list of calendars as a new Model (CalendarModel).
This new list of CalendarModel is then returned as a JSON response to the user.
At the time being due to my lack of knowledge, I am currently not returning an error code to the user if the CPS API fails to return a list of calendars, or if it returns an error code. For now, it is returning an empty list of calendars and printing the error code.

Feel free to provide any advice or criticism as I am always looking forward to learning more. Thanks for your time for reading this documentation of the project!

