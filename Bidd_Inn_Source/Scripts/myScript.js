window.setInterval(changeNums, 5000);

function changeNums() {
    var custs = "<%= custNum %>";
    var hots = "<%= hotelNum %>";
    document.getElementById("cNum").innerText = custs;
    document.getElementById("hNum").innerText = hots;
}