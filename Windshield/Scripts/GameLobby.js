$(document).ready(function () {
	var boardid = $("#board-id").html();
	var group = boardid;
	var hub = $.connection.gameHub;

	hub.client.Start = function (id) {
		self.location = "/../Games?idBoard=" + id;
	}

	hub.client.UpdateList = function () {
		$.ajax({
			type: "GET",
			contentType: "application/json; charset=utf-8",
			url: "/Games/GetPlayersInGameLobby",
			data: { 'id': boardid },
			dataType: "json",
			success: function (users) {
				$("#player-list").empty();
				$("#lobby-template").tmpl(users).appendTo("#player-list");
			},
			error: function () {
				alert("Developer error!");
			}

		});
	};

	$("#start-game-button").click(function (event) {
		event.preventDefault();
		self.location = "/../StartGame?idBoard=" + id;
		hub.server.startGame(group);
	})

	$.connection.hub.start().done(function () {
		hub.server.join(group);
		hub.server.refreshLobby(group);

	});
});