import * as Request from '../util/requesthandler'

export async function getByWinCount() {
    return await Request.send({
        url: `/api/ranking/by/wincount`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}

export async function getByWinRate() {
    return await Request.send({
        url: `/api/ranking/by/winrate`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}

export async function getByWinStreak() {
    return await Request.send({
        url: `/api/ranking/by/winstreak`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}

export async function getByWinSize() {
    return await Request.send({
        url: `/api/ranking/by/winsize`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}

export async function getByTrophyTime() {
    return await Request.send({
        url: `/api/ranking/by/trophytime`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}

export async function getByPointCount() {
    return await Request.send({
        url: `/api/ranking/by/pointcount`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}