The chosen project was as follows:

Domain: Poker Hands
Interface: Web API
Platform: .Net
Language: C#

Assumptions:

- As per requirements suits have no impact on value and therefore there can be a draw/tie.
- Web page solicits player names and poker hands from user and POSTS to web api controller via JQuery, AJAX, JSON. 
- Response is http response code with custom message indicating winner, tie or error.
- Not fully 'RESTful...more RPC style for the service being provided.
- Should be able to run web app in IIS Express and step through debugger.
- Tests should run. Tests only currently hitting domain library objects which contain the business logic.

