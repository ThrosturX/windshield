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
				alert('yay');
				for (i = 0 ; i < gameRatings.length ; i++)
				{
					alert(gameRatings.length);
				}
			}
			
		});
});