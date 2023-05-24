import axios from "axios"

export async function send(request) {
    try {
        let url = request.url;
        let headers = {}

        if (request.method === 'GET') {
            return await axios.get(url, { headers });
        } else if (request.method === 'POST') {
            return await axios.post(url, request.data, { headers });
        } else if (request.method === 'PUT') {
            return await axios.put(url, request.data, { headers });
        } else if (request.method === 'DELETE') {
            return await axios.delete(url, { headers });
        }
    } catch (error) {
        console.error(error);
    }
}

export async function handleResponse(response) {
    try {
        if (response) {
            if (response.status === 200) {
                return response.data;
            } else if (response.status !== 204) {
                console.warn("Unexpected API result!");
                console.warn(response);
            }
        }
    } catch (error) {
        console.error(error);
    }
}