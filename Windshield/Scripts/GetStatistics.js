$(document).ready(function () {
	
	var items = document.getElementById('dropdown');
	var opt = items.options[items.selectedIndex].text;
	
		$.ajax({
			type: "GET",
			contentType: "application/json; charset=utf-8",
			url: "/Home/GetStatistics",
			data: {'name' : opt},
			dataType: "json",
			success: function (gameRatings) {
				
					$(".statistics-table-row").remove();
					$("#statistics-table-row-template").tmpl(gameRatings).insertAfter("#statistics-table-body");
			},
			error: function () {
				$(".statistics-table-row").remove();
			}
			
		});

		$("#dropdown").change(function () {
			items = document.getElementById('dropdown');
			opt = items.options[items.selectedIndex].text;

			$.ajax({
				type: "GET",
				contentType: "application/json; charset=utf-8",
				url: "/Home/GetStatistics",
				data: { 'name': opt },
				dataType: "json",
				success: function (gameRatings) {
					
						$(".statistics-table-row").remove();
						$("#statistics-table-row-template").tmpl(gameRatings).appendTo("#statistics-table-body");
					
				},
				error: function () {
					$(".statistics-table-row").remove();
				}
			});
		});
});