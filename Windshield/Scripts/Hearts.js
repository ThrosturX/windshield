var card_left = [];
var card_top = [];

$(document).ready(function () {

	$(".cardback").click(function () {
		dealCards();
	});

});



function dealCards() {
	//CardPosition
	for (k = 1; k <= 13; k++) {
		card_left[k] = 23 + 3 * k;
		card_top[k] = 75;
	}
	for (k = 14; k <= 26; k++) {
		card_top[k] = -20 + 3 * k;
		card_left[k] = 10;
	}
	for (k = 27; k <= 39 ; k++) {
		card_left[k] = 23 + 3 * (k - 26);
		card_top[k] = 5;
	}
	for (k = 40; k <= 52 ; k++) {
		card_top[k] = -20 + 3 * (k - 26);
		card_left[k] = 80;
	}

	for (i = 1; i <= 52; i++) setTimeout("moveToPlace(" + i + ")", i * 100);
}

function moveToPlace(id) {
	el = document.getElementById(id);
	//var el = $("#card_"+ id);
	el.style["zIndex"] = 10;
	el.style["left"] = card_left[id] + "%";
	el.style["top"] = card_top[id] + "%";
	//el.style["WebkitTransform"] = "rotate(180deg)";
	el.style["zIndex"] = 0;

	if ((id > 13 && id < 27) || (id > 39 && id <= 52))
		el.style["WebkitTransform"] = "rotate(90deg)";
}

function showPlayer1Cards() {
	for (i = 1 ; i <= 13 ; i++) {
		var c = $("#" + i);
		
		
	}
}