var card_left = [];
var card_top = [];

function Card(rank, suit) {

	this.rank = rank;
	this.suit = suit;

	this.toString = cardToString;
	this.createNode = cardCreateNode;
}

$(document).ready(function () {
	


	$(".cardback").click(function () {
		dealCards();
	});

});

function dealCards() {

	dealCards(1);
	
	/*for (k = 13; k < 26; k++) {
		card_top[k] = -50 + 40 * k;
		card_left[k] = 50;
	}
	for (k = 26; k < 39 ; k++) {
		card_left[k] = 340 + 40 * (k - 12);
		card_top[k] = 0;
	}
	for (k = 39; k < 52 ; k++) {
		card_top[k] = -50 + 40 * (k - 18);
		card_left[k] = 700;
	}


	for (i = 1; i <= 52; i++) setTimeout("moveToPlace(" + i + ")", i * 100);
	started = true; */

}

function dealCards(playerNo)
{
	
		for (k = 1; k <= 13; k++) {
			card_left[k] = 240 + 40 * k;
			card_top[k] = 530;
		}
		for (k = 14; k <= 26; k++) {
			card_top[k] = -500 + 40 * k;
			card_left[k] = 50;
		}
		for (k = 27; k <= 39 ; k++) {
			card_left[k] = 235 + 40 * (k - 26);
			card_top[k] = 0;
		}
		for (k = 40; k <= 52 ; k++) {
			card_top[k] = 15 + 40 * (k - 39);
			card_left[k] = 970;
		}
		
		for (i = 1; i <= 52; i++) setTimeout("moveToPlace(" + i + ")", i * 100);
	

	showPlayer1Cards();

}

function moveToPlace(id) {
	el = document.getElementById("card_" + id);
	//var el = $("#card_"+ id);
	el.style["zIndex"] = 10;
	el.style["left"] = card_left[id] + "px";
	el.style["top"] = card_top[id] + "px";
	//el.style["WebkitTransform"] = "rotate(180deg)";
	el.style["zIndex"] = 0;

	if ((id > 13 && id < 27) || (id > 39 && id <= 52))
		el.style["WebkitTransform"] = "rotate(90deg)";
}
function showPlayer1Cards() {
	for (i = 1 ; i <= 13 ; i++) {
		id = "#card_" + i;
		spanid = id + " span";
		el = $(id);
		el.removeClass("cardback");
		el.addClass("card");
		//spanEl = $(".spanDisplay");
	
		if (el.hasClass("Club"))
			el.append("&clubs;");

		else if (el.hasClass("Spade"))
			el.append("&spades;");

		else if (el.hasClass("Heart"))
			el.append("&hearts;");

		else //el.hasClass("Diamond")
			el.append("&diams;");

		spanEl = $(spanid);
		spanEl.css({ "display": "inline" });
	}
}

// Original JavaScript code by Chirp Internet: www.chirp.com.au
// Please acknowledge use of this code by including this header.

/*var card_value = ['rank1 H', 'rank2 H', 'rank3 H', 'rank4 H', 'rank5 H', 'rank6 H', 'rank7 H'
                 , 'rank8 H', 'rank9 H', 'rank10 H', 'rank11 H', 'rank12 H', 'rank13 H', 'rank1 S'
				 , 'rank2 S', 'rank3 S', 'rank4 S', 'rank5 S', 'rank6 S', 'rank7 S', 'rank8 S'
				 , 'rank9 S', 'rank10 S', 'rank11 S', 'rank12 S', 'rank13 S', ];
				 */
// set default positions



/*var card_left = [];
var card_top = [];


function Card(rank, suit) {

	this.rank = rank;
	this.suit = suit;

	this.toString = cardToString;
	this.createNode = cardCreateNode;
}



//card positions
for (k = 0; k < 6; k++) {
	card_left[k] = 340 + 40 * k;
	card_top[k] = 380;
}
for (k = 6; k < 12; k++) {
	card_top[k] = -50 + 40 * k;
	card_left[k] = 50;
}
for (k = 12; k < 18 ; k++) {
	card_left[k] = 340 + 40 * (k - 12);
	card_top[k] = 0;
}
for (k = 18; k < 24 ; k++) {
	card_top[k] = -50 + 40 * (k - 18);
	card_left[k] = 700;
}
//card_left[i] = 340 + 40*i;
//card_top[i] =  250;


var started = false;
var cards_turned = 0;
var matches_found = 0;
var card1 = false;
var card2 = false;

function moveToPlace(id) {
	var el = document.getElementById("card_" + id);
	el.style["zIndex"] = 100;
	el.style["left"] = card_left[id] + "px";
	el.style["top"] = card_top[id] + "px";
	el.style["WebkitTransform"] = "rotate(180deg)";
	el.style["zIndex"] = 0;
}

function hideCard(id) {
	var el = document.getElementById("card_" + id);
	el.firstChild.src = "/images/cards/back.png";
	el.style["WebkitTransform"] = "scale(1.0) rotate(180deg)";
	el.style["MozTransform"] = "scale(1.0)";
	el.style["OTransform"] = "scale(1.0)";
	el.style["msTransform"] = "scale(1.0)";
}

function moveToPack(id) {
	hideCard(id);
	var el = document.getElementById("card_" + id);
	el.style["zIndex"] = 1000;
	el.style["left"] = "-140px";
	el.style["top"] = "100px";
	el.style["WebkitTransform"] = "rotate(0deg)";
	el.style["zIndex"] = 0;
}

// flip over card and check for match
function showCard(id) {
	if (id === card1) return;
	var el = document.getElementById("card_" + id);
	//el.firstChild.src = "/images/cards/" + card_value[id] + ".png";
	//el.firstchild.src = "http://d37rcl8t6g8sj5.cloudfront.net/wp-content/uploads/card-set.png" 
	el.style["WebkitTransform"] = "scale(1.2) rotate(185deg)";
	el.style["MozTransform"] = "scale(1.2)";
	el.style["OTransform"] = "scale(1.2)";
	el.style["msTransform"] = "scale(1.2)";
	if (++cards_turned == 2) {
		card2 = id;
		// check whether both cards have the same value
		if (parseInt(card_value[card1]) == parseInt(card_value[card2])) {
			setTimeout("moveToPack(" + card1 + "); moveToPack(" + card2 + ");", 1000);
			if (++matches_found == 8) {
				// game over
				matches_found = 0;
				started = false;
			}
		} else {
			setTimeout("hideCard(" + card1 + "); hideCard(" + card2 + ");", 800);
		}
		card1 = card2 = false;
		cards_turned = 0;
	} else {
		card1 = id;
	}
}

function cardClick(id) {
	if (started) {
		showCard(id);
	} else {
		//shuffle and deal cards
		card_value.sort(function () { return Math.round(Math.random()) - 0, 5; });
		for (i = 0; i < 26; i++) setTimeout("moveToPlace(" + i + ")", i * 100);
		started = true;
	}
}  */
