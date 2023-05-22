export function isArrow(e) {
    return e.keyCode >= 37 && e.keyCode <= 40;
}

export function isLeftArrow(e) {
    return e.keyCode === 37;
}

export function isUpArrow(e) {
    return e.keyCode === 38;
}

export function isRightArrow(e) {
    return e.keyCode === 39;
}

export function isDownArrow(e) {
    return e.keyCode === 40;
}

export function isEnter(e) {
    return e.keyCode === 13;
}

export function isBackspace(e) {
    return e.keyCode === 8;
}

export function isTab(e) {
    return e.keyCode === 9;
}

export function isHome(e) {
    return e.keyCode === 36;
}

export function isEnd(e) {
    return e.keyCode === 35;
}