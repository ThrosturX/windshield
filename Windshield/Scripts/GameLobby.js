$(document).ready(function () {
	var item = $("#board-id").html();
	alert(item);
	$.ajax({
		type: "GET",
		contentType: "application/json; charset=utf-8",
		url: "/Games/GetPlayersInGameLobby",
		data: { 'id': item},
		dataType: "json",
		success: function (users) {
			$(".list-of-players").remove();
			$("#lobby-template").tmpl(users).insertAfter("#userlist");
		},
		error: function () {
			$(".statistics-table-row").remove();
		}

	});

});