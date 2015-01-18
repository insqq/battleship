<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZTestPage.aspx.cs" Inherits="Web_battleship.ZTestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
	 
	   "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">

<head>

    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />

    <title>JavaScript Tutorial</title>

    <style type="text/css">
        #h3style {
            color: gray;
        }
    </style>

</head>

<body>

    <script type="text/javascript">

        function changeColor() {
            document.getElementById("h3style").style.color = "red";
            document.getElementById("h3style").firstChild.nodeValue = "Excited";
            return true;
        }

        function changeAgain() {
            document.getElementById("h3style").style.color = "gray";
            document.getElementById("h3style").firstChild.nodeValue = "Bored";
            return true;
        }

        function showPara() {
            document.getElementById("first").style.visibility = (document.formex.firstpara.checked) ? "visible" : "hidden";
            document.getElementById("second").style.visibility = (document.formex.secondpara.checked) ? "visible" : "hidden";
            document.getElementById("third").style.visibility = (document.formex.thirdpara.checked) ? "visible" : "hidden";
            return true;
        }

        function changeImage() {
            document.getElementById("littlebrain").style.height = "35%";
            document.getElementById("littlebrain").style.width = "35%";
            return true;
        }
    </script>

    <noscript>
        <h3>This site requires JavaScript</h3>
    </noscript>

    <h3 id="h3style" onmouseover="changeColor();" onmouseout="changeAgain();">Rollover</h3>

    <p id="first">This is the first paragraph</p>
    <p id="second">This is the second paragraph</p>
    <p id="third">This is the third paragraph</p>




    <form name="formex">
        <input type="checkbox" name="firstpara" onclick="showPara();" />First Paragraph<br />
        <input type="checkbox" name="secondpara" onclick="showPara();" />Second Paragraph<br />
        <input type="checkbox" name="thirdpara" onclick="showPara();" />Third Paragraph<br />
        <p>
            <b>Text Input</b><br />
            <input type="text" name="textinput" onblur="alert('onBlur Triggered');"
                onchange="alert('onChange Triggered');" onfocus="alert('onFocus Triggered');" />
        </p>
        <br />
        <input type="text" name="mousex" />Mouse X Position<br />
        <input type="text" name="mousey" />Mouse Y Position<br />
        <input type="text" name="keypress" />Key Pressed<br />
        <input type="text" name="mousebutton" />Mouse Button Pressed<br />

    </form>




    <script type="text/javascript">


        var mie = (navigator.appName == "Microsoft Internet Explorer") ? true : false;

        if (!mie) {
            document.captureEvents(Event.MOUSEMOVE);            document.captureEvents(Event.MOUSEDOWN);
        }

        document.onkeypress = keyPressed;
        document.onmousemove = mousePos;
        document.onmousedown = mouseClicked;
        var mouseClick = 0;
        var keyClicked = 0;
        var mouseX = 0;
        var mouseY = 0;

        function mousePos(e) {
            if (!mie) {
                mouseX = e.pageX;
                mouseY = e.pageY;
            }
            else {
                mouseX = event.clientX + document.body.scrollLeft;
                mouseY = event.clientY + document.body.scrollTop;
            }

            document.formex.mousex.value = mouseX;
            document.formex.mousey.value = mouseY;
            return true;
        }

        function keyPressed(e) {
            if (mie) {
                e = window.event;
                keyClicked = e.keyCode;
            }
            else {
                keyClicked = String.fromCharCode(e.charCode); // Converts char code to character
            }

            if (!keyClicked) {
                return false;
            }
            document.formex.keypress.value = keyClicked;
            return true;
        }

        function mouseClicked(e) {
            if (mie) {
                switch (event.button) {
                    case 0:
                        document.formex.mousebutton.value = "Left";
                        break;
                    case 1:
                        document.formex.mousebutton.value = "Middle";
                        break;
                    default:
                        document.formex.mousebutton.value = "Right";
                        break;
                }
                return false;

            }
            else {
                switch (e.which) {
                    case 1:
                        document.formex.mousebutton.value = "Left";
                        break;
                    case 2:
                        document.formex.mousebutton.value = "Middle";
                        break;
                    default:
                        document.formex.mousebutton.value = "Right";
                        break;
                }
                return true;
            }
        }

        /* The other JavaScript Events
    OnAbort - Called when a page load is interrupted
    OnError - Called when an error occurs during page load
    OnKeyDown - When key is pressed but not released
    OnKeyUp - When key is released
    OnMouseUp - When mouse button is released
    OnReset - When reset button is clicked
    OnSelect - When text is selected
    OnSubmit - When submit button is clicked
    OnUnload - When user leaves a page
    */

    </script>

</body>
</html>

