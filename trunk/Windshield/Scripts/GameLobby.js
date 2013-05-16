$(document).ready(function () {
	var boardid = $("#board-id").html();
	var group = boardid;
	var hub = $.connection.gameHub;

	hub.client.Start = function (id) {
		self.location = "JoinGame?idBoard=" + id;
	}

	hub.client.UpdateList = function () {
		$.ajax({
			type: "GET",
			contentType: "application/json; charset=utf-8",
			url: "/Games/GetPlayersInGameLobby",
			data: { 'id': boardid },
			dataType: "json",
			success: function (users) {
				$(".list-of-players").remove();
				$("#lobby-template").tmpl(users).insertAfter("#userlist");
			},
			error: function () {
				alert("Developer error!");
			}

		});
	};

	$.connection.hub.start().done(function () {
		hub.server.join(group);
		hub.server.refreshLobby(group);
	}); 
});