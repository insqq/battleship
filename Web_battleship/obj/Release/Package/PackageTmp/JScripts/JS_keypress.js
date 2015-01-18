function doClick(buttonName, e) {
    //the purpose of this function is to allow the enter key to 
    //point to the correct button to click.
    var key;

    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = e.which;     //firefox

    if (key == 13) {
        //Get the button the user wants to have clicked
        var btn = document.getElementById(buttonName);
        if (btn != null) { //If we find the button click it
            btn.click();
            event.keyCode = 0
        }
    }
}