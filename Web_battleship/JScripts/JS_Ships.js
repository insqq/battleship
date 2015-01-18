var one = "Button_ship_size1";
var two = "Button_ship_size2";
var three = "Button_ship_size3";
var four = "Button_ship_size4";
var ShipsArray;
var pickedShip;

function Ship(_size) {
    this.size = _size;
    this.picked = false;
    this.placed = false;
    this.state = 1;
    this.pos = new Array(this.getSize());
    for (var i = 0; i < this.getSize() ; i++) {
        this.pos[i] = new Array(2);
    }
}

Ship.prototype.getSize = function () {
    return this.size;
}
Ship.prototype.getPlaced = function () {
    return this.placed;
}
Ship.prototype.getPicked = function () {
    return this.picked;
}
Ship.prototype.getState = function () {
    return this.state;
}

Ship.prototype.setPicked = function (newPicked) {
    if (typeof newPicked != 'undefined') {
        this.picked = newPicked;
    }
    else {
        document.write("picked wrong value");
    }
}
Ship.prototype.setState = function (newState) {
    if (typeof newState != 'undefined') {
        this.state = newState;
    }
    else {
        document.write("state wrong value");
    }
}

Ship.prototype.setPlaced = function (newPlaced) {
    if (typeof newPlaced != 'undefined') {
        this.placed = newPlaced;
    }
    else {
        document.write("placed wrong value");
    }
}

function initShips() {
    ShipsArray = new Array(10);
    var i = 1;
    ShipsArray[0] = new Ship(i);
    ShipsArray[1] = new Ship(i);
    ShipsArray[2] = new Ship(i);
    ShipsArray[3] = new Ship(i);
    i++;
    ShipsArray[4] = new Ship(i);
    ShipsArray[5] = new Ship(i);
    ShipsArray[6] = new Ship(i);
    i++;
    ShipsArray[7] = new Ship(i);
    ShipsArray[8] = new Ship(i);
    i++;
    ShipsArray[9] = new Ship(i);
}


function event_pick_ship(sender) {

    var fromB = 0;
    if (sender == "Button_ship_size1") fromB = 1;
    if (sender == "Button_ship_size2") fromB = 2;
    if (sender == "Button_ship_size3") fromB = 3;
    if (sender == "Button_ship_size4") fromB = 4;

    for (var i = 0; i < ShipsArray.length; ++i) {
        if (ShipsArray[i].getSize() == fromB && ShipsArray[i].getPlaced() == false) {
            for (var k = 0; k < ShipsArray.length; ++k) {
                ShipsArray[k].setPicked(false);
            }

            ShipsArray[i].setPicked(true);
            pickedShip = ShipsArray[i];

            break;
        }
    }
}

function event_onMouseOver(sender) {

    if (pickedShip != null) {
        var X = 0;
        var Y = 0;
        for (var i = 0; i < 10; i++) {
            for (var k = 0; k < 10; k++) {
                if (sender == "" + i + k) {
                    X = i;
                    Y = k;
                }
            }
        }

        try {
            if (pickedShip.getState() == 1) {

                for (var i = X; i < X + pickedShip.getSize() ; i++) {
                    var a = document.getElementById("" + i + Y);
                    if (a.style.backgroundColor.toLowerCase() != "green") {
                        a.style.backgroundColor = "Blue";
                    }
                }
            }


            if (pickedShip.getState() == 2) {
                for (var i = Y; i < Y + pickedShip.getSize() ; i++) {
                    var a = document.getElementById("" + X + i);
                    if (a.style.backgroundColor.toLowerCase() != "green") {
                        a.style.backgroundColor = "Blue";
                    }
                }
            }
        }
        catch (e) { }
    }
}
function event_onMouseOut(sender) {
    if (pickedShip != null) {
        var X = 0;
        var Y = 0;
        for (var i = 0; i < 10; i++) {
            for (var k = 0; k < 10; k++) {
                if (sender == "" + i + k) {
                    X = i;
                    Y = k;
                }
            }
        }

        try {
           
            if (pickedShip.getState() == 1) {
                for (var i = X; i < X + pickedShip.getSize() ; i++) {
                    var a = document.getElementById("" + i + Y);
                    if (a.style.backgroundColor.toLowerCase() == "blue") {
                        a.style.backgroundColor = "White";
                    }
                }
            }
                
            if (pickedShip.getState() == 2) {
                for (var i = Y; i < Y + pickedShip.getSize() ; i++) {
                    var a = document.getElementById("" + X + i);
                    if (a.style.backgroundColor.toLowerCase() != "green") {
                        a.style.backgroundColor = "White";
                    }
                }
            }
        }
        catch (e) { }
    }
}
function event_Wrap_ship() {
    if (pickedShip != null)
        if (pickedShip.getState() == 1) pickedShip.setState(2);
        else if (pickedShip != null)
            if (pickedShip.getState() == 2) pickedShip.setState(1);
}
function setShip(sender)      // try set ship
{
    var X = 0;
    var Y = 0;
    var n = 0;
    var m = 0;
    for (var i = 0; i < 10; i++) {
        for (var k = 0; k < 10; k++) {
            if (sender == "" + i + k) {
                X = i;
                Y = k;
            }
        }
    }
    if (pickedShip.getState() == 1) {
        n = X - 1 + pickedShip.getSize() + 2;
        m = Y - 1 + 3;
    }
    if (pickedShip.getState() == 2) {
        n = X - 1 + 3;
        m = Y - 1 + pickedShip.getSize() + 2;
    }

    for (var i = X - 1; i < n; i++) {
        for (var k = Y - 1; k < m; k++) {
            try {
                var a = document.getElementById("" + i + k);
                if (a.style.backgroundColor.toLowerCase() == "green") {
                    return;
                }
            }
            catch (e) { }
        }
    }
    if (pickedShip.getState() == 2) {
        for (var i = 11 - pickedShip.getSize() ; i < 10; i++) {
            for (var k = 0; k < 10; k++) {
                if (sender == "" + k + i) {
                    return;
                }
            }
        }
    }

    if (pickedShip.getState() == 1) {
        for (var i = 11 - pickedShip.getSize() ; i < 10; i++) {
            for (var k = 0; k < 10; k++) {
                if (sender == "" + i + k) {
                    return;
                }
            }
        }
    }

    for (var i = 0; i < ShipsArray.length; i++) {
        if (ShipsArray[i].getPicked()) {
            ShipsArray[i].setPicked(false);
            ShipsArray[i].setPlaced(true);
        }
    }

    var isSameShip = false;

    for (var i = 0; i < ShipsArray.length; i++) {
        if (pickedShip.getSize() == ShipsArray[i].getSize() && !ShipsArray[i].getPlaced()) {
            ShipsArray[i].setPicked(true);
            pickedShip = ShipsArray[i];
            isSameShip = true;
            break;
        }
    }

    if (!isSameShip) pickedShip = null;

}
function event_SetShip(sender)        // set ship
{
    if (pickedShip != null)
        setShip(sender);

}
function for_test() {
    alert("messaga");
}