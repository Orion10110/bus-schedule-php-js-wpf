$(document).ready(function() {
	clock();
	$("#box").delay(900).fadeIn();
	$(".table2 tbody tr").each(function(index, el) {
		$t = $(this);
	});

});






function just(){
	
	var tableElem = document.getElementById('mainSect');
  var elements = tableElem.getElementsByTagName('button');

  for (var i = 0; i < elements.length; i++) {
    var button = elements[i];
    button.onclick = function(e){
		
		var elem = document.getElementById('central');
		
		elem.setAttribute('value', e.currentTarget.getAttribute('value'));
		elem.click();
	}
	//clock();
}




  }

  function clock(){
	  setInterval(function(){
 var tmN=new Date()
 var dH=''+tmN.getHours();dH=dH.length<2?'0'+dH:dH
 var dM=''+tmN.getMinutes();dM=dM.length<2?'0'+dM:dM
 var dS=''+tmN.getSeconds();dS=dS.length<2?'0'+dS:dS
 var tmp=dH+':'+dM+':'+dS;
  $("#clock").html(tmp);

  
}, 900);
  }

function just1(){
	
	var tableElem = document.getElementById('st1');
	var elements = tableElem.getElementsByTagName('button');

		for (var i = 0; i < elements.length; i++) {
			var button = elements[i];
			button.onclick = function(e){
		
			var elem = document.getElementById('central');
			var routed = document.getElementById('route');
			elem.setAttribute('value', e.currentTarget.getAttribute('value'));
			routed.setAttribute('value', 'прямо');
			elem.click();
		}
	}
	tableElem = document.getElementById('st2');
	elements = tableElem.getElementsByTagName('button');

		for (var i = 0; i < elements.length; i++) {
			var button = elements[i];
			button.onclick = function(e){
		
			var elem = document.getElementById('central');
			var routed = document.getElementById('route');
			elem.setAttribute('value', e.currentTarget.getAttribute('value'));
			routed.setAttribute('value', 'обратно');
			elem.click();
		}
	}
	//clock();
}





var d=document
var NN=d.layers?true:(window.opera&&!d.createComment)?true:false
function showTime(){
 var tmN=new Date()
 var dH=''+tmN.getHours();dH=dH.length<2?'0'+dH:dH
 var dM=''+tmN.getMinutes();dM=dM.length<2?'0'+dM:dM
 var dS=''+tmN.getSeconds();dS=dS.length<2?'0'+dS:dS
 var tmp=dH+':'+dM+':'+dS
 if(NN)d.F.chas.value=tmp;else d.getElementById('tm').innerHTML=tmp
 var t=setTimeout('showTime()',1000)
}
