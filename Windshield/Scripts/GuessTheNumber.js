
$(document).ready(function () {
	/* retrieves neccesary information from the DOM */
	var group = $("#group-span").html();
	var hub = $.connection.gameHub;
	var currentUser = $("#current-user-span").html();
	var playerOne = $("#player-one").html();
	var playerTwo = $("#player-two").html();
	var playerThree = $("#player-three").html();
	var playerFour = $("#player-four").html();

	/* hides the restart button until the game has been finished */
	$("#game-restart").hide();

	/* the function that intercepts the answer from the server */
	hub.client.Broadcast = function (status) {

		/* split the status string */
		var statusArray = status.split('|');

		$("#guess-the-number-prompt").text(statusArray[3] + " can now make his guess");

		$("#guess-the-number-from").text(statusArray[1]);
		$("#guess-the-number-to").text(statusArray[2]);
		$("#player-one-points").text(statusArray[4].split(',')[1]);
		$("#player-two-points").text(statusArray[5].split(',')[1]);
		$("#player-three-points").text(statusArray[6].split(',')[1]);
		$("#player-four-points").text(statusArray[7].split(',')[1]);
		/* console.log(status); */
	};

	// a function that is called when the server determines that the game is over
	hub.client.GameOver = function (winner) {
		$("#guess-the-number-prompt").text(winner + " has guessed the correct number!");
	}

	$.connection.hub.start().done(function () {
		hub.server.join(group);
		hub.server.startGame(group);
		hub.server.tryAction(group, "checkAI", currentUser);

		$("#guess-the-number-send").click(function () {
			hub.server.tryAction(group, $("#guess-the-number-guess").val(), currentUser);
			$("#guess-the-number-guess").val("");
		});

		$("#guess-the-number-guess").bind("keydown", function (e) {

			if (e.keyCode === 13) {  // 13 is enter key
				hub.server.tryAction(group, $("#guess-the-number-guess").val(), currentUser);
				e.preventDefault();
				$("#guess-the-number-guess").val("");
			}

		});

		$("#game-restart").click(function () {
			hub.server.tryAction(group, "checkAI", currentUser);
			//setTimeout(function () { hub.server.refresh(group); }, 1250);
			$("#game-restart").hide();
		});

		setTimeout(function () {
			hub.server.tryAction(group, "refresh", currentUser);
		}, 2000);

	});

});