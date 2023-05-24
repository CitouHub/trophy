import * as Request from '../util/requesthandler'

export async function getRankings() {
    return await Request.send({
        url: `/api/ranking/`,
        method: 'GET'
    }).then((response) => {
        return Request.handleResponse(response)
    });
}