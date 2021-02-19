var xhttp = new XMLHttpRequest();
xhttp.onreadystatechange = function(){
    if(this.readyState === 4 && this.status === 200){
        document.getElementById("h1").innerHTML = this.responseText;
    } else {
        console.error(this.statusText);
    }
};

var obj = JSON.stringify({websiteId: "704MdV8W46xUuZaf769gU7zwm", location: window.location.pathname});

xhttp.open("POST", "http://localhost:49154/Scribe?version=v1", true);
xhttp.setRequestHeader("Content-type", "application/json");
xhttp.send(obj);