function startGame() {
    var Square = (function () {
        var self = this;
        function Square(x, y, pawns) {
            this.x = x;
            this.y = y;
            this.pawns = new Array()
            this.pawns.concat(pawns);
        }

        Square.prototype.clearPawns = function () {
            this.pawns.splice(0, 1);
        }

        return Square;
    }());
    var Pawn = (function () {
        function Pawn(id, x, y, playerNumber, startingX, startingY) {
            this.id = id;
            this.x = x;
            this.y = y;
            this.playerNumber = playerNumber;
            this.isInGame = false;
            this.startingX = startingX;
            this.startingY = startingY;
            this.FIELD = {
                STARTING_POSITION: -1,
                MAIN_FIELD: 0,
                INNER_RED_FIELD: 1,
                INNER_BLUE_FIELD: 2,
                INNER_GREEN_FIELD: 3,
                INNER_YELLOW_FIELD: 4,
            };
            this.position = {
                "field": this.FIELD.STARTING_POSITION,
                "position": -1
            };

            if (startingX === undefined) {
                startingX = 0;
            }
            if (startingY === undefined) {
                startingY = 0;
            }
        }

        Pawn.prototype.moveTo = function (positionX, positionY, pawnId) {
            var x = this.x;
            var y = this.y;

            $('div').filter(function () {
                var self = $(this);
                var boolean = self.css('bottom') === (x + 'px') &&
                    self.css('left') === (y + 'px') &&
                    self.attr('id') == (pawnId);
                return boolean;
            }).css({ "bottom": positionX + "px", "left": positionY + "px" });

            this.x = positionX;
            this.y = positionY;

            if (positionX != this.startingX) {
                this.isInGame = true;
            } else if (positionY != this.startingY) {
                this.isInGame = true;
            } else if (positionX == this.startingX
                && positionY == this.startingY) {
                this.isInGame = false;
            }

            var values = {
                "oldX": x,
                "oldY": y,
                "newX": positionX,
                "newY": positionY,
                "playerNumber": this.playerNumber,
                "id": this.id
            };

            return values;
        }

        return Pawn;
    }());
    var Player = (function () {
        this.startingPositions = [
            new Square()
        ];

        function Player(number) {
            this.score = 0;
            this.number = number;

            switch (number) {
                case 1: {
                    this.pawns = [
                        new Pawn(1, 565, 97, 1, 565, 97),
                        new Pawn(2, 565, 172, 1, 565, 172),
                        new Pawn(3, 620, 97, 1, 620, 97),
                        new Pawn(4, 620, 172, 1, 620, 172)
                    ];
                    this.startingPositions = [
                        new Square(565, 97, this.pawns[0]),
                        new Square(565, 172, this.pawns[1]),
                        new Square(620, 97, this.pawns[2]),
                        new Square(620, 172, this.pawns[3])
                    ];
                } break;
                case 2: {
                    this.pawns = [
                        new Pawn(1, 565, 565, 2, 565, 565),
                        new Pawn(2, 565, 640, 2, 565, 640),
                        new Pawn(3, 620, 565, 2, 620, 565),
                        new Pawn(4, 620, 640, 2, 620, 640)
                    ];
                    this.startingPositions = [
                        new Square(565, 565, this.pawns[0]),
                        new Square(565, 640, this.pawns[1]),
                        new Square(620, 565, this.pawns[2]),
                        new Square(620, 640, this.pawns[3])
                    ];
                } break;
                case 3: {
                    this.pawns = [
                        new Pawn(1, 97, 565, 3, 97, 565),
                        new Pawn(2, 97, 640, 3, 97, 640),
                        new Pawn(3, 153, 565, 3, 153, 565),
                        new Pawn(4, 153, 640, 3, 153, 640)
                    ];
                    this.startingPositions = [
                        new Square(97, 565, this.pawns[0]),
                        new Square(97, 640, this.pawns[1]),
                        new Square(153, 565, this.pawns[2]),
                        new Square(153, 640, this.pawns[3])
                    ];
                } break;
                case 4: {
                    this.pawns = [
                        new Pawn(1, 97, 97, 4, 97, 97),
                        new Pawn(2, 97, 172, 4, 97, 172),
                        new Pawn(3, 153, 97, 4, 153, 97),
                        new Pawn(4, 153, 172, 4, 153, 172)
                    ];
                    this.startingPositions = [
                        new Square(97, 97, this.pawns[0]),
                        new Square(97, 172, this.pawns[1]),
                        new Square(153, 97, this.pawns[2]),
                        new Square(153, 172, this.pawns[3]),
                    ];
                } break;
                default: throw new Error("The player number is wrong, it can be of value 1, 2, 3 or 4.");
                    break;
            }
        }

        Player.prototype.createPawns = function () {
            var pawnClass = "";

            switch (this.number) {
                case 1: pawnClass = "redPawn"; break;
                case 2: pawnClass = "bluePawn"; break;
                case 3: pawnClass = "greenPawn"; break;
                case 4: pawnClass = "yellowPawn"; break;
            }

            for (var i = 0; i < 4; i++) {
                var div = document.createElement("div");
                var string = "bottom:" + this.pawns[i].x + "px;left:" + this.pawns[i].y + "px;";
                div.setAttribute("style", string);
                div.setAttribute("class", pawnClass);
                div.setAttribute("id", (i + 1) + pawnClass);
                var temp = document.createElement("div");
                temp.appendChild(div);
                $("div[id=wrapper]").append(temp.innerHTML);
            }
        };

        return Player;
    }());
    var Game = (function () {
        function Game(field, innerRedField, innerGreenField, innerBlueField,
            innerYellowField, player1, player2, player3, player4) {
            this.field = field;
            this.innerRedField = innerRedField;
            this.innerGreenField = innerGreenField;
            this.innerBlueField = innerBlueField;
            this.innerYellowField = innerYellowField;
            this.diceNumber = 0;
            this.players = [
                player1,
                player2,
                player3,
                player4
            ];
            this.possiblePositions = {};
            this.currentPlayer = 1;
        }

        Game.prototype.movePawnTo = function (pawn, x, y) {
            var player = this.players[pawn.playerNumber - 1];

            var pawnClass = "";
            switch (pawn.playerNumber) {
                case 1: pawnClass = "redPawn"; break;
                case 2: pawnClass = "bluePawn"; break;
                case 3: pawnClass = "greenPawn"; break;
                case 4: pawnClass = "yellowPawn"; break;
            }

            var values = player.pawns[pawn.id - 1].moveTo(x, y, (pawn.id.toString() + pawnClass));
            this.game = removePawn(this, values);
            this.game = setPawn(this, values, player.pawns[pawn.id - 1]);
        };
        Game.prototype.getPositionsOfPawns = function () {
            function getSteppes(field, playerNumber) {
                var steppes = [];

                for (var i = 0; i < field.length; i++) {
                    for (var j = 0; j < field[i].pawns.length; j++) {
                        if (field[i].pawns[j].playerNumber == playerNumber) {
                            var step = {
                                "field": field[i].pawns[j].position.field,
                                "position": field[i].pawns[j].position.position,
                                "pawn": field[i].pawns[j]
                            };

                            steppes.push(step);
                            break;
                        }
                    }
                }

                return steppes;
            }

            var playerPawnsInGame = 0;
            var player = this.players[this.currentPlayer - 1];

            for (var i = 0; i < player.pawns.length; i++) {
                if (player.pawns[i].isInGame == true) {
                    playerPawnsInGame++;
                }
            }

            var steppes = getSteppes(this.field, this.currentPlayer);
            switch (this.currentPlayer) {
                case 1: steppes = steppes.concat(getSteppes(this.innerRedField, this.currentPlayer)); break;
                case 2: steppes = steppes.concat(getSteppes(this.innerBlueField, this.currentPlayer)); break;
                case 3: steppes = steppes.concat(getSteppes(this.innerGreenField, this.currentPlayer)); break;
                case 4: steppes = steppes.concat(getSteppes(this.innerYellowField, this.currentPlayer)); break;
            }

            return steppes;
        };
        Game.prototype.getPossiblePositions = function (steppes) {
            var validSteppes = [];

            if (this.diceNumber == 6) {
                var pawnToPutInGame = undefined;

                for (var i = 0; i < 4; i++) {
                    if (this.players[this.currentPlayer - 1].pawns[i].isInGame == false) {
                        pawnToPutInGame = this.players[this.currentPlayer - 1].pawns[i];
                        break;
                    }
                }

                var position = 0;
                switch (this.currentPlayer) {
                    case 1: position = 0; break;
                    case 2: position = 39; break;
                    case 3: position = 26; break;
                    case 4: position = 13; break;
                }

                if (pawnToPutInGame != undefined) {
                    validSteppes.push({ "field": 0, "position": position, "pawn": pawnToPutInGame });
                }
            }

            for (var i = 0; i < steppes.length; i++) {
                var fieldId = steppes[i].field;

                if (fieldId == 0) {
                    var nextPosition = steppes[i].position + this.diceNumber;

                    if (this.currentPlayer == 1) {
                        if (nextPosition < 52) {
                            validSteppes.push({ "field": fieldId, "position": nextPosition, "pawn": steppes[i].pawn });
                        }
                        else if (nextPosition < 58) {
                            validSteppes.push({ "field": 1, "position": (nextPosition - 52), "pawn": steppes[i].pawn });
                        }
                    } else if (this.currentPlayer == 2) {
                        if (nextPosition > 51) {
                            nextPosition -= 52;
                        }

                        if (nextPosition < 39) {
                            validSteppes.push({ "field": fieldId, "position": nextPosition, "pawn": steppes[i].pawn });
                        }
                        else if (nextPosition < 45 && steppes[i].position < 39) {
                            validSteppes.push({ "field": 2, "position": (nextPosition - 39), "pawn": steppes[i].pawn });
                        } else if (steppes[i].position >= 39) {
                            validSteppes.push({ "field": fieldId, "position": nextPosition, "pawn": steppes[i].pawn });
                        }
                    } else if (this.currentPlayer == 3) {
                        if (nextPosition > 51) {
                            nextPosition -= 52;
                        }

                        if (nextPosition < 26) {
                            validSteppes.push({ "field": fieldId, "position": nextPosition, "pawn": steppes[i].pawn });
                        }
                        else if (nextPosition < 32 && steppes[i].position < 26) {
                            validSteppes.push({ "field": 3, "position": (nextPosition - 26), "pawn": steppes[i].pawn });
                        } else if (steppes[i].position >= 26) {
                            validSteppes.push({ "field": fieldId, "position": nextPosition, "pawn": steppes[i].pawn });
                        }
                    } else if (this.currentPlayer == 4) {
                        if (nextPosition > 51) {
                            nextPosition -= 52;
                        }

                        if (nextPosition < 13) {
                            validSteppes.push({ "field": fieldId, "position": nextPosition, "pawn": steppes[i].pawn });
                        }
                        else if (nextPosition < 19 && steppes[i].position < 13) {
                            validSteppes.push({ "field": 4, "position": (nextPosition - 13), "pawn": steppes[i].pawn });
                        } else if (steppes[i].position >= 13) {
                            validSteppes.push({ "field": fieldId, "position": nextPosition, "pawn": steppes[i].pawn });
                        }
                    }
                } else if (0 < fieldId && fieldId < 5) {
                    var nextPosition = steppes[i].position + this.diceNumber;

                    if (nextPosition < 6) {
                        validSteppes.push({ "field": fieldId, "position": nextPosition, "pawn": steppes[i].pawn });
                    }
                }
            }

            this.possiblePositions = validSteppes;
        };
        Game.prototype.markPossiblePositions = function () {
            for (var i = 0; i < this.possiblePositions.length; i++) {
                var position = this.possiblePositions[i];
                var x = -1;
                var y = -1;

                if (position.field == 0) {
                    x = this.field[position.position].x - 8;
                    y = this.field[position.position].y;
                } else {
                    switch (position.field) {
                        case 1: {
                            x = this.innerRedField[position.position].x - 8;
                            y = this.innerRedField[position.position].y;
                        }; break;
                        case 2: {
                            x = this.innerBlueField[position.position].x - 8;
                            y = this.innerBlueField[position.position].y;
                        }; break;
                        case 3: {
                            x = this.innerGreenField[position.position].x - 8;
                            y = this.innerGreenField[position.position].y;
                        }; break;
                        case 4: {
                            x = this.innerYellowField[position.position].x - 8;
                            y = this.innerYellowField[position.position].y;
                        }; break;
                    }
                }

                if (x < 250) {
                    x += 2;
                }

                var div = document.createElement("DIV");
                var string = "width:50px;height:50px;position:absolute;bottom:"
                    + x + "px;left:" + y + "px";
                div.setAttribute("style", string);
                div.setAttribute("class", "glow-effect possible-position");
                div.setAttribute("data-pawnid", position.pawn.id);

                var temp = document.createElement("DIV");
                temp.appendChild(div);
                $("#wrapper").append(temp.innerHTML);
            }
        };
        Game.prototype.checkForEnemyPawns = function (x, y) {
            for (var i = 0; i < this.field.length; i++) {
                var square = this.field[i];
                var increaseI = false;

                for (var j = 0; j < square.pawns.length; j++) {
                    var pawn = square.pawns[j];

                    if (pawn.x == x && pawn.y == y
                        && pawn.playerNumber != this.currentPlayer) {

                        increaseI = true;
                        this.movePawnTo(pawn, pawn.startingX, pawn.startingY);
                        this.players[pawn.playerNumber - 1].startingPositions[pawn.id - 1].pawns.push(pawn);
                    }
                }

                if (increaseI) {
                    i--;
                }
            }
        }

        // Helper functions for moving a Pawn.
        function changeBackground(x, y, playerNumber, sameColorPawnCount) {
            function createDiv(x, y, background) {
                var div = document.createElement("div");
                var str = "url('../images/" + background + ".png')";
                var string = "width:50px;height:50px;position:absolute;bottom:" + (x - 8) + "px;left:" + y + "px;background-image:" + str;
                div.setAttribute("style", string);
                div.setAttribute("class", "stacked-pawns");
                var temp = document.createElement("div");
                temp.appendChild(div);
                $("div[id=wrapper]").append(temp.innerHTML);
            }
            function hide(x, y) {
                $('div').filter(function () {
                    var self = $(this);
                    return self.css('bottom') === (x + 'px') &&
                           self.css('left') === (y + 'px')
                }).hide();
            }

            switch (playerNumber) {
                case 1:
                    switch (sameColorPawnCount) {
                        case 1: break;
                        case 2: createDiv(x, y, "redPawnsStacked2"); hide(x, y); break;
                        case 3: createDiv(x, y, "redPawnsStacked3"); hide(x, y); break;
                        case 4: createDiv(x, y, "redPawnsStacked4"); hide(x, y); break;
                    }
                    break;
                case 2:
                    switch (sameColorPawnCount) {
                        case 1: break;
                        case 2: createDiv(x, y, "bluePawnsStacked2"); hide(x, y); break;
                        case 3: createDiv(x, y, "bluePawnsStacked3"); hide(x, y); break;
                        case 4: createDiv(x, y, "bluePawnsStacked4"); hide(x, y); break;
                    }
                    break;
                case 3:
                    switch (sameColorPawnCount) {
                        case 1: break;
                        case 2: createDiv(x, y, "greenPawnsStacked2"); hide(x, y); break;
                        case 3: createDiv(x, y, "greenPawnsStacked3"); hide(x, y); break;
                        case 4: createDiv(x, y, "greenPawnsStacked4"); hide(x, y); break;
                    }
                    break;
                case 4:
                    switch (sameColorPawnCount) {
                        case 1: break;
                        case 2: createDiv(x, y, "yellowPawnsStacked2"); hide(x, y); break;
                        case 3: createDiv(x, y, "yellowPawnsStacked3"); hide(x, y); break;
                        case 4: createDiv(x, y, "yellowPawnsStacked4"); hide(x, y); break;
                    }
                    break;
            }
        }
        function setPawn(game, values, pawn) {

            // Check if someone wins.
            if (pawn.position.field == pawn.FIELD.INNER_RED_FIELD
                && (pawn.position.position + game.diceNumber) == 5) {
                game.players[0].score++;

                // Remove pawn from game.
                game.innerRedField[5].pawns = [];
                var id = pawn.id + "redPawn";
                $("#" + id).remove();

                // Change icon on top.
                $("#red-title div:nth-child(" + (game.players[0].score + 1) + ")")
                    .css("background-image", "url('../images/redPawn.png')");

                if (game.players[0].score == 4) {
                    alert("player 1 wins!");
                    location.reload();
                    return game;
                }
            } else if (pawn.position.field == pawn.FIELD.INNER_BLUE_FIELD
                && (pawn.position.position + game.diceNumber) == 5) {
                game.players[1].score++;

                // Remove pawn from game.
                game.innerBlueField[5].pawns = [];
                var id = pawn.id + "bluePawn";
                $("#" + id).remove();

                // Change icon on top.
                $("#blue-title div:nth-child(" + (game.players[1].score + 1) + ")")
                    .css("background-image", "url('../images/bluePawn.png')");

                if (game.players[1].score == 4) {
                    alert("player 2 wins!");
                    location.reload();
                    return game;
                }
            } else if (pawn.position.field == pawn.FIELD.INNER_GREEN_FIELD
                && (pawn.position.position + game.diceNumber) == 5) {
                game.players[2].score++;

                // Remove pawn from game.
                game.innerGreenField[5].pawns = [];
                var id = pawn.id + "greenPawn";
                $("#" + id).remove();

                // Change icon on top.
                $("#green-title div:nth-child(" + (game.players[2].score + 1) + ")")
                    .css("background-image", "url('../images/greenPawn.png')");

                if (game.players[2].score == 4) {
                    alert("player 3 wins!");
                    location.reload();
                    return game;
                }
            } else if (pawn.position.field == pawn.FIELD.INNER_YELLOW_FIELD
                && (pawn.position.position + game.diceNumber) == 5) {
                game.players[3].score++;

                // Remove pawn from game.
                game.innerYellowField[5].pawns = [];
                var id = pawn.id + "yellowPawn";
                $("#" + id).remove();

                // Change icon on top.
                $("#yellow-title div:nth-child(" + (game.players[3].score + 1) + ")")
                    .css("background-image", "url('../images/yellowPawn.png')");

                if (game.players[3].score == 4) {
                    alert("player 4 wins!");
                    location.reload();
                    return game;
                }
            }

            game = checkFieldForPosition(game, 0, values, pawn, pawn.FIELD.MAIN_FIELD);
            game = checkFieldForPosition(game, 0, values, pawn, pawn.FIELD.INNER_RED_FIELD);
            game = checkFieldForPosition(game, 0, values, pawn, pawn.FIELD.INNER_BLUE_FIELD);
            game = checkFieldForPosition(game, 0, values, pawn, pawn.FIELD.INNER_GREEN_FIELD);
            game = checkFieldForPosition(game, 0, values, pawn, pawn.FIELD.INNER_YELLOW_FIELD);
            game = checkFieldForPosition(game, 1, values, pawn, pawn.STARTING_POSITION);
            game = checkFieldForPosition(game, 2, values, pawn, pawn.STARTING_POSITION);
            game = checkFieldForPosition(game, 3, values, pawn, pawn.STARTING_POSITION);
            game = checkFieldForPosition(game, 4, values, pawn, pawn.STARTING_POSITION);

            return game;
        }
        function checkFieldForPosition(game, playerNumber, values, pawn, fieldId) {
            var field = {};
            switch (fieldId) {
                case -1:
                    switch (playerNumber) {
                        case 1: field = game.players[0].startingPositions;
                        case 2: field = game.players[1].startingPositions;
                        case 3: field = game.players[2].startingPositions;
                        case 4: field = game.players[3].startingPositions;
                    }
                    break;
                case 0: field = game.field; break;
                case 1: field = game.innerRedField; break;
                case 2: field = game.innerBlueField; break;
                case 3: field = game.innerGreenField; break;
                case 4: field = game.innerYellowField; break;
            }

            for (var i = 0; i < field.length; i++) {
                if (field[i].x == values.newX && field[i].y == values.newY) {
                    pawn.position.field = fieldId;
                    pawn.position.position = i;

                    // Check if the field contains friendly pawns
                    var sameColorPawnCount = 1;
                    for (var j = 0; j < field[i].pawns.length; j++) {
                        if (field[i].pawns[j].playerNumber == pawn.playerNumber) {
                            sameColorPawnCount++;
                        }
                    }

                    if (sameColorPawnCount > 1) {
                        changeBackground(pawn.x, pawn.y, pawn.playerNumber, sameColorPawnCount);
                    }

                    field[i].pawns.push(pawn);
                    return game;
                }
            }

            return game;
        }
        function removePawn(game, values) {
            game.field = checkFieldForPawn(game.field, values);
            game.innerRedField = checkFieldForPawn(game.innerRedField, values);
            game.innerBlueField = checkFieldForPawn(game.innerBlueField, values);
            game.innerGreenField = checkFieldForPawn(game.innerGreenField, values);
            game.innerYellowField = checkFieldForPawn(game.innerYellowField, values);

            if (values.playerNumber == 1) {
                game.players[0].startingPositions[values.id - 1].pawns = [];
            } else if (values.playerNumber == 2) {
                game.players[1].startingPositions[values.id - 1].pawns = [];
            } else if (values.playerNumber == 3) {
                game.players[2].startingPositions[values.id - 1].pawns = [];
            } else if (values.playerNumber == 4) {
                game.players[3].startingPositions[values.id - 1].pawns = [];
            }

            return game;
        }
        function checkFieldForPawn(field, values) {
            for (var i = 0; i < field.length; i++) {
                var square = field[i];

                if (square.x == values.oldX && square.y == values.oldY) {
                    for (var j = 0; j < square.pawns.length; j++) {
                        var pawn = square.pawns[j];
                        var pawnId = pawn.id;
                        var pawnClass = "";
                        switch (pawn.playerNumber) {
                            case 1: pawnClass = "redPawn"; break;
                            case 2: pawnClass = "bluePawn"; break;
                            case 3: pawnClass = "greenPawn"; break;
                            case 4: pawnClass = "yellowPawn"; break;
                        }
                        pawnId += pawnClass;

                        if (pawn.playerNumber == values.playerNumber
                            && pawn.id == values.id) {
                            square.pawns.splice(j, 1);

                            // Remove the old background.
                            var v = Number(values.oldX) - 8;
                            $('div.stacked-pawns').filter(function () {
                                var self = $(this);
                                return self.css('bottom') === (v + 'px') &&
                                       self.css('left') === (values.oldY + 'px')
                            }).remove();

                            // Add the new background.
                            changeBackground(values.oldX, values.oldY, pawn.playerNumber, square.pawns.length);

                            // Show the moved pawn.
                            $('div').filter(function () {
                                var self = $(this);
                                var boolean = self.css('bottom') === (values.newX + 'px') &&
                                    self.css('left') === (values.newY + 'px') &&
                                    self.attr('id') == (pawnId);
                                return boolean;
                            }).show();

                            // Show the last pawn from the stack.
                            if (square.pawns.length == 1) {
                                $('div').filter(function () {
                                    var self = $(this);
                                    var boolean = self.css('bottom') === (values.oldX + 'px') &&
                                        self.css('left') === (values.oldY + 'px') &&
                                        self.attr('id') == (square.pawns[0].id + pawnClass);
                                    return boolean;
                                }).show();
                            }

                            return field;
                        }
                    }
                }
            }

            return field;
        }

        return Game;
    }());
    var TurnChanger = (function () {
        function TurnChanger() {
            this.number = 1;
        }

        TurnChanger.prototype.change = function () {
            this.number++;
            if (this.number == 5) {
                this.number = 1;
            }

            switch (this.number) {
                case 1: $("#turn").text("Player " + this.number + " turn");
                case 2: $("#turn").text("Player " + this.number + " turn");
                case 3: $("#turn").text("Player " + this.number + " turn");
                case 4: $("#turn").text("Player " + this.number + " turn");
            }
        }

        return TurnChanger;
    }());

    function changeTurn() {
        switch (turnChanger.number) {
            case 1:
                $("#red-title").removeClass("glow-effect-red");
                $("#blue-title").addClass("glow-effect-blue");
                break;
            case 2:
                $("#blue-title").removeClass("glow-effect-blue");
                $("#green-title").addClass("glow-effect-green");
                break;
            case 3:
                $("#green-title").removeClass("glow-effect-green");
                $("#yellow-title").addClass("glow-effect-yellow");
                break;
            case 4:
                $("#yellow-title").removeClass("glow-effect-yellow");
                $("#red-title").addClass("glow-effect-red");
                break;
        }

        turnChanger.change();
        game.currentPlayer = turnChanger.number;
        rollIsEnabled = true;
        $("#roll_dice_button").attr("class","glow-effect");
        $("#dice").removeAttr('style');
        game.possiblePositions = {};
        game.diceNumber = 0;
    }
    function rollDice() {
        var number = Math.floor((Math.random() * 6) + 1);

        $("#dice").css({
            "background-image": "url('../images/dice-sprite.png')",
            "background-repeat": "no-repeat"
        });
        switch (number) {
            case 1: $("#dice").css("background-position", "-2px -2px"); break;
            case 2: $("#dice").css("background-position", "-57px -2px"); break;
            case 3: $("#dice").css("background-position", "-112px -2px"); break;
            case 4: $("#dice").css("background-position", "-167px -2px"); break;
            case 5: $("#dice").css("background-position", "-222px -2px"); break;
            case 6: $("#dice").css("background-position", "-277px -2px"); break;
            default:
                break;
        }

        return number;
    }
    function createGame() {
        function addSquares(startBottom, startLeft, stepBottom, stepLeft) {
            var field = [];
            var bottom = startBottom;
            var left = startLeft;

            for (var i = 0; i < 6; i++) {
                square = new Square(bottom, left, []);
                field.push(square);
                left += stepLeft;
                bottom += stepBottom;
            }

            return field;
        }
        function createField() {
            var field = [];
            var bottomStart = 310;
            var leftStart = 4;
            var square = {};


            var subfield1 = addSquares(310, 4, 0, 52);
            var subfield2 = addSquares(255, 315, -52, 0);
            subfield2.push(new Square(-4, 367, []));
            var subfield3 = addSquares(-4, 419, 52, 0);
            var subfield4 = addSquares(310, 471, 0, 52);
            subfield4.push(new Square(362, 731, []));
            var subfield5 = addSquares(414, 731, 0, -52);
            var subfield6 = addSquares(466, 419, 52, 0);
            subfield6.push(new Square(726, 367, []));
            var subfield7 = addSquares(726, 315, -52, 0);
            var subfield8 = addSquares(414, 263, 0, -52);
            subfield8.push(new Square(362, 3, []));

            field = subfield1.concat(subfield2, subfield3, subfield4, subfield5, subfield6, subfield7, subfield8);
            return field;
        }

        // Start of field creation.
        var field = createField();
        var innerRedField = addSquares(362, 55, 0, 52);
        var innerGreenField = addSquares(362, 679, 0, -52);
        var innerBlueField = addSquares(674, 367, -52, 0);
        var innerYellowField = addSquares(310, 367, -52, 0);
        innerYellowField = innerYellowField.reverse();
        // End of field creation.

        // Start - create players.
        var player1 = new Player(1);
        player1.createPawns();
        var player2 = new Player(2);
        player2.createPawns();
        var player3 = new Player(3);
        player3.createPawns();
        var player4 = new Player(4);
        player4.createPawns();
        // End - create players.

        return game = new Game(field, innerRedField, innerGreenField, innerBlueField, innerYellowField,
            player1, player2, player3, player4);
    }

    var rollIsEnabled = true;
    var game = createGame();
    var turnChanger = new TurnChanger();

    $("#roll_dice_button").click(function () {
        if (rollIsEnabled == true) {
            game.diceNumber = rollDice();
            rollIsEnabled = false;
            $("#roll_dice_button").attr("class", "not-active");

            var pawnPositions = game.getPositionsOfPawns();
            game.getPossiblePositions(pawnPositions);
            game.markPossiblePositions();
            
            $(".possible-position").click(function () {
                var pawnId = $(this).attr('data-pawnid');
                var pawn = {};

                // Get the pawn which has the clicked possible position.
                for (var i = 0; i < 4; i++) {
                    if (game.players[turnChanger.number - 1].pawns[i].id == pawnId) {
                        pawn = game.players[turnChanger.number - 1].pawns[i];
                        break;
                    }
                }

                // Get that position's index.
                var possiblePositionIndex = 0;
                for (var i = 0; i < game.possiblePositions.length; i++) {
                    if (game.possiblePositions[i].pawn.id == pawnId) {
                        possiblePositionIndex = i;
                        break;
                    }
                }
                
                // Move the pawn to the new position.
                var position = {};
                switch (game.possiblePositions[possiblePositionIndex].field) {
                    case 0:
                        position = game.field[game.possiblePositions[possiblePositionIndex].position];
                        // Check for enemy pawns.
                        game.checkForEnemyPawns(position.x, position.y, pawn.playerNumber);
                        game.movePawnTo(pawn, position.x, position.y);
                        break;
                    case 1:
                        position = game.innerRedField[game.possiblePositions[possiblePositionIndex].position];
                        game.movePawnTo(pawn, position.x, position.y);
                        break;
                    case 2:
                        position = game.innerBlueField[game.possiblePositions[possiblePositionIndex].position];
                        game.movePawnTo(pawn, position.x, position.y);
                        break;
                    case 3:
                        position = game.innerGreenField[game.possiblePositions[possiblePositionIndex].position];
                        game.movePawnTo(pawn, position.x, position.y);
                        break;
                    case 4:
                        position = game.innerYellowField[game.possiblePositions[possiblePositionIndex].position];
                        game.movePawnTo(pawn, position.x, position.y);
                        break;
                }

                // Add an additional turn if 6 is rolled.
                if (game.diceNumber == 6) {
                    rollIsEnabled = true; 
                    $("#roll_dice_button").attr("class", "glow-effect"); 
                    $("#dice").removeAttr('style'); 
                    game.possiblePositions = {};
                    game.diceNumber = 0;
                } else {
                    changeTurn();
                }

                $(".possible-position").remove();
            });

            if (game.possiblePositions.length == 0) {
                alert("No possible steppes for this turn!");
                changeTurn();
            }
        }
    });    
}


