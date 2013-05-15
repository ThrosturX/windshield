$(document).ready(function () {

	$(".btn").click(function () {
		var item = $(this).html();
		$.ajax({
			type: "GET",
			contentType: "application/json; charset=utf-8",
			url: "/Home/GetPopularity",
			data: { 'name': item },
			dataType: "json",
			success: function (popularGames) {
				$(".popularlink").remove();
				$("#popularity-template").tmpl(popularGames).appendTo("#gamelist");

			},
			error: function () {
				//$(".statistics-table-row").remove();
			}

		});

	});


	
});