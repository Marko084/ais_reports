// JScript File
var screenW = screen.width;
var screenH = screen.height;

function resizeCenterPane()
{
    // if screen size is greater than 1024 pixels, 
    //resize the survey results panel.
    if(screenW>1024)
    {
        var screenOffset=screenW-1024;
        
        //find survey results window.
        var divObj=document.getElementById("centerPane");
        
        if(divObj !=null)
        {
            var newDivWidth=divObj.offsetWidth+screenOffset;
            setStyleById(divObj.id,"width",newDivWidth);
            
       }
    }

}

function adjustLayout()
{
    var winW = document.body.offsetWidth;
    
    //find survey results window.
    var divObj=document.getElementById("centerPane");
    
    if(divObj !=null)
    {
        var newDivWidth=divObj.offsetWidth+(winW-screenW);
        if(newDivWidth<500)
        {
            newDivWidth=500;
        }

        setStyleById(divObj.id,"width",newDivWidth);
    }
}

function setStyleById(elementId, attrib, value) 
{
	var n = document.getElementById(elementId);
	n.style[attrib] = value;
}

function showTooltip(ctrl) {
    
    var divID=ctrl.id.replace("lnkComments","divComments");
    var selectedDIV = document.getElementById(divID);
    var winEvent=window.event;
    var posX;
    var posY;


    if (winEvent == null) {
        
        posY=window.document.pageY-340;
        posX=window.document.pageX;
    }
    else {
        //alert('non win event!');
        posY=window.event.clientY-133;
        posX=window.event.clientX+6;
    }
    
    //alert("Y axis: " + posY);
    if (selectedDIV != null) {
        
        selectedDIV.style.top=posY;
        selectedDIV.style.left=posX;
        selectedDIV.style.display = "block";
    }
}

function hideTooltip(ctrl)
{
    var divID=ctrl.id.replace("lnkComments","divComments");
    var selectedDIV = document.getElementById(divID);
    
      if (selectedDIV != null) {
          selectedDIV.style.display = "none";
    }
  }

  function SetProgressPosition(e) {
      /* var posx = 0;
      var posy = 0;
      if (!e) var e = window.event;
      if (e.pageX || e.pageY) {
          posx = e.pageX;
          posy = e.pageY;
      }
      else if (e.clientX || e.clientY) {
          posx = e.clientX + document.documentElement.scrollLeft;
          posy = e.clientY + document.documentElement.scrollTop;
      }
      document.getElementById('divProgress').style.left = posx - 8 + "px";
      document.getElementById('divProgress').style.top = posy - 8 + "px"; */
     
  }


