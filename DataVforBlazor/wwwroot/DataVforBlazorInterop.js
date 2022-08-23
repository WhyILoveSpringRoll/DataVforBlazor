export function GetWidth(id) {
    var div = document.getElementById(id);
       var width = div.offsetWidth;
    return width;
}
export function GetHeight(id) {
    var div = document.getElementById(id);
    var height = div.offsetHeight;
    return height;
}
