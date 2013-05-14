$(document).ready(function () {

	$(".btn").click(function () {
		//alert('button clicked');
		var item = $(this).html();

		$.ajax({
			type: "GET",
			contentType: "application/json; charset=utf-8",
			url: "/Home/GetPopularity",
			data: { 'name': item },
			dataType: "json",
			success: function (popularGames) {
				alert('jeij')
				$(".thumbnails").remove();
				$("#popularity-template").tmpl(popularGames).insertAfter(".thumbnails");

			},
			error: function () {
				$(".statistics-table-row").remove();
			}

		});

	});


	
});