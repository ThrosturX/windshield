$(document).ready(function () {

	$(".game-images-radio-button").click(function () {
		var item = $(this).html();
		$.ajax({
			type: "GET",
			contentType: "application/json; charset=utf-8",
			url: "/Home/GetPopularity",
			data: { 'name': item },
			dataType: "json",
			success: function (popularGames) {
				$("#game-images").empty();
				$("#popularity-template").tmpl(popularGames).appendTo("#game-images");

			},
			error: function () {
				$("#game-images").empty();
			}

		});

	});


	
});