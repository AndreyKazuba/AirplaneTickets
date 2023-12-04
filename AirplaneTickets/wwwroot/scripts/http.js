const put = async (url, body) => {
    const response = await fetch(`/api/${url}`, {
        method: 'PUT',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json', },
        body: body ? JSON.stringify(body) : null,
    });

    return response;
};

const post = async (url, body) => {
    const response = await fetch(`/api/${url}`, {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json', },
        body: body ? JSON.stringify(body) : null,
    });

    return response;
};

const get = async (url) => {
    const response = await fetch(`/api/${url}`, {
        method: 'GET',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json', },
    });

    return response;
};