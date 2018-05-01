window.setInterval(changeNums, 5000);

function changeNums() {
    var custs = document.getElementById("custFLD").value;
    var hots = document.getElementById("hotFLD").value;
    document.getElementById("cNum").innerText = custs;
    document.getElementById("hNum").innerText = hots;
}