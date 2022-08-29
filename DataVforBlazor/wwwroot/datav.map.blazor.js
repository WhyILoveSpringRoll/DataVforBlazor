var _toConsumableArray2 = toConsumableArray;
var toConsumableArray = _toConsumableArray;
var arrayLikeToArray = _arrayLikeToArray;
var arrayWithoutHoles = _arrayWithoutHoles;
var iterableToArray = _iterableToArray;
var unsupportedIterableToArray = _unsupportedIterableToArray;
var nonIterableSpread = _nonIterableSpread;

export function _nonIterableSpread() {
    throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
}

export function _unsupportedIterableToArray(o, minLen) {
    if (!o) return;
    if (typeof o === "string") return arrayLikeToArray(o, minLen);
    var n = Object.prototype.toString.call(o).slice(8, -1);
    if (n === "Object" && o.constructor) n = o.constructor.name;
    if (n === "Map" || n === "Set") return Array.from(o);
    if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return arrayLikeToArray(o, minLen);
}

export function _iterableToArray(iter) {
    if (typeof Symbol !== "undefined" && Symbol.iterator in Object(iter)) return Array.from(iter);
}

export function _arrayLikeToArray(arr, len) {
    if (len == null || len > arr.length) len = arr.length;

    for (var i = 0, arr2 = new Array(len); i < len; i++) {
        arr2[i] = arr[i];
    }

    return arr2;
}

export function _arrayWithoutHoles(arr) {
    if (Array.isArray(arr)) return arrayLikeToArray(arr);
}


export function _toConsumableArray(arr) {
    return arrayWithoutHoles(arr) || iterableToArray(arr) || unsupportedIterableToArray(arr) || nonIterableSpread();
}
export function getTwoPointDistance(pointOne, pointTwo) {
    var minusX = Math.abs(pointOne[0] - pointTwo[0]);
    var minusY = Math.abs(pointOne[1] - pointTwo[1]);
    return Math.sqrt(minusX * minusX + minusY * minusY);
}
export function getPolylineLength(points) {
    var lineSegments = new Array(points.length - 1).fill(0).map(function (foo, i) {
        return [points[i], points[i + 1]];
    });
    var lengths = lineSegments.map(function (item) {
        return getTwoPointDistance.apply(void 0, _toConsumableArray(item));
    });
    return mulAdd(lengths);
}
export function mulAdd(nums) {
    nums = filterNonNumber(nums);
    return nums.reduce(function (all, num) {
        return all + num;
    }, 0);
}

export function filterNonNumber(array) {
    return array.filter(function (n) {
        return typeof n === 'number';
    });
}
export function line1Length(width, height) {
    let line1Points = [
        [0, height * 0.2], [width * 0.18, height * 0.2], [width * 0.2, height * 0.4], [width * 0.25, height * 0.4],
        [width * 0.27, height * 0.6], [width * 0.72, height * 0.6], [width * 0.75, height * 0.4],
        [width * 0.8, height * 0.4], [width * 0.82, height * 0.2], [width, height * 0.2]
    ];
    return getPolylineLength(line1Points);
}

export function line2Length(width, height) {
    let line2Points = [
        [width * 0.3, height * 0.8], [width * 0.7, height * 0.8]
    ]
    return getPolylineLength(line2Points);
}

