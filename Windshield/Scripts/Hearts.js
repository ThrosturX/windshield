var card_left = [];
var card_top = [];
var trick_left = [];
var trick_top = [];

function cardToShort(id) {
	var element = $("#" + id);
	var toString = "";

	if(element.hasClass("rank_A"))
	{
		toString += "A|";
	}
	else if (element.hasClass("rank_2"))
	{
		toString += "2|";
	}
	else if (element.hasClass("rank_3"))
	{
		toString += "3|";
	}
	else if (element.hasClass("rank_4"))
	{
		toString += "4|";
	}
	else if (element.hasClass("rank_5"))
	{
		toString += "5|";
	}
	else if (element.hasClass("rank_6"))
	{
		toString += "6|";
	}
	else if (element.hasClass("rank_7")) {
		toString += "7|";
	}
	else if (element.hasClass("rank_8")) {
		toString += "8|";
	}
	else if (element.hasClass("rank_9")) {
		toString += "9|";
	}
	else if (element.hasClass("rank_T")) {
		toString += "T|";
	}
	else if (element.hasClass("rank_J")) {
		toString += "J|";
	}
	else if (element.hasClass("rank_Q")) {
		toString += "D|";
	}
	else if (element.hasClass("rank_K")) {
		toString += "K|";
	}


	if(element.hasClass("Heart")) {
		toString += "H";
	}
	else if (element.hasClass("Spade")) {
		toString += "S";
	}
	else if (element.hasClass("Diamond")) {
		toString += "D";
	}
	else if (element.hasClass("Club")) {
		toString += "C";
	}

	return toString;
	
}

	function moveTricktoPosition() {
		for (i = 1; i <= 4 ; i++) {
			var el = document.getElementById("trick_" + i);
			el.style["zIndex"] = 10;
			el.style["left"] = trick_left[i];
			el.style["top"] = trick_top[i];
			// for some reason, I cant get the element with jquery :(
			//$("#trick_" + i).css({ "display": "inline" });

		}
	}

	function dealCards() {
		//CardPosition
		for (k = 1; k <= 13; k++) {
			card_left[k] = 23 + 3 * k;
			card_top[k] = 75;
		}
		for (k = 14; k <= 26; k++) {
			card_top[k] = -20 + 3 * k;
			card_left[k] = 10;
		}
		for (k = 27; k <= 39 ; k++) {
			card_left[k] = 23 + 3 * (k - 26);
			card_top[k] = 5;
		}
		for (k = 40; k <= 52 ; k++) {
			card_top[k] = -20 + 3 * (k - 26);
			card_left[k] = 80;
		}

		for (i = 1; i <= 52; i++) setTimeout("moveToPlace(" + i + ")", i * 100);
	}

	function moveToPlace(id) {
		el = document.getElementById(id);
		//var el = $("#card_"+ id);
		el.style["zIndex"] = 10;
		el.style["left"] = card_left[id] + "%";
		el.style["top"] = card_top[id] + "%";
		//el.style["WebkitTransform"] = "rotate(180deg)";
		el.style["zIndex"] = 0;

		if ((id > 13 && id < 27) || (id > 39 && id <= 52))
			el.style["WebkitTransform"] = "rotate(90deg)";
	}

	function showPlayer1Cards() {
		for (i = 1 ; i <= 13 ; i++) {
			var c = $("#" + i);
		
		
		}
	}

	function setTrickPosition() {

		trick_left[1] = "44%";
		trick_top[1] = "50%";
		trick_left[2] = "32%";
		trick_top[2] = "41%";
		trick_left[3] = "44%";
		trick_top[3] = "29%";
		trick_left[4] = "57%";
		trick_top[4] = "41%";

	}

	$(document).ready(function () {
		var group = $("#group-span").html();
		var hub = $.connection.gameHub;
		var currentUser = $("#currentUser-span").html();
		var playerOne = $("#playerOne-span").html();
		var playerTwo = $("#playerTwo-span").html();
		var playerThree = $("#playerThree-span").html();
		var playerFour = $("#playerFour-span").html();
		var card_left = [];
		var card_top = [];

		$("#GameRestart").hide();
		$("#winner-dialog-popup").hide();

		function setCard(card, pos) {
			var suit;
			var rank = "rank_" + card[0];

			$("#" + pos).show();

			if (card[1] === "H") {
				suit = "Heart";
			}
			else if (card[1] === "S") {
				suit = "Spade";
			}
			else if (card[1] === "D") {
				suit = "Diamond";
			}
			else if (card[1] === "C") {
				suit = "Club";
			}
			else {
				$("#" + pos).hide();
			}
			$("#" + pos).removeClass();
			$("#" + pos).addClass(rank + " " + suit + " card");
			//if (($(".spanDisplay_" + pos).is(':empty'))) {
			//alert($(".spanDisplay_" + pos).text());
		}

		hub.client.Broadcast = function (status) {
			var statusArray = status.split('|');
			var hArray = statusArray[1];
			var handArray = hArray.split('/');
			var index;

			// find which player's cards should be picked
			if (currentUser === playerOne) {
				index = 0;
			}
			else if (currentUser === playerTwo) {
				index = 1;
			}
			else if (currentUser === playerThree) {
				index = 2;
			}
			else if (currentUser === playerFour) {
				index = 3;
			}
			else {
				console.log("There was an error parsing the users.");
				console.log("CurrentUser: " + currentUser);
				console.log("Players:");
				console.log(playerOne);
				console.log(playerTwo);
				console.log(playerThree);
				console.log(playerFour);
			}

			var cardArray = handArray[index].split(",");

			// display the player's cards
			for (var i = 1; i <= 13; i++) {
				// extract card from string
				var cardstring = cardArray[i - 1];
				$("#" + i).removeClass('card');

				setCard(cardstring, i);
			}

			// place any trick cards into their positions
			var tArray = statusArray[0].split(',');

			console.log(tArray);

			for (var i = 1; i <= 4; i++) {
				console.log(i);
				var cString = tArray[i].split('-');
				console.log(cString);
				var cardstring = cString[0];

				setCard(cardstring, "trick_" + i);
				console.log(i);
			}

			console.log(status);
		};

		hub.client.GameOver = function (winner) {
			if (winner != "") {
				$("#winner-dialog-popup").text(winner + " wins!");
				if (winner == currentUser) {
					$("#GameWins").html(parseInt($("#GameWins").html(), 10) + 1);
				}
				else {
					$("#GameLoss").html(parseInt($("#GameLoss").html(), 10) + 1);
				}
			}
			else {
				$("#winner-dialog-popup").text("It's a draw!");
				$("#GameDraws").html(parseInt($("#GameDraws").html(), 10) + 1);
			}

			$("#winner-dialog-popup").show(2);

		}

		$.connection.hub.start().done(function () {
			hub.server.join(group);
			hub.server.startGame(group);
			hub.server.tryAction(group, "checkAI", currentUser);

			$("#felt").on('click', '.card', function () {
				hub.server.tryAction(group, "play|" + cardToShort(this.id), currentUser);
			});

			setTimeout(function () {
				hub.server.tryAction(group, "refresh", currentUser);
			}, 2000);

		});

		setTimeout(function () {
			dealCards();
			setTrickPosition();
			moveTricktoPosition();
			//setTimeout("displayTrick()", 5700);
			//displayTrick();
		}, 700);
	});