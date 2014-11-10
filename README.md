=================
BugTrackingSystem folder contains 4 projects.

The first project is the Bug tracking system's domain. It contains the repositories, unit of work classes, dbContext, 
the models and others.

The second project is the ASP.NET MVC application. It allows us to apply CRUD operations on the bugs. Add comments.
Paging is implemented and the switching between paging is done with AJAX. The application uses bootstrap to apply styles.

The third project is the ASP.NET Web API application. It allows us to to apply CRUD operations on the bugs by making
http requests to the server.

The fourth project contains the unit tests of the web api and mvc projects.

All projects uses the same local database, which is created at the first start of the application by entity framework.

=================
DogWebSite-Hangman-LUDO folder contains the LUDO game, Hangman game and a static dog related website.

- The LUDO game is created using HTML, CSS, JavaScript and JQuery. The game has a field and 4 players. 
Each player is a different colour and have 4 pawns. There is a dice which should be thrown after each turn. 
If 6 is thrown a player can put a pawn in the game in his specific starting square. If 6 is thrown the player 
gets an aditional turn. The pawns only move in one direction. When a pawn reaches it's player's starting square 
it can go inside the player's field. When a player take all of his pawns inside his field he wins the game. 
If a player step on top of an other player's pawns they are returned back to the starting area.

- The hangman game is created using HTML, CSS, JavaScript and JQuery. The game starts by clicking "New game". 
After that a word is generated and the player can start choosing letters. The player has 6 tries. If he fails 
to guess the word, he looses.

- The static dog website is created using HTML, CSS, JQuery. The website is simple and it only contains pages 
with links between them.
