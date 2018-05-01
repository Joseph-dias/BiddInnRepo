window.setInterval(changeNums, 1000);
var custs = "<%=customerNumber%>";
var hots = "<%=hotelNumber%>";

function changeNums() {
    document.getElementById("#cNum").innerText = custs;
    document.getElementById("#hNum").innerText = hots;
}