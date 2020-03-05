/* -----------------------------------------------
   Floating layer - v.1
   (c) 2006 www.haan.net
   contact: jeroen@haan.net
   You may use this script but please leave the credits on top intact.
   Please inform us of any improvements made.
   When usefull we will add your credits.
  ------------------------------------------------ */

x = 500;
y = 70;
function setVisible(obj)
{
	obj = document.getElementById(obj);
	obj.style.visibility = (obj.style.visibility == 'visible') ? 'hidden' : 'visible';
}
function placeIt(obj)
{
	obj = document.getElementById(obj);
	if (document.documentElement)
	{
		theLeft = document.documentElement.scrollLeft;
		theTop = document.documentElement.scrollTop;
	}
	else if (document.body)
	{
		theLeft = document.body.scrollLeft;
		theTop = document.body.scrollTop;
	}
	theLeft += x;
	theTop += y;
        if(window.innerWidth) {
            obj.style.left = parseInt(window.innerWidth / 2) - parseInt(obj.offsetWidth / 2) + "px";
        } else if(document.documentElement && document.documentElement.clientWidth) {
            obj.style.left = parseInt(document.documentElement.clientWidth / 2) - parseInt(obj.offsetWidth / 2) + "px";
        } else if(document.body && document.body.clientWidth) {
            obj.style.left = parseInt(document.body.clientWidth / 2) - parseInt(obj.offsetWidth / 2) + "px";
        }

        if(window.innerHeight) {
            obj.style.top = parseInt(window.innerHeight / 2) - parseInt(obj.offsetHeight / 2) + "px";
        } else if(document.documentElement && document.documentElement.clientHeight) {
            obj.style.top = parseInt(document.documentElement.clientHeight / 2) - parseInt(obj.offsetHeight / 2) + "px";
        } else if(document.body && document.body.clientHeight) {
            obj.style.top = parseInt(document.body.clientHeight / 2) - parseInt(obj.offsetHeight / 2) + "px";
        }

        setTimeout("placeIt('layerAyuda')",500);
}
window.onscroll = setTimeout("placeIt('layerAyuda')",500);