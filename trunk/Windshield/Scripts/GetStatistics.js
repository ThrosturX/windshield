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
				
					$(".statsrow").remove();
					$("#StatsTemplate").tmpl(gameRatings).insertAfter("#statbody");
			},
			error: function () {
				$(".statsrow").remove();
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
					
						$(".statsrow").remove();
						$("#StatsTemplate").tmpl(gameRatings).insertAfter("#statbody");
					
				},
				error: function () {
					$(".statsrow").remove();
				}
			});
		});
});