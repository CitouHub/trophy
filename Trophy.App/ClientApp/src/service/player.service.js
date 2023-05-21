import * as Request from '../util/requesthandler'

export async function addPlayer(name) {
    return await Request.send({
        url: `/api/player/${name}`,
        method: 'POST'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}

export async function getPlayers() {
    return await Request.send({
        url: `/api/player/`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}