import * as Request from '../util/requesthandler'

export async function getPlayers() {
    return await Request.send({
        url: `/api/player/`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}