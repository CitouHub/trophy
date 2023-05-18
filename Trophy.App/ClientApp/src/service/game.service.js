import * as Request from '../util/requesthandler'

export async function getTrophyHolder() {
    return await Request.send({
        url: `/api/game/trophy/holder/`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}

export async function getGames() {
    return await Request.send({
        url: `/api/game/`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}